<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="FitBitMonitor.aspx.cs" Inherits="HPFS.Pages.FitBitMonitor" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="insideHead" runat="server">
    <link href="../Content/css/pages/FitBitMonitor.min.css" rel="stylesheet" />
    <script src="../Scripts/FitBitMonitor.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <%--<div class="row">
        <h1 class="text-center">All Users Overview</h1>
        <hr />
        <div class="col-xs-12">--%>
    <%--NAV TABS--%>
    <%--<ul class="nav nav-tabs nav-justified" id="FitBitNavTabsAll">
                <li class="active"><a data-toggle="tab" href="#OverviewAll">Overview</a></li>
                <li><a data-toggle="tab" href="#DistanceAll">Distance</a></li>
                <li><a data-toggle="tab" href="#StepsAll">Steps</a></li>
                <li><a data-toggle="tab" href="#TimeAll">Minutes</a></li>
            </ul>
            <div class="tab-content">--%>
    <%--OVERVIEW TAB--%>
    <%--<div id="OverviewAll" class="tab-pane fade in active">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="col-xs-12 col-sm-4">
                                <h2 class="text-center">Distance Data</h2>
                                <hr />
                                <h3 class="text-center">Total Distance</h3>
                                <h4 class="text-center" id="allOverviewTotalDistance" runat="server"></h4>
                                <h3 class="text-center">Average Distance Per Day</h3>
                                <h4 class="text-center" id="allOverviewAverageDistance" runat="server"></h4>
                                <h3 class="text-center">Personal Record</h3>
                                <h4 class="text-center" id="allOverviewRecordDistance" runat="server"></h4>
                            </div>
                            <div class="col-xs-12 col-sm-4">
                                <h2 class="text-center">Step Data</h2>
                                <hr />
                                <h3 class="text-center">Total Steps</h3>
                                <h4 class="text-center" id="allOverviewTotalSteps" runat="server"></h4>
                                <h3 class="text-center">Average Steps Per Day</h3>
                                <h4 class="text-center" id="allOverviewAverageSteps" runat="server"></h4>
                                <h3 class="text-center">Personal Record</h3>
                                <h4 class="text-center" id="allOverviewRecordSteps" runat="server"></h4>
                            </div>
                            <div class="col-xs-12 col-sm-4">
                                <h2 class="text-center">Time Data</h2>
                                <hr />
                                <h3 class="text-center">Total Time</h3>
                                <h4 class="text-center" id="allOverviewTotalTime" runat="server"></h4>
                                <h3 class="text-center">Average Time Per Day</h3>
                                <h4 class="text-center" id="allOverviewAverageTime" runat="server"></h4>
                                <h3 class="text-center">Personal Record</h3>
                                <h4 class="text-center" id="allOverviewRecordTime" runat="server"></h4>
                            </div>
                        </div>
                    </div>
                </div>--%>
    <%--DISTANCE TAB ALL--%>
    <%--<div id="DistanceAll" class="tab-pane fade">
                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="text-center" id="overViewDistancesHeadingAll" runat="server">Distance in Last X Days (km)</h2>
                            <hr />
                            <div id="overViewDistanceChart" style="border: solid 1px black;"></div>
                        </div>
                    </div>
                </div>--%>
    <%--STEPS TAB ALL--%>
    <%--<div id="StepsAll" class="tab-pane fade">
                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="text-center" id="overViewStepsHeadingAll" runat="server">Steps in Last X Days</h2>
                            <hr />
                            <div id="overViewStepChart" style="border: solid 1px black;"></div>
                        </div>
                    </div>
                </div>--%>
    <%--MINUTES TAB ALL--%>
    <%--<div id="TimeAll" class="tab-pane fade">
                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="text-center" id="overViewMinutesHeadingAll" runat="server">Minutes in Last X Days</h2>
                            <hr />
                            <div id="overViewMinuteChart" style="border: solid 1px black;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <div class="row">
        <h1 class="text-center">Monitor User FitBit Data</h1>
        <hr />
        <div class="col-xs-12 col-sm-4"></div>
        <div class="col-xs-12 col-sm-4">
            <div class="form-group text-center">
                <asp:Label ID="lblUsers" runat="server" Text="Select a User:"></asp:Label>
                <asp:DropDownList ID="ddlUsers" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="lbUsers_SelectedIndexChanged" EnableViewState="true" AppendDataBoundItems="true"></asp:DropDownList>
            </div>
        </div>
        <div class="col-xs-12 col-sm-4"></div>
    </div>
    <%--<asp:Panel ID="pnlFitBitData" runat="server" Visible="false">--%>
    <div class="row">
        <hr />
        <div class="col-xs-12 ">
            <div class="col-xs-12 ">
                <%--NAV TABS--%>
                <ul class="nav nav-tabs nav-justified" id="FitBitNavTabs">
                    <li class="active"><a data-toggle="tab" href="#Overview">Overview</a></li>
                    <li><a data-toggle="tab" href="#Distance">Distance</a></li>
                    <li><a data-toggle="tab" href="#Steps">Steps</a></li>
                    <li><a data-toggle="tab" href="#Time">Minutes</a></li>
                </ul>
                <div class="tab-content">
                    <%--<%--<%--OVERVIEW TAB--%>
                    <div id="Overview" class="tab-pane fade in active">
                        <div class="row folder outerFolder" id="rowWidth">
                            <div class="col-xs-12 col-sm-4 imgResize" id="distancesImage" style="cursor: pointer;">
                                <img src="../Content/images/MonitorPanelDistance.jpg" style="border-radius: 20px;" />
                                <%--<h2 class="text-center">Distance Data</h2>
                                <hr />
                                <h3 class="text-center">Total Distance</h3>
                                <h4 class="text-center" id="userOverviewTotalDistance" runat="server"></h4>
                                <h3 class="text-center">Average Distance Per Day</h3>
                                <h4 class="text-center" id="userOverviewAverageDistance" runat="server"></h4>
                                <h3 class="text-center">Personal Record</h3>
                                <h4 class="text-center" id="userOverviewRecordDistance" runat="server"></h4>--%>
                            </div>
                            <div class="col-xs-12 col-sm-8" style="display: none;" id="distancesInfo">
                                <ul class="nav nav-tabs nav-justified" id="distanceInfoTabs">
                                    <li class="active"><a data-toggle="tab" href="#DistanceStatisticsTab">Statistics</a></li>
                                    <li><a data-toggle="tab" href="#DistanceGoalsTab">Goals</a></li>
                                </ul>
                                <div class="tab-content ">
                                    <div id="DistanceStatisticsTab" class="tab-pane fade in active">
                                        <div class="col-xs-12 folder innerFolder">
                                            <h3 class="text-center">Statistics</h3>
                                            <asp:Label ID="lblDistancesUserValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            <hr />
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblDistancesMaxHeading" class="text-center" runat="server">Max. Recorded Distance in a Day</h4>
                                                <asp:Label ID="lblDistancesMaxValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblDistancesMinHeading" class="text-center" runat="server">Min. Recorded Distance in a Day</h4>
                                                <asp:Label ID="lblDistancesMinValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblDistancesSumHeading" class="text-center" runat="server">Total Distance Overall</h4>
                                                <asp:Label ID="lblDistancesSumValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblDistancesAvgHeading" class="text-center" runat="server">Average Distance Overall</h4>
                                                <asp:Label ID="lblDistancesAvgValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblDistancesTotalRecordsHeading" class="text-center" runat="server">Total Amount of Entries</h4>
                                                <asp:Label ID="lblDistancesTotalRecordsValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblDistancesDateRangeHeading" class="text-center" runat="server">Date Range</h4>
                                                <asp:Label ID="lblDistancesDateRangeEntryValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DistanceGoalsTab" class="tab-pane fade">
                                        <div class="col-xs-12 folder">
                                            <h3 class="text-center">Goals</h3>
                                            <div class="table-style">
                                                <asp:Table ID="tblDistanceGoals" CssClass="table table-condensed sortable" runat="server">
                                                    <asp:TableHeaderRow TableSection="TableHeader">
                                                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>User</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>Goal Date</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>Goal Value</asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                </asp:Table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 imgResize" id="stepsImage" style="cursor: pointer;">
                                <img src="../Content/images/MonitorPanelSteps.jpg" style="border-radius: 20px;" />
                                <%--<h2 class="text-center">Step Data</h2>
                                <hr />
                                <h3 class="text-center">Total Steps</h3>
                                <h4 class="text-center" id="userOverviewTotalSteps" runat="server"></h4>
                                <h3 class="text-center">Average Steps Per Day</h3>
                                <h4 class="text-center" id="userOverviewAverageSteps" runat="server"></h4>
                                <h3 class="text-center">Personal Record</h3>
                                <h4 class="text-center" id="userOverviewRecordSteps" runat="server"></h4>--%>
                            </div>
                            <div class="col-xs-12 col-sm-8" style="display: none;" id="stepsInfo">
                                <%--NAV TABS--%>
                                <ul class="nav nav-tabs nav-justified" id="stepsInfoTabs">
                                    <li class="active"><a data-toggle="tab" href="#StepStatisticsTab">Statistics</a></li>
                                    <li><a data-toggle="tab" href="#StepGoalsTab">Goals</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div id="StepStatisticsTab" class="tab-pane fade in active">
                                        <div class="col-xs-12 folder innerFolder">
                                            <h3 class="text-center">Statistics</h3>
                                            <asp:Label ID="lblStepsUserValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            <hr />
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblStepsMaxHeading" class="text-center" runat="server">Max. Recorded Steps in a Day</h4>
                                                <asp:Label ID="lblStepsMaxValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblStepsMinHeading" class="text-center" runat="server">Min. Recorded Steps in a Day</h4>
                                                <asp:Label ID="lblStepsMinValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblStepsSumHeading" class="text-center" runat="server">Total Steps Overall</h4>
                                                <asp:Label ID="lblStepsSumValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblStepsAvgHeading" class="text-center" runat="server">Average Steps Overall</h4>
                                                <asp:Label ID="lblStepsAvgValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblStepsTotalRecordsHeading" class="text-center" runat="server">Total Amount of Entries</h4>
                                                <asp:Label ID="lblStepsTotalRecordsValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblStepsDateRangeHeading" class="text-center" runat="server">Date Range</h4>
                                                <asp:Label ID="lblStepsDateRangeValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="StepGoalsTab" class="tab-pane fade">
                                        <div class="col-xs-12 folder">
                                            <h3 class="text-center">Goals</h3>
                                            <div class="table-style">
                                                <asp:Table ID="tblStepGoals" CssClass="table table-condensed sortable" runat="server">
                                                    <asp:TableHeaderRow TableSection="TableHeader">
                                                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>User</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>Goal Date</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>Goal Value</asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                </asp:Table>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="col-xs-12 col-sm-4 imgResize" id="minutesImage" style="cursor: pointer;">
                                <img src="../Content/images/MonitorPanelMinutes.jpg" style="border-radius: 20px;" />
                                <%--<h2 class="text-center">Minutes Data</h2>
                                <hr />
                                <h3 class="text-center">Total Minutes</h3>
                                <h4 class="text-center" id="userOverviewTotalTime" runat="server"></h4>
                                <h3 class="text-center">Average Minutes Per Day</h3>
                                <h4 class="text-center" id="userOverviewAverageTime" runat="server"></h4>
                                <h3 class="text-center">Personal Record</h3>
                                <h4 class="text-center" id="userOverviewRecordTime" runat="server"></h4>--%>
                            </div>
                            <div class="col-xs-12 col-sm-8" style="display: none;" id="minutesInfo">
                                <ul class="nav nav-tabs nav-justified" id="minutesInfoTabs">
                                    <li class="active"><a data-toggle="tab" href="#MinuteStatisticsTab">Statistics</a></li>
                                    <li><a data-toggle="tab" href="#MinuteGoalsTab">Goals</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div id="MinuteStatisticsTab" class="tab-pane fade in active">
                                        <div class="col-xs-12 folder innerFolder">
                                            <h3 class="text-center">Statistics</h3>
                                            <asp:Label ID="lblMinutesUserValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            <hr />
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblMinutesMaxHeading" class="text-center" runat="server">Max. Recorded Minutes in a Day</h4>
                                                <asp:Label ID="lblMinutesMaxValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblMinutesMinHeading" class="text-center" runat="server">Min. Recorded Minutes in a Day</h4>
                                                <asp:Label ID="lblMinutesMinValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblMinutesSumHeading" class="text-center" runat="server">Total Minutes Overall</h4>
                                                <asp:Label ID="lblMinutesSumValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblMinutesAvgHeading" class="text-center" runat="server">Average Minutes Overall</h4>
                                                <asp:Label ID="lblMinutesAvgValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblMinutesTotalRecordsHeading" class="text-center" runat="server">Total Amount of Entries</h4>
                                                <asp:Label ID="lblMinutesTotalRecordsValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <h4 id="lblMinutesDateRangeHeading" class="text-center" runat="server">Date Range</h4>
                                                <asp:Label ID="lblMinutesDateRangeValue" CssClass="makeBlock" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="MinuteGoalsTab" class="tab-pane fade">
                                        <div class="col-xs-12 folder innerFolder">
                                            <h3 class="text-center">Goals</h3>
                                            <div class="table-style">
                                                <asp:Table ID="tblMinuteGoals" CssClass="table table-condensed sortable" runat="server">
                                                    <asp:TableHeaderRow TableSection="TableHeader">
                                                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>User</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>Goal Date</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>Goal Value</asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                </asp:Table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                    <%--DISTANCE TAB--%>
                    <div id="Distance" class="tab-pane fade">
                        <div class="row folder outerFolder">
                            <div class="col-xs-12">
                                <h2 class="text-center" id="userDistancesHeading" runat="server">Distance in Last X Days (km)</h2>
                                <hr />
                                <div id="userDistancesChart"></div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-primary btn-block form-control button-border shadow-up-bro" data-id="userDistancesChart" name="bar">Bar Graph</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userDistancesChart" name="line">Line Graph</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userDistancesChart" name="scatter">Scatter Plot</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userDistancesChart" name="spline">Spline Graph</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--STEPS TAB--%>
                    <div id="Steps" class="tab-pane fade">
                        <div class="row folder outerFolder">
                            <div class="col-xs-12">
                                <h2 class="text-center" id="userStepsHeading" runat="server">Steps in Last X Days (km)</h2>
                                <hr />
                                <div id="userStepsChart"></div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-primary btn-block form-control button-border shadow-up-bro" data-id="userStepsChart" name="bar">Bar Graph</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userStepsChart" name="line">Line Graph</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userStepsChart" name="scatter">Scatter Plot</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userStepsChart" name="spline">Spline Graph</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--MINUTES TAB--%>
                    <div id="Time" class="tab-pane fade">
                        <div class="row folder outerFolder">
                            <div class="col-xs-12">
                                <h2 class="text-center" id="userMinutesHeading" runat="server">Minutes in Last X Days (km)</h2>
                                <hr />
                                <div id="userMinutesChart"></div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-primary btn-block form-control button-border shadow-up-bro" data-id="userMinutesChart" name="bar">Bar Graph</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userMinutesChart" name="line">Line Graph</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userMinutesChart" name="scatter">Scatter Plot</a>
                                </div>
                                <div class="col-xs-12 col-sm-3" style="padding: 0;">
                                    <a href="#" class="btn btn-default btn-block form-control button-border shadow-up-bro" data-id="userMinutesChart" name="spline">Spline Graph</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--</asp:Panel>--%>
    </div>
    <div class="row">
        <div class="col-xs-12 text-center">
            <a class="btn btn-default form-control shadow-up-bro" id="distancesBackButton" style="display: none;">Back</a>
            <a class="btn btn-default form-control shadow-up-bro" id="stepsBackButton" style="display: none;">Back</a>
            <a class="btn btn-default form-control shadow-up-bro" id="minutesBackButton" style="display: none;">Back</a>
        </div>
    </div>
</asp:Content>
