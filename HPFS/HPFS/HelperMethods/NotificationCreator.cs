using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFS.HelperMethods
{
    public class NotificationCreator
    {
        public static HPSDB db = new HPSDB();
        public static void CreateNotification(string[] roleNamesArray, string title, string description, DateTime date, string priority, int? fileId, int? calendarEventId)
        {
            // Get all users within the role where the notification needs to be sent to
            var allUsers = db.HPSUsers.Select(u => u);
            List<List<HPSUser>> usersInRole = new List<List<HPSUser>>();

            // Check for leading and ending spaces to clean up the query
            char[] charsToTrim = { ' ' };

            // Preset flag to indicate the admin user is not included
            bool adminIncluded = false;
            // Loop through roleNames array to get all users from every role passed into the function
            for (int i = 0; i < roleNamesArray.Length; i++)
            {
                // If the role is everyone, add all users to the role names array and set the admin included flag to true
                if(roleNamesArray[i] == "Everyone")
                {
                    adminIncluded = true;
                    usersInRole.Clear();
                    usersInRole.Add(allUsers.ToList());
                    break;
                }

                string roleName = roleNamesArray[i].Trim(charsToTrim);
                usersInRole.Add(allUsers.Where(r => r.RoleName.Contains(roleName)).ToList());

                // if the admin user is included in the list of roles change the boolean to indicate so
                if(roleNamesArray[i] == "Administrator")
                {
                    adminIncluded = true;
                }
            }

            // Add all adminstrator roles also if it wasn't already included in the list of roles
            if (!adminIncluded)
            {
                usersInRole.Add(allUsers.Where(admin => admin.RoleName.Contains("Administrator")).ToList());
            }

            // Search the list containining lists of users and create a notification for each user
            int j = 0;
            foreach (var u in usersInRole)
            {
                foreach (var r in usersInRole[j])
                {
                    // Create new notification for each user in that role
                    Notification n = new Notification();
                    n.Title = title;
                    n.Description = description;
                    n.NotificationDate = date;
                    n.IsRead = false;
                    n.Priority = priority;
                    n.UserId = r.AspNetUser.Id;
                    n.FileId = fileId;
                    n.CalendarEventId = calendarEventId;

                    // Add to database
                    db.Notifications.Add(n);
                }
                // Reset counter variable
                j++;
            }

            try
            {
                // Save
                db.SaveChanges();
            }
            catch (DataException)
            {
                // Catch errors
            }
        }
    }
}
