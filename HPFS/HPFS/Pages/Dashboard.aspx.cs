using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;
using HPFS.Models;

namespace HPFS
{
    public partial class Dashboard : System.Web.UI.Page
    {
        // if I declared it as static any changes to the first or last name 
        // wouldn't take effect until the user logged out and back in.
        public HPSDB db = new HPSDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("Dashboard", (int)UserActionEnum.Navigated);
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                if (!Page.User.IsInRole("Administrator"))
                {
                    pnlUserManager.Visible = false;
                    pnlSlideShowManager.Visible = false;
                    pnlFeedbackManager.Visible = false;
                    pnlActivityTracker.Visible = false;
                    pnlFitBitMonitor.Visible = false;
                    pnlProgramsManager.Visible = false;
                }

                // Grab current user Id
                string userId = Session["UserId"].ToString();

                
                // Query the db and grab the data using the Id
                HPSUser user = db.HPSUsers.Where(u => u.UserId == userId).SingleOrDefault();

                // Display first name in the dashboard h1 tag
                welcome.InnerText = "Welcome " + user.FirstName + " " + user.LastName + "!";
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }
    }
}