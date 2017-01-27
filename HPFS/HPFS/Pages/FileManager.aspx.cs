using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using HPFS.Models;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Threading;

namespace HPFS
{
    public partial class FileManager : System.Web.UI.Page
    {
        // Declare instance of DB
        public static HPSDB db = new HPSDB();
        public static bool deleteClicked = false;
        public static bool notification = false;
        public static string notificationMessage = "";
        public static string notificationStyle = "";

        // Variables to hold file data for download
        public static byte[] fileToBeDownloaded = null;
        public static string fileContentType = "";
        public static string fileName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("File Manager", (int)UserActionEnum.Navigated);
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                if (!User.IsInRole("Administrator"))
                {
                    divCreateFolder.Visible = false;
                    divCreateFolderInfo.Visible = false;
                    divSearchFiles.Attributes["class"] = "col-xs-12";
                    divSearchFilesInfo.Attributes["class"] = "col-xs-12 text-center";
                }

                // Check if there is a file that needs to be download
                if (fileToBeDownloaded != null)
                {
                    // Send the file to the browser
                    HttpContext.Current.Response.AddHeader("Content-type", fileContentType);
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    HttpContext.Current.Response.BinaryWrite(fileToBeDownloaded);
                    HttpContext.Current.Response.Flush();

                    // Reset the file data back to null before the response ends to avoid downloading files twice
                    fileToBeDownloaded = null;
                    fileContentType = "";
                    fileName = "";

                    // End the response
                    HttpContext.Current.Response.End();
                }

                // Display the users permission level on the page
                // Get the user id thats currently logged in
                bool id = Page.User.IsInRole("Administrator");
                string userId = HttpContext.Current.Session["UserId"].ToString();
                var role = db.HPSUsers.Select(u => u)
                    .Where(uid => uid.UserId == userId)
                    .SingleOrDefault();

                // Add access level to page
                HtmlGenericControl permissionLevel = new HtmlGenericControl("label");
                permissionLevel.InnerHtml = "Access Level: " + role.RoleName;
                plPermissionLevel.Controls.Add(permissionLevel);

                if (!IsPostBack || deleteClicked)
                {
                    // Build the drop down lists
                    PopulateFolderDropDownLists();

                    // Build folders
                    BuildFolders();

                    // Build modals to go with folders for CRUD
                    BuildModals();

                    // Create checkbox list depending on role of current user
                    CreateCheckBoxList();

                    // Set the flag for the delete button
                    deleteClicked = false;
                }

                // Check if theres a notification
                if (notification)
                {
                    lblCRUDMessage.Text = notificationMessage;
                    lblCRUDMessage.CssClass = notificationStyle;
                    notification = false;
                }
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }

        public void SwitchMenuControls(bool isVisible)
        {
            PlaceHolder plhLogout = (PlaceHolder)Master.FindControl("plhLogout");
            PlaceHolder plhLogin = (PlaceHolder)Master.FindControl("plhLogin");
            PlaceHolder plhDashboard = (PlaceHolder)Master.FindControl("plhDashboard");

            plhLogout.Visible = isVisible;
            plhLogin.Visible = !isVisible;
            plhDashboard.Visible = isVisible;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Uploaded a File", (int)UserActionEnum.Created);
            UploadFile.Upload(NewFile, ddlUploadFileFolder, lblCRUDMessage);

            // Repopulate the notifications table
            Table table = (Table)Master.FindControl("tblNotifications");
            string userId = HttpContext.Current.Session["UserId"].ToString();
            TableBuilder.BuildNotificationTable(userId, table);

            // Rebuild folders
            BuildFolders();

            // Rebuild modals to go with folders for CRUD
            BuildModals();
        }

        protected void PopulateFolderDropDownLists()
        {
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var role = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            // Get all folders
            var folders = (role.RoleName == "Administrator") ? db.Folders.Select(i => i) : db.Folders.Select(i => i)
                .Where(r => r.RoleName.Contains(role.RoleName) || r.RoleName.Contains("Everyone"));


            // Clear the drop down lists
            ddlUploadFileFolder.Items.Clear();
            ddlSearchFolders.Items.Clear();

            // Add select a folder option to search folder drop down
            ddlSearchFolders.Items.Add(new ListItem("Select a Folder...", "-1"));

            // Populate folders drop down lists located in the modal and search field
            foreach (var folder in folders)
            {
                ddlUploadFileFolder.Items.Add(new ListItem(folder.FolderName, folder.Id.ToString()));
                ddlSearchFolders.Items.Add(new ListItem(folder.FolderName, folder.FolderName));
            }
        }

        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Created a new Folder", (int)UserActionEnum.Created);
            // Check for existing folder to prevent duplicates
            var checkExistingFolder = db.Folders.Where(f => f.FolderName == txtFolderName.Text);

            if (!checkExistingFolder.Any())
            {
                // Create new instance of folder 
                Folder folder = new Folder();

                try
                {
                    // Create variable to hold role names for trimming
                    string roleNames = "";

                    // Loop through checkbox list and add roles to rolename in folder
                    for (int i = 0; i < chkFolderPermissions.Items.Count; i++)
                    {
                        if (chkFolderPermissions.Items[i].Selected)
                        {
                            roleNames += chkFolderPermissions.Items[i].Value + ", ";
                        }
                    }

                    // Trim the trailing and leading comma's and extra spaces
                    char[] charsToTrim = { ',', ' ' };
                    folder.RoleName = roleNames.Trim(charsToTrim);
                    folder.FolderName = txtFolderName.Text;

                    // Add userid to folder to see who created it
                    folder.UserId = Session["UserId"].ToString();

                    // Add new record to database and save
                    db.Folders.Add(folder);
                    db.SaveChanges();

                    // Hold the folder name in a variable before it gets added
                    string folderName = folder.FolderName;
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

                    // Repopulate the notifications table
                    string userId = HttpContext.Current.Session["UserId"].ToString();
                    var user = db.HPSUsers.Select(u => u)
                    .Where(uid => uid.UserId == userId)
                    .SingleOrDefault();

                    // Create a notification for the database
                    NotificationCreator.CreateNotification(roleNamesArray, "Folder Created", user.AspNetUser.UserName + " created the " + folderName + " folder.", DateTime.Now, "Info", null, null);

                    // Get the table from master page and repopulate
                    Table table = (Table)Master.FindControl("tblNotifications");
                    TableBuilder.BuildNotificationTable(userId, table);

                    // Rebuild folders
                    BuildFolders();

                    // Rebuild modals to go with folders for CRUD
                    BuildModals();

                    // Build the drop down lists
                    PopulateFolderDropDownLists();

                    // Set the notification
                    lblCRUDMessage.Text = folder.FolderName + " folder was successfully created."; ;
                    lblCRUDMessage.CssClass = "text-success"; ;
                }
                catch (DataException dx)
                {
                    // Set the notification
                    lblCRUDMessage.Text = folder.FolderName + " folder could not be created at this time. Please try again later or inform an Administrator.";
                    lblCRUDMessage.CssClass = "text-danger";

                    // Write error to log file Log File Writer
                    LogFile.WriteToFile("FileManager.aspx.cs", "btnCreateFolder_Click", dx, User.Identity.Name + "tried to create a Folder named " + txtFolderName.Text + ".", "HPSErrorLog.txt");
                }
                catch (Exception ex)
                {
                    // Set the notification
                    lblCRUDMessage.Text = folder.FolderName + " folder could not be created at this time. Please try again later or inform an Administrator.";
                    lblCRUDMessage.CssClass = "text-danger";

                    // Write error to log file Log File Writer
                    LogFile.WriteToFile("FileManager.aspx.cs", "btnCreateFolder_Click", ex, User.Identity.Name + "tried to create a Folder named " + txtFolderName.Text + ".", "HPSErrorLog.txt");
                }
            }
            else
            {
                // Set the error message and repopulate folders
                lblCRUDMessage.Text = txtFolderName.Text + " already exists. Please choose another name.";
                lblCRUDMessage.CssClass = "text-danger";

                // Build the drop down list for folders in the modal
                PopulateFolderDropDownLists();

                // Build folders
                BuildFolders();

                // Build modals to go with folders for CRUD
                BuildModals();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Searched for a File", (int)UserActionEnum.Searched);
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var role = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            // Parse the input
            DateTime startDate = (txtSearchStartDate.Text != String.Empty) ? Convert.ToDateTime(txtSearchStartDate.Text) : DateTime.MinValue;
            DateTime endDate = (txtSearchStartDate.Text != String.Empty) ? Convert.ToDateTime(txtSearchEndDate.Text) : DateTime.MaxValue;
            string fileName = (txtSearchFileName.Text != String.Empty) ? txtSearchFileName.Text : String.Empty;
            string fileExtension = (ddlSearchFileType.SelectedValue != "-1") ? ddlSearchFileType.SelectedValue : String.Empty;
            string folderName = (ddlSearchFolders.SelectedValue != "-1") ? ddlSearchFolders.SelectedValue : String.Empty;

            // Declare list of HPS files
            List<HPSFileViewModel> files = new List<HPSFileViewModel>();
            //{ "#", "File Name", "Folder Name", "Size", "Type", "Date Uploaded", "Action" };
            // Check if admin is searching or other users
            if (role.RoleName == "Administrator")
            {
                // If role is admin, grab all the files
                files = db.HPSFiles
                            .Where(t => t.FileName.Contains(fileName) && t.FileExtension.Contains(fileExtension))
                            .Where(t => t.FileDate >= startDate && t.FileDate <= endDate)
                            .Where(t => t.Folder.FolderName.Contains(folderName))
                            .Select(file => new HPSFileViewModel { Id = file.Id, FileName = file.FileName, FileSize = file.FileSize, FileExtension = file.FileExtension, FileDate = file.FileDate, Folder = file.Folder })
                            .OrderBy(date => date.FileDate)
                            .ToList();
            }
            else
            {
                // Search for files based on role the user is in
                files = db.HPSFiles
                            .Where(t => t.FileName.Contains(fileName) && t.FileExtension.Contains(fileExtension))
                            .Where(t => t.FileDate >= startDate && t.FileDate <= endDate)
                            .Where(t => t.RoleName.Contains(role.RoleName) || t.RoleName.Contains("Everyone"))
                            .Where(t => t.Folder.FolderName.Contains(folderName))
                            .Select(file => new HPSFileViewModel { Id = file.Id, FileName = file.FileName, FileSize = file.FileSize, FileExtension = file.FileExtension, FileDate = file.FileDate, Folder = file.Folder })
                            .OrderBy(date => date.FileDate)
                            .ToList();
            }


            if (files.Count > 0)
            {
                TableBuilder.BuildSearchTable(tblFiles, files, role.RoleName);
                lblNoResults.Visible = false;
            }
            else
            {
                lblNoResults.Visible = true;
                lblNoResults.Text = "No results found.";
            }

            // Show the search results panel
            pnlSearchResults.Visible = true;

            // Rebuild folders
            BuildFolders();

            // Rebuild modals to go with folders for CRUD
            BuildModals();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showTab", "$.bootstrapSortable(true);", true);

        }

        // Ajax delete web method
        [WebMethod]
        public static void DeleteFolder(int id)
        {
            ActivityTracker.Track("Deleted a Folder", (int)UserActionEnum.Deleted);

            // Get all files attached to folder
            var files = db.HPSFiles.Select(s => s).Where(f => f.FolderId == id);

            // Loop through files and remove files
            foreach (var f in files)
            {
                if (files.Any())
                {
                    HPSFile file = db.HPSFiles.Find(f.Id);
                    db.HPSFiles.Remove(file);
                }
            }

            // Find the folder and remove it
            Folder folder = db.Folders.Find(id);

            // Remove folder
            db.Folders.Remove(folder);

            // Hold the folder name in a variable before it gets deleted
            string folderName = folder.FolderName;
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

            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var user = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            try
            {
                // Save changes
                db.SaveChanges();

                // Create a notification for the database
                //NotificationCreator.CreateNotification("File Deleted", user.AspNetUser.UserName + " deleted '" + file.FileName + "' from the " + folderName + " folder on " + DateTime.Now.ToShortDateString(), DateTime.Now, "Info", userId, id, null);
                NotificationCreator.CreateNotification(roleNamesArray, "Folder Deleted", user.AspNetUser.UserName + " deleted the " + folderName + " folder.", DateTime.Now, "Info", null, null);

                // Set the delete flag and refresh the page so we can rebuild the folders/files
                deleteClicked = true;

                // Set the notification
                notificationMessage = folder.FolderName + " folder was successfully deleted.";
                notificationStyle = "text-success";
                notification = true;
            }
            catch (DataException dx)
            {
                // Set the notification
                notificationMessage = folder.FolderName + " folder could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FileManager.aspx.cs", "DeleteFolder", dx, user.AspNetUser.UserName + "tried to delete a Folder named " + folderName + ".", "HPSErrorLog.txt");
            }
            catch(Exception ex)
            {
                // Set the notification
                notificationMessage = folder.FolderName + " folder could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FileManager.aspx.cs", "DeleteFolder", ex, user.AspNetUser.UserName + "tried to delete a Folder named " + folderName + ".", "HPSErrorLog.txt");
            }
        }

        // Ajax delete web method
        [WebMethod]
        public static void DownloadFile(int id)
        {
            ActivityTracker.Track("Downloaded a File", (int)UserActionEnum.Downloaded);
            // Get the file from the database
            var file = db.HPSFiles.Select(s => s)
                .Where(f => f.Id == id)
                .SingleOrDefault();

            // Turn data into byte array
            fileToBeDownloaded = file.FileData;
            fileContentType = file.FileContentType;
            fileName = file.FileName;
        }

        // Ajax delete web method
        [WebMethod]
        public static void DeleteFile(int id)
        {
            ActivityTracker.Track("Deleted a File", (int)UserActionEnum.Deleted);
            // Get all files attached to folder
            HPSFile file = db.HPSFiles.Find(id);

            // Hold the folder name in a variable before it gets deleted
            string folderName = file.Folder.FolderName;
            string roles = file.Folder.RoleName;
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

            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var user = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            try
            {
                // Remove the file
                db.HPSFiles.Remove(file);

                // Save Changes to the database
                db.SaveChanges();

                // Create a notification for the database
                NotificationCreator.CreateNotification(roleNamesArray, "File Deleted", user.AspNetUser.UserName + " deleted '" + file.FileName + "' from the " + folderName + " folder.", DateTime.Now, "Info", id, null);

                // Set the delete flag and refresh the page so we can rebuild the folders/files
                deleteClicked = true;

                // Set the notification
                notificationMessage = "'" + file.FileName + "'  was successfully deleted.";
                notificationStyle = "text-success";
                notification = true;
            }
            catch (DataException dx)
            {
                // Set the notification
                notificationMessage = "'" + file.FileName + " could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FileManager.aspx.cs", "DeleteFile", dx, user.AspNetUser.UserName + "tried to delete a File named " + file.FileName, "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                // Set the notification
                notificationMessage = "'" + file.FileName + " could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FileManager.aspx.cs", "DeleteFile", ex, user.AspNetUser.UserName + "tried to delete a File named " + file.FileName, "HPSErrorLog.txt");
            }
        }

        public void BuildFolders()
        {
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var role = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            // Get all images in the database
            // SHOW FOLDERS BASED ON ROLE
            var folders = (role.RoleName == "Administrator") ? db.Folders.Select(i => i) : db.Folders.Select(i => i).Where(r => r.RoleName.Contains(role.RoleName) || r.RoleName.Contains("Everyone"));

            // Check if any records are returned
            if (folders.Any())
            {
                // Loop through the folders and set up the CRUD operations
                foreach (var folder in folders)
                {
                    // Get the file count for the current folder
                    string fileCount = db.HPSFiles.Select(i => i.FolderId == folder.Id)
                        .Count(j => j).ToString();

                    using (HtmlGenericControl div = new HtmlGenericControl(),
                    well = new HtmlGenericControl("div"),
                    div2 = new HtmlGenericControl("div"),
                    folderIcon = new HtmlGenericControl("span"),
                    fileAmount = new HtmlGenericControl("label"),
                    //br = new HtmlGenericControl("br"),
                    h4 = new HtmlGenericControl("h4"),
                    //permissions = new HtmlGenericControl("small"),
                    hr = new HtmlGenericControl("hr"),
                    div3 = new HtmlGenericControl("div"),
                    btnGroup = new HtmlGenericControl("div"),
                    tooltipAdd = new HtmlGenericControl("span"),
                    addFileIcon = new HtmlGenericControl("span"),
                    tooltipView = new HtmlGenericControl("span"),
                    viewFilesIcon = new HtmlGenericControl("span"))
                    {
                        using (LinkButton addFile = new LinkButton(),
                        viewFiles = new LinkButton())
                        {
                            // Create div with column classes
                            div.Attributes.Add("class", "col-xs-12 col-sm-6 col-md-3");
                            div.ID = folder.Id.ToString();

                            // Create a div with well class
                            well.Attributes.Add("class", "well text-center");

                            // Create a div that displays the number of files in the folder
                            div2.Attributes.Add("style", "height: 150px;");

                            // Draw the glyphicon in the well
                            folderIcon.Attributes.Add("class", "glyphicon glyphicon-folder-open");
                            folderIcon.Attributes.Add("style", "font-size: 100px;");

                            // Assign the file count for this folder to the label
                            fileAmount.InnerHtml = fileCount + " Files";

                            // Display the folder name in the heading
                            h4.InnerHtml = folder.FolderName;

                            //// Display the folder name in the heading
                            //permissions.InnerHtml = "Permissions: " + folder.RoleName;

                            btnGroup.Attributes.Add("class", "btn-group");

                            tooltipAdd.Attributes.Add("data-toggle", "tooltip");
                            tooltipAdd.Attributes.Add("data-container", "body");
                            tooltipAdd.Attributes.Add("title", "Add New File");

                            // Create the add file button
                            addFile.CssClass = "btn btn-success";
                            addFile.Attributes.Add("data-toggle", "modal");
                            addFile.Attributes.Add("data-target", "#mdlUploadFile");
                            addFile.Attributes.Add("data-id", folder.Id.ToString());
                            addFile.Attributes.Add("name", "add");

                            // Create the add file icon
                            addFileIcon.Attributes.Add("class", "glyphicon glyphicon-plus");

                            // Create the tooltip for the view button
                            tooltipView.Attributes.Add("data-toggle", "tooltip");
                            tooltipView.Attributes.Add("data-container", "body");
                            tooltipView.Attributes.Add("title", "View All Files");

                            // Create the view files button
                            viewFiles.CssClass = "btn btn-primary";
                            viewFiles.Attributes.Add("data-toggle", "modal");
                            viewFiles.Attributes.Add("data-target", "#Body_mdl" + folder.Id);

                            // create the view files icon
                            viewFilesIcon.Attributes.Add("class", "glyphicon glyphicon-eye-open");

                            // Add the icons to the buttons
                            addFile.Controls.Add(addFileIcon);
                            viewFiles.Controls.Add(viewFilesIcon);

                            // Add the buttons to the tooltips
                            tooltipAdd.Controls.Add(addFile);
                            tooltipView.Controls.Add(viewFiles);

                            // Add the buttons to the button group
                            btnGroup.Controls.Add(tooltipAdd);
                            btnGroup.Controls.Add(tooltipView);

                            // Check if user is administrator and add remove option for folders only if so
                            if (User.IsInRole("Administrator"))
                            {
                                using (HtmlGenericControl tooltipRemove = new HtmlGenericControl(),
                                removeFoldersIcon = new HtmlGenericControl("span"))
                                {
                                    using (LinkButton removeFolder = new LinkButton())
                                    {
                                        if(!(folder.FolderName == "Slide Show Images"))
                                        {
                                            // Create the tooltip for the remove button
                                            tooltipRemove.Attributes.Add("data-toggle", "tooltip");
                                            tooltipRemove.Attributes.Add("data-container", "body");
                                            tooltipRemove.Attributes.Add("title", "Delete Folder");

                                            // Create the remove folders button
                                            removeFolder.CssClass = "btn btn-danger";
                                            removeFolder.Attributes.Add("name", "remove");
                                            removeFolder.Attributes.Add("data-id", folder.Id.ToString());

                                            // create the remove folders icon
                                            removeFoldersIcon.Attributes.Add("class", "glyphicon glyphicon-remove");

                                            // Only add remove button if user is in administrator role
                                            removeFolder.Controls.Add(removeFoldersIcon);
                                            tooltipRemove.Controls.Add(removeFolder);
                                            btnGroup.Controls.Add(tooltipRemove);
                                        }
                                    }
                                }
                            }
                            // Add the button group the the centering div
                            div3.Controls.Add(btnGroup);

                            // Add Folder icon to placeholder div
                            div2.Controls.Add(folderIcon);

                            // Add the number of files div, the heading, the hr, and the buttons to the well
                            well.Controls.Add(div2);
                            well.Controls.Add(h4);
                            well.Controls.Add(fileAmount);
                            //well.Controls.Add(br);
                            //well.Controls.Add(permissions);
                            well.Controls.Add(hr);
                            well.Controls.Add(div3);

                            // Add the well to the column
                            div.Controls.Add(well);

                            // Add the column to the placeholder
                            plFolders.Controls.Add(div);
                        }
                    }

                    //// Only allow rows of 4 before adding the clearfix to fix the design 
                    //decimal mod2 = folderCount % 2;
                    //if ((folderCount % 2 == 0 && folderCount % 2 != 1) || folderCount % 2 == 2)
                    //{
                    //    HtmlGenericControl clearFix = new HtmlGenericControl("div");
                    //    clearFix.Attributes.Add("class", "clearfix");
                    //    plFolders.Controls.Add(clearFix);
                    //    //mynumber is a Perfect Number
                    //    folderCount = 1;
                    //}
                    //else
                    //{
                    //    folderCount++;
                    //}
                }
            }
            else
            {
                using (HtmlGenericControl message = new HtmlGenericControl("h2"))
                {
                    // Create a blank row with no results text
                    message.Attributes.Add("class", "text-center");
                    message.InnerHtml = "You currently have no viewable folders.";
                    plFolders.Controls.Add(message);
                }
            }
        }

        public void BuildModals()
        {
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var role = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            // Get all folders in the database based on role
            var folders = (role.RoleName == "Administrator") ? db.Folders.Select(i => i) : db.Folders.Select(i => i).Where(r => r.RoleName.Contains(role.RoleName) || r.RoleName.Contains("Everyone"));

            // Loop through the images and generate thumbnails of all of them
            foreach (var folder in folders)
            {
                using (HtmlGenericControl modal = new HtmlGenericControl(),
                modalDialog = new HtmlGenericControl("div"),
                modalContent = new HtmlGenericControl("div"),
                modalHeader = new HtmlGenericControl("div"),
                modalCloseButton = new HtmlGenericControl("button"),
                modalHeaderText = new HtmlGenericControl("h4"),
                modalBody = new HtmlGenericControl("div"),
                modalFooter = new HtmlGenericControl("div"),
                modalFooterCancelButton = new HtmlGenericControl("button"),
                modalPermissionsText = new HtmlGenericControl("small"))
                {

                    // Create modals
                    modal.ID = "mdl" + folder.Id;
                    modal.Attributes.Add("class", "modal fade");
                    modal.Attributes.Add("tabindex", "-1");
                    modal.Attributes.Add("role", "dialog");
                    modal.Attributes.Add("aria-labellby", "mdl" + folder.Id + "-label");
                    modal.Attributes.Add("aria-hidden", "true");

                    // Generic the modal dialog
                    modalDialog.Attributes.Add("class", "modal-dialog modal-lg");

                    // Generate the modal content
                    modalContent.Attributes.Add("class", "modal-content");

                    // Generate the modal header
                    modalHeader.Attributes.Add("class", "modal-header text-center");

                    // Generate the modal close button
                    modalCloseButton.Attributes.Add("class", "close");
                    modalCloseButton.Attributes.Add("data-dismiss", "modal");
                    modalCloseButton.Attributes.Add("aria-hidden", "true");
                    modalCloseButton.InnerHtml = "&times;";

                    // Generate the modal headertext
                    modalHeaderText.Attributes.Add("class", "modal-title");
                    modalHeaderText.ID = "mdl" + folder.Id + "-label";
                    modalHeaderText.InnerHtml = folder.FolderName + " Folder";

                    // Generate the modal permissions text 
                    modalPermissionsText.InnerHtml = "<b>Permissions: </b>" + folder.RoleName;

                    // generate the modal body
                    modalBody.Attributes.Add("class", "modal-body");
                    modalBody.Attributes.Add("style", "overflow: auto; max-height: 800px;");

                    // Build table from other function by passing in the folder id and add to modal body
                    Table table = TableBuilder.BuildFilesTable(folder.Id, role.RoleName);
                    modalBody.Controls.Add(table);

                    // Generate the modal footer
                    modalFooter.Attributes.Add("class", "modal-footer");

                    // Generate the modal footer cancel button
                    modalFooterCancelButton.Attributes.Add("class", "btn btn-danger");
                    modalFooterCancelButton.Attributes.Add("data-dismiss", "modal");
                    modalFooterCancelButton.InnerHtml = "Cancel";

                    // Add controls to modal footer
                    modalFooter.Controls.Add(modalFooterCancelButton);

                    // Add close button and header text to modal header
                    modalHeader.Controls.Add(modalCloseButton);
                    modalHeader.Controls.Add(modalHeaderText);
                    modalHeader.Controls.Add(modalPermissionsText);


                    // Add header, body and footer to modal content
                    modalContent.Controls.Add(modalHeader);
                    modalContent.Controls.Add(modalBody);
                    modalContent.Controls.Add(modalFooter);

                    // Add modal content to modal dialog
                    modalDialog.Controls.Add(modalContent);

                    // Add modal dialog to modal
                    modal.Controls.Add(modalDialog);

                    // Add modal to placeholder for modals
                    plViewModals.Controls.Add(modal);
                }
            }
        }

        protected void CreateCheckBoxList()
        {
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var role = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            // Clear checkbox
            chkFolderPermissions.Items.Clear();

            // Check the role the current user is in
            if (role.RoleName.Contains("Administrator"))
            {
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Administrator", "Administrator"));
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Client", "Client"));
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Board Member", "Board Member"));
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Everyone", "Everyone"));
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Family Association", "Family Association"));
            }
            else if (role.RoleName.Contains("Client"))
            {
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Client", "Client"));
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Everyone", "Everyone"));
            }
            else if (role.RoleName.Contains("Board Member"))
            {
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Board Member", "Board Member"));
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Everyone", "Everyone"));
            }
            else if (role.RoleName.Contains("Family Association"))
            {
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Everyone", "Everyone"));
                chkFolderPermissions.Items.Add(new ListItem("&nbsp;Family Association", "Family Association"));
            }
        }
    }
}