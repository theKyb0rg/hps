using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.Models;
using HPFS.HelperMethods;

namespace HPFS.Pages
{
    public partial class ActivityTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Activity Tracker", (int)UserActionEnum.Navigated);
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null && User.IsInRole("Administrator"))
            {
                if (!IsPostBack)
                {
                    // Declare list of checkbox ids
                    List<int> activitys = new List<int>();

                    // CHeck which ones are checked and add to list
                    if (chkActivityClickedHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.Clicked);
                    }
                    if (chkActivityCreatedHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.Created);
                    }
                    if (chkActivityDeletedHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.Deleted);
                    }
                    if (chkActivityDownloadedHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.Downloaded);
                    }
                    if (chkActivityLoggedInHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.LoggedIn);
                    }
                    if (chkActivityNavigatedHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.Navigated);
                    }
                    if (chkActivitySearchedHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.Searched);
                    }
                    if (chkActivityUpdatedHidden.Checked)
                    {
                        activitys.Add((int)UserActionEnum.Updated);
                    }

                    // Build dem tables
                    TableBuilder.BuildUserTrackingTable(tblActivityData, activitys);

                    // Reinitialized bootstrap sortable table
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "resortTable", "$.bootstrapSortable(true)", true);
                }
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }

        protected void btnFilterActivity_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Searched for Activity", (int)UserActionEnum.Searched);
            // Declare list of checkbox ids
            List<int> activitys = new List<int>();

            // CHeck which ones are checked and add to list
            if (chkActivityClickedHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.Clicked);
            }
            if (chkActivityCreatedHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.Created);
            }
            if (chkActivityDeletedHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.Deleted);
            }
            if (chkActivityDownloadedHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.Downloaded);
            }
            if (chkActivityLoggedInHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.LoggedIn);
            }
            if (chkActivityNavigatedHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.Navigated);
            }
            if (chkActivitySearchedHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.Searched);
            }
            if (chkActivityUpdatedHidden.Checked)
            {
                activitys.Add((int)UserActionEnum.Updated);
            }

            // Build dem tables
            TableBuilder.BuildUserTrackingTable(tblActivityData, activitys);

            // Reinitialized bootstrap sortable table
            ScriptManager.RegisterStartupScript(this, this.GetType(), "resortTable", "$.bootstrapSortable(true)", true);

        }
    }
}