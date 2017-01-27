using System;
using System.Collections.Generic;
using System.Web.UI;
using HPFS.HelperMethods;
using HPFS.Models;

namespace HPFS.Pages
{
    public partial class FeedbackManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Feedback Manager", (int)UserActionEnum.Navigated);

            // This will need to be added to all restricted pages, or else people can just physically go to each page.
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null && Page.User.IsInRole("Administrator"))
            {
                if (!IsPostBack)
                {
                    GenerateTable();
                }
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }

        protected void btnFbSearch_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Searched for Feedback", (int)UserActionEnum.Searched);

            GenerateTable();
        }

        protected void btnFbClear_Click(object sender, EventArgs e)
        {
            txtFbStartDate.Text = "";
            txtFbEndDate.Text = "";
            ddlFbSiteArea.SelectedIndex = 0;
            ddlAccRating.SelectedIndex = 0;
            ddlAppRating.SelectedIndex = 0;
            ddlNavRating.SelectedIndex = 0;
            ddlRecRating.SelectedIndex = 0;
            ddlFbAvgStart.SelectedIndex = 0;
            ddlFbAvgEnd.SelectedIndex = 0;

            GenerateTable();
        }

        protected void GenerateTable()
        {
            // List of strings that will be passed to table function
            List<string> fbData = new List<string>();

            // Add items to list
            fbData.Add(txtFbStartDate.Text);
            fbData.Add(txtFbEndDate.Text);
            fbData.Add(ddlFbSiteArea.SelectedValue);
            fbData.Add(ddlAccRating.SelectedValue);
            fbData.Add(ddlAppRating.SelectedValue);
            fbData.Add(ddlNavRating.SelectedValue);
            fbData.Add(ddlRecRating.SelectedValue);
            fbData.Add(ddlFbAvgStart.SelectedValue);
            fbData.Add(ddlFbAvgEnd.SelectedValue);

            // Build table
            TableBuilder.BuildFeedbackTable(fbData, tblFeedback);

            // Reinitialized bootstrap sortable table
            ScriptManager.RegisterStartupScript(this, this.GetType(), "resortTable", "$.bootstrapSortable(true)", true);
        }
    }
}