<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="FitBitManager.aspx.cs" Inherits="HPFS.FitBitManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - FitBit Manager</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <link href="/Content/css/pages/Fitbit.min.css" rel="stylesheet" />
    <script src="../Scripts/FitBitManager.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="text-center">
        <h1>FitBit Manager</h1>
        <asp:Label ID="lblCRUDMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <hr />
    <%--GOALS SECTION--%>
    <div class="row">
        <div class="col-xs-12 text-center">
            <div class="row">
                <div class="col-xs-6 col-sm-3 fourSquare">
                    <h4>Create New Goal</h4>
                    <span data-toggle="modal" data-target="#mdlGoalModal">
                        <button type="button" class="glyphLarge btn btn-success" data-toggle="tooltip" title="Create a New Goal" data-placement="top">
                            <span class="glyphicon glyphicon-plus-sign"></span>
                        </button>
                    </span>
                </div>
                <div class="col-xs-6 col-sm-3 fourSquare">
                    <h4>Remove All Goals</h4>
                    <span data-toggle="modal" data-target="#mdlRemoveAllGoals">
                        <button type="button" class="glyphLarge btn btn-danger" data-toggle="tooltip" data-placement="right" title="Remove All Goals">
                            <span class="glyphicon glyphicon-minus-sign"></span>
                        </button>
                    </span>
                </div>
                <div class="col-xs-6 col-sm-3 fourSquare">
                    <h4>View All Goals</h4>
                    <span data-toggle="modal" data-target="#mdlViewAllGoals">
                        <button type="button" class="glyphLarge btn btn-info" data-toggle="tooltip" data-placement="bottom" title="View All Goals">
                            <span class="glyphicon glyphicon-eye-open"></span>
                        </button>
                    </span>
                </div>
                <div class="col-xs-6 col-sm-3 fourSquare">
                    <h4>Help Menu</h4>
                    <span data-toggle="modal" data-target="#mdlHelpModal">
                        <button type="button" class="glyphLarge btn btn-primary" data-toggle="tooltip" data-placement="bottom" title="Help">
                            <span class="glyphicon glyphicon-info-sign"></span>
                        </button>
                    </span>
                </div>
            </div>
            <hr />
        </div>
    </div>

    <%--GRAPH SECTION--%>
    <div class="row">
        <div class="col-xs-12">
            <asp:PlaceHolder ID="plTitle" runat="server"></asp:PlaceHolder>
            <hr />
        </div>
    </div>
    <div class="row" style="border: 1px solid black; padding: 10px;">
        <div class="col-xs-12 col-sm-10">
            <div id="chart"></div>
        </div>
        <div class="col-xs-12 col-sm-2 text-center">
            <%--<h2 style="padding-top: 0; margin-top: 0; font-weight: 500; display: inline-block;">FitBit</h2>--%>
            <div class="btn-group" style="width: 100%;">
                <button type="button" class="btn btn-default dropdown-toggle form-control" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Steps&nbsp;<span class="caret"></span>
                </button>
                <ul class="dropdown-menu pull-right form-control" style="padding-top: 0;">
                    <li>
                        <button type="button" id="btnSteps7Days" class="btn btn-primary form-control" runat="server" onserverclick="btnSteps_Click" data-id="7">7 Days</button></li>
                    <li>
                        <button type="button" id="btnSteps14Days" class="btn btn-primary form-control" runat="server" onserverclick="btnSteps_Click" data-id="14">14 Days</button></li>
                    <li>
                        <button type="button" id="btnSteps28Days" class="btn btn-primary form-control" runat="server" onserverclick="btnSteps_Click" data-id="28">28 Days</button></li>
                </ul>
            </div>
            <div class="btn-group" style="width: 100%;">
                <button type="button" class="btn btn-default dropdown-toggle form-control" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Distances&nbsp;<span class="caret"></span>
                </button>
                <ul class="dropdown-menu pull-right form-control" style="padding-top: 0;">
                    <li>
                        <button type="button" id="btnDistances7Days" class="btn btn-primary form-control" runat="server" onserverclick="btnDistances_Click" data-id="7">7 Days</button></li>
                    <li>
                        <button type="button" id="btnDistances14Days" class="btn btn-primary form-control" runat="server" onserverclick="btnDistances_Click" data-id="14">14 Days</button></li>
                    <li>
                        <button type="button" id="btnDistances28Days" class="btn btn-primary form-control" runat="server" onserverclick="btnDistances_Click" data-id="28">28 Days</button></li>
                </ul>
            </div>
            <div class="btn-group" style="width: 100%;">
                <button type="button" class="btn btn-default dropdown-toggle form-control" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Minutes&nbsp;<span class="caret"></span>
                </button>
                <ul class="dropdown-menu pull-right form-control" style="padding-top: 0;">
                    <li>
                        <button type="button" id="btnMinutes7Days" class="btn btn-primary form-control" runat="server" onserverclick="btnMinutes_Click" data-id="7">7 Days</button></li>
                    <li>
                        <button type="button" id="btnMinutes14Days" class="btn btn-primary form-control" runat="server" onserverclick="btnMinutes_Click" data-id="14">14 Days</button></li>
                    <li>
                        <button type="button" id="btnMinutes28Days" class="btn btn-primary form-control" runat="server" onserverclick="btnMinutes_Click" data-id="28">28 Days</button></li>
                </ul>
            </div>
            <asp:PlaceHolder ID="plAvgHeading" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="plAvgValue" runat="server"></asp:PlaceHolder>
            <hr />
            <asp:PlaceHolder ID="plTotalsHeading" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="plTotalsValue" runat="server"></asp:PlaceHolder>
        </div>
        <div class="col-xs-12 text-center">
            <asp:Button ID="btnUploadData" runat="server" CssClass="btn btn-default" Text="Synchronize FitBit Data" OnClick="btnUploadData_Click" />
        </div>
    </div>

    <hr />
    <div class="row">
        <h1 class="text-center">Goals</h1>
        <hr />
        <div class="col-xs-12 col-sm-4">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Most Recent Step Goal</h4>
                </div>
                <div class="panel-body">
                    <table class="table table-condensed">
                        <tr>
                            <td>
                                <h5><b>Date Range</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plDateRangeSteps" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5><b>Step Goal</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plStepGoal" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5><b>Current Steps</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plCurrentStepGoal" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                    <h4 id="stepGoalStatus" runat="server"></h4>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-4">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Most Recent Distance Goal</h4>
                </div>
                <div class="panel-body">
                    <table class="table table-condensed">
                        <tr>
                            <td>
                                <h5><b>Date Range</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plDateRangeDistance" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5><b>Distance Goal</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plDistanceGoal" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5><b>Current Distance</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plCurrentDistance" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                    <h4 id="distanceGoalStatus" runat="server"></h4>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-4">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Most Recent Minutes Goal</h4>
                </div>
                <div class="panel-body">
                    <table class="table table-condensed">
                        <tr>
                            <td>
                                <h5><b>Date Range</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plDateRangeMinutes" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5><b>Minutes Goal</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plMinutesGoal" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5><b>Current Minutes</b></h5>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="plCurrentMinutesGoal" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                    <h4 id="minuteGoalStatus" runat="server"></h4>
                </div>
            </div>
        </div>
    </div>

    <%--GOAL MODAL--%>
    <div class="modal fade" id="mdlGoalModal" tabindex="-1" role="dialog" aria-labelledby="mdlGoalModal-label" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="mdlGoalModal-label">Set New Goal</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblGoal" runat="server" Text="Goal:"></asp:Label>
                                <asp:TextBox ID="txtGoal" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblGoalType" runat="server" Text="Type:"></asp:Label>
                                <asp:DropDownList ID="ddlGoalType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="-1">Select a type of Goal...</asp:ListItem>
                                    <asp:ListItem Value="Steps">Steps</asp:ListItem>
                                    <asp:ListItem Value="Distance">Distance</asp:ListItem>
                                    <asp:ListItem Value="Minutes">Minutes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblGoalStartDate" runat="server" Text="Start Date:"></asp:Label>
                                <asp:TextBox ID="txtGoalStartDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblGoalEndDate" runat="server" Text="End Date:"></asp:Label>
                                <asp:TextBox ID="txtGoalEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-xs-12 text-center">
                        <div class="form-group">
                            <asp:Button ID="btnSetNewGoal" runat="server" CssClass="btn btn-default" Text="Set New Goal" OnClick="btnSetNewGoal_Click" />
                            <br />
                            <label id="lblGoalErrorMessages" class="text-danger"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--REMOVE GOALS MODAL--%>
    <div class="modal fade" id="mdlRemoveAllGoals" tabindex="-1" role="dialog" aria-labelledby="mdlRemoveAllGoals-label" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="mdlRemoveAllGoals-label">Remove All Goals</h4>
                </div>
                <div class="modal-body text-center">
                    <h1 class="text-danger">WARNING</h1>
                    <p>By clicking the 'Confirm' button you will remove all Goals that you have set for your FitBit, are you sure you want to do this?</p>
                    <small>Note: This will not affect any of your information related to the actual FitBit website.</small>
                </div>
                <div class="modal-footer text-center">
                    <asp:LinkButton ID="btnRemoveAllGoals" CssClass="btn btn-danger" runat="server" OnClick="btnRemoveAllGoals_Click">Confirm</asp:LinkButton>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <%--VIEW ALL GOALS MODAL--%>
    <div class="modal fade" id="mdlViewAllGoals" tabindex="-1" role="dialog" aria-labelledby="mdlViewAllGoals-label" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="mdlViewAllGoals-label">View All Goals</h4>
                </div>
                <div class="modal-body">
                    <%--NAV TABS--%>
                    <ul class="nav nav-tabs nav-justified" id="RehabilitationPage_NavTabs">
                        <li class="active"><a data-toggle="tab" href="#StepGoalsTab">Step Goals</a></li>
                        <li><a data-toggle="tab" href="#DistanceGoalsTab">Distance Goals</a></li>
                        <li><a data-toggle="tab" href="#MinuteGoalsTab">Minute Goals</a></li>
                    </ul>

                    <div class="tab-content">
                        <%--IN PROGRESS TAB--%>
                        <div id="StepGoalsTab" class="tab-pane fade in active">
                            <div class="row folder">
                                <div class="col-xs-12 text-center">
                                    <asp:Table ID="tblStepGoals" CssClass="table table-condensed sortable" runat="server"></asp:Table>
                                </div>
                            </div>
                        </div>
                        <div id="DistanceGoalsTab" class="tab-pane fade">
                            <div class="row folder">
                                <div class="col-xs-12 text-center">
                                    <asp:Table ID="tblDistanceGoals" CssClass="table table-condensed sortable" runat="server"></asp:Table>
                                </div>
                            </div>
                        </div>
                         <div id="MinuteGoalsTab" class="tab-pane fade">
                            <div class="row folder">
                                <div class="col-xs-12 text-center">
                                    <asp:Table ID="tblMinuteGoals" CssClass="table table-condensed sortable" runat="server"></asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <%--HELP MODAL--%>
    <div class="modal fade" id="mdlHelpModal" tabindex="-1" role="dialog" aria-labelledby="mdlHelpModal-label" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title text-center" id="mdlHelpModal-label">Help</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <table class="table table-condensed">
                                <thead>
                                    <tr>
                                        <th class="text-center">Action</th>
                                        <th class="text-center">Description</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center">
                                            <button class="btn btn-default">Synchronize FitBit Data</button>
                                        </td>
                                        <td>
                                            <p>Clicking this button will synchronize your FitBit account with your HPS account so you can set your goals through the HPS website instead of using the complicated FitBit system.</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="btn-group" style="width: 100%;">
                                                <button type="button" class="btn btn-default dropdown-toggle form-control" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Steps&nbsp;<span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu form-control" style="padding-top: 0;">
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">7 Days</button></li>
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">14 Days</button></li>
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">28 Days</button></li>
                                                </ul>
                                            </div>
                                        </td>
                                        <td>
                                            <p>Clicking this button will allow you to display the most recent Steps from FitBit in the past 7, 14, or 28 days in the form of a bar graph.</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="btn-group" style="width: 100%;">
                                                <button type="button" class="btn btn-default dropdown-toggle form-control" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Distances&nbsp;<span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu form-control" style="padding-top: 0;">
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">7 Days</button></li>
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">14 Days</button></li>
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">28 Days</button></li>
                                                </ul>
                                            </div>
                                        </td>
                                        <td>
                                            <p>Clicking this button will allow you to display the most recent Distances from FitBit in the past 7, 14, or 28 days in the form of a bar graph.</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="btn-group" style="width: 100%;">
                                                <button type="button" class="btn btn-default dropdown-toggle form-control" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Minutes&nbsp;<span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu form-control" style="padding-top: 0;">
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">7 Days</button></li>
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">14 Days</button></li>
                                                    <li>
                                                        <button type="button" class="btn btn-primary form-control">28 Days</button></li>
                                                </ul>
                                            </div>
                                        </td>
                                        <td>
                                            <p>Clicking this button will allow you to display the most recent Minutes from FitBit in the past 7, 14, or 28 days in the form of a bar graph.</p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-xs-12 text-center">
                        <div class="form-group">
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
