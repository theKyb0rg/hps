using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.Models;
using System.Web.UI.HtmlControls;
using System.Data;
using HPFS.HelperMethods;

namespace HPFS.Pages
{
    public partial class EventManager : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();
        public dynamic events;
        public string userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Event Manager", (int)UserActionEnum.Navigated);
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                PopulateCalendarData();

                if (!IsPostBack)
                {
                    // Get the role the current user is in
                    userId = HttpContext.Current.Session["UserId"].ToString();
                    var role = db.HPSUsers.Select(u => u)
                        .Where(uid => uid.UserId == userId)
                        .SingleOrDefault();

                    // Declare lists of folders and listitems
                    List<Folder> folders = new List<Folder>();

                    if (User.IsInRole("Administrator"))
                    {
                        folders = db.Folders.Select(s => s).ToList();
                    }
                    else
                    {
                        folders = db.Folders.Where(u => u.RoleName.Contains(role.RoleName) || u.RoleName.Contains("Everyone")).ToList();
                    }

                    // Clear the drop down and add initial selections
                    ddlEventFile.Items.Clear();
                    ddlEventFile.Items.Add(new ListItem("No file selected.", "null"));

                    // Loop through all folders and get the files within each folder to add to the drop down list
                    foreach (var fl in folders)
                    {
                        var filesData = db.HPSFiles.Where(file => file.FolderId == fl.Id)
                            .Select(fileViewModel => new HPSFileViewModel { Id = fileViewModel.Id, FileName = fileViewModel.FileName })
                            .OrderBy(n => n.FileName);

                        foreach (var f in filesData)
                        {
                            ddlEventFile.Items.Add(new ListItem(f.FileName, f.Id.ToString()));
                        }
                    }

                    // Check if user is client, then hide the panel
                    if (Page.User.IsInRole("Client") || Page.User.IsInRole("Board Member") || Page.User.IsInRole("Family Association"))
                    {
                        pnlCreateEvent.Visible = false;
                    }

                    //// Swap panels
                    //pnlSetFile.Visible = true;
                    //pnlDownloadFile.Visible = false;
                }
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }

        protected void EventCalendar_DayRender(object sender, DayRenderEventArgs e)
        {

            if (events != null)
            {
                foreach (var evnt in events)
                {
                    if (Convert.ToDateTime(evnt.CalendarEventDate).ToShortDateString() == e.Day.Date.ToShortDateString())
                    {
                        HyperLink link = new HyperLink();
                        link.Text = evnt.CalendarEventName;
                        link.NavigateUrl = Page.ClientScript.GetPostBackClientHyperlink(btnCalendarEvent, evnt.Id.ToString(), true);
                        e.Cell.Controls.Add(link);

                        if (e.Cell.Controls.Count > 1)
                        {
                            e.Cell.CssClass = "calendar-day-style overflowFix";
                            e.Cell.Controls.Add(new HtmlGenericControl("br"));
                        }
                    }
                }
            }
        }

        protected void PopulateCalendarData()
        {
            // Get current role of user
            userId = HttpContext.Current.Session["UserId"].ToString();
            var role = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            // Create instance of object to hold visibility settings depending on which user role is logged in
            List<AspNetRole> roleVisibilitySettings = new List<AspNetRole>();

            if (User.IsInRole("Administrator"))
            {
                // If role is admin, allow visibility to be set to every role
                events = db.CalendarEvents.Select(s => s).ToList();
                roleVisibilitySettings = db.AspNetRoles.Select(s => s).OrderBy(s => s.Name).ToList();
            }
            else if (User.IsInRole("Board Member"))
            {
                // If role is director, allow visibility to be set to everyone, client and director role
                events = db.CalendarEvents.Where(c => c.RoleName.Contains(role.RoleName) || c.RoleName.Contains("Client") || c.RoleName.Contains("Everyone")).ToList();
                roleVisibilitySettings = db.AspNetRoles.Where(r => r.Name.Contains(role.RoleName) || r.Name.Contains("Family Association") || r.Name.Contains("Client") || r.Name.Contains("Everyone")).OrderBy(s => s.Name).ToList();
            }
            else if (User.IsInRole("Family Association"))
            {
                // If role is Family Association, allow visibility to be set to everyone, client and Family Association role
                events = db.CalendarEvents.Where(c => c.RoleName.Contains(role.RoleName) || c.RoleName.Contains("Client") || c.RoleName.Contains("Everyone")).ToList();
                roleVisibilitySettings = db.AspNetRoles.Where(r => r.Name.Contains(role.RoleName) || r.Name.Contains("Client") || r.Name.Contains("Everyone")).OrderBy(s => s.Name).ToList();

            }
            else if(User.IsInRole("Client"))
            {
                // Only allow client to make events for clients and everyone
                events = db.CalendarEvents.Where(c => c.RoleName.Contains(role.RoleName) || c.RoleName.Contains("Everyone")).ToList();
                roleVisibilitySettings = db.AspNetRoles.Where(r => r.Name.Contains(role.RoleName) || r.Name.Contains("Everyone")).OrderBy(s => s.Name).ToList();
            }

            if (!IsPostBack)
            {
                // Clear the drop down
                ddlVisibilty.Items.Clear();

                // Add initial record to drop down
                ddlVisibilty.Items.Add(new ListItem("Who can see this event?", "-1"));

                // Add appropriate visibility settings to drop down
                foreach (var r in roleVisibilitySettings)
                {
                    ddlVisibilty.Items.Add(new ListItem(r.Name, r.Name));
                }
            }
        }
        protected void btnCalendarEvent_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Viewed a Calendar Event", (int)UserActionEnum.Clicked);
            // Get the id from the clicked link button
            int id = Convert.ToInt32(Request.Form["__EVENTARGUMENT"]);

            // Query the db for that specific record
            CalendarEvent calEvent = db.CalendarEvents.Select(c => c)
                .Where(c => c.Id == id)
                .SingleOrDefault();

            // Get the file associated with this calendar event
            HPSFile file = db.HPSFiles.Where(f => f.Id == calEvent.FileId).SingleOrDefault();

            // Add data to controls
            txtEventName.Text = calEvent.CalendarEventName;
            ddlVisibilty.SelectedValue = calEvent.RoleName;
            txtEventDescription.Text = calEvent.CalendarEventDescription;
            txtEventDate.Text = Convert.ToDateTime(calEvent.CalendarEventDate).ToString("yyyy-MM-dd");
            txtEventTime.Text = Convert.ToDateTime(calEvent.CalendarEventDate).ToString("HH:mm");
            hdrEventModalTitle.InnerHtml = "<b>" + calEvent.CalendarEventName + "</b>" + " - " + Convert.ToDateTime(calEvent.CalendarEventDate).ToLongDateString();

            // if user is not administrator, disable controls
            if (!Page.User.IsInRole("Administrator"))
            {
                txtEventName.Enabled = false;
                ddlVisibilty.Enabled = false;
                txtEventDescription.Enabled = false;
                txtEventDate.Enabled = false;
                txtEventTime.Enabled = false;
                ddlEventFile.Enabled = false;
            }

            // Check if there is a file assocaited with this event
            if (file != null)
            {
                btnEventFileDownload.Attributes.Add("data-id", file.Id.ToString());
                btnEventFileDownload.OnClientClick = "";
                btnEventFileDownload.Attributes["title"] = "Download: " + file.FileName;
                ddlEventFile.SelectedValue = file.Id.ToString();
            }
            else
            {
                btnEventFileDownload.OnClientClick = "return false";
                btnEventFileDownload.Attributes["title"] = "No File to Download.";
                ddlEventFile.SelectedValue = "null";
            }

            //pnlSetFile.Visible = false;
            //pnlDownloadFile.Visible = true;

            // Add id's to button clicks
            btnDeleteEvent.Attributes.Add("data-id", calEvent.Id.ToString());
            btnSaveEvent.Attributes.Add("data-id", calEvent.Id.ToString());

            // If these 3 roles are the user thats log in, allow update and delete priviledges
            if (User.IsInRole("Administrator"))
            {
                btnDeleteEvent.Visible = true;
                btnSaveEvent.Visible = true;
                btnCreateEvent.Visible = false;
            }
            else
            {
                btnDeleteEvent.Visible = false;
                btnSaveEvent.Visible = false;
                btnCreateEvent.Visible = false;
            }

            // Open the modal to display information
            ScriptManager.RegisterStartupScript(this, this.GetType(), "eventModal", "$('#mdlEventsCalendar').modal('show');", true);
        }

        protected void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Deleted a Calendar Event", (int)UserActionEnum.Deleted);
            try
            {
                // Get the record id from the button
                int id = Convert.ToInt32(btnDeleteEvent.Attributes["data-id"]);

                // Find the record
                CalendarEvent calEvent = db.CalendarEvents.Find(id);

                // Remove the record
                db.CalendarEvents.Remove(calEvent);

                // Save to db
                db.SaveChanges();

                // Create Notification
                string[] roleNamesArray = { calEvent.RoleName };
                NotificationCreator.CreateNotification(roleNamesArray, "Event Deleted", calEvent.CalendarEventName, DateTime.Now, "Info", null, null);

                // Reset the modal controls
                ResetModalControls();
                ResetButtons(false);
                PopulateCalendarData();
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("EventManager.aspx.cs", "btnDeleteEvent_Click", dx, "An Event failed to be deleted", "HPSErrorLog.txt");
            }
        }

        protected void btnSaveEvent_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Saved an Edited Calendar Event", (int)UserActionEnum.Updated);
            try
            {
                // Get the record id from the button
                int id = Convert.ToInt32(btnSaveEvent.Attributes["data-id"]);

                // Find the record
                CalendarEvent calEvent = db.CalendarEvents.Find(id);

                // Update the information
                calEvent.CalendarEventName = txtEventName.Text;
                calEvent.CalendarEventDescription = txtEventDescription.Text;
                calEvent.CalendarEventDate = Convert.ToDateTime(txtEventDate.Text + " " + txtEventTime.Text);
                calEvent.RoleName = ddlVisibilty.SelectedValue;

                // Assign null if no file is selected
                int? noFileSelected = null;
                calEvent.FileId = (ddlEventFile.SelectedValue == "null") ? noFileSelected : Convert.ToInt32(ddlEventFile.SelectedValue);

                // Change the entry state
                db.Entry(calEvent).State = System.Data.Entity.EntityState.Modified;

                // Save to DB
                db.SaveChanges();

                // Create Notification
                string[] roleNamesArray = { calEvent.RoleName };
                NotificationCreator.CreateNotification(roleNamesArray, "Event Updated", calEvent.CalendarEventName, DateTime.Now, "Info", null, null);

                // Reset the modal controls
                ResetModalControls();
                ResetButtons(false);
                PopulateCalendarData();
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("EventManager.aspx.cs", "btnSaveEvent_Click", dx, "An Event failed to be updated", "HPSErrorLog.txt");
            }
        }

        protected void ResetModalControls()
        {
            // Clear the textboxes and reset the heading
            txtEventName.Text = "";
            txtEventDescription.Text = "";
            txtEventDate.Text = "";
            txtEventTime.Text = "";
            ddlVisibilty.SelectedValue = "-1";

            // Reset the heading
            hdrEventModalTitle.InnerHtml = "Create New Event";
        }

        protected void ResetButtons(bool isVisible)
        {
            btnCreateEvent.Visible = !isVisible;
            btnSaveEvent.Visible = isVisible;
            btnDeleteEvent.Visible = isVisible;
        }

        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Created a new Calendar Event", (int)UserActionEnum.Created);
            try
            {
                // Declare instance of calendar
                CalendarEvent calEvent = new CalendarEvent();
                calEvent.CalendarEventName = txtEventName.Text;
                calEvent.CalendarEventDescription = txtEventDescription.Text;
                calEvent.CalendarEventDate = Convert.ToDateTime(txtEventDate.Text + " " + txtEventTime.Text);
                calEvent.RoleName = ddlVisibilty.SelectedValue;

                // Assign null if no file is selected
                int? noFileSelected = null;
                calEvent.FileId = (ddlEventFile.SelectedValue == "null") ? noFileSelected : Convert.ToInt32(ddlEventFile.SelectedValue);

                // Add record and save to db
                db.CalendarEvents.Add(calEvent);
                db.SaveChanges();

                // Create Notification
                string[] roleNamesArray = { calEvent.RoleName };
                NotificationCreator.CreateNotification(roleNamesArray, "Event Created", calEvent.CalendarEventName, DateTime.Now, "Info", null, null);

                // Reset the controls
                ResetModalControls();
                ResetButtons(true);
                PopulateCalendarData();
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("EventManager.aspx.cs", "btnCreateEvent_Click", dx, "An Event failed to be created", "HPSErrorLog.txt");
            }
        }

        protected void btnCancelEvent_Click(object sender, EventArgs e)
        {
            ddlEventFile.SelectedValue = "null";
            ResetModalControls();
            ResetButtons(false);
        }


        protected void btnEventFileDownload_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Downloaded a File from an Event", (int)UserActionEnum.Downloaded);
            int id = Convert.ToInt32(btnEventFileDownload.Attributes["data-id"]);

            // Get the file from the database
            var file = db.HPSFiles.Select(s => s)
                .Where(f => f.Id == id)
                .SingleOrDefault();

            // Variables to hold file data for download
            byte[] fileToBeDownloaded = file.FileData; ;
            string fileContentType = file.FileContentType;
            string fileName = file.FileName;

            // Send the file to the browser
            HttpContext.Current.Response.AddHeader("Content-type", fileContentType);
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(fileToBeDownloaded);
            HttpContext.Current.Response.Flush();
        }
    }
}