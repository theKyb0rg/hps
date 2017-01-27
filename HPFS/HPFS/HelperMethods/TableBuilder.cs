using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HPFS.Models;
using System.Web.UI;

namespace HPFS.HelperMethods
{
    public class TableBuilder
    {
        public static HPSDB db = new HPSDB();

        public static void BuildSearchTable(Table table, dynamic query, string role)
        {
            // Decalre in using so we can dispose of the object after its done being used
            using (TableHeaderRow th = new TableHeaderRow())
            {
                th.TableSection = TableRowSection.TableHeader;
                string[] headerArray = { "#", "File Name", "Folder Name", "Size", "Type", "Date Uploaded", "Action" };
                for (int i = 0; i < headerArray.Length; i++)
                {
                    using (TableHeaderCell cell = new TableHeaderCell())
                    {
                        cell.Font.Bold = true;
                        cell.Text = headerArray[i];
                        th.Cells.Add(cell);
                    }
                }
                // Add table header to the table
                table.Rows.Add(th);
            }

            // Placeholder column number, instead of displaying id's
            int index = 1;

            // Create table with data
            foreach (var f in query)
            {
                using (TableRow tr = new TableRow())
                {
                    using (TableCell number = new TableCell(),
                    fileName = new TableCell(),
                    folderName = new TableCell(),
                    fileSize = new TableCell(),
                    fileExtension = new TableCell(),
                    fileDate = new TableCell(),
                    action = new TableCell())
                    {
                        // Construct link buttons
                        using (LinkButton download = new LinkButton(), delete = new LinkButton())
                        {
                            // Add text to cells
                            number.Text = Convert.ToString(index++);
                            fileName.Text = f.FileName;
                            folderName.Text = f.Folder.FolderName;
                            fileSize.Text = (f.FileSize != 0) ? (Math.Round(((decimal)f.FileSize / 1048576), 2)).ToString() + " MB" : 0.00 + " MB";
                            fileExtension.Text = f.FileExtension;
                            fileDate.Text = f.FileDate.ToString();

                            // Code for the edit button
                            download.CssClass = "btn btn-default btn-xs";
                            download.Attributes.Add("data-toggle", "tooltip");
                            download.Attributes.Add("data-id", f.Id.ToString());
                            download.Attributes.Add("title", "Download");
                            download.Attributes.Add("name", "download");
                            download.Attributes.Add("runat", "server");
                            download.Text = "<span class='glyphicon glyphicon-download-alt'></span>";
                            action.Controls.Add(download);

                            // Only add delete button if user is in admin role
                            if (role == "Administrator")
                            {
                                // Code for the remove button
                                delete.CssClass = "btn btn-default btn-xs";
                                delete.Attributes.Add("data-toggle", "tooltip");
                                delete.Attributes.Add("data-id", f.Id.ToString());
                                delete.Attributes.Add("title", "Delete");
                                delete.Attributes.Add("name", "delete");
                                delete.Attributes.Add("runat", "server");
                                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                                action.Controls.Add(delete);
                            }

                            // Ad cells to rows
                            tr.Cells.Add(number);
                            tr.Cells.Add(fileName);
                            tr.Cells.Add(folderName);
                            tr.Cells.Add(fileSize);
                            tr.Cells.Add(fileExtension);
                            tr.Cells.Add(fileDate);
                            tr.Cells.Add(action);

                            // Add row to table
                            table.Rows.Add(tr);
                        }
                    }
                }
            }
        }

        public static Table BuildFilesTable(int folderId, string role)
        {
            using (Table table = new Table())
            {
                // Query DB
                var files = db.HPSFiles.Where(f => f.FolderId == folderId)
                    .Select(file => new HPSFileViewModel { Id = file.Id, FileName = file.FileName, FileSize = file.FileSize, FileExtension = file.FileExtension, FileDate = file.FileDate, Folder = file.Folder })
                    .OrderBy(date => date.FileDate);


                if (files.Any())
                {
                    // Set the table class if there is data
                    table.CssClass = "table table-condensed sortable";

                    // Build the table headers and add them to the table
                    using (TableHeaderRow th = new TableHeaderRow())
                    {
                        th.TableSection = TableRowSection.TableHeader;
                        string[] headerArray = { "#", "File Name", "Folder Name", "Size", "Type", "Date Uploaded", "Action" };
                        for (int i = 0; i < headerArray.Length; i++)
                        {
                            using (TableHeaderCell cell = new TableHeaderCell())
                            {
                                cell.Font.Bold = true;
                                cell.Text = headerArray[i];
                                th.Cells.Add(cell);
                            }
                        }
                        // Add table header to the table
                        table.Rows.Add(th);
                    }

                    // Placeholder column number, instead of displaying id's
                    int index = 1;

                    // Create table with data
                    foreach (var f in files)
                    {
                        using (TableRow tr = new TableRow())
                        {
                            using (TableCell number = new TableCell(),
                            fileName = new TableCell(),
                            folderName = new TableCell(),
                            fileSize = new TableCell(),
                            fileExtension = new TableCell(),
                            fileDate = new TableCell(),
                            action = new TableCell())
                            {
                                // Construct link buttons
                                using (LinkButton download = new LinkButton(), delete = new LinkButton())
                                {
                                    // Code for the download button
                                    download.CssClass = "btn btn-default btn-xs";
                                    download.Attributes.Add("data-toggle", "tooltip");
                                    download.Attributes.Add("data-id", f.Id.ToString());
                                    download.Attributes.Add("title", "Download");
                                    download.Attributes.Add("name", "download");
                                    download.Attributes.Add("runat", "server");
                                    download.Text = "<span class='glyphicon glyphicon-download-alt'></span>";

                                    // Add text to cells
                                    number.Text = Convert.ToString(index++);
                                    fileName.Text = f.FileName;
                                    folderName.Text = f.Folder.FolderName;
                                    fileSize.Text = (f.FileSize != 0) ? Math.Round(((decimal)f.FileSize / 1048576), 2).ToString() + " MB" : 0.00 + " MB";
                                    fileExtension.Text = f.FileExtension;
                                    fileDate.Text = f.FileDate.ToString();
                                    action.Controls.Add(download);

                                    // Only add delete button if user is in administrator role
                                    if (role == "Administrator")
                                    {
                                        // Code for the remove button
                                        delete.CssClass = "btn btn-default btn-xs";
                                        delete.Attributes.Add("data-toggle", "tooltip");
                                        delete.Attributes.Add("data-id", f.Id.ToString());
                                        delete.Attributes.Add("title", "Delete");
                                        delete.Attributes.Add("name", "delete");
                                        delete.Attributes.Add("runat", "server");
                                        delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                                        action.Controls.Add(delete);

                                    }

                                    // Add cells to rows
                                    tr.Cells.Add(number);
                                    tr.Cells.Add(fileName);
                                    tr.Cells.Add(folderName);
                                    tr.Cells.Add(fileSize);
                                    tr.Cells.Add(fileExtension);
                                    tr.Cells.Add(fileDate);
                                    tr.Cells.Add(action);

                                    // Add row to table
                                    table.Rows.Add(tr);
                                }
                            }
                        }

                    }
                    return table;
                }
                else
                {
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell td = new TableCell())
                        {
                            using (Label message = new Label())
                            {
                                message.Text = "There are no files in this folder.";

                                // Add row, cell and message to table
                                td.Controls.Add(message);
                                tr.Controls.Add(td);
                                table.Controls.Add(tr);
                            }
                        }
                    }
                    return table;
                }
            }
        }

        public static void BuildUsersTable(Table table, List<HPSUser> users, bool isFitBitTable)
        {

            // Clear the table before rebuilds
            table.Rows.Clear();

            // Decalre in using so we can dispose of the object after its done being used
            using (TableHeaderRow th = new TableHeaderRow())
            {
                th.TableSection = TableRowSection.TableHeader;
                string[] headerArray = { "#", "Username", "Name", "Role", "Created On", "Action" };
                for (int i = 0; i < headerArray.Length; i++)
                {
                    using (TableHeaderCell cell = new TableHeaderCell())
                    {
                        cell.Font.Bold = true;
                        cell.Text = headerArray[i];
                        th.Cells.Add(cell);
                    }

                }
                // Add table header to the table
                table.Rows.Add(th);
            }

            // Set a placeholder counter
            int index = 1;

            foreach (var u in users)
            {
                // Construct table row
                using (TableRow tr = new TableRow())
                {
                    // Construct Table cells
                    using (TableCell number = new TableCell(),
                    name = new TableCell(),
                    userName = new TableCell(),
                    userType = new TableCell(),
                    userStartDate = new TableCell(),
                    action = new TableCell())
                    {
                        // Construct link buttons
                        using (LinkButton view = new LinkButton(),
                        delete = new LinkButton())
                        {
                            if (isFitBitTable)
                            {
                                // Code for the remove button
                                view.CssClass = "btn btn-default btn-xs";
                                view.Attributes.Add("data-toggle", "tooltip");
                                view.Attributes.Add("data-id", u.UserId.ToString());
                                view.Attributes.Add("title", "View Data");
                                view.Attributes.Add("name", "view-fitbit-data");
                                view.Attributes.Add("runat", "server");
                                view.Text = "<span class='glyphicon glyphicon-eye-open'></span>";
                                action.Controls.Add(view);
                            }
                            else
                            {
                                // Code for the remove button
                                delete.CssClass = "btn btn-default btn-xs";
                                delete.Attributes.Add("data-toggle", "tooltip");
                                delete.Attributes.Add("data-id", u.UserId.ToString());
                                delete.Attributes.Add("title", "Delete");
                                delete.Attributes.Add("name", "delete");
                                delete.Attributes.Add("runat", "server");
                                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                                action.Controls.Add(delete);
                            }

                            // Add text to cells
                            number.Text = Convert.ToString(index++);
                            userName.Text = u.AspNetUser.UserName;
                            name.Text = u.FirstName + " " + u.LastName;
                            userType.Text = u.RoleName;
                            userStartDate.Text = Convert.ToDateTime(u.CreatedOn).ToShortDateString();

                            // Ad cells to rows
                            tr.Cells.Add(number);
                            tr.Cells.Add(userName);
                            tr.Cells.Add(name);
                            tr.Cells.Add(userType);
                            tr.Cells.Add(userStartDate);
                            tr.Cells.Add(action);

                            // Add row to table
                            table.Rows.Add(tr);
                        }
                    }
                }
            }
        }

        public static Table BuildNotificationTable(string userId, Table table)
        {
            // Clear the table to avoid duplicate table rows
            table.Rows.Clear();

            using (table)
            {
                // Query DB
                var notiQuery =
                    from noti in db.Notifications
                    join unet in db.AspNetUsers on noti.UserId equals unet.Id
                    where noti.UserId == userId && noti.IsRead == false
                    select noti;

                // Count how many notifications there are for this user
                int notificationCount = notiQuery.Count();

                // Get the master page from the class file
                var pageHandler = HttpContext.Current.CurrentHandler;
                if (pageHandler is System.Web.UI.Page)
                {
                    // Get the badge control
                    HtmlGenericControl badge = (HtmlGenericControl)((System.Web.UI.Page)pageHandler).Master.FindControl("notificationCount");

                    // Assign the badge control the number of notifications
                    badge.InnerText = notificationCount.ToString();
                }

                if (notiQuery.Any())
                {
                    // Build the table headers and add them to the table
                    using (TableHeaderRow th = new TableHeaderRow())
                    {
                        th.TableSection = TableRowSection.TableHeader;
                        string[] headerArray = { "Priority", "Title", "Description", "Date", "Action" };
                        for (int i = 0; i < headerArray.Length; i++)
                        {
                            using (TableHeaderCell cell = new TableHeaderCell())
                            {
                                cell.Font.Bold = true;
                                cell.Text = headerArray[i];
                                th.Cells.Add(cell);
                            }
                        }

                        // Add table header to the table
                        table.Rows.Add(th);
                    }

                    // Create table with data
                    foreach (var noti in notiQuery)
                    {
                        using (TableRow tr = new TableRow())
                        {
                            using (TableCell priority = new TableCell(),
                            title = new TableCell(),
                            description = new TableCell(),
                            date = new TableCell(),
                            action = new TableCell())
                            {
                                // Add CSS classes
                                action.CssClass = "text-center";

                                // Set the widths of the cells
                                title.Width = 150;
                                description.Width = 300;

                                // Construct link buttons
                                using (HtmlGenericControl btnRead = new HtmlGenericControl("a"))
                                {
                                    // Code for the read button
                                    btnRead.Attributes.Add("class", "btn btn-default btn-xs");
                                    btnRead.Attributes.Add("data-toggle", "tooltip");
                                    btnRead.Attributes.Add("data-id", noti.Id.ToString());
                                    btnRead.Attributes.Add("title", "Mark As Read");
                                    btnRead.Attributes.Add("name", "read");
                                    btnRead.InnerHtml = "<span class='glyphicon glyphicon-pushpin'></span>";

                                    // Add text to cells                    
                                    title.Text = noti.Title;
                                    description.Text = noti.Description;
                                    date.Text = Convert.ToDateTime(noti.NotificationDate).ToShortDateString();
                                    priority.Text = noti.Priority;
                                    action.Controls.Add(btnRead);

                                    // Check priority level and apply appropriate class
                                    switch (noti.Priority)
                                    {
                                        case "High":
                                            priority.CssClass = "text-danger";
                                            break;
                                        case "Low":
                                            priority.CssClass = "text-success";
                                            break;
                                        case "Normal":
                                            priority.CssClass = "text-warning";
                                            break;
                                        default:
                                            priority.CssClass = "text-primary";
                                            break;
                                    }

                                    // Add cells to rows                    
                                    tr.Cells.Add(priority);
                                    tr.Cells.Add(title);
                                    tr.Cells.Add(description);
                                    tr.Cells.Add(date);
                                    tr.Cells.Add(action);

                                    // Add row to table
                                    table.Rows.Add(tr);
                                }
                            }
                        }
                    }
                    return table;
                }
                else
                {
                    // Create a blank row with no results text
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell td = new TableCell())
                        {
                            using (Label message = new Label())
                            {
                                message.Text = "<b>No notifications to show.</b>";

                                // Add row, cell and message to table
                                td.Controls.Add(message);
                                tr.Controls.Add(td);
                                table.Controls.Add(tr);
                                td.BorderStyle = BorderStyle.None;
                            }
                        }
                    }
                    return table;
                }
            }
        }

        public static Table BuildFeedbackTable(List<string> fbData, Table table)
        {
            // Clear the table to avoid duplicate table rows
            table.Rows.Clear();

            using (table)
            {
                // Declaring variables
                string siteArea = fbData.ElementAt(2).ToString();
                int accRating = Convert.ToInt32(fbData.ElementAt(3));
                int appRating = Convert.ToInt32(fbData.ElementAt(4));
                int navRating = Convert.ToInt32(fbData.ElementAt(5));
                int recRating = Convert.ToInt32(fbData.ElementAt(6));
                double avgStart = Convert.ToDouble(fbData.ElementAt(7));
                double avgEnd = Convert.ToDouble(fbData.ElementAt(8));

                // Query DB
                var fbQuery = from feed in db.Feedbacks
                              select feed;

                // Build Where Clause for query
                if (fbData.ElementAt(0) != "" && fbData.ElementAt(1) != "")
                {
                    DateTime startDate = Convert.ToDateTime(fbData.ElementAt(0));
                    DateTime endDate = Convert.ToDateTime(fbData.ElementAt(1));
                    fbQuery = fbQuery.Where(d => d.FeedBackDate >= startDate && d.FeedBackDate <= endDate);
                }
                else if (fbData.ElementAt(0) != "" && fbData.ElementAt(1) == "")
                {
                    DateTime startDate = Convert.ToDateTime(fbData.ElementAt(0));
                    fbQuery = fbQuery.Where(d => d.FeedBackDate >= startDate);
                }
                else if (fbData.ElementAt(0) == "" && fbData.ElementAt(1) != "")
                {
                    DateTime endDate = Convert.ToDateTime(fbData.ElementAt(1));
                    fbQuery = fbQuery.Where(d => d.FeedBackDate <= endDate);
                }

                if (siteArea != "Overall") { fbQuery = fbQuery.Where(d => d.FeedBackArea == siteArea); }

                if (accRating != 0) { fbQuery = fbQuery.Where(d => d.FeedBackAccRating == accRating); }

                if (appRating != 0) { fbQuery = fbQuery.Where(d => d.FeedBackAppRating == appRating); }

                if (navRating != 0) { fbQuery = fbQuery.Where(d => d.FeedBackNavRating == navRating); }

                if (recRating != 0) { fbQuery = fbQuery.Where(d => d.FeedBackRecRating == recRating); }

                if (avgStart != 0 && avgEnd != 0) { fbQuery = fbQuery.Where(d => d.FeedBackAvg >= avgStart && d.FeedBackAvg <= avgEnd); }
                else if (avgStart != 0 && avgEnd == 0) { fbQuery = fbQuery.Where(d => d.FeedBackAvg >= avgStart); }
                else if (avgStart == 0 && avgEnd != 0) { fbQuery = fbQuery.Where(d => d.FeedBackAvg <= avgEnd); }

                if (fbQuery.Any())
                {
                    // Build the table headers and add them to the table
                    using (TableHeaderRow th = new TableHeaderRow())
                    {
                        th.TableSection = TableRowSection.TableHeader;
                        string[] headerArray = { "Date", "Accessibility", "Appearance", "Recommendation", "Navigation", "Average", "Area", "Comment", "Action" };
                        for (int i = 0; i < headerArray.Length; i++)
                        {
                            using (TableHeaderCell cell = new TableHeaderCell())
                            {
                                cell.Font.Bold = true;
                                cell.Text = headerArray[i];
                                cell.CssClass = "text-center";
                                th.Cells.Add(cell);
                            }
                        }

                        // Add table header to the table
                        table.Rows.Add(th);
                    }

                    // Create table with data
                    foreach (var fb in fbQuery)
                    {
                        using (TableRow tr = new TableRow())
                        {
                            using (TableCell date = new TableCell(),
                                             starsAcc = new TableCell(),
                                             starsApp = new TableCell(),
                                             starsRec = new TableCell(),
                                             starsNav = new TableCell(),
                                             starsAvg = new TableCell(),
                                             webArea = new TableCell(),
                                             comment = new TableCell(),
                                             action = new TableCell())
                            {
                                using (HtmlGenericControl btnExpand = new HtmlGenericControl("a"))
                                {
                                    // Code for the read button
                                    btnExpand.Attributes.Add("class", "btn btn-default btn-xs");
                                    btnExpand.Attributes.Add("data-toggle", "tooltip");
                                    btnExpand.Attributes.Add("data-id", fb.Id.ToString());
                                    btnExpand.Attributes.Add("title", "View full comment");
                                    btnExpand.Attributes.Add("name", "expand");
                                    btnExpand.InnerHtml = "<span class='glyphicon glyphicon-chevron-up'></span>";

                                    // Add text to cells 
                                    date.Text = fb.FeedBackDate.ToShortDateString();
                                    starsAcc.Text = fb.FeedBackAccRating.ToString();
                                    starsApp.Text = fb.FeedBackAppRating.ToString();
                                    starsNav.Text = fb.FeedBackNavRating.ToString();
                                    starsRec.Text = fb.FeedBackRecRating.ToString();
                                    starsAvg.Text = fb.FeedBackAvg.ToString();
                                    comment.Text = fb.FeedBackComment.ToString();

                                    // Only show 25 characters of comment
                                    if (fb.FeedBackComment.Length >= 40)
                                    {
                                        comment.Text = fb.FeedBackComment.Substring(0, 39) + "...";
                                        action.Controls.Add(btnExpand);
                                    }
                                    else if (fb.FeedBackComment == "") { comment.Text = "No comment"; }
                                    else { comment.Text = fb.FeedBackComment.ToString(); }

                                    // Try parse to string
                                    try { webArea.Text = fb.FeedBackArea.ToString(); }
                                    catch { webArea.Text = fb.FeedBackArea; }

                                    // Add cells to rows  
                                    tr.Cells.Add(date);
                                    tr.Cells.Add(starsAcc);
                                    tr.Cells.Add(starsApp);
                                    tr.Cells.Add(starsNav);
                                    tr.Cells.Add(starsRec);
                                    tr.Cells.Add(starsAvg);
                                    tr.Cells.Add(webArea);
                                    tr.Cells.Add(comment);
                                    tr.Cells.Add(action);

                                    // Add row to table
                                    table.Rows.Add(tr);
                                }
                            }
                        }
                    }
                    return table;
                }
                else
                {
                    // Create a blank row with no results text
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell td = new TableCell())
                        {
                            using (Label message = new Label())
                            {
                                message.Text = "<b>No Results Found.</b>";

                                // Add row, cell and message to table
                                td.Controls.Add(message);
                                tr.Controls.Add(td);
                                table.Controls.Add(tr);
                                td.BorderStyle = BorderStyle.None;
                            }
                        }
                    }
                    return table;
                }
            }
        }

        public static void BuildStepGoalsTable(Table table, string userId)
        {
            // Clear the table before rebuilds
            table.Rows.Clear();

            // Get all step goals for this user
            var stepGoals = db.StepGoals.Where(s => s.UserId == userId);

            if (stepGoals.Any())
            {
                // Decalre in using so we can dispose of the object after its done being used
                using (TableHeaderRow th = new TableHeaderRow())
                {
                    th.TableSection = TableRowSection.TableHeader;
                    string[] headerArray = { "#", "Goal", "Date Range", "Action" };
                    for (int i = 0; i < headerArray.Length; i++)
                    {
                        using (TableHeaderCell cell = new TableHeaderCell())
                        {
                            cell.Font.Bold = true;
                            cell.Text = headerArray[i];
                            th.Cells.Add(cell);
                        }

                    }
                    // Add table header to the table
                    table.Rows.Add(th);
                }

                // Set a placeholder counter
                int index = 1;

                foreach (var s in stepGoals)
                {
                    // Construct table row
                    using (TableRow tr = new TableRow())
                    {
                        // Construct Table cells
                        using (TableCell number = new TableCell(),
                        goal = new TableCell(),
                        date = new TableCell(),
                        action = new TableCell())
                        {
                            // Construct link buttons
                            using (LinkButton delete = new LinkButton())
                            {

                                // Code for the remove button
                                delete.CssClass = "btn btn-default btn-xs";
                                delete.Attributes.Add("data-toggle", "tooltip");
                                delete.Attributes.Add("data-id", s.Id.ToString());
                                delete.Attributes.Add("title", "Delete");
                                delete.Attributes.Add("name", "delete-step-goal");
                                delete.Attributes.Add("runat", "server");
                                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                                action.Controls.Add(delete);

                                // Add text to cells
                                number.Text = Convert.ToString(index++);
                                goal.Text = s.GoalSteps.ToString();
                                date.Text = Convert.ToDateTime(s.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(s.GoalEndDate).ToShortDateString();

                                // Ad cells to rows
                                tr.Cells.Add(number);
                                tr.Cells.Add(goal);
                                tr.Cells.Add(date);
                                tr.Cells.Add(action);

                                // Add row to table
                                table.Rows.Add(tr);
                            }
                        }
                    }
                }
            }
            else
            {
                // Create a blank row with no results text
                using (TableRow tr = new TableRow())
                {
                    using (TableCell td = new TableCell())
                    {
                        using (Label message = new Label())
                        {
                            message.Text = "<b>No Step Goals found.</b>";

                            // Add row, cell and message to table
                            td.Controls.Add(new HtmlGenericControl("br"));
                            td.Controls.Add(message);
                            tr.Controls.Add(td);
                            td.BorderStyle = BorderStyle.None;
                            table.Controls.Add(tr);
                            table.Rows.Add(tr);
                        }
                    }
                }
            }
        }

        public static void BuildDistanceGoalsTable(Table table, string userId)
        {
            // Clear the table before rebuilds
            table.Rows.Clear();

            // Get distance goals for this user
            var distanceGoals = db.DistanceGoals.Where(s => s.UserId == userId);

            if (distanceGoals.Any())
            {
                // Decalre in using so we can dispose of the object after its done being used
                using (TableHeaderRow th = new TableHeaderRow())
                {
                    th.TableSection = TableRowSection.TableHeader;
                    string[] headerArray = { "#", "Goal", "Date Range", "Action" };
                    for (int i = 0; i < headerArray.Length; i++)
                    {
                        using (TableHeaderCell cell = new TableHeaderCell())
                        {
                            cell.Font.Bold = true;
                            cell.Text = headerArray[i];
                            th.Cells.Add(cell);
                        }

                    }
                    // Add table header to the table
                    table.Rows.Add(th);
                }

                // Set a placeholder counter
                int index = 1;
                foreach (var s in distanceGoals)
                {
                    // Construct table row
                    using (TableRow tr = new TableRow())
                    {
                        // Construct Table cells
                        using (TableCell number = new TableCell(),
                        goal = new TableCell(),
                        date = new TableCell(),
                        action = new TableCell())
                        {
                            // Construct link buttons
                            using (LinkButton delete = new LinkButton())
                            {

                                // Code for the remove button
                                delete.CssClass = "btn btn-default btn-xs";
                                delete.Attributes.Add("data-toggle", "tooltip");
                                delete.Attributes.Add("data-id", s.Id.ToString());
                                delete.Attributes.Add("title", "Delete");
                                delete.Attributes.Add("name", "delete-distance-goal");
                                delete.Attributes.Add("runat", "server");
                                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                                action.Controls.Add(delete);

                                // Add text to cells
                                number.Text = Convert.ToString(index++);
                                goal.Text = Math.Round(s.GoalDistance, 2) + " km";
                                date.Text = Convert.ToDateTime(s.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(s.GoalEndDate).ToShortDateString();

                                // Ad cells to rows
                                tr.Cells.Add(number);
                                tr.Cells.Add(goal);
                                tr.Cells.Add(date);
                                tr.Cells.Add(action);

                                // Add row to table
                                table.Rows.Add(tr);
                            }
                        }
                    }
                }
            }
            else
            {
                // Create a blank row with no results text
                using (TableRow tr = new TableRow())
                {
                    using (TableCell td = new TableCell())
                    {
                        using (Label message = new Label())
                        {
                            message.Text = "<b>No Distance Goals found.</b>";

                            // Add row, cell and message to table
                            td.Controls.Add(new HtmlGenericControl("br"));
                            td.Controls.Add(message);
                            tr.Controls.Add(td);
                            td.BorderStyle = BorderStyle.None;
                            table.Controls.Add(tr);
                            table.Rows.Add(tr);
                        }
                    }
                }
            }
        }

        public static void BuildMinuteGoalsTable(Table table, string userId)
        {
            // Clear the table before rebuilds
            table.Rows.Clear();

            // Get all minute goals for this user
            var minuteGoals = db.MinuteGoals.Where(s => s.UserId == userId);

            if (minuteGoals.Any())
            {
                // Decalre in using so we can dispose of the object after its done being used
                using (TableHeaderRow th = new TableHeaderRow())
                {
                    th.TableSection = TableRowSection.TableHeader;
                    string[] headerArray = { "#", "Goal", "Date Range", "Action" };
                    for (int i = 0; i < headerArray.Length; i++)
                    {
                        using (TableHeaderCell cell = new TableHeaderCell())
                        {
                            cell.Font.Bold = true;
                            cell.Text = headerArray[i];
                            th.Cells.Add(cell);
                        }

                    }
                    // Add table header to the table
                    table.Rows.Add(th);
                }

                // Set a placeholder counter
                int index = 1;

                foreach (var s in minuteGoals)
                {
                    // Construct table row
                    using (TableRow tr = new TableRow())
                    {
                        // Construct Table cells
                        using (TableCell number = new TableCell(),
                        goal = new TableCell(),
                        date = new TableCell(),
                        action = new TableCell())
                        {
                            // Construct link buttons
                            using (LinkButton delete = new LinkButton())
                            {

                                // Code for the remove button
                                delete.CssClass = "btn btn-default btn-xs";
                                delete.Attributes.Add("data-toggle", "tooltip");
                                delete.Attributes.Add("data-id", s.Id.ToString());
                                delete.Attributes.Add("title", "Delete");
                                delete.Attributes.Add("name", "delete-minute-goal");
                                delete.Attributes.Add("runat", "server");
                                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                                action.Controls.Add(delete);

                                // Add text to cells
                                number.Text = Convert.ToString(index++);
                                goal.Text = Math.Round(s.GoalMinute, 2) + " min";
                                date.Text = Convert.ToDateTime(s.GoalStartDate).ToShortDateString() + " - " + Convert.ToDateTime(s.GoalEndDate).ToShortDateString();

                                // Ad cells to rows
                                tr.Cells.Add(number);
                                tr.Cells.Add(goal);
                                tr.Cells.Add(date);
                                tr.Cells.Add(action);

                                // Add row to table
                                table.Rows.Add(tr);
                            }
                        }
                    }
                }
            }
            else
            {
                // Create a blank row with no results text
                using (TableRow tr = new TableRow())
                {
                    using (TableCell td = new TableCell())
                    {
                        using (Label message = new Label())
                        {
                            message.Text = "<b>No Minute Goals found.</b>";

                            // Add row, cell and message to table
                            td.Controls.Add(new HtmlGenericControl("br"));
                            td.Controls.Add(message);
                            tr.Controls.Add(td);
                            td.BorderStyle = BorderStyle.None;
                            table.Controls.Add(tr);
                            table.Rows.Add(tr);
                        }
                    }
                }
            }

        }

        public static void BuildUserTrackingTable(Table table, List<int> activitys)
        {
            // Clear the table before rebuilds
            table.Rows.Clear();

            List<ActivityLog> activityLogs = new List<ActivityLog>();

            if (activitys.Count == 0)
            {
                db.ActivityLogs.Select(a => a).ToList();
            }
            else
            {
                for (int i = 0; i < activitys.Count; i++)
                {
                    int id = activitys[i];
                    var listLogs = db.ActivityLogs.Where(act => act.UserActionID == id).ToList();
                    activityLogs.AddRange(listLogs);
                }
            }

            if (activityLogs.Any())
            {
                // Declare in using so we can dispose of the object after its done being used
                using (TableHeaderRow th = new TableHeaderRow())
                {
                    th.TableSection = TableRowSection.TableHeader;
                    string[] headerArray = { "#", "Action", "Date" };
                    for (int i = 0; i < headerArray.Length; i++)
                    {
                        using (TableHeaderCell cell = new TableHeaderCell())
                        {
                            cell.Font.Bold = true;
                            cell.Text = headerArray[i];
                            th.Cells.Add(cell);
                        }

                    }
                    // Add table header to the table
                    table.Rows.Add(th);
                }

                // Set a placeholder counter
                int index = 1;

                foreach (var a in activityLogs)
                {
                    // Construct table row
                    using (TableRow tr = new TableRow())
                    {
                        // Construct Table cells
                        using (TableCell number = new TableCell(),
                        action = new TableCell(),
                        date = new TableCell())
                        {
                            // Construct link buttons
                            using (LinkButton delete = new LinkButton())
                            {

                                // Add text to cells
                                number.Text = Convert.ToString(index++);
                                action.Text = a.PageAccessed.ToString();
                                date.Text = Convert.ToDateTime(a.DateAccessed).ToString();

                                // Ad cells to rows
                                tr.Cells.Add(number);
                                tr.Cells.Add(action);
                                tr.Cells.Add(date);

                                // Add row to table
                                table.Rows.Add(tr);
                            }
                        }
                    }
                }
            }
            else
            {
                // Create a blank row with no results text
                using (TableRow tr = new TableRow())
                {
                    using (TableCell td = new TableCell())
                    {
                        using (Label message = new Label())
                        {
                            message.Text = "<b>No Results Found.</b>";

                            // Add row, cell and message to table
                            td.Controls.Add(new HtmlGenericControl("br"));
                            td.Controls.Add(message);
                            tr.Controls.Add(td);
                            td.BorderStyle = BorderStyle.None;
                            table.Controls.Add(tr);
                            table.Rows.Add(tr);
                        }
                    }
                }
            }
        }

        public static Table BuildFilteredNotificationLogTable(string[] filters, Table table)
        {
            // Clear the table to avoid duplicate table rows
            table.Rows.Clear();

            using (table)
            {
                string priority = filters[0];
                string user = filters[1];
                string start = filters[2];
                string end = filters[3];

                // Query DB
                var notiQuery =
                    from noti in db.Notifications
                    join unet in db.AspNetUsers on noti.UserId equals unet.Id
                    select noti;

                // Build Where Clause for query
                if (filters[4] != "")
                {
                    bool isread = Convert.ToBoolean(filters[4]);
                    notiQuery = notiQuery.Where(r => r.IsRead == isread);
                }

                if (priority != "") { notiQuery = notiQuery.Where(p => p.Priority == priority); }
                if (user != "") { notiQuery = notiQuery.Where(u => u.AspNetUser.UserName == user); }

                if (start != "" && end != "")
                {
                    DateTime startDate = Convert.ToDateTime(start);
                    DateTime endDate = Convert.ToDateTime(end);
                    notiQuery = notiQuery.Where(d => d.NotificationDate >= startDate && d.NotificationDate <= endDate);
                }
                else if (start != "" && end == "")
                {
                    DateTime startDate = Convert.ToDateTime(start);
                    notiQuery = notiQuery.Where(d => d.NotificationDate >= startDate);
                }
                else if (start == "" && end != "")
                {
                    DateTime endDate = Convert.ToDateTime(end);
                    notiQuery = notiQuery.Where(d => d.NotificationDate <= endDate);
                }

                if (notiQuery.Any())
                {
                    // Build the table headers and add them to the table
                    using (TableHeaderRow th = new TableHeaderRow())
                    {
                        th.TableSection = TableRowSection.TableHeader;
                        string[] headerArray = { "Priority", "Read", "Title", "Description", "User", "Date" };
                        for (int i = 0; i < headerArray.Length; i++)
                        {
                            using (TableHeaderCell cell = new TableHeaderCell())
                            {
                                cell.Font.Bold = true;
                                cell.Text = headerArray[i];
                                th.Cells.Add(cell);
                            }
                        }

                        // Add table header to the table
                        table.Rows.Add(th);
                    }

                    // Create table with data
                    foreach (var noti in notiQuery)
                    {
                        using (TableRow tr = new TableRow())
                        {
                            using (TableCell prior = new TableCell(),
                                            title = new TableCell(),
                                            description = new TableCell(),
                                            date = new TableCell(),
                                            username = new TableCell(),
                                            read = new TableCell())
                            {
                                // Set the widths of the cells
                                title.Width = 150;
                                description.Width = 300;

                                // Add text to cells                    
                                title.Text = noti.Title;
                                description.Text = noti.Description;
                                date.Text = Convert.ToDateTime(noti.NotificationDate).ToShortDateString();
                                prior.Text = noti.Priority;
                                read.Text = noti.IsRead.ToString();
                                username.Text = noti.AspNetUser.UserName;

                                // Check priority level and apply appropriate class
                                switch (noti.Priority)
                                {
                                    case "High":
                                        prior.CssClass = "text-danger";
                                        break;
                                    case "Low":
                                        prior.CssClass = "text-success";
                                        break;
                                    case "Normal":
                                        prior.CssClass = "text-warning";
                                        break;
                                    default:
                                        prior.CssClass = "text-primary";
                                        break;
                                }

                                // Add cells to rows                    
                                tr.Cells.Add(prior);
                                tr.Cells.Add(read);
                                tr.Cells.Add(title);
                                tr.Cells.Add(description);
                                tr.Cells.Add(username);
                                tr.Cells.Add(date);

                                // Add row to table
                                table.Rows.Add(tr);
                            }
                        }
                    }
                    return table;
                }
                else
                {
                    // Create a blank row with no results text
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell td = new TableCell())
                        {
                            using (Label message = new Label())
                            {
                                message.Text = "<b>No notifications to show.</b>";

                                // Add row, cell and message to table
                                td.Controls.Add(message);
                                tr.Controls.Add(td);
                                table.Controls.Add(tr);
                                td.BorderStyle = BorderStyle.None;
                            }
                        }
                    }
                    return table;
                }
            }
        }

        public static Table BuildNotificationLogTable(Table table)
        {
            // Clear the table to avoid duplicate table rows
            table.Rows.Clear();

            using (table)
            {
                // Query DB
                var notiQuery =
                    from noti in db.Notifications
                    join unet in db.AspNetUsers on noti.UserId equals unet.Id
                    select noti;

                if (notiQuery.Any())
                {
                    // Build the table headers and add them to the table
                    using (TableHeaderRow th = new TableHeaderRow())
                    {
                        th.TableSection = TableRowSection.TableHeader;
                        string[] headerArray = { "Priority", "Read", "Title", "Description", "User", "Date" };
                        for (int i = 0; i < headerArray.Length; i++)
                        {
                            using (TableHeaderCell cell = new TableHeaderCell())
                            {
                                cell.Font.Bold = true;
                                cell.Text = headerArray[i];
                                th.Cells.Add(cell);
                            }
                        }

                        // Add table header to the table
                        table.Rows.Add(th);
                    }

                    // Create table with data
                    foreach (var noti in notiQuery)
                    {
                        using (TableRow tr = new TableRow())
                        {
                            using (TableCell priority = new TableCell(),
                                            title = new TableCell(),
                                            description = new TableCell(),
                                            date = new TableCell(),
                                            user = new TableCell(),
                                            read = new TableCell())
                            {
                                // Set the widths of the cells
                                title.Width = 150;
                                description.Width = 300;

                                // Add text to cells                    
                                title.Text = noti.Title;
                                description.Text = noti.Description;
                                date.Text = Convert.ToDateTime(noti.NotificationDate).ToShortDateString();
                                priority.Text = noti.Priority;
                                read.Text = noti.IsRead.ToString();
                                user.Text = noti.AspNetUser.UserName;

                                // Check priority level and apply appropriate class
                                switch (noti.Priority)
                                {
                                    case "High":
                                        priority.CssClass = "text-danger";
                                        break;
                                    case "Low":
                                        priority.CssClass = "text-success";
                                        break;
                                    case "Normal":
                                        priority.CssClass = "text-warning";
                                        break;
                                    default:
                                        priority.CssClass = "text-primary";
                                        break;
                                }

                                // Add cells to rows                    
                                tr.Cells.Add(priority);
                                tr.Cells.Add(read);
                                tr.Cells.Add(title);
                                tr.Cells.Add(description);
                                tr.Cells.Add(user);
                                tr.Cells.Add(date);

                                // Add row to table
                                table.Rows.Add(tr);
                            }
                        }
                    }
                    return table;
                }
                else
                {
                    // Create a blank row with no results text
                    using (TableRow tr = new TableRow())
                    {
                        using (TableCell td = new TableCell())
                        {
                            using (Label message = new Label())
                            {
                                message.Text = "<b>No notifications to show.</b>";

                                // Add row, cell and message to table
                                td.Controls.Add(message);
                                tr.Controls.Add(td);
                                table.Controls.Add(tr);
                                td.BorderStyle = BorderStyle.None;
                            }
                        }
                    }
                    return table;
                }
            }
        }
    }
}
