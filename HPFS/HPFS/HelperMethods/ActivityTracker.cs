using HPFS.Models;
using System;
using System.Data;

namespace HPFS.HelperMethods
{
    public class ActivityTracker
    {
        public static HPSDB db = new HPSDB();

        public static void Track(string PageAccessed, int ActionType)
        {
            ActivityLog log = new ActivityLog();
            log.PageAccessed = PageAccessed;
            log.UserActionID = ActionType;

            try
            {
                db.ActivityLogs.Add(log);
                db.SaveChanges();
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile(PageAccessed, "Action Type ID: " + ActionType, dx, "Activity Log Track Method Failed", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                LogFile.WriteToFile(PageAccessed, "Action Type ID: " + ActionType, ex, "Activity Log Track Method Failed", "HPSErrorLog.txt");
            }

        }
    }
}