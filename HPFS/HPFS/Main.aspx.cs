using HPFS.HelperMethods;
using HPFS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("Home", (int)UserActionEnum.Navigated);

            if (Session["login"] != null)
            {
                // Open the login modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "loginModal", "$('#mdlLogin').modal('show');", true);
                Session["login"] = null;
            }
        }
    }
}