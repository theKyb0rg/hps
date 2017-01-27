using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS
{
    public partial class FitBitManagerAuthentication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                if (Session["FitbitAuthToken"] == null || Session["FitbitAuthTokenSecret"] == null || Session["FitbitUserId"] == null)
                {
                    FitBit.FitBit.Authorize();
                }
                else
                {
                    Response.Redirect("/Pages/FitBitManager.aspx");
                }
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }
    }
}