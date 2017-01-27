using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;
using System.Data.Entity.Migrations;
using HPFS.Models;
using System.Data;
using System.Web.Services;

namespace HPFS
{
    public partial class UserManager : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();
        public static string userId;
        public static dynamic user;

        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("User Manager", (int)UserActionEnum.Navigated);

            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null && Page.User.IsInRole("Administrator"))
            {
                // Get the user id thats currently logged in
                userId = HttpContext.Current.Session["UserId"].ToString();
                user = db.HPSUsers.Select(u => u)
                    .Where(uid => uid.UserId == userId)
                    .SingleOrDefault();
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Created a New User", (int)UserActionEnum.Created);
            try
            {
                // Create instance of role manager and store
                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>();
                RoleManager<IdentityRole> roleMgr = new RoleManager<IdentityRole>(roleStore);

                if (!roleMgr.RoleExists(ddlRole.Text))
                {
                    IdentityResult roleResult = roleMgr.Create(new IdentityRole(ddlRole.Text));
                }

                // Declare UserStore and UserManager
                UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

                // Declare/create new user and store in manager object in the userstore
                IdentityUser user = new IdentityUser(txtUsername.Text);
                user.Email = txtEmail.Text;

                // Store result of user creation
                IdentityResult idResult = manager.Create(user, txtPassword.Text);

                // Check if user was created and added to role
                if (idResult.Succeeded)
                {
                    // Add user to role
                    IdentityResult userResult = manager.AddToRole(user.Id, ddlRole.SelectedValue);
                    lblMessage.Text = "User " + user.UserName + " was created successfully!";
                    lblMessage.CssClass = "text-success";

                    // Add other user information to separate table
                    HPSUser hpsUser = new HPSUser();
                    hpsUser.FirstName = txtUserFirstName.Text;
                    hpsUser.LastName = txtUserLastName.Text;
                    hpsUser.CreatedOn = DateTime.Now;
                    hpsUser.UserId = user.Id;
                    hpsUser.RoleName = ddlRole.SelectedValue;
                    db.HPSUsers.Add(hpsUser);

                    // Add empty row to db for fitbit to help with update if sign up date is on the same day as a synchronization
                    db.Steps.AddOrUpdate(new Step { StepCount = 0, StepDate = DateTime.Now.AddDays(-28), UserId = user.Id });
                    db.Distances.AddOrUpdate(new Distance { DistanceCount = 0, DistanceDate = DateTime.Now.AddDays(-28), UserId = user.Id });
                    db.Minutes.AddOrUpdate(new Minute { MinuteCount = 0, MinuteDate = DateTime.Now.AddDays(-28), UserId = user.Id });

                    // create notification for admin users
                    string[] role = { "Administrator" };
                    NotificationCreator.CreateNotification(role, "User Created", Page.User.Identity.Name + " created the user named '" + user.UserName + "'.", DateTime.Now, "Info", null, null);

                    // Save changes tgo db
                    db.SaveChanges();
                }
                else
                {
                    lblMessage.Text = idResult.Errors.FirstOrDefault();
                }
            }
            catch (DataException dx)
            {
                lblMessage.Text = "A data error occured. Please try again later or contact your Administrator if this continues to happen.";
                LogFile.WriteToFile("UserManager.aspx.cs", "btnCreateUser_Click", dx, "Data error when creating user", "HPSErrorLog.txt");
            }
            catch(Exception ex)
            {
                lblMessage.Text = "An error occured. Please try again later or contact your Administrator if this continues to happen.";
                LogFile.WriteToFile("UserManager.aspx.cs", "btnCreateUser_Click", ex, "Error when creating user", "HPSErrorLog.txt");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Searched For User", (int)UserActionEnum.Clicked);
            db = new HPSDB();

            // Get the creation dates
            DateTime startDate = (txtSearchStartDate.Text != String.Empty) ? Convert.ToDateTime(txtSearchStartDate.Text) : DateTime.MinValue;
            DateTime endDate = (txtSearchStartDate.Text != String.Empty) ? Convert.ToDateTime(txtSearchEndDate.Text) : DateTime.MaxValue;

            // Get the normal textboxes
            string firstName = (txtSearchFirstName.Text != String.Empty) ? txtSearchFirstName.Text : String.Empty;
            string lastName = (txtSearchLastName.Text != String.Empty) ? txtSearchLastName.Text : String.Empty;
            string email = (txtSearchEmail.Text != String.Empty) ? txtSearchEmail.Text : String.Empty;
            string username = (txtSearchUserName.Text != String.Empty) ? txtSearchUserName.Text : String.Empty;
            string role = (ddlSearchUserRole.SelectedValue != "-1") ? ddlSearchUserRole.SelectedValue : String.Empty;

            // Search based on criteria
            var users = db.HPSUsers
                .Where(f => f.FirstName.Contains(firstName))
                .Where(l => l.LastName.Contains(lastName))
                .Where(em => em.AspNetUser.Email.Contains(email))
                .Where(u => u.AspNetUser.UserName.Contains(username))
                .Where(s => s.CreatedOn >= startDate && s.CreatedOn <= endDate)
                .Where(a => a.RoleName.Contains(role))
                .ToList();


            if (users.Any())
            {
                // Build the results table
                TableBuilder.BuildUsersTable(tblUsers, users, false);
            }
            else
            {
                lblNoResults.Visible = true;
                lblNoResults.Text = "No results found.";
            }

            // Show the panel
            pnlSearchResults.Visible = true;
        }
    }
}