<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="NotificationLog.aspx.cs" Inherits="HPFS.Pages.NotificationLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Notification Log</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="insideHead" runat="server">
    <script src="/Scripts/NotificationLog.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <h1 class="text-center">Notification Log</h1>
    <div class="row">
        <div class="col-xs-12 shadow-up-bro" style="padding: 10px; margin-bottom: 15px; border: #AFAFAF 1px solid;">
            <div class="col-xs-12 col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblPriority" runat="server" Text="Priority:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlPriority" CssClass="form-control" runat="server">
                        <asp:ListItem Selected="True" Value="" Text="Select a Priority"></asp:ListItem>
                        <asp:ListItem Value="High" Text="High"></asp:ListItem>
                        <asp:ListItem Value="Normal" Text="Normal"></asp:ListItem>
                        <asp:ListItem Value="Low" Text="Low"></asp:ListItem>
                        <asp:ListItem Value="Info" Text="Info"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblUsername" runat="server" Text="Username:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblNotificationStart" runat="server" Text="Start Date:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtNotificationStartDate" CssClass="form-control" PlaceHolder="mm/dd/yyyy" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-6 col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblNotificationEnd" runat="server" Text="End Date:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtNotificationEndDate" CssClass="form-control" PlaceHolder="mm/dd/yyyy" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblIsRead" runat="server" Text="Read:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlIsRead" runat="server" CssClass="form-control">
                        <asp:ListItem Selected="True" Text="Select a Value" Value=""></asp:ListItem>
                        <asp:ListItem Text="True" Value="true"></asp:ListItem>
                        <asp:ListItem Text="False" Value="false"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 text-center">
                <asp:Label ID="lblNotificationErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                <hr />
                <asp:Button ID="btnNotificationSearch" runat="server" CssClass="btn btn-default" Text="Search" OnClick="btnNotificationSearch_Click" OnClientClick="ValidateDates();"/>
                <asp:Button ID="btnNotificationClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnNotificationClear_Click"/>
            </div>
        </div>
        <div class="col-xs-12 text-center">
            <asp:UpdatePanel ID="upNotificationMgr" runat="server">
                <ContentTemplate>
                    <script type="text/javascript"> function pageLoad() { $('#notificationSearch').on("keyup", filterNotificationRows); } </script>
                    <div class="col-xs-6 col-xs-offset-3 text-center" style="margin-top: 15px; margin-bottom: 15px;">
                        <h3>Filter</h3>
                        <input id="notificationSearch" type="text" class="form-control" placeholder="Enter text to filter results..." />
                    </div>
                    <div class="col-xs-12" style="max-height: 500px; overflow: auto;">
                        <br />
                        <asp:Table ID="tblNotificationLog" runat="server" CssClass="table table-condensed sortable"></asp:Table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNotificationSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
