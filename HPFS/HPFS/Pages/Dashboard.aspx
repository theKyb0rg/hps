<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HPFS.Dashboard"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Dashboard</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <%--<script type="text/javascript">
        $(function () {
            $("#sortable").sortable();
            $("#sortable").disableSelection();
        });
    </script>--%>
    <h1 class="text-center">Dashboard</h1>
    <h3 id="welcome" class="text-center" runat="server"></h3>
    <hr />
    <div class="row">
        <%--ACTIVITY TRACKER--%>
        <asp:Panel ID="pnlActivityTracker" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="ActivityTracker.aspx">
                            <img src="/Content/Images/ActivityTrackerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <%--BUDGET MANAGER--%>
        <asp:Panel ID="pnlBudgetManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="BudgetPlanner.aspx">
                            <img src="/Content/Images/BudgetManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--EVENT MANAGER--%>
        <asp:Panel ID="pnlEventManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="EventManager.aspx">
                            <img src="/Content/Images/EventManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--FEEDBACK MANAGER--%>
        <asp:Panel ID="pnlFeedbackManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="FeedbackManager.aspx">
                            <img src="/Content/Images/FeedbackManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--FITBIT MANAGER--%>
        <asp:Panel ID="pnlFitBitManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="FitBitManagerAuthentication.aspx">
                            <img src="/Content/Images/FitbitManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--FITBIT MONITOR--%>
        <asp:Panel ID="pnlFitBitMonitor" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="FitBitMonitor.aspx">
                            <img src="/Content/Images/FitbitMonitorBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--FILE MANAGER--%>
        <asp:Panel ID="pnlFileManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="FileManager.aspx">
                            <img src="/Content/Images/FileManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--PROGRAM MANAGER--%>
        <asp:Panel ID="pnlProgramsManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="ProgramManager.aspx">
                            <img src="/Content/Images/ProgramsManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--SLIDE SHOW MANAGER--%>
        <asp:Panel ID="pnlSlideShowManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="SlideShowManager.aspx">
                            <img src="/Content/Images/SlideShowManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%--USER MANAGER--%>
        <asp:Panel ID="pnlUserManager" runat="server">
            <div class="col-xs-12 col-sm-6 darker">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <a href="UserManager.aspx">
                            <img src="/Content/Images/UserManagerBanner.jpg" alt="" class="img-responsive center-block" />
                        </a>
                    </div>
                </div>
            </div>
        </asp:Panel>
        

        


    </div>
</asp:Content>
