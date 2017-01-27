using HPFS.HelperMethods;
using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS.Pages
{
    public partial class ProgramManager : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("ProgramManager", (int)UserActionEnum.Navigated);

            if (!Page.User.Identity.IsAuthenticated || Session["UserId"] == null || !Page.User.IsInRole("Administrator"))
            {
                Response.Redirect("/Main.aspx");
            }
        }

        protected void ddlSelectProgramTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Grab program id
            int id = 0;

            if (Int32.TryParse(ddlSelectProgramTab.SelectedValue, out id))
            {
                // Grab program data
                Program prog = db.Programs.Find(id);

                // Populate textboxes
                txtProgramCoordinator.Text = prog.ProgramCoordinator;
                txtProgramDescription.Text = prog.ProgramDescription;
                txtProgramEmail.Text = prog.ProgramEmail;
                txtProgramGoals.Text = prog.ProgramGoals;
                txtProgramLocation.Text = prog.ProgramLocation;
                txtProgramMap.Text = prog.ProgramMap;
                txtProgramPhone.Text = prog.ProgramPhone;
                txtProgramSchedule.Text = prog.ProgramSchedule.Replace("<br />", "\n");
            }
        }

        protected void btnSaveProgramData_Click(object sender, EventArgs e)
        {
            HelperMethods.ActivityTracker.Track("Program Information Updated", (int)UserActionEnum.Updated);

            lblProgramDataSuccess.Text = "";
            lblProgramDataError.Text = "";

            try
            {
                // Grab program data
                Program prog = db.Programs.Find(Convert.ToInt32(ddlSelectProgramTab.SelectedValue));

                // Update Data
                prog.ProgramCoordinator = txtProgramCoordinator.Text;
                prog.ProgramDescription = txtProgramDescription.Text;
                prog.ProgramEmail = txtProgramEmail.Text;
                prog.ProgramGoals = txtProgramGoals.Text;
                prog.ProgramLocation = txtProgramLocation.Text;
                prog.ProgramPhone = txtProgramPhone.Text;
                prog.ProgramSchedule = txtProgramSchedule.Text.Replace("\n", "<br />");
                prog.ProgramMap = (txtProgramMap.Text.StartsWith("h")) ? txtProgramMap.Text : txtProgramMap.Text.Split('"')[1];

                // Change the entry state
                db.Entry(prog).State = System.Data.Entity.EntityState.Modified;

                // Save to DB
                db.SaveChanges();

                // alert user of success
                lblProgramDataSuccess.Text = "Changes Saved.";

                // Create a notification for the database
                string[] role = { "Everyone" };
                NotificationCreator.CreateNotification(role, "Program Updated:", prog.ProgramName, DateTime.Now, "Info", null, null);
            }
            catch (DataException dx)
            {
                LogFile.WriteToFile("ProgramManager.aspx.cs", "SaveSlideShow", dx, "An Administrator tried to update Program Information.", "HPSErrorLog.txt");
                lblProgramDataError.Text = "Changes Failed to Save, try again.";
            }
            catch (Exception ex)
            {
                LogFile.WriteToFile("ProgramManager.aspx.cs", "SaveSlideShow", ex, "An Administrator tried to update Program Information.", "HPSErrorLog.txt");
                lblProgramDataError.Text = "Changes Failed to Save, try again.";
            }

        }

        protected void btnCancelProgramData_Click(object sender, EventArgs e)
        {
            // Reset all items
            txtProgramCoordinator.Text = "";
            txtProgramDescription.Text = "";
            txtProgramEmail.Text = "";
            txtProgramGoals.Text = "";
            txtProgramLocation.Text = "";
            txtProgramMap.Text = "";
            txtProgramPhone.Text = "";
            txtProgramSchedule.Text = "";
            ddlSelectProgramTab.SelectedIndex = 0;
            lblProgramDataSuccess.Text = "";
            lblProgramDataError.Text = "";
        }
    }
}