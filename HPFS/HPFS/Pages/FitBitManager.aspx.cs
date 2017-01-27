using Fitbit.Api;
using Fitbit.Models;
using HPFS.HelperMethods;
using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HPFS
{
    public partial class FitBitManager : System.Web.UI.Page
    {
        // Declare instance of the db
        public static HPSDB db = new HPSDB();
        public static bool notification = false;
        public static string notificationMessage = "";
        public static string notificationStyle = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("FitBit Manager", (int)UserActionEnum.Navigated);
            if (Page.User.Identity.IsAuthenticated && Session["UserId"] != null)
            {
                // Find the HPSUser that is currently logged in
                string userId = Session["UserId"].ToString();

                // There are no session cookies for this user, callback and repopulate the session
                if (Session["FitbitAuthToken"] == null || Session["FitbitAuthTokenSecret"] == null || Session["FitbitUserId"] == null)
                {
                    FitBit.FitBit.Callback();
                }

                if (!IsPostBack)
                {
                    try
                    {
                        HPSUser user = db.HPSUsers.Where(u => u.AspNetUser.Id == userId).SingleOrDefault();

                        // Check if the fitbituserId for this user has been set, if not set it
                        if (user.FitBitUserId == null)
                        {
                            string fitBitUserId = Session["FitbitUserId"].ToString();
                            user.FitBitUserId = fitBitUserId;

                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                    catch (DataException dx)
                    {
                        LogFile.WriteToFile("FitBitManager.aspx.cs", "Page_Load", dx, "The system failed when trying to automatically set the current user's FitBitId.", "HPSErrorLog.txt");
                    }
                    catch (Exception ex)
                    {
                        LogFile.WriteToFile("FitBitManager.aspx.cs", "Page_Load", ex, "The system failed when trying to automatically set the current user's FitBitId.", "HPSErrorLog.txt");
                    }

                    // Automatically Synchronize fitbit data and load the rest of the data
                    UploadFitBitData();

                    // Draw initial chart
                    DrawChart(this.Page, 7, "Steps");
                }

                // Load step, minutes, and distance data
                GetStepGoals();
                GetDistanceGoals();
                GetMinuteGoals();

                // Build tables for viewing all goals
                TableBuilder.BuildStepGoalsTable(tblStepGoals, userId);
                TableBuilder.BuildDistanceGoalsTable(tblDistanceGoals, userId);
                TableBuilder.BuildMinuteGoalsTable(tblMinuteGoals, userId);

                // Check if theres a notification
                if (notification)
                {
                    lblCRUDMessage.Text = notificationMessage;
                    lblCRUDMessage.CssClass = notificationStyle;
                    notification = false;

                    // Draw initial chart
                    DrawChart(this.Page, 7, "Steps");
                }
            }
            else
            {
                Response.Redirect("/Main.aspx");
            }
        }

        public void DrawChart(Page page, int dayCount, string chartType)
        {
            // Initialize a list of list of strings
            List<List<string>> data = GatherFitBitChartData(dayCount, chartType);

            // Pick Get Lists for dates and steps           
            List<string> values = data[0];
            List<string> dates = data[1];

            // Start chart
            string script = "<script type='text/javascript'>" +
                                "$(document).ready(function () {" +
                                    "var chart = c3.generate({" +
                                        "bindto: '#chart'," +
                                        "data: {" +
                                            "x: 'x'," +
                                            "xFormat: '%m/%d/%Y'," +
                                            "columns: [" +
                                                "['x'";
            // Add dates to x axis
            foreach (var d in dates)
            {
                string parseDate = Convert.ToDateTime(d).ToShortDateString();
                script += ", '" + parseDate + "'";
            }

            // Start step data array
            script += "]," + "['" + chartType + "'";

            // Initialize value counter
            decimal valueCount = 0;

            // Add in step data array
            foreach (var s in values)
            {
                valueCount += Convert.ToDecimal(s);
                script += ", " + s;
            }

            // Parse the values to the appropriate decimal places based on the type of chart we are generating
            decimal avgValue = (chartType == "Steps") ? Math.Round((valueCount / dayCount)) : Math.Round((valueCount / dayCount), 2);
            valueCount = (chartType == "Steps") ? Math.Round(valueCount) : Math.Round(valueCount, 2);

            // Finish Building chart
            script += "]" +
                    "]," +
                    "type: 'bar'" +
                "}," +
                "legend: {" +
                    "show: false" +
                "}," +
                "grid: {" +
                    "x: {" +
                        "show: true" +
                    "}," +
                    "y: {" +
                        "show: true" +
                    "}" +
                "}," +
                "axis: {" +
                    "x: {" +
                        "type: 'timeseries'," +
                        "tick: {" +
                            "format: '%m/%d/%Y'" +
                        "}" +
                    "}," +
                    "y: {" +
                        "label: {" +
                            "text: '" + chartType + " from Last " + dayCount.ToString() + " Days'," +
                            "position: 'outer-middle'" +
                        "}" +
                    "}" +
                "}" +
            "});" +
        "});" +
    "</script>";

            // Initialize the script on the page
            page.ClientScript.RegisterStartupScript(page.GetType(), chartType + "Chart", script);

            // Adjust title
            plTitle.Controls.Add(new LiteralControl("<h1 class='text-center'>" + chartType + " - " + dayCount + " Days</h1>"));

            // Adjust headings
            plAvgHeading.Controls.Add(new LiteralControl("<h3>Avg. " + chartType + "</h3>"));
            plTotalsHeading.Controls.Add(new LiteralControl("<h3>Total " + chartType + "</h3>"));

            // Parse the output depending on charttype
            if (chartType == "Steps")
            {
                plAvgValue.Controls.Add(new LiteralControl("<h4>" + avgValue + "</h4>"));
                plTotalsValue.Controls.Add(new LiteralControl("<h4>" + valueCount + "</h4>"));
            }
            else if (chartType == "Distance")
            {
                plAvgValue.Controls.Add(new LiteralControl("<h4>" + avgValue + " km</h4>"));
                plTotalsValue.Controls.Add(new LiteralControl("<h4>" + valueCount + " km</h4>"));
            }
            else if (chartType == "Minutes")
            {
                plAvgValue.Controls.Add(new LiteralControl("<h4>" + avgValue + " min</h4>"));
                plTotalsValue.Controls.Add(new LiteralControl("<h4>" + valueCount + " min</h4>"));
            }
        }

        protected void btnDistances_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Viewed the Distances Fitbit Data", (int)UserActionEnum.Clicked);
            // Get button that was clicked
            HtmlButton edit = (HtmlButton)sender;

            // Get id from button
            int dayCount = Convert.ToInt32(edit.Attributes["data-id"]);

            // Morph graph to appropriate day count
            DrawChart(this.Page, dayCount, "Distance");
        }

        protected void btnSteps_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Viewed the Steps Fitbit Data", (int)UserActionEnum.Clicked);
            // Get button that was clicked
            HtmlButton edit = (HtmlButton)sender;

            // Get id from button
            int dayCount = Convert.ToInt32(edit.Attributes["data-id"]);

            // Morph graph to appropriate day count
            DrawChart(this.Page, dayCount, "Steps");
        }

        protected void btnMinutes_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Viewed the Minutes Fitbit Data", (int)UserActionEnum.Clicked);
            // Get button that was clicked
            HtmlButton edit = (HtmlButton)sender;

            // Get id from button
            int dayCount = Convert.ToInt32(edit.Attributes["data-id"]);

            // Morph graph to appropriate day count
            DrawChart(this.Page, dayCount, "Minutes");
        }

        public List<List<string>> GatherFitBitChartData(int dayCount, string type)
        {
            // Get the fitbit client data
            FitbitClient client = GetFitbitClient();

            // Store data into a dual list of strings
            List<List<string>> data = new List<List<string>>();
            List<string> values = new List<string>();
            List<string> dates = new List<string>();

            // Check which type of data to get
            if (type == "Steps")
            {
                var steps = client.GetTimeSeries(TimeSeriesResourceType.Steps, DateTime.Now.AddDays(-dayCount), DateTime.Now).DataList.ToList();

                // Loop through data and assign data to its own seperate list
                foreach (var result in steps)
                {
                    values.Add(result.Value.ToString());
                    dates.Add(result.DateTime.ToString());
                }

                // Add lists together and return
                data.Add(values);
                data.Add(dates);
                return data;
            }
            else if (type == "Distance")
            {
                var distances = client.GetTimeSeries(TimeSeriesResourceType.Distance, DateTime.Now.AddDays(-dayCount), DateTime.Now).DataList.ToList();

                // Loop through data and assign data to its own seperate list
                foreach (var result in distances)
                {
                    values.Add(result.Value.ToString());
                    dates.Add(result.DateTime.ToString());
                }

                // Add lists together and return
                data.Add(values);
                data.Add(dates);
                return data;
            }
            else
            {
                TimeSeriesDataList lightlyActive = client.GetTimeSeries(TimeSeriesResourceType.MinutesLightlyActive, DateTime.Now.AddDays(-dayCount), DateTime.Now);
                TimeSeriesDataList fairlyActive = client.GetTimeSeries(TimeSeriesResourceType.MinutesFairlyActive, DateTime.Now.AddDays(-dayCount), DateTime.Now);
                var minutes = lightlyActive.DataList.Zip(fairlyActive.DataList, (lightly, fairly) => new { LightlyActive = lightly, FairlyActive = fairly }).ToList();

                // Loop through data and assign data to its own seperate list
                foreach (var result in minutes)
                {
                    values.Add((Convert.ToDecimal(result.FairlyActive.Value) + Convert.ToDecimal(result.LightlyActive.Value)).ToString());
                    dates.Add(result.FairlyActive.DateTime.ToString());
                }

                // Add lists together and return
                data.Add(values);
                data.Add(dates);
                return data;
            }
        }

        private FitbitClient GetFitbitClient()
        {
            FitbitClient client = new FitbitClient(ConfigurationManager.AppSettings["FitbitConsumerKey"],
                ConfigurationManager.AppSettings["FitbitConsumerSecret"],
                 HttpContext.Current.Session["FitbitAuthToken"].ToString(),
                 HttpContext.Current.Session["FitbitAuthTokenSecret"].ToString());

            return client;
        }

        protected void btnUploadData_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Added Their Fitbit Data to the DB", (int)UserActionEnum.Created);
            // Upload fitbit data
            UploadFitBitData();

            // Repopulate goals
            GetStepGoals();
            GetDistanceGoals();
            GetMinuteGoals();

            // Redraw the initial chart
            DrawChart(this.Page, 7, "Steps");
        }

        protected void UploadFitBitData()
        {
            try
            {
                // Get the fitbit client data
                FitbitClient client = GetFitbitClient();

                // Get the most recent upload date for steps
                string userId = Session["UserId"].ToString();

                var latestSteps = db.Steps.Where(m => m.AspNetUser.Id == userId)
                    .OrderByDescending(m => m.StepDate)
                    .FirstOrDefault();

                // Convert lastUpload date to DateTime for comparison
                DateTime lastUploadDateSteps = (DateTime)latestSteps.StepDate;

                // Get total days between now and last upload
                int daysSteps = Convert.ToInt32((DateTime.Now - lastUploadDateSteps).TotalDays);
                TimeSeriesDataList stepsData = client.GetTimeSeries(TimeSeriesResourceType.Steps, DateTime.Now.AddDays(-daysSteps), DateTime.Now);

                // Loop through results to total steps since last upload
                int dayCountSteps = stepsData.DataList.Count - 1;
                foreach (var d in stepsData.DataList)
                {
                    int steps = Convert.ToInt32(d.Value);
                    if (Convert.ToDateTime(latestSteps.StepDate).AddDays(-(dayCountSteps--)) == d.DateTime)
                    {
                        Step lastSteps = db.Steps.Where(m => m.AspNetUser.Id == userId && m.StepDate == d.DateTime)
                            .FirstOrDefault();
                        lastSteps.StepCount = steps;

                        // Change state to modified
                        db.Entry(lastSteps).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        // Add the record to the database
                        Step step = new Step();
                        step.StepCount = steps;
                        step.StepDate = d.DateTime;
                        step.UserId = Session["UserId"].ToString();
                        db.Steps.Add(step);
                        db.SaveChanges();
                    }
                }

                // Get the most recent upload date for distances
                var latestDistance = db.Distances.Where(m => m.AspNetUser.Id == userId)
                    .OrderByDescending(m => m.DistanceDate)
                    .FirstOrDefault();

                // Convert lastUpload date to DateTime for comparison
                DateTime lastUploadDateDistances = (DateTime)latestDistance.DistanceDate;

                // Get total days between now and last upload
                int daysDistances = Convert.ToInt32((DateTime.Now - lastUploadDateDistances).TotalDays);
                TimeSeriesDataList distanceData = client.GetTimeSeries(TimeSeriesResourceType.Distance, DateTime.Now.AddDays(-daysDistances), DateTime.Now);

                // Loop through results to total steps since last upload
                int dayCountDistances = distanceData.DataList.Count - 1;
                foreach (var d in distanceData.DataList)
                {
                    decimal distances = Convert.ToDecimal(d.Value);
                    if (Convert.ToDateTime(latestDistance.DistanceDate).AddDays(-(dayCountDistances--)) == d.DateTime)
                    {
                        Distance lastDistances = db.Distances.Where(m => m.AspNetUser.Id == userId && m.DistanceDate == d.DateTime)
                            .FirstOrDefault();
                        lastDistances.DistanceCount = distances;

                        // Change state to modified
                        db.Entry(lastDistances).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        // Add the record to the database
                        Distance distance = new Distance();
                        distance.DistanceCount = distances;
                        distance.DistanceDate = d.DateTime;
                        distance.UserId = Session["UserId"].ToString();
                        db.Distances.Add(distance);
                        db.SaveChanges();
                    }
                }

                // Get the most recent upload date for minutes
                var latestMinutes = db.Minutes.Where(m => m.AspNetUser.Id == userId)
                    .OrderByDescending(m => m.MinuteDate)
                    .FirstOrDefault();

                // Convert lastUpload date to DateTime for comparison
                DateTime lastUploadDateMinutes = (DateTime)latestMinutes.MinuteDate;

                // Get total days between now and last upload
                int daysMinutes = Convert.ToInt32((DateTime.Now - lastUploadDateMinutes).TotalDays);
                TimeSeriesDataList lightlyActive = client.GetTimeSeries(TimeSeriesResourceType.MinutesLightlyActive, DateTime.Now.AddDays(-daysMinutes), DateTime.Now);
                TimeSeriesDataList fairlyActive = client.GetTimeSeries(TimeSeriesResourceType.MinutesFairlyActive, DateTime.Now.AddDays(-daysMinutes), DateTime.Now);

                // Join the results of both minute datas together so we only have to use one loop to get total minutes
                var minutesData = lightlyActive.DataList.Zip(fairlyActive.DataList, (lightly, fairly) => new { LightlyActive = lightly, FairlyActive = fairly }).ToList();

                // Loop through results to total steps since last upload
                int dayCountMinutes = minutesData.Count - 1;
                foreach (var d in minutesData)
                {
                    decimal minutes = Convert.ToDecimal(d.FairlyActive.Value) + Convert.ToDecimal(d.LightlyActive.Value);
                    if (Convert.ToDateTime(latestMinutes.MinuteDate).AddDays(-(dayCountMinutes--)) == d.FairlyActive.DateTime)
                    {
                        Minute lastMinutes = db.Minutes.Where(m => m.AspNetUser.Id == userId && m.MinuteDate == d.FairlyActive.DateTime)
                            .FirstOrDefault();
                        lastMinutes.MinuteCount = minutes;

                        // Change state to modified
                        db.Entry(lastMinutes).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        // Add the record to the database
                        Minute minute = new Minute();
                        minute.MinuteCount = minutes;
                        minute.MinuteDate = d.FairlyActive.DateTime;
                        minute.UserId = Session["UserId"].ToString();
                        db.Minutes.Add(minute);
                        db.SaveChanges();
                    }
                }

                lblCRUDMessage.Text = "FitBit Data successfully synchronized.";
                lblCRUDMessage.CssClass = "text-success";

            }
            catch (DataException dx)
            {
                lblCRUDMessage.Text = "Unable to synchronize FitBit Data. To try again, click the 'Synchronize FitBit Data' button. If the error persists, please inform an Administrator.";
                lblCRUDMessage.CssClass = "text-danger";
                LogFile.WriteToFile("FitBitManager.aspx.cs", "UploadFitBitData", dx, User.Identity.Name + " tried to synchronize FitBit Data.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblCRUDMessage.Text = "Unable to synchronize FitBit Data. To try again, click the 'Synchronize FitBit Data' button. If the error persists, please inform an Administrator.";
                lblCRUDMessage.CssClass = "text-danger";
                LogFile.WriteToFile("FitBitManager.aspx.cs", "UploadFitBitData", ex, User.Identity.Name + " tried to synchronize FitBit Data.", "HPSErrorLog.txt");
            }
        }

        protected void btnSetNewGoal_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Created a New Goal", (int)UserActionEnum.Created);
            try
            {
                if (ddlGoalType.SelectedValue == "Steps")
                {
                    StepGoal sg = new StepGoal();
                    sg.GoalSteps = Convert.ToInt32(txtGoal.Text);
                    sg.GoalStartDate = Convert.ToDateTime(txtGoalStartDate.Text) + new TimeSpan(0, 0, 0);
                    sg.GoalEndDate = Convert.ToDateTime(txtGoalEndDate.Text) + new TimeSpan(0, 0, 0);
                    sg.UserId = Session["UserId"].ToString();
                    db.StepGoals.Add(sg);
                    db.SaveChanges();

                    // Display message
                    lblCRUDMessage.Text = "New Steps Goal saved.";
                    lblCRUDMessage.CssClass = "text-success";
                }
                else if (ddlGoalType.SelectedValue == "Distance")
                {
                    DistanceGoal dg = new DistanceGoal();
                    dg.GoalDistance = Convert.ToDecimal(txtGoal.Text);
                    dg.GoalStartDate = Convert.ToDateTime(txtGoalStartDate.Text) + new TimeSpan(0, 0, 0);
                    dg.GoalEndDate = Convert.ToDateTime(txtGoalEndDate.Text) + new TimeSpan(0, 0, 0);
                    dg.UserId = Session["UserId"].ToString();
                    db.DistanceGoals.Add(dg);
                    db.SaveChanges();

                    // Display message
                    lblCRUDMessage.Text = "New Distance Goal saved.";
                    lblCRUDMessage.CssClass = "text-success";
                }
                else if (ddlGoalType.SelectedValue == "Minutes")
                {
                    MinuteGoal mg = new MinuteGoal();
                    mg.GoalMinute = Convert.ToDecimal(txtGoal.Text);
                    mg.GoalStartDate = Convert.ToDateTime(txtGoalStartDate.Text) + new TimeSpan(0, 0, 0);
                    mg.GoalEndDate = Convert.ToDateTime(txtGoalEndDate.Text) + new TimeSpan(0, 0, 0);
                    mg.UserId = Session["UserId"].ToString();
                    db.MinuteGoals.Add(mg);
                    db.SaveChanges();

                    // Display message
                    lblCRUDMessage.Text = "New Minutes Goal saved.";
                    lblCRUDMessage.CssClass = "text-success";
                }

                // Reset copntrols
                txtGoal.Text = "";
                txtGoalEndDate.Text = "";
                txtGoalStartDate.Text = "";
                ddlGoalType.SelectedValue = "-1";
            }
            catch (DataException dx)
            {
                lblCRUDMessage.Text = "Goal could not be saved at this time. Please try again later or inform an Administrator.";
                lblCRUDMessage.CssClass = "text-danger";
                LogFile.WriteToFile("FitBitManager.aspx.cs", "btnSetNewGoal_Click", dx, User.Identity.Name + " tried to set a new goal.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblCRUDMessage.Text = "Goal could not be saved at this time. Please try again later or inform an Administrator.";
                lblCRUDMessage.CssClass = "text-danger";
                LogFile.WriteToFile("FitBitManager.aspx.cs", "btnSetNewGoal_Click", ex, User.Identity.Name + " tried to set a new goal.", "HPSErrorLog.txt");
            }

            // Rebuild the chart and step goals
            DrawChart(this.Page, 7, "Steps");
            GetStepGoals();
            GetDistanceGoals();
            GetMinuteGoals();

            // Build tables for viewing all goals
            string userId = Session["UserId"].ToString();
            TableBuilder.BuildStepGoalsTable(tblStepGoals, userId);
            TableBuilder.BuildDistanceGoalsTable(tblDistanceGoals, userId);
            TableBuilder.BuildMinuteGoalsTable(tblMinuteGoals, userId);
        }

        protected void GetStepGoals()
        {
            // Find specific goal for certain user
            string userId = Session["UserId"].ToString();
            var latest = db.StepGoals.Where(m => m.AspNetUser.Id == userId)
                .OrderByDescending(m => m.GoalStartDate)
                .FirstOrDefault();

            // Clear controls
            plDateRangeSteps.Controls.Clear();
            plStepGoal.Controls.Clear();
            plCurrentStepGoal.Controls.Clear();

            if (latest != null)
            {
                // Get current steps based on most recent goal
                int stepTotal = db.Steps.Where(m => m.StepDate >= latest.GoalStartDate && m.StepDate <= latest.GoalEndDate)
                    .Where(u => u.AspNetUser.Id == userId)
                    .Sum(s => s.StepCount);

                // Get step goal
                int goal = latest.GoalSteps;

                // Add data to controls
                plDateRangeSteps.Controls.Add(new LiteralControl("<h5>" + Convert.ToDateTime(latest.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(latest.GoalEndDate).ToShortDateString() + "</h5>"));
                plStepGoal.Controls.Add(new LiteralControl("<h5>" + goal + "</h5>"));
                plCurrentStepGoal.Controls.Add(new LiteralControl("<h5>" + stepTotal + " / " + latest.GoalSteps + "</h5>"));

                // Change status message to see if goal has been met or not
                if (stepTotal >= goal)
                {
                    stepGoalStatus.Attributes["class"] = "text-center text-success";
                    stepGoalStatus.InnerText = "GOAL ACHIEVED";
                }
                else
                {
                    stepGoalStatus.Attributes["class"] = "text-center text-primary";
                    stepGoalStatus.InnerText = "IN PROGRESS";
                }
            }
            else
            {
                // Display not set
                plDateRangeSteps.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));
                plStepGoal.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));
                plCurrentStepGoal.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));

                // Prompt user to set a goal
                stepGoalStatus.Attributes["class"] = "text-center text-danger";
                stepGoalStatus.InnerText = "SET A GOAL";
            }
        }

        protected void GetDistanceGoals()
        {
            // Find specific goal for certain user
            string userId = Session["UserId"].ToString();
            var latest = db.DistanceGoals.Where(m => m.AspNetUser.Id == userId)
                .OrderByDescending(m => m.GoalStartDate)
                .FirstOrDefault();

            // Clear controls
            plDateRangeDistance.Controls.Clear();
            plDistanceGoal.Controls.Clear();
            plCurrentDistance.Controls.Clear();

            if (latest != null)
            {
                // Get total distance based on most recent goal
                decimal distanceTotal = db.Distances
                    .Where(u => u.AspNetUser.Id == userId)
                    .Where(m => m.DistanceDate >= latest.GoalStartDate && m.DistanceDate <= latest.GoalEndDate)
                    .Sum(d => d.DistanceCount);

                // Determine values based on goal
                decimal goal = latest.GoalDistance;

                // Clear controls
                plDateRangeDistance.Controls.Clear();
                plDistanceGoal.Controls.Clear();
                plCurrentDistance.Controls.Clear();

                // Add data to controls
                plDateRangeDistance.Controls.Add(new LiteralControl("<h5>" + Convert.ToDateTime(latest.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(latest.GoalEndDate).ToShortDateString() + "</h5>"));
                plDistanceGoal.Controls.Add(new LiteralControl("<h5>" + Math.Round(goal, 2) + " km</h5>"));
                plCurrentDistance.Controls.Add(new LiteralControl("<h5>" + Math.Round(distanceTotal, 2) + " km / " + Math.Round(latest.GoalDistance, 2) + " km</h5>"));

                // Change status message to see if goal has been met or not
                if (distanceTotal >= goal)
                {
                    distanceGoalStatus.Attributes["class"] = "text-center text-success";
                    distanceGoalStatus.InnerText = "GOAL ACHIEVED";
                }
                else
                {
                    distanceGoalStatus.Attributes["class"] = "text-center text-primary";
                    distanceGoalStatus.InnerText = "IN PROGRESS";
                }
            }

            else
            {
                // Add data to controls
                plDateRangeDistance.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));
                plDistanceGoal.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));
                plCurrentDistance.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));

                // Prompt user to set a goal
                distanceGoalStatus.Attributes["class"] = "text-center text-danger";
                distanceGoalStatus.InnerText = "SET A GOAL";
            }
        }
        protected void GetMinuteGoals()
        {
            // Find specific goal for certain user
            string userId = Session["UserId"].ToString();
            var latest = db.MinuteGoals.Where(m => m.AspNetUser.Id == userId)
                .OrderByDescending(m => m.GoalStartDate)
                .FirstOrDefault();

            // Clear controls
            plDateRangeMinutes.Controls.Clear();
            plMinutesGoal.Controls.Clear();
            plCurrentMinutesGoal.Controls.Clear();

            if (latest != null)
            {
                // Get current steps based on most recent goal
                decimal minuteTotal = db.Minutes
                    .Where(u => u.AspNetUser.Id == userId)
                    .Where(m => m.MinuteDate >= latest.GoalStartDate && m.MinuteDate <= latest.GoalEndDate)
                    .Sum(m => m.MinuteCount);

                // Determine values based on goal
                decimal goal = latest.GoalMinute;

                // Add data to controls
                plDateRangeMinutes.Controls.Add(new LiteralControl("<h5>" + Convert.ToDateTime(latest.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(latest.GoalEndDate).ToShortDateString() + "</h5>"));
                plMinutesGoal.Controls.Add(new LiteralControl("<h5>" + Math.Round(goal) + " min</h5>"));
                plCurrentMinutesGoal.Controls.Add(new LiteralControl("<h5>" + Math.Round(minuteTotal) + " min / " + Math.Round(latest.GoalMinute) + " min</h5>"));

                // Change status message to see if goal has been met or not
                if (minuteTotal >= goal)
                {
                    minuteGoalStatus.Attributes["class"] = "text-center text-success";
                    minuteGoalStatus.InnerText = "GOAL ACHIEVED";
                }
                else
                {
                    minuteGoalStatus.Attributes["class"] = "text-center text-primary";
                    minuteGoalStatus.InnerText = "IN PROGRESS";
                }
            }
            else
            {
                // Add data to controls
                plDateRangeMinutes.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));
                plMinutesGoal.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));
                plCurrentMinutesGoal.Controls.Add(new LiteralControl("<h5>Not Set</h5>"));

                // Prompt user to set a goal
                minuteGoalStatus.Attributes["class"] = "text-center text-danger";
                minuteGoalStatus.InnerText = "SET A GOAL";
            }
        }

        protected void btnRemoveAllGoals_Click(object sender, EventArgs e)
        {
            ActivityTracker.Track("Removed ALL Their Fitbit Goals", (int)UserActionEnum.Deleted);
            // Get userId from session
            string userId = Session["UserId"].ToString();
            try
            {
                // Remove all related goal records
                db.MinuteGoals.RemoveRange(db.MinuteGoals.Where(c => c.UserId == userId));
                db.DistanceGoals.RemoveRange(db.DistanceGoals.Where(c => c.UserId == userId));
                db.StepGoals.RemoveRange(db.StepGoals.Where(c => c.UserId == userId));
                db.SaveChanges();

                // Show message to user
                lblCRUDMessage.Text = "All goals have been removed.";
                lblCRUDMessage.CssClass = "text-success";
            }
            catch (DataException dx)
            {
                lblCRUDMessage.Text = "Unable to remove Goals at this time. Please try again later or inform an Administrator.";
                lblCRUDMessage.CssClass = "text-danger";
                LogFile.WriteToFile("FitBitManager.aspx.cs", "btnRemoveAllGoals_Click", dx, User.Identity.Name + " tried to remove all of their Goals.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                lblCRUDMessage.Text = "Unable to remove Goals at this time. Please try again later or inform an Administrator.";
                lblCRUDMessage.CssClass = "text-danger";
                LogFile.WriteToFile("FitBitManager.aspx.cs", "btnRemoveAllGoals_Click", ex, User.Identity.Name + " tried to remove all of their Goals.", "HPSErrorLog.txt");
            }

            // Reload step, minutes, and distance data
            GetStepGoals();
            GetDistanceGoals();
            GetMinuteGoals();

            // Redraw the initial graph
            DrawChart(this.Page, 7, "Steps");

            // Build tables for viewing all goals
            TableBuilder.BuildStepGoalsTable(tblStepGoals, userId);
            TableBuilder.BuildDistanceGoalsTable(tblDistanceGoals, userId);
            TableBuilder.BuildMinuteGoalsTable(tblMinuteGoals, userId);
        }

        [WebMethod]
        public static void DeleteDistanceGoal(int id)
        {
            ActivityTracker.Track("Deleted a Distance Goal", (int)UserActionEnum.Deleted);
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var user = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            try
            {
                // Get the distance goal to be deleted
                DistanceGoal distanceGoal = db.DistanceGoals.Find(id);
                db.DistanceGoals.Remove(distanceGoal);

                // Save changes
                db.SaveChanges();

                // Set the notification
                notificationMessage = "Distance Goal was successfully deleted.";
                notificationStyle = "text-success";
                notification = true;

            }
            catch (DataException dx)
            {
                // Set the notification
                notificationMessage = "Distance Goal could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FitBitManager.aspx.cs", "DeleteDistanceGoal", dx, user.AspNetUser.UserName + " tried to delete a Distance Goal.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                // Set the notification
                notificationMessage = "Distance Goal could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FitBitManager.aspx.cs", "DeleteDistanceGoal", ex, user.AspNetUser.UserName + " tried to delete a Distance Goal.", "HPSErrorLog.txt");
            }
        }

        [WebMethod]
        public static void DeleteStepGoal(int id)
        {
            ActivityTracker.Track("Deleted a Step Goal", (int)UserActionEnum.Deleted);
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var user = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            try
            {
                // Get the distance goal to be deleted
                StepGoal stepGoal = db.StepGoals.Find(id);
                db.StepGoals.Remove(stepGoal);

                // Save changes
                db.SaveChanges();

                // Set the notification
                notificationMessage = "Step Goal was successfully deleted.";
                notificationStyle = "text-success";
                notification = true;

            }
            catch (DataException dx)
            {
                // Set the notification
                notificationMessage = "Step Goal could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FitBitManager.aspx.cs", "DeleteStepGoal", dx, user.AspNetUser.UserName + " tried to delete a Step Goal.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                // Set the notification
                notificationMessage = "Step Goal could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FitBitManager.aspx.cs", "DeleteStepGoal", ex, user.AspNetUser.UserName + " tried to delete a Step Goal.", "HPSErrorLog.txt");
            }
        }

        [WebMethod]
        public static void DeleteMinuteGoal(int id)
        {
            ActivityTracker.Track("Deleted a Minutes Goal", (int)UserActionEnum.Deleted);
            // Get the user id thats currently logged in
            string userId = HttpContext.Current.Session["UserId"].ToString();
            var user = db.HPSUsers.Select(u => u)
                .Where(uid => uid.UserId == userId)
                .SingleOrDefault();

            try
            {
                // Get the distance goal to be deleted
                MinuteGoal minuteGoal = db.MinuteGoals.Find(id);
                db.MinuteGoals.Remove(minuteGoal);

                // Save changes
                db.SaveChanges();

                // Set the notification
                notificationMessage = "Minute Goal was successfully deleted.";
                notificationStyle = "text-success";
                notification = true;

            }
            catch (DataException dx)
            {
                // Set the notification
                notificationMessage = "Minute Goal could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FitBitManager.aspx.cs", "DeleteMinuteGoal", dx, user.AspNetUser.UserName + " tried to delete a Minute Goal.", "HPSErrorLog.txt");
            }
            catch (Exception ex)
            {
                // Set the notification
                notificationMessage = "Minute Goal could not be deleted at this time. Please try again later or inform an Administrator.";
                notificationStyle = "text-danger";
                notification = true;

                // Write error to log file Log File Writer
                LogFile.WriteToFile("FitBitManager.aspx.cs", "DeleteMinuteGoal", ex, user.AspNetUser.UserName + " tried to delete a Minute Goal.", "HPSErrorLog.txt");
            }
        }
    }
}