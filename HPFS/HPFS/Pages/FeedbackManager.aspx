<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="FeedbackManager.aspx.cs" Inherits="HPFS.Pages.FeedbackManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Feedback Manager</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="insideHead" runat="server">
    <script src="/Scripts/FeedbackManager.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <h1 class="text-center">Feedback Manager</h1>
    <div class="row">
        <div class="col-xs-12 shadow-up-bro" style="padding: 10px; margin-bottom: 15px; border: #AFAFAF 1px solid;">
            <div class="col-xs-6 col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblFbStart" runat="server" Text="Start Date:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtFbStartDate" CssClass="form-control" PlaceHolder="mm/dd/yyyy" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblFbEnd" runat="server" Text="End Date:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtFbEndDate" CssClass="form-control" PlaceHolder="mm/dd/yyyy" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblFbSiteArea" runat="server" Text="Area of Site:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlFbSiteArea" CssClass="form-control" runat="server">
                        <asp:ListItem Value="Overall" Selected="True" Text="Overall"></asp:ListItem>
                        <asp:ListItem Text="Contact" Value="Contact"></asp:ListItem>
                        <asp:ListItem Text="Education" Value="Education"></asp:ListItem>
                        <asp:ListItem Text="About Us" Value="About"></asp:ListItem>
                        <asp:ListItem Text="Rehabilitation" Value="Rehabilitation"></asp:ListItem>
                        <asp:ListItem Text="Programs" Value="Programs"></asp:ListItem>
                        <asp:ListItem Text="Home" Value="Home"></asp:ListItem>
                        <asp:ListItem Text="Landing Page" Value="LandingPage"></asp:ListItem>
                        <asp:ListItem Text="Dashboard" Value="Dashboard"></asp:ListItem>
                        <asp:ListItem Text="User Settings" Value="Settings"></asp:ListItem>
                        <asp:ListItem Text="Public Events" Value="PublicEvents"></asp:ListItem>
                        <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFbNavRating" runat="server" Text="Navigation:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlNavRating" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All Stars" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFbAccRating" runat="server" Text="Accessibility:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlAccRating" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All Stars" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFbAppRating" runat="server" Text="Appearance:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlAppRating" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All Stars" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFbRating" runat="server" Text="Recommendation:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlRecRating" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All Stars" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFbAvgStart" runat="server" Text="Average Start:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlFbAvgStart" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All Stars" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFbAvgEnd" runat="server" Text="Average End:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlFbAvgEnd" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All Stars" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 text-center">
                <asp:Label ID="lblFbErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                <hr />
                <asp:Button ID="btnFbSearch" runat="server" CssClass="btn btn-default" Text="Search" OnClick="btnFbSearch_Click" OnClientClick="ValidateDates();" />
                <asp:Button ID="btnFbClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnFbClear_Click" />
            </div>
        </div>
        <div class="col-xs-12 text-center">
            <asp:UpdatePanel ID="upFeedbackMgr" runat="server">
                <ContentTemplate>
                    <script type="text/javascript"> function pageLoad() { $('#feedbackSearch').on("keyup", filterFeedBackRows); $("#Body_tblFeedback").on("click", "a[name='expand']", BuildModal); } </script>
                    <div class="col-xs-6 col-xs-offset-3 text-center" style="margin-top: 15px; margin-bottom: 15px;">
                        <h3>Filter</h3>
                        <input id="feedbackSearch" type="text" class="form-control" placeholder="Enter text to filter results..." />
                    </div>
                    <div class="col-xs-12" style="max-height: 500px; overflow: auto;">
                        <asp:Table ID="tblFeedback" runat="server" CssClass="table table-condensed sortable"></asp:Table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFbSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--FEEDBACK COMMENT MODAL--%>
    <div id="mdlFeedbackComment" class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Full Comment</h4>
                </div>
                <div class="modal-body">
                    <p id="txtFeedbackFull"></p>
                </div>
                <div class="modal-footer text-center">
                    <button type="button" class="btn btn-default positionNotificationsCloseButton" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
