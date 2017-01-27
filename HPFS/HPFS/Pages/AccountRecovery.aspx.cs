using HPFS.HelperMethods;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;
using Twilio;
using HPFS.Models;

namespace HPFS.Pages
{
    public partial class AccountRecovery : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();
        private static string secCode;
        private static bool isPhone = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Account Recovery", (int)UserActionEnum.Navigated);
        }

        protected void btnSendCode_Click(object sender, EventArgs e)
        {
            // declare variables
            string userId = txtInfo.Text;
            lblErrors.Text = "";

            // determine whether they entered a phone # or email address
            if (Regex.IsMatch(userId, @"^\d{10}$"))
            {
                // set bool isPhone to true
                isPhone = true;

                // check if a user exists with the entered phone number
                AspNetUser u = db.AspNetUsers.Where(p => p.PhoneNumber == userId)
                                             .SingleOrDefault();

                if (u != null)
                {
                    if (u.PhoneNumberConfirmed == false)
                    {
                        lblErrors.Text = "You Have Entered the Correct Phone Number, but it hasn't been verified."
                                       + " Contact the Administrator to Reset your password.";
                    }
                    else
                    {
                        try
                        {
                            //declare variables
                            TwilioRestClient client =
                            new TwilioRestClient(ConfigurationManager.AppSettings["TwilioSID"],
                                                 ConfigurationManager.AppSettings["TwilioTOKEN"]);

                            string callerId = ConfigurationManager.AppSettings["TwilioID"];

                            // generate code
                            GenerateCode();

                            var result = client.SendMessage(callerId, userId, secCode);

                            if (result.RestException != null)
                            {
                                lblErrors.Text += result.RestException.Message;
                                LogFile.WriteToFile("AccountRecovery.aspx.cs", "btnSendCode_Click", result.RestException.Message, "Error when sending sms message", "HPSErrorLog.txt");
                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            LogFile.WriteToFile("AccountRecovery.aspx.cs", "btnSendCode_Click", ex, "Sometimes throws a null reference exception", "HPSErrorLog.txt");
                        }

                        ScriptManager.RegisterStartupScript(this,
                        GetType(),
                        "slideDiv",
                        "$('#recPass').animate({"
                            + "left: '250px',"
                            + "opacity: '0',"
                            + "width: '384px'"
                            + "}, 300, function() {"
                            + "$(this).hide();"
                            + "$('#recCode').fadeIn(300);"
                            + "});"
                        , true);

                        lblSuccess.Text = "Your security code has been sent to your phone."
                                        + "<br/>It could take up to 5 minutes to receive.";
                    }
                }
                else
                {
                    lblErrors.Text += "Either you have not added a phone number to your account or you have entered it wrong."
                                    + " Try again and if the problem persists contact the administrator.";
                }
            }
            else // email
            {
                // isPhone bool is false by default 

                // search database for a user with the entered email
                AspNetUser user = db.AspNetUsers.Where(m => m.Email == userId)
                                                .SingleOrDefault();

                if (user != null && user.EmailConfirmed)
                {
                    // Generate recovery code;
                    GenerateCode();

                    // Build mail message and Smtp Client
                    using (MailMessage mm =
                                new MailMessage(ConfigurationManager.AppSettings["Email"], userId))
                    {
                        mm.Subject = "Password Recovery";
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

                            ScriptManager.RegisterStartupScript(this,
                            GetType(),
                            "slideDiv",
                            "$('#recPass').animate({"
                                + "left: '250px',"
                                + "opacity: '0',"
                                + "width: '384px'"
                                + "}, 300, function() {"
                                + "$(this).hide();"
                                + "$('#recCode').fadeIn(300);"
                                + "});"
                            , true);

                            lblErrors.Text = "";
                            lblSuccess.Text = "Your security code has been sent to your email address."
                                            + "<br/>If you don't see the email, check your junk folder."
                                            + "<br/>It could take up to 5 minutes to receive.";
                        }
                        catch (SmtpException se)
                        {
                            lblErrors.Text += "Email failed to send try again. You may have reached your daily limit. "  
                                                                + "If the problem persists contact your administrator.";

                            LogFile.WriteToFile("AccountRecovery.aspx.cs", "btnSendCode_Click", se, "smtp exception when sending email", "HPSErrorLog.txt");
                        }
                        catch (Exception ex)
                        {
                            lblErrors.Text += " An error occured try again. If the problem persists contact your administrator.<br>";
                            LogFile.WriteToFile("AccountRecovery.aspx.cs", "btnSendCode_Click", ex, "Error trying to build and send email", "HPSErrorLog.txt");
                        }
                    }
                }
                else if(user.EmailConfirmed != true)
                {
                    lblSuccess.Text = "";
                    lblErrors.Text += "The email address you entered is associated with an HPS account"
                                    + " but it has not been Verified. "
                                    + "Please contact the administrator to Reset your Password.";
                }
                else // Invalid email address
                {
                    lblSuccess.Text = "";
                    lblErrors.Text += "The email address you entered is not associated with any of the HPS accounts."
                                    + " Please check that you have entered your email address properly, if you have done so already"
                                    + " contact the administrator.";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["login"] = "True";
            Response.Redirect("/Main.aspx");
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Verified Account Information", (int)UserActionEnum.Clicked);
            // compare verification code
            if (txtCode.Text == secCode)
            {
                lblSuccessReset.Text = "You entered the correct security code.<br/>You can now reset your password.";

                ScriptManager.RegisterStartupScript(this,
                GetType(),
                "slideDiv",
                "$('#recCode').animate({"
                    + "left: '250px',"
                    + "opacity: '0',"
                    + "width: '384px'"
                    + "}, 300, function() {"
                    + "$(this).hide();"
                    + "$('#recReset').fadeIn(300);"
                    + "});"
                , true);
            }
            else
            {
                lblSuccess.Text = "";
                lblSecErrors.Text = "The code you have entered is incorrect, try again.";
            }
        }

        protected void btnSavePass_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Saved a Password Reset", (int)UserActionEnum.Updated);
            try
            {
                if (isPhone)
                {
                    // Grab user with the phone entered
                    AspNetUser user = db.AspNetUsers.Where(u => u.PhoneNumber == txtInfo.Text).SingleOrDefault();

                    // no need to check if the user exists again

                    // Create UserManager 
                    UserManager<IdentityUser> userManager =
                        new UserManager<IdentityUser>(new UserStore<IdentityUser>());

                    // Remove old pass and add new pass
                    userManager.RemovePassword(user.Id);
                    userManager.AddPassword(user.Id, txtPass.Text);

                    db.SaveChanges();

                    // Create a notification for the database
                    string[] role = { "Administrator" };
                    NotificationCreator.CreateNotification(role, "Password Reset:", user.UserName + " reset their password", DateTime.Now, "Info", null, null);

                    ScriptManager.RegisterStartupScript(this,
                    GetType(),
                    "slideDiv",
                    "$('#recReset').animate({"
                        + "left: '250px',"
                        + "opacity: '0',"
                        + "width: '384px'"
                        + "}, 300, function() {"
                        + "$(this).hide();"
                        + "$('#recComplete').fadeIn(300);"
                        + "});"
                    , true);
                }
                else // is Email
                {
                    // Grab user with the email entered
                    AspNetUser user = db.AspNetUsers.Where(u => u.Email == txtInfo.Text).SingleOrDefault();

                    // no need to check if the user exists again

                    // Create UserManager 
                    UserManager<IdentityUser> userManager =
                        new UserManager<IdentityUser>(new UserStore<IdentityUser>());

                    // Remove old pass and add new pass
                    userManager.RemovePassword(user.Id);
                    userManager.AddPassword(user.Id, txtPass.Text);

                    db.SaveChanges();

                    // Create a notification for the database
                    string[] role = { "Administrator" };
                    NotificationCreator.CreateNotification( role, "Password Reset:", user.UserName + " reset their password", DateTime.Now, "Info", null, null);

                    ScriptManager.RegisterStartupScript(this,
                    GetType(),
                    "slideDiv",
                    "$('#recReset').animate({"
                        + "left: '250px',"
                        + "opacity: '0',"
                        + "width: '384px'"
                        + "}, 300, function() {"
                        + "$(this).hide();"
                        + "$('#recComplete').fadeIn(300);"
                        + "});"
                    , true);
                }
            }
            catch (DataException dx)
            {
                lblErrors.Text += "An error occured when saving the password. Contact your administrator.<br>";
                LogFile.WriteToFile("AccountRecovery.aspx.cs", "btnSavePass_Click", dx, "Data Error when updating password", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblErrors.Text += "An error occured when saving the password. Contact your administrator.<br>";
                LogFile.WriteToFile("AccountRecovery.aspx.cs", "btnSavePass_Click", ex, "Error when updating password.", "HPSErrorLog.txt");
            }
        }

        protected void btnUsernameNext_Click(object sender, EventArgs e)
        {
            // check if there is a user with said first and last name
            var userQuery =
                    from user in db.HPSUsers
                    join unet in db.AspNetUsers on user.UserId equals unet.Id
                    where user.FirstName == txtFirst.Text && user.LastName == txtLast.Text
                    select unet;

            if (userQuery.Any())
            {
                ScriptManager.RegisterStartupScript(this,
                GetType(),
                "slideDiv",
                "$('#recUser').animate({"
                    + "left: '250px',"
                    + "opacity: '0',"
                    + "width: '384px'"
                    + "}, 300, function() {"
                    + "$(this).hide();"
                    + "$('#recDisplay').fadeIn(300);"
                    + "});"
                , true);

                foreach (var u in userQuery)
                {
                    lblUsername.Text = u.UserName;
                }                
            }
            else
            {
                lblUsernameErrors.Text = "There is no account for someone with the name:<br/>" 
                                       + txtFirst.Text + " " + txtLast.Text;
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