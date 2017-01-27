using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;

namespace HPFS
{
    public partial class ContactUs : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("Contact Us", (int)UserActionEnum.Navigated);

            // Dynamically change contact and hours fields
            Contact data = db.Contacts.Where(u => u.Id == 1).SingleOrDefault();

            // Mobile Hours
            hours3.InnerHtml = "<p><b>Monday: </b>" + data.Monday + "</p>"
                             + "\n<p><b>Tuesday: </b>" + data.Tuesday + "</p>\n<p><b>Wednesday: </b>" + data.Wednesday + "</p>"
                             + "\n<p><b>Thursday: </b>" + data.Thursday + "</p>\n<p><b>Friday: </b>" + data.Friday + "</p>"
                             + "\n<p><b>Saturday: </b>" + data.Saturday + "</p>\n<p><b>Sunday: </b>" + data.Sunday + "</p>";

            // Set tooltips
            tt1.Title = data.Sunday;
            tt2.Title = data.Monday;
            tt3.Title = data.Tuesday;
            tt4.Title = data.Wednesday;
            tt5.Title = data.Thursday;
            tt6.Title = data.Friday;
            tt7.Title = data.Saturday;

            // Contact info
            contact3.InnerHtml = "<h3 style='font-weight: 400;'>" + data.Company + "</h3>\n"
                               + "<h5>" + data.Address + "</h5>\n<h5 class='call'><b>Telephone: </b>"
                               + "<a href='tel:" + data.Telephone + "'>" + data.Telephone + "</a></h5>\n"
                               + "<h5 class='callOff'><b>Telephone: </b>" + data.Telephone + "</h5>\n"
                               + "<h5><b>Fax: </b>" + data.Fax + "</h5>\n"
                               + "<h5><b>Email: </b><a href='mailto:" + data.Email + "' class='mail'>" + data.Email + "</a></h5>";
        }
    }
}