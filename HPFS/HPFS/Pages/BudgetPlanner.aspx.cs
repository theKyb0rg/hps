using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;
using HPFS.Models;

namespace HPFS.Pages
{
    public partial class BudgetPlanner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated || Session["UserId"] == null)
            {
                Response.Redirect("/Main.aspx");
            }

            HelperMethods.ActivityTracker.Track("Budget Planner", (int)UserActionEnum.Navigated);
        }
    }
}