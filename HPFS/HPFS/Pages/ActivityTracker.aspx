<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="ActivityTracker.aspx.cs" Inherits="HPFS.Pages.ActivityTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Activity Tracker
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="insideHead" runat="server">
    <link href="/Content/bootstrap-switch.css" rel="stylesheet" />
    <%--<link href="../Content/css/pages/ActivityManager.min.css" rel="stylesheet" />--%>
    <script src="/Scripts/bootstrap-switch.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-xs-12 text-center">
            <h1>Activity Tracking</h1>
        </div>
        <div class="col-xs-12 text-center shadow-up-bro" style="padding: 10px; margin-bottom: 15px; border: #AFAFAF 1px solid;">
            <div class="col-xs-6 col-md-3">
                <label>Clicked</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivityClicked"/>
                <asp:CheckBox ID="chkActivityClickedHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-6 col-md-3">
                <label>Created</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivityCreated" />
                <asp:CheckBox ID="chkActivityCreatedHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-6 col-md-3">
                <label>Deleted</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivityDeleted" />
                <asp:CheckBox ID="chkActivityDeletedHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-6 col-md-3">
                <label>Downloaded</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivityDownloaded" />
                <asp:CheckBox ID="chkActivityDownloadedHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-6 col-md-3">
                <label>Logged In</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivityLoggedIn" />
                <asp:CheckBox ID="chkActivityLoggedInHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-6 col-md-3">
                <label>Navigated</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivityNavigated" />
                <asp:CheckBox ID="chkActivityNavigatedHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-6 col-md-3">
                <label>Searched</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivitySearched"/>
                <asp:CheckBox ID="chkActivitySearchedHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-6 col-md-3">
                <label>Updated</label>
                <br />
                <input type="checkbox" name="checkbox-switch" id="chkActivityUpdated" />
                <asp:CheckBox ID="chkActivityUpdatedHidden" Checked="true" CssClass="hide-me" runat="server" ClientIDMode="Predictable" />
            </div>
            <div class="col-xs-12 text-center">
                <hr />
                <asp:Button ID="btnFilterActivity" runat="server" Text="Search" CssClass="btn btn-default" OnClick="btnFilterActivity_Click" />
            </div>
        </div>


        <div class="col-xs-12" style="max-height:300px; overflow:auto;">
            <asp:UpdatePanel ID="upActivityData" runat="server">
                <ContentTemplate>
                    <script type="text/javascript"> function pageLoad() { $('#search').on("keyup", filterRows); } </script>
                    <div class="col-xs-6 col-xs-offset-3 text-center" style="margin-top: 15px; margin-bottom: 15px;">
                        <input id="search" type="text" class="form-control" placeholder="Enter text to filter results..." />
                    </div>
                    <div class="col-xs-12" style="max-height:500px; overflow:auto;">
                        <asp:Table ID="tblActivityData" runat="server" CssClass="table table-condensed sortable"></asp:Table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFilterActivity" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <script>
        $(function () {
            //This is for the activity tracking page, it shows sets all the filters to ON by default
            $('input[name="checkbox-switch"]').bootstrapSwitch('state', true, true);

            // Toggle the asp checkbox to checked/unchecked for use with C# server side code
            $('input[name="checkbox-switch"]').on('switchChange.bootstrapSwitch', function (event, state) {
                var id = $(this).attr("id");
                if (state) {
                    $("#Body_" + id + "Hidden").prop("checked", true);
                }
                else {
                    $("#Body_" + id + "Hidden").prop("checked", false);
                }
            });
        })
    </script>
</asp:Content>
