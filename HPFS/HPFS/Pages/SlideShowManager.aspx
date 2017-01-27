<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="SlideShowManager.aspx.cs" Inherits="HPFS.Pages.SlideShowManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Slide Show Manager</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <script src="/Scripts/SlideShowManager.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="text-center">
        <h1>Slide Show Manager</h1>
        <asp:Label ID="lblCRUDMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <hr />
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>Slide Show Manager</h4>

                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" Text="Web Page:">Web Page:</asp:Label>
                                <asp:DropDownList ID="ddlPage" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                    <asp:ListItem Value="-1">Pick a Web Page...</asp:ListItem>
                                    <%--<asp:ListItem Value="Home">About Us</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="EducationAndResearch">Education & Research</asp:ListItem>--%>
                                    <asp:ListItem Value="Programs">Programs</asp:ListItem>
                                    <%--<asp:ListItem Value="RehabilitationAndTreatment">Rehabilitation & Treatment</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <label id="lblPageError" class="text-danger"></label>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblSlideShow" runat="server" Text="Slide Show:"></asp:Label>
                                <asp:DropDownList ID="ddlSlideShow" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSlideShow_SelectedIndexChanged">
                                    <asp:ListItem Value="-1">No Slide Shows to show.</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label id="lblSlideShowError" class="text-danger"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlSlideShowImages" runat="server" Visible="false" EnableViewState="false">
        <div class="currentSortable">
            <div id="current" class="row current" runat="server" style="min-height: 255px;">
                <div class="text-center">
                    <h3>Current Images</h3>
                </div>
                <hr />
                <asp:PlaceHolder ID="plhCurrentImages" runat="server"></asp:PlaceHolder>
            </div>
            <div class="text-center save" style="display: none;" id="saveButton" runat="server">
                <hr />
                <button id="btnSaveSlideShow" class="btn btn-default">Save Slide Show</button>
            </div>
        </div>
        <hr />
        <div class="allSortable">
            <div id="all" class="row all" runat="server" style="min-height: 255px; overflow: auto; max-height: 500px;">
                <div class="text-center">
                    <h3>All Images</h3>
                </div>
                <hr />
                <asp:PlaceHolder ID="plhImages" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </asp:Panel>
    <asp:PlaceHolder ID="plModals" runat="server"></asp:PlaceHolder>
</asp:Content>
