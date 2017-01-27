using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using HPFS.HelperMethods;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HPFS.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace HPFS
{
    public partial class HPFSMaster : System.Web.UI.MasterPage
    {
        public static HPSDB db = new HPSDB();
        public dynamic events;

        protected void EventCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (events != null)
            {
                foreach (var evnt in events)
                {
                    if (Convert.ToDateTime(evnt.CalendarEventDate).ToShortDateString() == e.Day.Date.ToShortDateString())
                    {
                        HtmlGenericControl link = new HtmlGenericControl("a");
                        link.InnerHtml = evnt.CalendarEventName;
                        link.Attributes.Add("data-id", evnt.Id.ToString());
                        link.Attributes.Add("name", "calendar-event");
                        link.Attributes.Add("href", "#");

                        //link.NavigateUrl = Page.ClientScript.GetPostBackClientHyperlink(btnCalendarEvent, evnt.Id.ToString(), true);
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
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                // Get the calendar events
                events = db.CalendarEvents.Where(s => s.RoleName.Contains("Everyone")).ToList();
                //foreach(var e in events)
                //{

                //}
            }
            catch (InvalidOperationException dx)
            {
                LogFile.WriteToFile("HPFSMaster.Master.cs", "Page_Init", dx, "InvalidOperationException", "HPSErrorLog.txt");
            }
            catch (NullReferenceException ex)
            {
                LogFile.WriteToFile("HPFSMaster.Master.cs", "Page_Init", ex, "The Page loads and throws a null reference exception", "HPSErrorLog.txt");
            }

            // By Default hide Admin Settings
            SwapAdminControls(false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                try
                {
                    string userId = Session["UserId"].ToString();
                    TableBuilder.BuildNotificationTable(userId, tblNotifications);

                    // Restricting access depending on role
                    if (Page.User.IsInRole("Administrator"))
                    {
                        SwapAdminControls(true);

                        indexDashLink.Visible = true;
                        dashboardList.Visible = true;
                        indexList.Attributes.Remove("class");
                        indexList.Attributes.Add("class", "col-xs-4 col-md-4");
                        aboutList.Attributes.Remove("class");
                        aboutList.Attributes.Add("class", "col-xs-4 col-md-4");

                        if (!IsPostBack)
                        {
                            PopulateContactInfo();
                        }
                    }
                    else
                    {
                        SwapAdminControls(false);

                        indexDashLink.Visible = true;
                        dashboardList.Visible = true;
                        indexList.Attributes.Remove("class");
                        indexList.Attributes.Add("class", "col-xs-4 col-md-4");
                        aboutList.Attributes.Remove("class");
                        aboutList.Attributes.Add("class", "col-xs-4 col-md-4");
                    }
                }
                catch (DataException dx)
                {
                    LogFile.WriteToFile("HPFSMaster.Master.cs", "Page_Load", dx, "Failed to run page load", "HPSErrorLog.txt");
                }

                lbUserName.Text = Page.User.Identity.Name + "&nbsp;<span class='caret'></span>";
                SwitchMenuControls(true);
            }
            else
            {
                SwitchMenuControls(false);

                indexDashLink.Visible = false;
                dashboardList.Visible = false;
                indexList.Attributes.Remove("class");
                indexList.Attributes.Add("class", "col-xs-6 col-md-6");
                aboutList.Attributes.Remove("class");
                aboutList.Attributes.Add("class", "col-xs-6 col-md-6");
            }

            // Check for page to determine active class on the navbar
            String activepage = Request.RawUrl;

            if (activepage.Contains("/Main.aspx"))
            {
                page1.Attributes["class"] = "navActive";
            }
            if (activepage.Contains("/Pages/RehabilitationAndTreatment.aspx"))
            {
                page2.Attributes["class"] = "navActive";
            }
            else if (activepage.Contains("/Pages/EducationAndResearch.aspx"))
            {
                page3.Attributes["class"] = "navActive";
            }
            else if (activepage.Contains("/Pages/Programs.aspx"))
            {
                page4.Attributes["class"] = "navActive";
            }
            else if (activepage.Contains("/Pages/ContactUs.aspx"))
            {
                page5.Attributes["class"] = "navActive";
            }
            else if (activepage.Contains("/Pages/AboutUs.aspx"))
            {
                page6.Attributes["class"] = "navActive";
                page6Mobile.Attributes["class"] = "navActive";
            }
            else if (activepage.Contains("/Pages/Dashboard.aspx"))
            {
                page7.Attributes["class"] = "navActive";
                page7Mobile.Attributes["class"] = "navActive";
            }

            // Dynamically change contact info and office hours fields in footer  
            DynamicInformation();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Logged In", (int)UserActionEnum.LoggedIn);

            //declare the collection of users
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();

            //declare the user manager
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

            //try to find the user
            IdentityUser user = manager.Find(txtUsername.Text, txtPassword.Text);

            if (user == null)
            {
                lblMessage.Text = "Username or Password is incorrect";
            }
            else
            {
                //authenticate user
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(userIdentity);

                Session["UserId"] = user.Id;
                Response.Redirect("/Pages/Dashboard.aspx");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Clear Login Textboxes
            txtUsername.Text = "";
            txtPassword.Text = "";
            lblMessage.Text = "";
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            SwitchMenuControls(false);
            Response.Redirect("/Main.aspx");
        }

        public void SwitchMenuControls(bool isVisible)
        {
            plhLogout.Visible = isVisible;
            plhLogin.Visible = !isVisible;
            plhDashboard.Visible = isVisible;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pages/AccountRecovery.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Submitted Feedback", (int)UserActionEnum.Created);

            // Clear error label
            lblFeedbackErrors.Text = "";

            try
            {
                // declare variables 
                int rec = Convert.ToInt32(radRec.SelectedValue);
                int nav = Convert.ToInt32(radNav.SelectedValue);
                int app = Convert.ToInt32(radAppear.SelectedValue);
                int acc = Convert.ToInt32(radAccess.SelectedValue);
                double avg = (Convert.ToDouble(rec) + Convert.ToDouble(nav) + Convert.ToDouble(app) + Convert.ToDouble(acc)) / 4;

                // Create new entry and build it 
                var fb = new FeedBack();
                fb.FeedBackAccRating = acc;
                fb.FeedBackAppRating = app;
                fb.FeedBackNavRating = nav;
                fb.FeedBackRecRating = rec;
                fb.FeedBackAvg = avg;
                fb.FeedBackDate = DateTime.Now.Date;
                fb.FeedBackComment = txtComment.InnerText;
                fb.FeedBackArea = ddlSiteArea.SelectedValue.ToString();

                // Add item to db and save changes
                db.Feedbacks.Add(fb);
                db.SaveChanges();

                // Create a notification for the database
                string[] role = { "Administrator" };
                string comment = "";
                if (txtComment.InnerText.Length >= 24)
                {
                    comment = txtComment.InnerText.Substring(0, 24);
                }
                else
                {
                    comment = txtComment.InnerText;
                }

                NotificationCreator.CreateNotification(role, "Feedback Submitted", "Comment: " + comment + "...", DateTime.Now, "Info", null, null);
            }
            catch (DataException dx)
            {
                // Display error to user and log it.
                lblFeedbackErrors.Text = "Your feedback failed to submit, please try again.\nIf the problem persists contact the administrator.";
                LogFile.WriteToFile("HPFSMaster.Master.cs", "btnSubmit_Click", dx, "Feedback failed to save in database", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblFeedbackErrors.Text = "Your feedback failed to submit, please try again.\nIf the problem persists contact the administrator.";
                LogFile.WriteToFile("HPFSMaster.Master.cs", "btnSubmit_Click", ex, "Feedback failed to submit", "HPSErrorLog.txt");
            }

            // reset feedback form values
            txtComment.Value = "";
            radAccess.SelectedValue = "5";
            radAppear.SelectedValue = "5";
            radNav.SelectedValue = "5";
            radRec.SelectedValue = "5";
        }

        // Update Contact & Hours Information
        protected void btnSaveAdminSettings_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Administrator Settings Changed", (int)UserActionEnum.Updated);

            // Clear error label
            lblAdminSettingsErrors.Text = "";

            // Load Administrator Settings Modal Data To Update it
            Contact info = db.Contacts.Find(1);

            // Contact Information
            info.Address = txtAddress.Text;
            info.Company = txtCompany.Text;
            info.Email = txtEmail.Text;
            info.Fax = txtFax.Text;
            info.Telephone = txtTelephone.Text;

            // Office Hours
            info.Monday = txtMonday.Text;
            info.Tuesday = txtTuesday.Text;
            info.Wednesday = txtWednesday.Text;
            info.Thursday = txtThursday.Text;
            info.Friday = txtFriday.Text;
            info.Saturday = txtSaturday.Text;
            info.Sunday = txtSunday.Text;

            // Banner Message
            info.BannerMessage = txtBanner.Text;
            info.BannerColor = colorPicker.Attributes["value"];

            try
            {
                // Change the entry state
                db.Entry(info).State = System.Data.Entity.EntityState.Modified;

                // Save to DB
                db.SaveChanges();

                // Create a notification for the database
                string[] role = { "Everyone" };
                NotificationCreator.CreateNotification(role, "Information Updated", "Contact Info and Hours Changed", DateTime.Now, "Info", null, null);

                // To add new info to page
                Response.Redirect(Request.RawUrl);
            }
            catch (DataException dx)
            {
                // Display error to user and log it.
                lblAdminSettingsErrors.Text = "The changes you made failed to save, please try again.\nIf the problem persists contact the administrator.";
                LogFile.WriteToFile("HPFSMaster.Master.cs", "btnSaveAdminSettings_Click", dx, "Admin Settings Information change failed to save in db", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblAdminSettingsErrors.Text = "The changes you made failed to save, please try again.\nIf the problem persists contact the administrator.";
                LogFile.WriteToFile("HPFSMaster.Master.cs", "btnSaveAdminSettings_Click", ex, "Admin Settings Information failed to update", "HPSErrorLog.txt");
            }
        }

        protected void PopulateContactInfo()
        {
            // Load Administrator Settings Modal Data
            Contact info = db.Contacts.Find(1);

            // Contact Information
            txtAddress.Text = info.Address;
            txtCompany.Text = info.Company;
            txtEmail.Text = info.Email;
            txtFax.Text = info.Fax;
            txtTelephone.Text = info.Telephone;

            // Office Hours
            txtMonday.Text = info.Monday;
            txtTuesday.Text = info.Tuesday;
            txtWednesday.Text = info.Wednesday;
            txtThursday.Text = info.Thursday;
            txtFriday.Text = info.Friday;
            txtSaturday.Text = info.Saturday;
            txtSunday.Text = info.Sunday;

            //Banner
            txtBanner.Text = info.BannerMessage;
            colorPicker.Attributes["value"] = info.BannerColor;
        }

        protected void SwapAdminControls(bool isVisible)
        {
            fitbitMgr.Visible = isVisible;
            activityTracker.Visible = isVisible;
            slideshowMgr.Visible = isVisible;
            userMgr.Visible = isVisible;
            feedbackMgr.Visible = isVisible;
            adminSettings.Visible = isVisible;
            pgAM.Visible = isVisible;
            pgFM.Visible = isVisible;
            pgAM_mobile.Visible = isVisible;
            pgFM_mobile.Visible = isVisible;
            pnlDashHidden.Visible = isVisible;
            pnlDashMobileHidden.Visible = isVisible;
            programMgr.Visible = isVisible;
            notificationLog.Visible = isVisible;
            adminSettingsMobile.Visible = isVisible;
            notificationLogMobile.Visible = isVisible;
        }

        protected void DynamicInformation()
        {
            try
            {
                Contact data = db.Contacts.Find(1);

                string hoursInfo = "<p><b>Monday: </b>" + data.Monday + "</p>"
                                 + "\n<p><b>Tuesday: </b>" + data.Tuesday + "</p>\n<p><b>Wednesday: </b>" + data.Wednesday + "</p>"
                                 + "\n<p><b>Thursday: </b>" + data.Thursday + "</p>\n<p><b>Friday: </b>" + data.Friday + "</p>"
                                 + "\n<p><b>Saturday: </b>" + data.Saturday + "</p>\n<p><b>Sunday: </b>" + data.Sunday + "</p>";

                string contactInfo = "<p>" + data.Company + "</p>"
                                   + "\n<p><b>Telephone: </b>" + data.Telephone + "</p>\n<p><b>Fax: </b>" + data.Fax + "</p>"
                                   + "\n<p><b>Email: </b><a href='mailto: " + data.Email + "' class='mail'>" + data.Email + "</a></p>"
                                   + "\n<p><b>Address: </b>" + data.Address + "</p>";

                hours.InnerHtml = "<h3>Office Hours</h3>";
                hiddenHours.InnerHtml = "<h3>Office Hours</h3>";
                contact.InnerHtml = "<h3>Contact Information</h3>";

                // Populate page with info from db
                hours.InnerHtml += hoursInfo;
                hours2.InnerHtml = hoursInfo;
                hiddenHours.InnerHtml += hoursInfo;
                contact.InnerHtml += contactInfo;
                contact2.InnerHtml = contactInfo;

                if (data.BannerMessage != "")
                {
                    bannerMsg.Attributes.Remove("style");
                    bannerMsg.Attributes.Add("style", "color:" + data.BannerColor);
                    bannerMsg.InnerHtml = data.BannerMessage;
                }
            }
            catch (DataException dx)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Unable to load contact data from database. Try reloading the page.');", true);
                LogFile.WriteToFile("HPFSMaster.Master.cs", "DynamicInformation()", dx, "Contact info failed to load from db.", "HPSErrorLog.txt");
            }
            catch (InvalidOperationException ioex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Unable to load contact data from database. Try reloading the page.');", true);
                LogFile.WriteToFile("HPFSMaster.Master.cs", "DynamicInformation()", ioex, "Contact info failed to load.", "HPSErrorLog.txt");
            }
        }

        protected void btnContactSend_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Sent Contact Email", (int)UserActionEnum.Created);

            // Create an info object to dynamically set email address
            Contact info = db.Contacts.Find(1);

            using (MailMessage mm =
                                new MailMessage(ConfigurationManager.AppSettings["Email"], info.Email))
            {
                mm.Subject = txtContactSubject.Text;
                mm.Body = "Reply Address: " + txtContactEmail.Text + "\n\n" + tarContactMessage.InnerText;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-mail.outlook.com";
                smtp.EnableSsl = true;

                NetworkCredential NetworkCred =
                    new NetworkCredential(ConfigurationManager.AppSettings["Email"],
                                          ConfigurationManager.AppSettings["Password"]);

                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;

                try
                {
                    // Clear error label
                    lblContactFormErrors.Text = "";

                    // try sending email              
                    smtp.Send(mm);

                    // Create a notification for the database
                    string[] role = { "Administrator" };
                    NotificationCreator.CreateNotification(role, "Contact Email Sent", "Subject: " + txtContactSubject.Text, DateTime.Now, "Info", null, null);

                    // Alert user of success
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Your email has been successfully sent. We will get back to you as soon as possible.');", true);
                }
                catch (SmtpException se)
                {
                    lblContactFormErrors.Text += "Email failed to send try again. You may have reached your daily limit. "
                                               + "If the problem persists contact your administrator.";

                    LogFile.WriteToFile("HPFSMaster.Master.cs", "btnContactSend_Click", se, "Contact email failed to send.", "HPSErrorLog.txt");
                }
                catch (Exception ex)
                {
                    lblContactFormErrors.Text += " An error occured try again. If the problem persists contact your administrator.<br>";
                    LogFile.WriteToFile("HPFSMaster.Master.cs", "btnContactSend_Click", ex, "Error caused Contact email to fail.", "HPSErrorLog.txt");
                }
            }
        }

        protected void lbLandingPage_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Cookies["LandingPage"] != null)
            {
                HttpCookie LandingPage = HttpContext.Current.Request.Cookies["LandingPage"];
                HttpContext.Current.Response.Cookies.Remove("LandingPage");
                LandingPage.Expires = DateTime.Now.AddDays(-10);
                LandingPage.Value = null;
                HttpContext.Current.Response.SetCookie(LandingPage);
                Response.Redirect("/Pages/LandingPage.aspx");
            }
            else
            {
                Response.Redirect("/Pages/LandingPage.aspx");
            }
        }
    }
}