using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;

namespace HPFS.Pages
{
    public partial class NotificationLog : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated || Session["UserId"] == null || !Page.User.IsInRole("Administrator"))
            {
                Response.Redirect("/Main.aspx");
            }
            else
            {
                TableBuilder.BuildNotificationLogTable(tblNotificationLog);

                // Reinitialized bootstrap sortable table
                ScriptManager.RegisterStartupScript(this, this.GetType(), "resortTable", "$.bootstrapSortable(true)", true);
            }
        }

        protected void btnNotificationClear_Click(object sender, EventArgs e)
        {
            ddlPriority.SelectedIndex = 0;
            txtUsername.Text = "";
            txtNotificationStartDate.Text = "";
            txtNotificationEndDate.Text = "";
            ddlIsRead.SelectedIndex = 0;
            lblNotificationErrors.Text = "";
        }

        protected void btnNotificationSearch_Click(object sender, EventArgs e)
        {
            string[] filters = {
                                ddlPriority.SelectedValue,
                                txtUsername.Text,
                                txtNotificationStartDate.Text,
                                txtNotificationEndDate.Text,
                                ddlIsRead.SelectedValue };

            TableBuilder.BuildFilteredNotificationLogTable(filters, tblNotificationLog);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "resortTable", "$.bootstrapSortable(true)", true);
        }
    }
}