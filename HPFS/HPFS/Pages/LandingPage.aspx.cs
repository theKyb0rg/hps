using HPFS.HelperMethods;
using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS.Pages
{
    public partial class LandingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Landing Page", (int)UserActionEnum.Navigated);

            if (Request.Cookies["LandingPage"]!= null)
            {
                Response.Redirect("/Main.aspx");
            }
            else
            {
                HttpCookie LandingPage = new HttpCookie("LandingPage");
                LandingPage.Expires = DateTime.Now.AddMonths(6);
                LandingPage.Value = "True";
                Response.Cookies.Add(LandingPage);
            }
        }

        protected void signIn_Click(object sender, EventArgs e)
        {
            Session["login"] = "True";
            Response.Redirect("/Main.aspx");
        }
    }
}