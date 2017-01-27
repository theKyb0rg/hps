using HPFS.HelperMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace HPFS.WebServices
{
    /// <summary>
    /// Summary description for UserManager
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserManager : System.Web.Services.WebService
    {
        public static HPSDB db = new HPSDB();

        [WebMethod(EnableSession = true)]
        public string DeleteUser(string id)
        {
            // Find the other half of the user in the aspnetuser table
            AspNetUser deletedUser = db.AspNetUsers.Find(id);

            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var user = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            try
            {
                // Remove the details from both users
                db.AspNetUsers.Remove(deletedUser);

                // Store deleted user name before the saving changes to add to notifcation creator
                string deletedUserName = deletedUser.UserName;

                // Only admins will see the users delete so hard code in the role required for this
                string[] roleArray = { "Administrator" };

                // Save Changes to the database
                db.SaveChanges();

                // Create a notification for the database
                NotificationCreator.CreateNotification(roleArray, "User Deleted", user.AspNetUser.UserName + " deleted the user named '" + deletedUserName + "'.", DateTime.Now, "Info", null, null);

                return user.AspNetUser.UserName + " was successfully deleted.";
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("UserManager.asmx.cs", "DeleteUser", dx, "Webmethod failed to delete user from database", "HPSErrorLog.txt");
                return "A data error occurred. " + user.AspNetUser.UserName + " could not be deleted at this time. Please try again later or inform an Administrator.";
            }
            catch (Exception ex)
            {
                LogFile.WriteToFile("UserManager.asmx.cs", "DeleteUser", ex, "Error when trying to delete user", "HPSErrorLog.txt");
                return "An error occurred. " + user.AspNetUser.UserName + " could not be deleted at this time. Please try again later or inform an Administrator.";
            }
        }
    }
}
