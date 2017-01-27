using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Twilio;
using HPFS.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using HPFS.HelperMethods;

namespace HPFS.Pages
{
    public partial class UserSettings : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();
        private static string secCode;

        private void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null) { Fill(); }
            else { Response.Redirect("/Main.aspx"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("User Settings", (int)UserActionEnum.Navigated);

            // Grab currently logged in user's ID
            string userId = HttpContext.Current.Session["UserId"].ToString();

            // Query the db for user contact info
            var userQuery =
                    from user in db.HPSUsers
                    join unet in db.AspNetUsers on user.UserId equals unet.Id
                    where user.UserId == userId
                    select new { EmailConf = unet.EmailConfirmed, PhoneConf = unet.PhoneNumberConfirmed };

            foreach (var user in userQuery)
            {
                txtIsEmailVerified.Text = user.EmailConf.ToString();
                txtIsMobileVerified.Text = user.PhoneConf.ToString();
            }

            // Reset a few labels that caused issues when filling out both forms at the same time 
            lblContactErrors.Text = "";
            lblContactSuccess.Text = "";
            lblPassSuccess.Text = "";
            lblPassErrors.Text = "";
            lblOldPass.Text = "Current Password:";
            lblVerifyEmail.Text = "";
            lblVerifyEmailSuccess.Text = "";
        }

        // Fill textboxes for Contact Info when page initially loads and changes take place.
        private void Fill()
        {
            try
            {
                // Grab currently logged in user's ID
                string userId = HttpContext.Current.Session["UserId"].ToString();

                // Query the db for user contact info
                var userQuery =
                        from user in db.HPSUsers
                        join unet in db.AspNetUsers on user.UserId equals unet.Id
                        where user.UserId == userId
                        select new { First = user.FirstName, Last = user.LastName, Phone = unet.PhoneNumber, Email = unet.Email, EmailConf = unet.EmailConfirmed, PhoneConf = unet.PhoneNumberConfirmed };

                // Loop through setting the textboxes equal to the corresponding user values
                foreach (var user in userQuery)
                {
                    txtFN.Text = user.First;
                    txtLN.Text = user.Last;
                    txtPhone.Text = user.Phone;
                    txtEmail.Text = user.Email;
                    txtIsEmailVerified.Text = user.EmailConf.ToString();
                    txtIsMobileVerified.Text = user.PhoneConf.ToString();
                }
            }
            catch (DataException dx)
            {
                lblContactErrors.Text = "A data error occured. Please try again later or contact your Administrator if this continues to happen.";
                LogFile.WriteToFile("UserSettings.aspx.cs", "Fill", dx, "Database failed to grab user data", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblContactErrors.Text = "An error occured. Please try again later or contact your Administrator if this continues to happen.";
                LogFile.WriteToFile("UserSettings.aspx.cs", "Fill", ex, "Error trying to populate textboxes", "HPSErrorLog.txt");
            }
        }

        protected void btnSaveContact_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Saved New Contact Info", (int)UserActionEnum.Updated);

            try
            {
                // Grab current user ID
                string userId = HttpContext.Current.Session["UserId"].ToString();

                // Query database
                HPSUser user = db.HPSUsers.Where(u => u.UserId == userId).SingleOrDefault();

                // Update the information
                user.FirstName = txtFN.Text;
                user.LastName = txtLN.Text;

                if (txtEmail.Text != user.AspNetUser.Email)
                {
                    user.AspNetUser.EmailConfirmed = false;
                }

                user.AspNetUser.Email = txtEmail.Text;

                if (txtPhone.Text != user.AspNetUser.PhoneNumber)
                {
                    user.AspNetUser.PhoneNumberConfirmed = false;
                }

                user.AspNetUser.PhoneNumber = txtPhone.Text;

                // Change the entry state
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                // Save to DB
                db.SaveChanges();

                // Display success message
                lblContactSuccess.Text = "<br><br>Contact Information Changes Saved.";
            }
            catch (DataException dx)
            {
                lblContactErrors.Text += "A data error occured, contact your administrator if this continues to happen.<br>";
                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSaveContact_Click", dx, "Database failed to save updated contact information", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblContactErrors.Text += "An error occured, contact your administrator if this continues to happen.<br>";
                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSaveContact_Click", ex, "Database failed to save updated contact information", "HPSErrorLog.txt");
            }
        }

        protected void btnCancelContact_Click(object sender, EventArgs e) { Fill(); }

        protected void btnSavePass_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Saved New Password", (int)UserActionEnum.Updated);
            try
            {
                // Clear labels 
                lblPassErrors.Text = "";
                lblPassSuccess.Text = "";
                lblOldPass.Text = "Current Password:";

                // Grab current user ID and store Textbox strings (jquery to filter data gathered)
                string userId = HttpContext.Current.Session["UserId"].ToString(),
                       newPass = txtRePass.Text,
                       rePass = txtNewPass.Text,
                       oldPass = txtOldPass.Text;

                // Query database
                AspNetUser user = db.AspNetUsers.Where(u => u.Id == userId).SingleOrDefault();

                // Create UserManager 
                UserManager<IdentityUser> userManager =
                new UserManager<IdentityUser>(new UserStore<IdentityUser>());

                // Verify that the db password and the current password field entry are the same
                PasswordVerificationResult check = (userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, oldPass));
                bool test = Convert.ToBoolean(check);

                if (test)
                {
                    // Change password for the user
                    userManager.ChangePassword(userId, oldPass, newPass);

                    // Save changes
                    db.SaveChanges();

                    // Display success message
                    lblPassSuccess.Text = "<br><br>Password Changed.";
                }
                else
                {
                    lblPassErrors.Text = "<br><br>You have not entered your current password correctly.<br>Make sure Caps Lock is off.";
                    lblOldPass.Text = "Current Password: <span class='text-danger'>*</span>";
                }
            }
            catch (DataException dx)
            {
                lblPassErrors.Text += "A data error occured, contact your administrator if this continues to happen.<br>";
                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSavePass_Click", dx, "Database failed to save updated password", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblPassErrors.Text += "An error occured, contact your administrator if this continues to happen.<br>";
                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSavePass_Click", ex, "Error when updating password", "HPSErrorLog.txt");
            }
        }

        protected void btnCancelPass_Click(object sender, EventArgs e)
        {
            // Reset textboxes
            txtNewPass.Text = "";
            txtOldPass.Text = "";
            txtRePass.Text = "";
        }

        protected void btnSendCellCode_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Sent A Verification Code to a Mobile Phone", (int)UserActionEnum.Clicked);

            //Clear Labels
            lblVerifyCell.Text = "";
            lblVerifyCellConflict.Text = "";

            try
            {
                TwilioRestClient client =
                    new TwilioRestClient(ConfigurationManager.AppSettings["TwilioSID"],
                                         ConfigurationManager.AppSettings["TwilioTOKEN"]);

                string twilioId = ConfigurationManager.AppSettings["TwilioID"];

                string CELL = txtVerifyCell.Text;
                string userId = HttpContext.Current.Session["UserId"].ToString();

                if (userId != null)
                {
                    // Query database
                    AspNetUser user = db.AspNetUsers.Where(u => u.Id == userId).SingleOrDefault();

                    if (user.PhoneNumber != txtVerifyCell.Text)
                    {
                        lblVerifyCell.Text = "You Must First Add Your Mobile to your Contact Information before you can Verify it.";
                    }
                    else if (user.PhoneNumber == txtVerifyCell.Text)
                    {
                        // verify and then add phone number entered
                        var callerId = client.AddOutgoingCallerId(CELL, user.UserName + " Cell", null, null);

                        if (callerId.RestException != null)
                        {
                            if (callerId.RestException.Message == "Phone number is already verified.")
                            {
                                lblVerifyCellConflict.Text = callerId.RestException.Message;

                                user.PhoneNumberConfirmed = true;

                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                                db.SaveChanges();

                                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSendCellCode_Click", callerId.RestException.Message, "Number already verified with twilio.", "HPSErrorLog.txt");
                            }
                            else
                            {
                                user.PhoneNumberConfirmed = false;

                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                                db.SaveChanges();

                                lblVerifyCellConflict.Text = callerId.RestException.Message;
                                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSendCellCode_Click", callerId.RestException.Message, "Error when trying to verify mobile number.", "HPSErrorLog.txt");
                            }                            
                        }
                        else
                        {
                            // display code to user for them to enter on their phone
                            txtVerifyCellCode.Text = callerId.ValidationCode;

                            user.PhoneNumberConfirmed = true;

                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    lblVerifyCell.Text = "Could not find your information in the database, Please refresh the page and try again.";
                }
            }
            catch (DataException dx)
            {
                lblVerifyCell.Text += " An error occured try again. If the problem persists contact your administrator.<br>";
                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSendCellCode_Click", dx, "Data error when searching for user with the entered phone number.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblVerifyCell.Text += " An error occured try again. If the problem persists contact your administrator.<br>";
                LogFile.WriteToFile("UserSettings.aspx.cs", "btnSendCellCode_Click", ex, "error when trying to find user and create rest client.", "HPSErrorLog.txt");
            }
        }

        protected void btnSendEmailCode_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Sent A Verification Code to an Email Address", (int)UserActionEnum.Clicked);

            // Grab current user ID
            string userId = HttpContext.Current.Session["UserId"].ToString();

            // Query database
            AspNetUser user = db.AspNetUsers.Where(u => u.Id == userId).SingleOrDefault();

            if (user.EmailConfirmed && user.Email == txtVerifyEmail.Text)
            {
                lblVerifyEmailConflict.Text = "The Email Entered has Already Been Verified";
            }
            else if (user.EmailConfirmed && user.Email != txtVerifyEmail.Text)
            {
                lblVerifyEmailConflict.Text = "The Email Entered is Not Associated With Your Account."
                                            + "<br />You Must First Add it to Your Contact Information to Verify it.";
            }
            else if (!user.EmailConfirmed && user.Email != txtVerifyEmail.Text)
            {
                lblVerifyEmailConflict.Text = "The Email Entered is Not Associated With Your Account."
                                            + "<br />You Must First Add it to Your Contact Information to Verify it.";
            }
            else
            {
                // Clear labels
                lblVerifyEmailConflict.Text = "";
                lblVerifyEmail.Text = "";

                // Generate recovery code;
                GenerateCode();

                // Build mail message and Smtp Client
                using (MailMessage mm =
                            new MailMessage(ConfigurationManager.AppSettings["Email"], txtVerifyEmail.Text))
                {
                    mm.Subject = "Email Verification";
                    mm.Body = "Here is your security code: " + secCode;
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
                        // try sending email              
                        smtp.Send(mm);

                        lblVerifyEmail.Text = "";
                        lblVerifyEmailSuccess.Text = "Your security code has been sent to your email address."
                                                   + "<br/>If you don't see the email, check your junk folder."
                                                   + "<br/>It could take up to 5 minutes to receive.";
                    }
                    catch (SmtpException se)
                    {
                        lblVerifyEmail.Text += "Email failed to send try again. You may have reached your daily limit. "
                                            + "If the problem persists contact your administrator.";

                        LogFile.WriteToFile("UserSettings.aspx.cs", "btnSendEmailCode_Click", se, "SmtpException when sending email to user", "HPSErrorLog.txt");
                    }
                    catch (Exception ex)
                    {
                        lblVerifyEmail.Text += " An error occured try again. If the problem persists contact your administrator.<br>";
                        LogFile.WriteToFile("UserSettings.aspx.cs", "btnSendEmailCode_Click", ex, "Exception when sending email to user", "HPSErrorLog.txt");
                    }
                }
            }
        }

        protected void btnVerifyEmailCode_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Checked Email Verificaton Code", (int)UserActionEnum.Clicked);

            if (txtVerifyEmailCode.Text == secCode)
            {
                try
                {
                    // Find User
                    string userId = HttpContext.Current.Session["UserId"].ToString();

                    AspNetUser user = db.AspNetUsers.Where(u => u.Id == userId).SingleOrDefault();

                    // Update User info
                    user.EmailConfirmed = true;

                    // Change the entry state
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                    // Save to DB
                    db.SaveChanges();

                    lblVerifyEmailSuccess.Text = "You Have Successfully Verfied Your Email."
                                               + "<br />Some Changes Won't Appear Until the Page has Been Reloaded.";
                }
                catch (DataException dx)
                {
                    lblVerifyEmail.Text = "There was an Error When Updating Your Information, Please Try Again.";
                    LogFile.WriteToFile("UserSettings.aspx.cs", "btnVerifyEmailCode_Click", dx, "Data error when updating user info", "HPSErrorLog.txt");
                }
                catch (Exception ex)
                {
                    lblVerifyEmail.Text = "There was an Error When Updating Your Information, Please Try Again.";
                    LogFile.WriteToFile("UserSettings.aspx.cs", "btnVerifyEmailCode_Click", ex, "Error when verifying code and updating user info", "HPSErrorLog.txt");
                }

            }
            else
            {
                lblVerifyEmail.Text = "You Have Entered The Incorrect Code."
                                    + "<br />Try again, if the problem persists contact the administrator.";
            }
        }

        protected void GenerateCode()
        {
            string code = "";
            string nums = "1234567890";
            Random randGen = new Random();
            int rand = 0;

            // generate random code
            for (int i = 0; i < 7; i++)
            {
                rand = randGen.Next(0, 10);
                code += nums[rand];
            }

            secCode = code;
        }
    }
}