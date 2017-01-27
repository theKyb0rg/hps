using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;
using HPFS.Models;
using System.Web.Services;
using System.Data;

namespace HPFS.Pages
{
    public partial class SlideShowManager : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();
        public static bool notification = false;
        public static string notificationMessage = "";
        public static string notificationStyle = "";

        private static int? slideId = null;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                // Check if theres a notification
                if (notification)
                {
                    lblCRUDMessage.Text = notificationMessage;
                    lblCRUDMessage.CssClass = notificationStyle;
                    notification = false;

                    if (slideId != null)
                    {
                        // Get images from current slide and the rest that are not within it
                        GetImages(slideId);
                        pnlSlideShowImages.Visible = true;
                        slideId = null;
                    }
                }
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Slideshow Manager", (int)UserActionEnum.Navigated);

            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    // Clear drop down list
                    ddlPage.Items.Clear();

                    // Add initially list item
                    ddlPage.Items.Add(new ListItem("Select a Web Page...", "-1"));

                    // Populate drop down list with Web Pages
                    var webpages = db.WebPages.Select(s => s);
                    foreach (var w in webpages)
                    {
                        ddlPage.Items.Add(new ListItem(w.WebPageName, w.Id.ToString()));
                    }
                }
            }
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear drop down list
            ddlSlideShow.Items.Clear();

            if (ddlPage.SelectedValue != "-1")
            {
                // Add initially list item
                ddlSlideShow.Items.Add(new ListItem("Select a Slide Show...", "-1"));

                // get id of webpage
                int pageId = Convert.ToInt32(ddlPage.SelectedValue);

                // Populate slideshows List
                var slideshows = db.SlideShows.Select(s => s)
                    .Where(si => si.WebPageId == pageId);

                foreach (var s in slideshows)
                {
                    ddlSlideShow.Items.Add(new ListItem(s.SlideShowName, s.Id.ToString()));
                }
            }
            else
            {
                ddlSlideShow.Items.Add(new ListItem("No Slide Shows to show.", "-1"));
            }
        }

        protected void ddlSlideShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show the images panel
            pnlSlideShowImages.Visible = true;

            // Get slide show Id
            int slideShowId = Convert.ToInt32(ddlSlideShow.SelectedValue);

            // Get images from current slide and the rest that are not within it
            GetImages(slideShowId);
        }

        [WebMethod]
        public static void SaveSlideShow(string[] imageArray, int slideShowId, string[] captionHeadingArray, string[] captionTextArray, string slideShowName)
        {
            HelperMethods.ActivityTracker.Track("Saved a Slideshow", (int)UserActionEnum.Updated);
            // Check if there are any images already in current slideshow
            if (currentSlideShowImages.Count != 0)
            {
                // Delete the old records
                foreach (var s in currentSlideShowImages)
                {
                    db.SlideShowImages.Remove(s);
                }
            }

            // Add the new records
            for (int i = 0; i < imageArray.Length; i++)
            {
                SlideShowImage newSlide = new SlideShowImage();
                newSlide.FileId = Convert.ToInt32(imageArray[i]);
                newSlide.SlideShowPosition = i + 1;
                newSlide.SlideShowHeading = captionHeadingArray[i];
                newSlide.SlideShowText = captionTextArray[i];
                newSlide.SlideShowId = slideShowId;
                db.SlideShowImages.Add(newSlide);
            }

            try
            {
                // Set the notification
                notificationMessage = "The " + slideShowName + " slideshow was successfully updated.";
                notificationStyle = "text-success";
                notification = true;

                slideId = slideShowId;

                // Save
                db.SaveChanges();

            }
            catch (DataException dx)
            {
                // Set the notification
                notificationMessage = "The " + slideShowName + " slideshow could not be updated at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                slideId = slideShowId;

                // Write to log file
                LogFile.WriteToFile("SlideShowManager.aspx.cs", "SaveSlideShow", dx, "An Administrator tried to save a SlideShow.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                // Set the notification
                notificationMessage = "The " + slideShowName + " slideshow could not be updated at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                slideId = slideShowId;

                // Write to log file
                LogFile.WriteToFile("SlideShowManager.aspx.cs", "SaveSlideShow", ex, "An Administrator tried to save a SlideShow.", "HPSErrorLog.txt");
            }
        }

        // Class level variable to hold slide show ids of slides within the current slide show
        public static List<SlideShowImage> currentSlideShowImages = new List<SlideShowImage>();

        protected void GetImages(int? slideShowId)
        {
            // Get all images in the db
            var images = db.HPSFiles.Where(f => f.Folder.FolderName.Contains("Slide Show Images"))
                .Where(im => im.FileContentType.Contains("image"))
                .Select(i => new HPSFileViewModel { Id = i.Id, Thumbnail = i.Thumbnail, FileName = i.FileName });

            // Get all the records for slide show images
            var slideShows = db.SlideShowImages.Where(s => s.SlideShowId == slideShowId);

            // Store id files that are in the current slide show for use when saving/updating
            List<SlideShowImage> slideShowImages = new List<SlideShowImage>();
            foreach (var sl in slideShows)
            {
                slideShowImages.Add(sl);
            }
            currentSlideShowImages = slideShowImages;

            // Set a counter variable to check how many images are in the current slideshow
            int imagesInSlideShow = 0;

            // Loop through the images and generate thumbnails of all of them
            foreach (var image in images)
            {
                using (HtmlGenericControl div = new HtmlGenericControl("div"),
                a = new HtmlGenericControl("a"),
                div2 = new HtmlGenericControl("div"),
                span = new HtmlGenericControl("span"),
                position = new HtmlGenericControl("label"),
                label = new HtmlGenericControl("label"),
                modalButton = new HtmlGenericControl("a"),
                modal = new HtmlGenericControl("div"),
                modalDialog = new HtmlGenericControl("div"),
                modalContent = new HtmlGenericControl("div"),
                modalHeader = new HtmlGenericControl("div"),
                modalCloseButton = new HtmlGenericControl("button"),
                modalHeaderText = new HtmlGenericControl("h4"),
                modalBody = new HtmlGenericControl("div"),
                modalBodyColumnOne = new HtmlGenericControl("div"),
                modalBodyHeadingFormGroupOne = new HtmlGenericControl("div"),
                modalImageCaptionHeadingLabel = new HtmlGenericControl("label"),
                modalBodyColumnTwo = new HtmlGenericControl("div"),
                modalBodyHeadingFormGroupTwo = new HtmlGenericControl("div"),
                modalImageCaptionTextLabel = new HtmlGenericControl("label"),
                modalFooter = new HtmlGenericControl("div"),
                modalFooterCancelButton = new HtmlGenericControl("a"),
                modalFooterOKButton = new HtmlGenericControl("button"),
                row = new HtmlGenericControl("div"))
                {
                    using (Image img = new Image())
                    {

                        using (TextBox modalBodyHeadingTextBox = new TextBox(),
                        modalBodyCaptionTextBox = new TextBox())
                        {
                            // Create div with column classes
                            div.Attributes.Add("class", "col-xs-6 col-sm-2 sort");
                            div.Attributes.Add("data-id", image.Id.ToString());

                            // Assign Id of image to div
                            div.ID = image.Id.ToString();

                            // Create a tag with thumbnail class
                            a.Attributes.Add("href", "#");
                            a.Attributes.Add("class", "thumbnail");

                            // Get image from db and apply to image object
                            byte[] bytes = image.Thumbnail;
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            img.ImageUrl = "data:image/jpg;base64," + base64String;
                            img.Attributes.Add("style", "max-height: 100px; height: 100px;");
                            img.Attributes.Add("style", "cursor: move;");

                            // Create inner div and add text align style for checkbox centering
                            div2.Attributes.Add("style", "text-align: center;");
                            div2.Attributes.Add("class", "well");

                            // Set the file name to a span tag
                            span.InnerText = (image.FileName.Length > 16) ? image.FileName.Remove(13) + "..." : image.FileName;

                            // Create a label that will activate the check box
                            label.Attributes.Add("for", image.Id.ToString());

                            modalButton.Attributes.Add("class", "btn btn-default");
                            modalButton.ID = "btn" + image.Id.ToString();
                            modalButton.InnerHtml = "Edit";
                            modalButton.Attributes.Add("style", "display: none;");
                            modalButton.Attributes.Add("data-toggle", "modal");
                            modalButton.Attributes.Add("data-target", "#Body_mdl" + image.Id);

                            // Add controls to column and add column to placeholder
                            div2.Controls.Add(span);
                            a.Controls.Add(img);
                            div.Controls.Add(a);
                            div.Controls.Add(div2);
                            div.Controls.Add(modalButton);
                            //plhImages.Controls.Add(div);

                            // Add class position to label in order to modify text when using javascript
                            position.Attributes.Add("class", "position");

                            // Check if slideshow returned records
                            if (slideShows.Any())
                            {
                                // Check if slide show image already exists within the slide show
                                foreach (var s in slideShows)
                                {
                                    if (s.FileId == image.Id)
                                    {
                                        // Set the style to block instead of none if the image is in this slideshow
                                        modalButton.Attributes["style"] = "display: block;";

                                        // Add current image to the images in the current slideshow
                                        plhCurrentImages.Controls.Add(div);

                                        // Increment the counter
                                        imagesInSlideShow++;

                                        // Add heading and caption text to textboxes
                                        modalBodyHeadingTextBox.Text = s.SlideShowHeading;
                                        modalBodyCaptionTextBox.Text = s.SlideShowText;

                                        break;
                                    }
                                    else
                                    {
                                        plhImages.Controls.Add(div);
                                    }
                                }
                            }
                            else
                            {
                                // Add position label for use with javascript
                                a.Controls.Add(position);
                                plhImages.Controls.Add(div);
                            }

                            // Create modals
                            modal.ID = "mdl" + image.Id;
                            modal.Attributes.Add("class", "modal fade");
                            modal.Attributes.Add("tabindex", "-1");
                            modal.Attributes.Add("role", "dialog");
                            modal.Attributes.Add("aria-labellby", "mdl" + image.Id + "-label");
                            modal.Attributes.Add("aria-hidden", "true");

                            // Generic the modal dialog
                            modalDialog.Attributes.Add("class", "modal-dialog");

                            // Generate the modal content
                            modalContent.Attributes.Add("class", "modal-content");

                            // Generate the modal header
                            modalHeader.Attributes.Add("class", "modal-header");

                            // Generate the modal close button
                            modalCloseButton.Attributes.Add("class", "close");
                            modalCloseButton.Attributes.Add("data-dismiss", "modal");
                            modalCloseButton.Attributes.Add("aria-hidden", "true");
                            modalCloseButton.InnerHtml = "&times;";

                            // Generate the modal headertext
                            modalHeaderText.Attributes.Add("class", "modal-title");
                            modalHeaderText.ID = "mdl" + image.Id + "-label";
                            modalHeaderText.InnerHtml = "Edit Caption Settings for " + image.FileName;

                            // generate the modal body
                            modalBody.Attributes.Add("class", "modal-body");

                            // Generate column for inside modal
                            modalBodyColumnOne.Attributes.Add("class", "col-xs-12");

                            // Create form group
                            modalBodyHeadingFormGroupOne.Attributes.Add("class", "form-group");

                            modalImageCaptionHeadingLabel.Attributes.Add("for", "lblCaptionHeading" + image.Id);
                            modalImageCaptionHeadingLabel.InnerHtml = "Caption Heading:";

                            modalBodyHeadingTextBox.Attributes.Add("type", "textbox");
                            modalBodyHeadingTextBox.ID = "txtCaptionHeading" + image.Id;
                            modalBodyHeadingTextBox.CssClass = "form-control";

                            // Generate column for inside modal
                            modalBodyColumnTwo.Attributes.Add("class", "col-xs-12");

                            // Create form group
                            modalBodyHeadingFormGroupTwo.Attributes.Add("class", "form-group");

                            // Create captuion text label
                            modalImageCaptionTextLabel.Attributes.Add("for", "lblCaptionText" + image.Id);
                            modalImageCaptionTextLabel.InnerHtml = "Caption Text:";

                            // Create caption textbox
                            modalBodyCaptionTextBox.Attributes.Add("type", "textbox");
                            modalBodyCaptionTextBox.ID = "txtCaptionText" + image.Id;
                            modalBodyCaptionTextBox.CssClass = "form-control";

                            // Generate the modal footer
                            modalFooter.Attributes.Add("class", "modal-footer");

                            // Generate the modal footer cancel button
                            modalFooterCancelButton.Attributes.Add("class", "btn btn-primary");
                            modalFooterCancelButton.Attributes.Add("name", "reset");
                            modalFooterCancelButton.Attributes.Add("data-id", image.Id.ToString());
                            modalFooterCancelButton.InnerHtml = "Reset";

                            // Generate the modal footer OK button
                            modalFooterOKButton.Attributes.Add("class", "btn btn-success");
                            modalFooterOKButton.Attributes.Add("data-dismiss", "modal");
                            modalFooterOKButton.InnerHtml = "OK";

                            row.Attributes.Add("class", "row");

                            // Add controls to modal footer
                            modalFooter.Controls.Add(modalFooterOKButton);
                            modalFooter.Controls.Add(modalFooterCancelButton);

                            // Add close button and header text to modal header
                            modalHeader.Controls.Add(modalCloseButton);
                            modalHeader.Controls.Add(modalHeaderText);

                            // Add heading controsl to first form group
                            modalBodyHeadingFormGroupOne.Controls.Add(modalImageCaptionHeadingLabel);
                            modalBodyHeadingFormGroupOne.Controls.Add(modalBodyHeadingTextBox);

                            // Add caption controls to second form group
                            modalBodyHeadingFormGroupTwo.Controls.Add(modalImageCaptionTextLabel);
                            modalBodyHeadingFormGroupTwo.Controls.Add(modalBodyCaptionTextBox);

                            // Add form groups to columns
                            modalBodyColumnOne.Controls.Add(modalBodyHeadingFormGroupOne);
                            modalBodyColumnTwo.Controls.Add(modalBodyHeadingFormGroupTwo);

                            // Add columns to the row
                            row.Controls.Add(modalBodyColumnOne);
                            row.Controls.Add(modalBodyColumnTwo);

                            // Add row to modal body
                            modalBody.Controls.Add(row);

                            // Add header, body and footer to modal content
                            modalContent.Controls.Add(modalHeader);
                            modalContent.Controls.Add(modalBody);
                            modalContent.Controls.Add(modalFooter);

                            // Add modal content to modal dialog
                            modalDialog.Controls.Add(modalContent);

                            // Add modal dialog to modal
                            modal.Controls.Add(modalDialog);

                            // Add modal to placeholder for modals
                            plModals.Controls.Add(modal);

                            //// Check how much memory is used
                            //long gc = GC.GetTotalMemory(false);
                        }
                    }
                }
            }

            // Add the message if no images are present in the current slideshow
            HtmlGenericControl noImages = new HtmlGenericControl("p");
            noImages.Attributes.Add("class", "text-center");
            noImages.Attributes.Add("id", "currentText");
            noImages.InnerHtml = "No images added. To add an image to the current Slide Show, drag and drop images from below here.";

            // If images present in slide show, hide the message
            if (imagesInSlideShow > 0)
            {
                // Add display none to the message
                noImages.Attributes.Add("style", "display: none;");

                // Remove the display: none propertyA
                saveButton.Attributes.Remove("style");
            }

            // Add text to placeholder
            plhCurrentImages.Controls.Add(noImages);
        }
    }
}