<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="EventManager.aspx.cs" Inherits="HPFS.Pages.EventManager" maintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Event Manager</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <script src="/Scripts/EventManager.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-xs-12 text-center">
            <h1>Event Manager</h1>
            <asp:Panel ID="pnlCreateEvent" runat="server">
                <hr />
                <button type="button" class="btn btn-default" data-dismiss="modal" data-target="#mdlEventsCalendar" data-toggle="modal">Create New Event</button>
            </asp:Panel>
            <hr />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="calendar-height">
                <asp:LinkButton ID="btnCalendarEvent" runat="server" Visible="false" OnClick="btnCalendarEvent_Click"></asp:LinkButton>
                <asp:Calendar ID="EventCalendar" runat="server" CssClass="calendar" Font-Bold="False" DayNameFormat="Full" Font-Names="Source Sans Pro" NextMonthText="&amp;gt; &amp;gt;" PrevMonthText="&amp;lt; &amp;lt;" ShowGridLines="True" OnDayRender="EventCalendar_DayRender">
                    <DayHeaderStyle CssClass="text-center" Font-Bold="True" />
                    <DayStyle CssClass="calendar-day-style" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:Calendar>
            </div>
        </div>
    </div>
    <%--EVENT MODAL--%>
    <div class="modal fade" id="mdlEventsCalendar" tabindex="-1" role="dialog" aria-labelledby="mdlEvents-label" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title text-center" id="hdrEventModalTitle" runat="server">Create New Event</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblEventName" runat="server" Text="Event Name:"></asp:Label>
                                <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblVisibilty" runat="server" Text="Visibility:"></asp:Label>
                                <asp:DropDownList ID="ddlVisibilty" runat="server" CssClass="form-control" AppendDataBoundItems="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblEventDate" runat="server" Text="Date:"></asp:Label>
                                <asp:TextBox ID="txtEventDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblEventTime" runat="server" Text="Time:"></asp:Label>
                                <asp:TextBox ID="txtEventTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label ID="lblEventDescription" runat="server" Text="Description:"></asp:Label>
                                <asp:TextBox ID="txtEventDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xs-10">
                            <div class="form-group">
                                <asp:Label ID="lblEventFile" runat="server" Text="File:"></asp:Label>
                                <asp:DropDownList ID="ddlEventFile" runat="server" CssClass="form-control" AppendDataBoundItems="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-2">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblEventFileDownload" runat="server" Text=""></asp:Label>
                                <asp:LinkButton ID="btnEventFileDownload" CssClass="btn btn-default btn-block" OnClientClick="return false" data-toggle="tooltip" data-container="body" title="No File to Download." runat="server" Text="Download" OnClick="btnEventFileDownload_Click">
                                    <span class='glyphicon glyphicon-download-alt'></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <asp:Label ID="lblEventErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class=" text-center">
                        <asp:Button ID="btnSaveEvent" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSaveEvent_Click" Visible="false" />
                        <asp:Button ID="btnDeleteEvent" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteEvent_Click" Visible="false" />
                        <asp:Button ID="btnCreateEvent" runat="server" Text="Create" CssClass="btn btn-primary" OnClick="btnCreateEvent_Click" />
                        <asp:Button ID="btnCancelEvent" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancelEvent_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
