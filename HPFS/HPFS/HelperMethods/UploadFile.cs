using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace HPFS.HelperMethods
{
    public class UploadFile
    {
        public static HPSDB db = new HPSDB();

        public static void Upload(FileUpload newFile, DropDownList folderName, Label lblMessage)
        {
            try
            {
                // Get the files about to be uploaded
                IList<HttpPostedFile> filePaths = newFile.PostedFiles;
                string message = "";

                // Get the user id thats currently logged in
                string userId = HttpContext.Current.Session["UserId"].ToString();
                var user = db.HPSUsers.Select(u => u)
                   .Where(uid => uid.UserId == userId)
                   .SingleOrDefault();

                // Get the role the current folder is in to assign the file to that role as well
                int folderId = Convert.ToInt32(folderName.SelectedValue);
                var folder = db.Folders.Select(a => a)
                    .Where(b => b.Id == folderId)
                    .SingleOrDefault();

                // Hold the folder name in a variable before it gets deleted
                string nameOfFolder = folder.FolderName;
                string roles = folder.RoleName;
                string[] roleNamesArray = new string[1];

                // Check for multiple roles there will always be at least 1 role
                if (roles.Contains(','))
                {
                    roleNamesArray = roles.Split(',');
                }
                else
                {
                    roleNamesArray[0] = roles;
                }

                foreach (var file in filePaths)
                {
                    string filePath = file.FileName;
                    string filename = Path.GetFileName(filePath);
                    string ext = Path.GetExtension(filename).ToLower();
                    string contenttype = String.Empty;

                    switch (ext)
                    {
                        case ".doc":
                            contenttype = "application/vnd.ms-word";
                            break;
                        case ".docx":
                            contenttype = "application/vnd.ms-word";
                            break;
                        case ".xls":
                            contenttype = "application/vnd.ms-excel";
                            break;
                        case ".xlsx":
                            contenttype = "application/vnd.ms-excel";
                            break;
                        case ".jpg":
                            contenttype = "image/jpg";
                            break;
                        case ".png":
                            contenttype = "image/png";
                            break;
                        case ".gif":
                            contenttype = "image/gif";
                            break;
                        case ".jpeg":
                            contenttype = "image/jpeg";
                            break;
                        case ".pdf":
                            contenttype = "application/pdf";
                            break;
                        case ".txt":
                            contenttype = "text/plain";
                            break;
                        case ".avi":
                            contenttype = "video/avi";
                            break;
                        case ".mov":
                            contenttype = "video/quicktime";
                            break;
                        case ".mp3":
                            contenttype = "audio/mpeg3";
                            break;
                        case ".mpeg":
                            contenttype = "video/mpeg";
                            break;
                        case ".mpg":
                            contenttype = "video/mpeg";
                            break;
                        case ".mp4":
                            contenttype = "video/mp4";
                            break;
                    }

                    // Decalre instance of new file object
                    HPSFile f = new HPSFile();

                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        // Get bytes
                        var memoryBytes = binaryReader.ReadBytes(file.ContentLength);
                        
                        // Assign bytes to fileData
                        f.FileData = memoryBytes;
                        f.FileSize = memoryBytes.Length;
                        // Check if type is image, if so, compress and resize the image
                        using (var memoryStream = new MemoryStream(memoryBytes))
                        {
                            if (contenttype.Contains("image"))
                            {
                                System.Drawing.Image imageStream = System.Drawing.Image.FromStream(memoryStream);
                                f.Thumbnail = ImageToByte(ResizeImage(imageStream, 100, 100));
                            }
                            else
                            {
                                f.Thumbnail = null;
                            }
                        }
                    }

                    // Assign the rest of the details to the file object
                    f.FileName = filename;
                    f.FolderId = folderId;
                    f.FileContentType = contenttype;
                    f.FileExtension = ext;
                    f.FileDate = DateTime.Now;
                    f.UserId = userId;
                    f.RoleName = folder.RoleName;

                    // Declare insatnace of null thumbnail
                    //Byte[] thumbnail = null;

                    

                    //// Create blank bitmap
                    //Bitmap bmp;
                    //if (contenttype.Contains("image"))
                    //{
                    //    using (var ms = new MemoryStream(bytes))
                    //    {
                    //        bmp = new Bitmap(ms);
                    //        thumbnail = ImageToByte(ResizeImage(bmp, 100, 100));

                    //        // Clear these out of memory to allow more images to be uploaded
                    //        bmp.Dispose();
                    //    }
                    //}

                    //// Set the column names for the new file
                    //HPSFile f = new HPSFile();
                    //f.FileName = filename;
                    //f.FolderId = folderId;
                    //f.FileSize = file.ContentLength;
                    //f.FileContentType = contenttype;
                    //f.FileExtension = ext;
                    //f.Thumbnail = thumbnail;
                    //f.FileDate = DateTime.Now;
                    //f.UserId = userId;
                    //f.RoleName = folder.RoleName;

                    // Add the file to the database
                    db.HPSFiles.Add(f);

                    // Add the file names to the message
                    message += "'" + filename + "', ";
                }

                // Trim the trailing and leading comma's and extra spaces
                char[] charsToTrim = { ',', ' ' };
                message = message.Trim(charsToTrim);

                try
                {
                    // Save to the database
                    db.SaveChanges();

                    // Create a notification for the database
                    NotificationCreator.CreateNotification(roleNamesArray, "File Uploaded", filePaths.Count + " file(s) were uploaded to the " + folder.FolderName + " folder by " + user.AspNetUser.UserName, DateTime.Now, "Info", null, null);

                    // Set the message
                    lblMessage.CssClass = "text-success";
                    lblMessage.Text = filePaths.Count + " file(s) were successfully uploaded to the " + folder.FolderName + " folder.";


                }
                catch (DataException dx)
                {
                    HttpContext.Current.Response.Write(dx.ToString());
                    // Set the message
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Text = filePaths.Count + " file(s) " + message + " could not be uploaded at this time. Please try again later or inform an administrator";
                }
            }
            catch(OutOfMemoryException ox)
            {
                HttpContext.Current.Response.Write(ox.ToString());
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
            
        }

        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                // Low quality images
                graphics.CompositingQuality = CompositingQuality.Default;
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.SmoothingMode = SmoothingMode.Default;
                graphics.PixelOffsetMode = PixelOffsetMode.Default;

                // High quality images
                //graphics.CompositingQuality = CompositingQuality.HighQuality;
                //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //graphics.SmoothingMode = SmoothingMode.HighQuality;
                //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
    }
}
