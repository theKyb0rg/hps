using HPFS.HelperMethods;
using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HPFS.WebServices
{
    /// <summary>
    /// Summary description for Calendar
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Calendar : System.Web.Services.WebService
    {
        public static HPSDB db = new HPSDB();
        
        [WebMethod(EnableSession = true)]
        public List<Object> GetCalendarEventInformation(int id)
        {
            // Get the calendar event
            CalendarEvent c = db.CalendarEvents.Find(id);

            // Declare a list of objects to pass back to json
            List<Object> list = new List<Object>();

            // Cast the time as an object for JSON and preconvert into the appropriate time here
            Object time = (Object)Convert.ToDateTime(c.CalendarEventDate).ToString("HH:mm");

            // Add objects to list
            list.Add(c);
            list.Add(time);

            return list;
        }

        [WebMethod]
        public string BuildMobileEvents()
        {
            try
            {
                string events = "<table align='center' cellpadding='10'>"
                          + "<tr><th style='padding-right:10px;'>Date/Time</th>"
                          + "<th style='padding-left:10px;'>Event Name</th></tr>";

                var ce = db.CalendarEvents.Where(p => p.RoleName.Contains("Everyone")).ToList();
            
                if (ce.Any())
                {
                    foreach (var c in ce)
                    {
                        DateTime dt = Convert.ToDateTime(c.CalendarEventDate);
                
                        events += "<tr align='center'>"
                                + "<td style='padding-right:10px;'>" + dt.ToShortDateString() + "</td>"
                                + "<td style='padding-left:10px;'><a href='#' data-id='" + c.Id + "' name='mobCalEvnt' data-toggle='modal' data-target='#mdlMasterEventsCalendar'>" + c.CalendarEventName.ToString() + "</td>"
                                + "</tr>";
                    }                    
                }
                else
                {
                    events += "<tr align='center'>"
                            + "<td style='padding-right:10px;'> </td>"
                            + "<td style='padding-left:10px;'> No Public Events Found </td>"
                            + "</tr>";
                }

                events += "</table>";
                return events;
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("Calendar.asmx.cs", "BuildMobileEvents", dx, "Data Error when searching for 'everyone' records to populate mobile calendar modal", "HPSErrorLog.txt");
                return "<p>An Error Occured.</p>";
            }
            catch (Exception ex)
            {
                LogFile.WriteToFile("Calendar.asmx.cs", "BuildMobileEvents", ex, "Error when searching for 'everyone' records to populate mobile calendar modal", "HPSErrorLog.txt");
                return "<p>An Error Occured.</p>";
            }
        }
    }
}
