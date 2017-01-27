using HPFS.HelperMethods;
using HPFS.Models;
using System;

namespace HPFS
{
    public partial class AboutUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("About Us", (int)UserActionEnum.Navigated);
        }
    }
}