using HPFS.HelperMethods;
using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace HPFS.Pages
{
    public partial class FitBitMonitor : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated || Session["UserId"] == null || !Page.User.IsInRole("Administrator"))
            {
                Response.Redirect("/Main.aspx");
            }

            HelperMethods.ActivityTracker.Track("FitBit Monitor", (int)UserActionEnum.Navigated);
            if (!IsPostBack)
            {
                // Get users from database to use for drop down list
                var users = db.HPSUsers.Select(s => s)
               .OrderBy(s => s.LastName)
               .OrderBy(s => s.FirstName);

                // Clear the list, add initial item, and add users to list
                ddlUsers.Items.Clear();
                ddlUsers.Items.Add(new ListItem("All Users", ""));
                foreach (var u in users)
                {
                    ddlUsers.Items.Add(new ListItem(u.LastName + ", " + u.FirstName, u.AspNetUser.Id));
                }

                // If first param is "" it will get all records
                //GetOverviewData("", "Steps");
                //GetOverviewData("", "Distances");
                //GetOverviewData("", "Minutes");

                // If first param is "" it will get all records
                PrepareCharts("", 50, "Steps", "userStepsChart");
                PrepareCharts("", 50, "Distances", "userDistancesChart");
                PrepareCharts("", 50, "Minutes", "userMinutesChart");

                BuildGoalsTable("", tblDistanceGoals, "Distances");
                BuildGoalsTable("", tblStepGoals, "Steps");
                BuildGoalsTable("", tblMinuteGoals, "Minutes");

                PopulateStatistics("", "Distances");
                PopulateStatistics("", "Steps");
                PopulateStatistics("", "Minutes");
            }
        }

        protected void lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Create list of objects and find selected user
            string userId = ddlUsers.SelectedValue;

            // Draw distance data
            PrepareCharts(userId, 50, "Distances", "userDistancesChart");
            PrepareCharts(userId, 50, "Steps", "userStepsChart");
            PrepareCharts(userId, 50, "Minutes", "userMinutesChart");

            BuildGoalsTable(userId, tblDistanceGoals, "Distances");
            BuildGoalsTable(userId, tblStepGoals, "Steps");
            BuildGoalsTable(userId, tblMinuteGoals, "Minutes");

            PopulateStatistics(userId, "Distances");
            PopulateStatistics(userId, "Steps");
            PopulateStatistics(userId, "Minutes");

            //PopulateDistanceGoalData(userId);
            //PopulateStepGoalData(userId);
            //PopulateMinuteGoalData(userId);

            //GetOverviewData(userId, "Steps");
            //GetOverviewData(userId, "Distances");
            //GetOverviewData(userId, "Minutes");

            //pnlFitBitData.Visible = true;
        }

        protected void PopulateMinuteGoalData(string userId)
        {
            // Get most recent distance goal
            var minuteGoal = db.MinuteGoals.Where(d => d.UserId == userId).OrderByDescending(d => d.GoalEndDate).SingleOrDefault();
            var minuteAchievedForMinuteGoal = db.Minutes.Where(d => d.UserId == d.UserId)
                .Where(m => m.MinuteDate >= minuteGoal.GoalStartDate && m.MinuteDate <= minuteGoal.GoalEndDate)
                .ToList();

            decimal totalMinutesAchieved = 0;
            foreach (var d in minuteAchievedForMinuteGoal)
            {
                totalMinutesAchieved += d.MinuteCount;
            }

            //// Set the goals information
            //headingMinuteGoal.InnerText = minuteGoal.GoalMinute.ToString() + " min";
            //headingTotalMinutes.InnerText = totalMinutesAchieved + " min";
            //headingMinuteGoalDate.InnerText = Convert.ToDateTime(minuteGoal.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(minuteGoal.GoalEndDate).ToShortDateString();
            //headingMinutePercentage.InnerText = Math.Round((totalMinutesAchieved / minuteGoal.GoalMinute) * 100, 2).ToString() + "%";
        }

        protected void PopulateDistanceGoalData(string userId)
        {
            // Get most recent distance goal
            var distanceGoal = db.DistanceGoals.Where(d => d.UserId == userId).OrderByDescending(d => d.GoalEndDate).SingleOrDefault();
            var distanceAchievedForDistanceGoal = db.Distances.Where(d => d.UserId == d.UserId)
                .Where(m => m.DistanceDate >= distanceGoal.GoalStartDate && m.DistanceDate <= distanceGoal.GoalEndDate)
                .ToList();

            decimal totalDistancesAchieved = 0;
            foreach (var d in distanceAchievedForDistanceGoal)
            {
                totalDistancesAchieved += d.DistanceCount;
            }

            // Set the goals information
            //headingDistanceGoal.InnerText = distanceGoal.GoalDistance.ToString() + " km";
            //headingTotalDistance.InnerText = totalDistancesAchieved + " km";
            //headingDistanceGoalDate.InnerText = Convert.ToDateTime(distanceGoal.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(distanceGoal.GoalEndDate).ToShortDateString();
            //headingDistancePercentage.InnerText = Math.Round((totalDistancesAchieved / distanceGoal.GoalDistance) * 100, 2).ToString() + "%";
        }

        protected void PopulateStepGoalData(string userId)
        {
            // Get most recent distance goal
            var stepGoal = db.StepGoals.Where(d => d.UserId == userId).OrderByDescending(d => d.GoalEndDate).SingleOrDefault();
            var stepsAchievedForDistanceGoal = db.Steps.Where(d => d.UserId == d.UserId)
                .Where(m => m.StepDate >= stepGoal.GoalStartDate && m.StepDate <= stepGoal.GoalEndDate)
                .ToList();

            decimal totalStepsAchieved = 0;
            foreach (var d in stepsAchievedForDistanceGoal)
            {
                totalStepsAchieved += d.StepCount;
            }

            //// Set the goals information
            //headingStepsGoal.InnerText = stepGoal.GoalSteps.ToString();
            //headingTotalSteps.InnerText = totalStepsAchieved.ToString();
            //headingStepsGoalDate.InnerText = Convert.ToDateTime(stepGoal.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(stepGoal.GoalEndDate).ToShortDateString();
            //headingStepsPercentage.InnerText = Math.Round((totalStepsAchieved / stepGoal.GoalSteps) * 100, 2).ToString() + "%";
        }

        //protected void GetOverviewData(string userId, string dataType)
        //{
        //    if (dataType == "Steps")
        //    {
        //        // Get all time personal records, totals, and averages
        //        double personalRecordSteps = db.Steps.Where(d => d.UserId.Contains(userId)).Max(s => s.StepCount);
        //        double totalSteps = db.Steps.Where(d => d.UserId.Contains(userId)).Sum(s => s.StepCount);
        //        double avgSteps = db.Steps.Where(d => d.UserId.Contains(userId)).Average(s => s.StepCount);

        //        //// If userId is "" then it gets all records data
        //        //if (userId == "")
        //        //{
        //        //    // Add data to their appropriate labels
        //        //    allOverviewAverageSteps.InnerText = Math.Round(avgSteps).ToString();
        //        //    allOverviewRecordSteps.InnerText = Math.Round(personalRecordSteps).ToString();
        //        //    allOverviewTotalSteps.InnerText = Math.Round(totalSteps).ToString();
        //        //}
        //        //else
        //        //{
        //        // Add data to their appropriate labels
        //        userOverviewAverageSteps.InnerText = Math.Round(avgSteps).ToString();
        //        userOverviewRecordSteps.InnerText = Math.Round(personalRecordSteps).ToString();
        //        userOverviewTotalSteps.InnerText = Math.Round(totalSteps).ToString();
        //        //}
        //    }
        //    else if (dataType == "Distances")
        //    {
        //        // Get all time personal records, totals, and averages
        //        decimal personalRecordDistance = db.Distances.Where(d => d.UserId.Contains(userId)).Max(s => s.DistanceCount);
        //        decimal totalDistance = db.Distances.Where(d => d.UserId.Contains(userId)).Sum(s => s.DistanceCount);
        //        decimal avgDistance = db.Distances.Where(d => d.UserId.Contains(userId)).Average(s => s.DistanceCount);

        //        //// If userId is "" then it gets all records data
        //        //if (userId == "")
        //        //{
        //        //    // Add data to their appropriate labels
        //        //    allOverviewAverageDistance.InnerText = Math.Round(avgDistance, 2) + " km";
        //        //    allOverviewRecordDistance.InnerText = Math.Round(personalRecordDistance, 2).ToString() + " km";
        //        //    allOverviewTotalDistance.InnerText = Math.Round(totalDistance, 2).ToString() + " km";
        //        //}
        //        //else
        //        //{
        //        // Add data to their appropriate labels
        //        userOverviewAverageDistance.InnerText = Math.Round(avgDistance, 2) + " km";
        //        userOverviewRecordDistance.InnerText = Math.Round(personalRecordDistance, 2).ToString() + " km";
        //        userOverviewTotalDistance.InnerText = Math.Round(totalDistance, 2).ToString() + " km";
        //        //}
        //    }
        //    else
        //    {
        //        // Get all time personal records, totals, and averages
        //        decimal personalRecordMinutes = db.Minutes.Where(d => d.UserId.Contains(userId)).Max(s => s.MinuteCount);
        //        decimal totalMinutes = db.Minutes.Where(d => d.UserId.Contains(userId)).Sum(s => s.MinuteCount);
        //        decimal avgMinutes = db.Minutes.Where(d => d.UserId.Contains(userId)).Average(s => s.MinuteCount);

        //        //// If userId is "" then it gets all records data
        //        //if (userId == "")
        //        //{
        //        //    // Add data to their appropriate labels
        //        //    allOverviewAverageTime.InnerText = Math.Round(avgMinutes).ToString() + " min";
        //        //    allOverviewRecordTime.InnerText = Math.Round(personalRecordMinutes).ToString() + " min";
        //        //    allOverviewTotalTime.InnerText = Math.Round(totalMinutes).ToString() + " min";
        //        //}
        //        //else
        //        //{
        //        // Add data to their appropriate labels
        //        userOverviewAverageTime.InnerText = Math.Round(avgMinutes).ToString() + " min";
        //        userOverviewRecordTime.InnerText = Math.Round(personalRecordMinutes).ToString() + " min";
        //        userOverviewTotalTime.InnerText = Math.Round(totalMinutes).ToString() + " min";
        //        //}
        //    }
        //}

        public void PrepareCharts(string userId, int dayCount, string chartType, string chartId)
        {
            DateTime startDate = DateTime.Today.AddDays(-dayCount);
            if (chartType == "Steps")
            {
                // Get steps between a date range
                var steps = db.Steps.Where(d => d.UserId.Contains(userId))
                    .Where(d => d.StepDate > startDate && d.StepDate <= DateTime.Today);

                // Get total of all users steps within that range
                var stepsTotal = steps.GroupBy(s => s.StepDate)
                    .Select(d => new FitBitObject { Total = d.Sum(st => st.StepCount), Date = d.Key.Value })
                    .ToList();

                // Draw the chart to the appropriate div
                DrawChart(this.Page, chartId, stepsTotal, chartType, userId);

                //if (userId == "")
                //{
                //    // Set the heading
                //    overViewStepsHeadingAll.InnerText = "Steps in Last " + stepsTotal.Count + " Days";
                //}
                //else
                //{
                // Set the graph heading
                userStepsHeading.InnerText = "Steps in Last " + stepsTotal.Count + " Days";
                //}
            }
            else if (chartType == "Distances")
            {
                // Get distances between a date range
                var distances = db.Distances.Where(d => d.UserId.Contains(userId))
                    .Where(d => d.DistanceDate > startDate && d.DistanceDate <= DateTime.Today);

                // Get total of all users distances within that range
                var distancesTotal = distances.GroupBy(s => s.DistanceDate)
                    .Select(d => new FitBitObject { Total = d.Sum(st => st.DistanceCount), Date = d.Key.Value })
                    .ToList();

                // Draw the chart to the appropriate div
                DrawChart(this.Page, chartId, distancesTotal, chartType, userId);

                //if (userId == "")
                //{
                //    // Set the heading
                //    overViewDistancesHeadingAll.InnerText = "Distances in Last " + distancesTotal.Count + " Days (km)";
                //}
                //else
                //{
                // Set the graph heading
                userDistancesHeading.InnerText = "Distance in Last " + distancesTotal.Count + " Days(km)";
                //}
            }
            else
            {
                // Get minutes between a date range
                var minutes = db.Minutes.Where(d => d.UserId.Contains(userId))
                    .Where(d => d.MinuteDate > startDate && d.MinuteDate <= DateTime.Today);

                // Get total of all users minutes within that range
                var minutesTotal = minutes.GroupBy(s => s.MinuteDate)
                    .Select(d => new FitBitObject { Total = d.Sum(st => st.MinuteCount), Date = d.Key.Value })
                    .ToList();

                // Draw the chart to the appropriate div
                DrawChart(this.Page, chartId, minutesTotal, chartType, userId);

                //if (userId == "")
                //{
                //    // Set the heading
                //    overViewMinutesHeadingAll.InnerText = "Minutes in Last " + minutesTotal.Count + " Days";
                //}
                //else
                //{
                // Set the graph heading
                userMinutesHeading.InnerText = "Minutes in Last " + minutesTotal.Count + " Days";
                //}
            }
        }

        protected void DrawChart(Page page, string chartId, List<FitBitObject> data, string chartType, string userId)
        {
            // Start chart
            string script = "<script type='text/javascript'>";
            script += (chartType == "Steps") ? "var stepChart;" : (chartType == "Distances") ? "var distanceChart;" : "var minuteChart;";
            script += "$(document).ready(function () {";
            script += (chartType == "Steps") ? "stepChart = c3.generate({" : (chartType == "Distances") ? "distanceChart = c3.generate({" : "minuteChart = c3.generate({";
            script += "bindto: '#" + chartId + "'," +
                        "data: {" +
                            "x: 'x'," +
                            "xFormat: '%m/%d/%Y'," +
                            "columns: [" +
                                "['x'";
            // Add dates to x axis
            foreach (var d in data)
            {
                string parseDate = Convert.ToDateTime(d.Date).ToShortDateString();
                script += ", '" + parseDate + "'";
            }

            // Start step data array
            script += "]," + "['" + chartType + "'";

            // Initialize value counter
            decimal valueCount = 0;

            // Add in step data array
            foreach (var s in data)
            {
                valueCount += Convert.ToDecimal(s.Total);
                script += ", " + s.Total;
            }

            // POSSIBLE DIVIDE BY ZERO EXCEPTION PUT TRY CATCH ON
            // Parse the values to the appropriate decimal places based on the type of chart we are generating
            decimal avgValue = (valueCount == 0) ? 0 : (chartType == "Steps") ? Math.Round((valueCount / data.Count)) : Math.Round((valueCount / data.Count), 2);
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
                            "text: '" + chartType + " from Last " + data.Count.ToString() + " Days'," +
                            "position: 'outer-middle'" +
                        "}" +
                    "}" +
                "}" +
            "});" +
        "});" +
    "</script>";

            // Initialize the script on the page
            page.ClientScript.RegisterStartupScript(page.GetType(), chartType + "Chart-" + userId, script);
        }

        protected void PopulateStatistics(string userId, string statisticType)
        {
            HPSUser user = db.HPSUsers.Where(u => u.UserId == userId).SingleOrDefault();

            if (statisticType == "Steps")
            {
                // Get All Data
                int? max = db.Steps.Where(d => d.UserId.Contains(userId)).Max(s => (int?)s.StepCount);
                int? sum = db.Steps.Where(d => d.UserId.Contains(userId)).Sum(s => (int?)s.StepCount);
                double? avg = db.Steps.Where(d => d.UserId.Contains(userId)).Average(s => (double?)s.StepCount);
                int? min = db.Steps.Where(d => d.UserId.Contains(userId)).Where(s => s.StepCount > 0).Min(s => (int?)s.StepCount);
                int? total = db.Steps.Where(d => d.UserId.Contains(userId)).Count();
                DateTime? minDate = Convert.ToDateTime(db.Steps.Where(d => d.UserId.Contains(userId)).Min(d => d.StepDate));
                DateTime? maxDate = Convert.ToDateTime(db.Steps.Where(d => d.UserId.Contains(userId)).Max(d => d.StepDate));

                // Display Data
                lblStepsUserValue.Text = (user == null) ? "All Users Combined" : user.FirstName + " " + user.LastName;
                lblStepsMaxValue.Text = (max == 0 || max == null) ? "No data found." : ((int)max).ToString("N0");
                lblStepsSumValue.Text = (sum == 0 || sum == null) ? "No data found." : ((int)sum).ToString("N0");
                lblStepsAvgValue.Text = (avg == 0 || avg == null) ? "No data found." : ((double)avg).ToString("N0");
                lblStepsMinValue.Text = (min == 0 || min == null) ? "No data found." : ((int)min).ToString("N0");
                lblStepsTotalRecordsValue.Text = (total == 1 || max == null) ? "No data found." : ((int)total) + " records";
                lblStepsDateRangeValue.Text = (max == null || max == 0 || sum == null || sum == 0 || avg == null || avg == 0 || min == null || min == 0 || total == null || total == 1) ? "No data found." : ((DateTime)minDate).ToShortDateString() + " - " + ((DateTime)maxDate).ToShortDateString();
            }
            else if (statisticType == "Distances")
            {
                // Get All Data
                decimal? max = db.Distances.Where(d => d.UserId.Contains(userId)).Max(s => (decimal?)s.DistanceCount);
                decimal? sum = db.Distances.Where(d => d.UserId.Contains(userId)).Sum(s => (decimal?)s.DistanceCount);
                decimal? avg = db.Distances.Where(d => d.UserId.Contains(userId)).Average(s => (decimal?)s.DistanceCount);
                decimal? min = db.Distances.Where(d => d.UserId.Contains(userId)).Where(s => s.DistanceCount > 0).Min(s => (decimal?)s.DistanceCount);
                int? total = db.Distances.Where(d => d.UserId.Contains(userId)).Count();
                DateTime? minDate = Convert.ToDateTime(db.Distances.Where(d => d.UserId.Contains(userId)).Min(d => d.DistanceDate));
                DateTime? maxDate = Convert.ToDateTime(db.Distances.Where(d => d.UserId.Contains(userId)).Max(d => d.DistanceDate));

                // Display Data
                lblDistancesUserValue.Text = (user == null) ? "All Users Combined" : user.FirstName + " " + user.LastName;
                lblDistancesMaxValue.Text = (max == 0 || max == null) ? "No data found." : ((decimal)max).ToString("N2") + " km";
                lblDistancesSumValue.Text = (sum == 0 || sum == null) ? "No data found." : ((decimal)sum).ToString("N2") + " km";
                lblDistancesAvgValue.Text = (avg == 0 || avg == null) ? "No data found." : ((decimal)avg).ToString("N2") + " km";
                lblDistancesMinValue.Text = (min == 0 || min == null) ? "No data found." : ((decimal)min).ToString("N2") + " km";
                lblDistancesTotalRecordsValue.Text = (total == 1 || max == null) ? "No data found." : ((int)total) + " records";
                lblDistancesDateRangeEntryValue.Text = (max == null || max == 0 || sum == null || sum == 0 || avg == null || avg == 0 || min == null || min == 0 || total == null || total == 1) ? "No data found." : ((DateTime)minDate).ToShortDateString() + " - " + ((DateTime)maxDate).ToShortDateString();
            }
            else
            {
                // Get All Data
                decimal? max = db.Minutes.Where(d => d.UserId.Contains(userId)).Max(s => (decimal?)s.MinuteCount);
                decimal? sum = db.Minutes.Where(d => d.UserId.Contains(userId)).Sum(s => (decimal?)s.MinuteCount);
                decimal? avg = db.Minutes.Where(d => d.UserId.Contains(userId)).Average(s => (decimal?)s.MinuteCount);
                decimal? min = db.Minutes.Where(d => d.UserId.Contains(userId)).Where(s => s.MinuteCount > 0).Min(s => (decimal?)s.MinuteCount);
                int? total = db.Minutes.Where(d => d.UserId.Contains(userId)).Count();
                DateTime? minDate = Convert.ToDateTime(db.Minutes.Where(d => d.UserId.Contains(userId)).Min(d => d.MinuteDate));
                DateTime? maxDate = Convert.ToDateTime(db.Minutes.Where(d => d.UserId.Contains(userId)).Max(d => d.MinuteDate));

                // Display Data
                lblMinutesUserValue.Text = (user == null) ? "All Users Combined" : user.FirstName + " " + user.LastName;
                lblMinutesMaxValue.Text = (max == 0 || max == null) ? "No data found." : ((decimal)max).ToString("N0") + " min";
                lblMinutesSumValue.Text = (sum == 0 || sum == null) ? "No data found." : ((decimal)sum).ToString("N0") + " min";
                lblMinutesAvgValue.Text = (avg == 0 || avg == null) ? "No data found." : ((decimal)avg).ToString("N0") + " min";
                lblMinutesMinValue.Text = (min == 0 || min == null) ? "No data found." : ((decimal)min).ToString("N0") + " min";
                lblMinutesTotalRecordsValue.Text = (total == 1 || max == null) ? "No data found." : ((int)total) + " records";
                lblMinutesDateRangeValue.Text = (max == null || max == 0 || sum == null || sum == 0 || avg == null || avg == 0 || min == null || min == 0 || total == null || total == 1) ? "No data found." : ((DateTime)minDate).ToShortDateString() + " - " + ((DateTime)maxDate).ToShortDateString();
            }
        }

        protected void BuildGoalsTable(string userId, Table table, string goalType)
        {
            if (goalType == "Steps")
            {
                var stepGoals = db.StepGoals.Where(u => u.UserId.Contains(userId))
                    .Select(s => new FitBitObject { GoalStartDate = s.GoalStartDate, GoalEndDate = s.GoalEndDate, GoalSteps = s.GoalSteps, Username = s.AspNetUser.UserName });
                int i = 1;
                foreach (var s in stepGoals)
                {
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell number = new TableCell(),
                        user = new TableCell(),
                        dateRange = new TableCell(),
                        goalValue = new TableCell())
                        {
                            number.Text = Convert.ToString(i++);
                            user.Text = s.Username;
                            dateRange.Text = Convert.ToDateTime(s.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(s.GoalEndDate).ToShortDateString();
                            goalValue.Text = s.GoalSteps.ToString();

                            tr.Cells.Add(number);
                            tr.Cells.Add(user);
                            tr.Cells.Add(dateRange);
                            tr.Cells.Add(goalValue);

                            table.Rows.Add(tr);
                        }
                    }
                }
            }
            else if (goalType == "Distances")
            {
                var distanceGoals = db.DistanceGoals.Where(u => u.UserId.Contains(userId))
                    .Select(s => new FitBitObject { GoalStartDate = s.GoalStartDate, GoalEndDate = s.GoalEndDate, GoalDistance = s.GoalDistance, Username = s.AspNetUser.UserName });
                int i = 1;
                foreach (var d in distanceGoals)
                {
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell number = new TableCell(),
                        user = new TableCell(),
                        dateRange = new TableCell(),
                        goalValue = new TableCell())
                        {
                            number.Text = Convert.ToString(i++);
                            user.Text = d.Username;
                            dateRange.Text = Convert.ToDateTime(d.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(d.GoalEndDate).ToShortDateString();
                            goalValue.Text = d.GoalDistance.ToString();

                            tr.Cells.Add(number);
                            tr.Cells.Add(user);
                            tr.Cells.Add(dateRange);
                            tr.Cells.Add(goalValue);

                            table.Rows.Add(tr);
                        }
                    }
                }
            }
            else
            {
                var minuteGoals = db.MinuteGoals.Where(u => u.UserId.Contains(userId))
                    .Select(s => new FitBitObject { GoalStartDate = s.GoalStartDate, GoalEndDate = s.GoalEndDate, GoalMinutes = s.GoalMinute, Username = s.AspNetUser.UserName });
                int i = 1;
                foreach (var m in minuteGoals)
                {
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell number = new TableCell(),
                        user = new TableCell(),
                        dateRange = new TableCell(),
                        goalValue = new TableCell())
                        {
                            number.Text = Convert.ToString(i++);
                            user.Text = m.Username;
                            dateRange.Text = Convert.ToDateTime(m.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(m.GoalEndDate).ToShortDateString();
                            goalValue.Text = m.GoalMinutes.ToString();

                            tr.Cells.Add(number);
                            tr.Cells.Add(user);
                            tr.Cells.Add(dateRange);
                            tr.Cells.Add(goalValue);

                            table.Rows.Add(tr);
                        }
                    }
                }
            }
        }
    }

    public class FitBitObject
    {
        public decimal Total { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? GoalStartDate { get; set; }
        public DateTime? GoalEndDate { get; set; }
        public int GoalSteps { get; set; }
        public decimal GoalDistance { get; set; }
        public decimal GoalMinutes { get; set; }
        public string Username { get; set; }

    }
}