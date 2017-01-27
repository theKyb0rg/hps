using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HPFS.HelperMethods
{
    public class LogFile
    {
        public static void WriteToFile(string codeFileName, string methodName, Exception exception, string additionalInformation, string file)
        {
            // Set the date and time of this event
            string date = DateTime.Now.ToString();

            // Store the log file in the app data folder
            string fileName = Path.Combine(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data"), file);

            try
            {
                // Write error information to a file
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("Date: " + date);
                    sw.WriteLine("File Name: " + codeFileName);
                    sw.WriteLine("Method: " + methodName);
                    sw.WriteLine("Error: " + exception.ToString());
                    sw.WriteLine("Additional Information: " + additionalInformation);
                    sw.WriteLine();
                }
            }
            catch (IOException ex)
            {
                // Write error information to a file
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("Date: " + date);
                    sw.WriteLine("File Name: LogFile.cs");
                    sw.WriteLine("Method: LogFile.WriteToFile");
                    sw.WriteLine("Error: " + ex.ToString());
                    sw.WriteLine();
                }
            }
            catch (Exception ex)
            {
                // Write error information to a file
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("Date: " + date);
                    sw.WriteLine("File Name: LogFile.cs");
                    sw.WriteLine("Method: LogFile.WriteToFile");
                    sw.WriteLine("Error: " + ex.ToString());
                    sw.WriteLine();
                }
            }
        }

        // Overloaded To contain a string exception instead of a system exception
        public static void WriteToFile(string codeFileName, string methodName, string exception, string additionalInformation, string file)
        {
            // Set the date and time of this event
            string date = DateTime.Now.ToString();

            // Store the log file in the app data folder
            string fileName = Path.Combine(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data"), file);

            try
            {
                // Write error information to a file
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("Date: " + date);
                    sw.WriteLine("File Name: " + codeFileName);
                    sw.WriteLine("Method: " + methodName);
                    sw.WriteLine("Error: " + exception.ToString());
                    sw.WriteLine("Additional Information: " + additionalInformation);
                    sw.WriteLine();
                }
            }
            catch (IOException ex)
            {
                // Write error information to a file
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("Date: " + date);
                    sw.WriteLine("File Name: LogFile.cs");
                    sw.WriteLine("Method: LogFile.WriteToFile");
                    sw.WriteLine("Error: " + ex.ToString());
                    sw.WriteLine();
                }
            }
            catch (Exception ex)
            {
                // Write error information to a file
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("Date: " + date);
                    sw.WriteLine("File Name: LogFile.cs");
                    sw.WriteLine("Method: LogFile.WriteToFile");
                    sw.WriteLine("Error: " + ex.ToString());
                    sw.WriteLine();
                }
            }
        }
    }
}
