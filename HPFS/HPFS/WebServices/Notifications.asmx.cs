using HPFS.HelperMethods;
using HPFS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS.WebServices
{
    /// <summary>
    /// Summary description for Notifications
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Notifications : System.Web.Services.WebService
    {
        public static HPSDB db = new HPSDB();

        [WebMethod(EnableSession = true)]
        public Array UpdateNotification(string id)
        {
            try
            {
                int ID = Convert.ToInt32(id);

                // Find the record
                Notification notiRecord = db.Notifications.Find(ID);

                // Update the information
                notiRecord.IsRead = true;

                // Change the entry state
                db.Entry(notiRecord).State = System.Data.Entity.EntityState.Modified;

                // Save to DB
                db.SaveChanges();

                string userId = HttpContext.Current.Session["UserId"].ToString();

                var notiQuery =
                            db.Notifications
                            .Where(u => u.UserId == userId)
                            .Where(n => n.IsRead == false)
                            .Select(noti => new NotificationViewModel { Id = noti.Id, Title = noti.Title, Description = noti.Description, NotificationDate = noti.NotificationDate, Priority = noti.Priority })
                            .ToArray();

                return notiQuery;
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("Notifications.asmx.cs", "UpdateNotification", dx, "Data error when trying to Update a Notification", "HPSErrorLog.txt");
                string[] empty = { };
                return empty;
            }
            catch (Exception ex)
            {
                LogFile.WriteToFile("Notifications.asmx.cs", "UpdateNotification", ex, "Error when trying to Update a Notification", "HPSErrorLog.txt");
                string[] empty = { };
                return empty;
            }
        }

        [WebMethod(EnableSession = true)]
        public Array ReadAllNotifications()
        {
            try
            {
                string userId = HttpContext.Current.Session["UserId"].ToString();

                // Find the record
                var records = db.Notifications.Where(u => u.UserId == userId)
                                              .Where(r => r.IsRead == false)
                                              .ToArray();

                for (int i = 0; i < records.Count(); i++)
                {
                    // Update the information
                    records[i].IsRead = true;
                }

                // Save to DB
                db.SaveChanges();

                var notiQuery =
                            db.Notifications
                            .Where(u => u.UserId == userId)
                            .Where(n => n.IsRead == false)
                            .Select(noti => new NotificationViewModel { Id = noti.Id, Title = noti.Title, Description = noti.Description, NotificationDate = noti.NotificationDate, Priority = noti.Priority })
                            .ToArray();

                return notiQuery;
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("Notifications.asmx.cs", "ReadAllNotifications", dx, "Data error when trying to Update all Notifications", "HPSErrorLog.txt");
                string[] empty = { };
                return empty;
            }
            catch (Exception ex)
            {
                LogFile.WriteToFile("Notifications.asmx.cs", "ReadAllNotifications", ex, "Error when trying to Update all Notifications", "HPSErrorLog.txt");
                string[] empty = { };
                return empty;
            }
        }
    }
}
