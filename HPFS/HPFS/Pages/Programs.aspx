<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="Programs.aspx.cs" Inherits="HPFS.Programs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Programs</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-xs-12 col-sm-3">
            <h1 class="text-center">Programs</h1>
            <hr />
            <div class="panel-group hidden-xs" id="ProgramSideMenu">
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs activePanel shadow-up-bro" id="CK">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#CollectiveKitchen" hidden="hidden"></a>
                            <asp:LinkButton ID="btnCollectiveKitchenTab" runat="server" OnClick="LoadCarousel">Collective Kitchen</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="CT">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#ComputerTutoring" hidden="hidden"></a>
                            <asp:LinkButton ID="btnComputerTutoringTab" runat="server" OnClick="LoadCarousel">Computer Tutoring</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="CS">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#CottageStudio" hidden="hidden"></a>
                            <asp:LinkButton ID="btnCottageStudioTab" runat="server" OnClick="LoadCarousel">Cottage Studio</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="FSG">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#FridaySocialGroup" hidden="hidden"></a>
                            <asp:LinkButton ID="btnFridaySocialGroupTab" runat="server" OnClick="LoadCarousel">Friday Social Group</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="GG">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#GamingGroup" hidden="hidden"></a>
                            <asp:LinkButton ID="btnGamingGroupTab" runat="server" OnClick="LoadCarousel">Gaming Group</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="MG">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#MovieGroup" hidden="hidden"></a>
                            <asp:LinkButton ID="btnMovieGroupTab" runat="server" OnClick="LoadCarousel">Movie Group</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="SDG">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#SweetDonationsGroup" hidden="hidden"></a>
                            <asp:LinkButton ID="btnSweetDonationsGroupTab" runat="server" OnClick="LoadCarousel">Sweet Donations Group</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="TC">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#TravellingCup" hidden="hidden"></a>
                            <asp:LinkButton ID="btnTravellingCupTab" runat="server" OnClick="LoadCarousel">Travelling Cup</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="VSS">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#VolleyballSummerSports" hidden="hidden"></a>
                            <asp:LinkButton ID="btnVolleyballSummerSportsTab" runat="server" OnClick="LoadCarousel">Volleyball / Summer Sports</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="WG">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#WalkingGroup" hidden="hidden"></a>
                            <asp:LinkButton ID="btnWalkingGroupTab" runat="server" OnClick="LoadCarousel">Walking Group</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="WLG">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#WednesdayLeisureGroup" hidden="hidden"></a>
                            <asp:LinkButton ID="btnWednesdayLeisureGroupTab" runat="server" OnClick="LoadCarousel">Wednesday Leisure Group</asp:LinkButton>
                        </h4>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading panelTabs shadow-up-bro" id="OV">
                        <h4 class="panel-title text-center">
                            <a data-toggle="tab" href="#Overview" hidden="hidden"></a>
                            <asp:LinkButton ID="btnOverviewTab" runat="server" OnClick="LoadCarousel">Schedule Overview</asp:LinkButton>
                        </h4>
                    </div>
                </div>
            </div>
            <select id="ddlSelectYourProgram" class="form-control hidden-sm hidden-md hidden-lg">
                <option selected="selected" value="1">Collective Kitchen</option>
                <option value="2">Computer Tutoring</option>
                <option value="3">Cottage Studio</option>
                <option value="4">Friday Social Group</option>
                <option value="5">Gaming Group</option>
                <option value="6">Movie Group</option>
                <option value="7">Sweet Donations Group</option>
                <option value="8">Travelling Cup</option>
                <option value="9">Volleyball / Summer Sports</option>
                <option value="10">Walking Group</option>
                <option value="11">Wednesday Leisure Group</option>
                <option value="12">Schedule Overview</option>
            </select>
        </div>
        <div class="col-xs-12 col-sm-9">
            <div class="tab-content">
                <div id="CollectiveKitchen" class="tab-pane fade in active">
                    <h1 class="text-center">Collective Kitchen</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upCollectiveKitchen" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plCollectiveKitchen" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCollectiveKitchenTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="colKitDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="colKitGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="colKitCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="colKitEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="colKitPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="colKitSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="colKitLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="colKitMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="ComputerTutoring" class="tab-pane fade">
                    <h1 class="text-center">Computer Tutoring</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upComputerTutoring" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plComputerTutoring" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnComputerTutoringTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="compTutDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="compTutGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="compTutCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="compTutEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="compTutPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="compTutSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="compTutLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="compTutMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="CottageStudio" class="tab-pane fade">
                    <h1 class="text-center">Cottage Studio</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upCottageStudio" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plCottageStudio" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCottageStudioTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="cotStudioDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="cotStudioGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="cotStudioCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="cotStudioEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="cotStudioPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="cotStudioSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="cotStudioLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="cotStudioMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="FridaySocialGroup" class="tab-pane fade">
                    <h1 class="text-center">Friday Social Group</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upFridaySocialGroup" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plFridaySocialGroup" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnFridaySocialGroupTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="friSocDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="friSocGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="friSocCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="friSocEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="friSocPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="friSocSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="friSocLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="friSocMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="GamingGroup" class="tab-pane fade">
                    <h1 class="text-center">Gaming Group</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upGamingGroup" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plGamingGroup" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGamingGroupTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="gameGrpDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="gameGrpGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="gameGrpCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="gameGrpEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="gameGrpPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="gameGrpSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="gameGrpLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="gameGrpMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="MovieGroup" class="tab-pane fade">
                    <h1 class="text-center">Movie Group</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upMovieGroup" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plMovieGroup" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnMovieGroupTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="movieGrpDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="movieGrpGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="movieGrpCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="movieGrpEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="movieGrpPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="movieGrpSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="movieGrpLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="movieGrpMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="SweetDonationsGroup" class="tab-pane fade">
                    <h1 class="text-center">Sweet Donations Group</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upSweetDonationsGroup" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plSweetDonationsGroup" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSweetDonationsGroupTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="sweetGrpDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="sweetGrpGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="sweetGrpCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sweetGrpEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sweetGrpPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sweetGrpSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sweetGrpLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="sweetGrpMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="TravellingCup" class="tab-pane fade">
                    <h1 class="text-center">Travelling Cup</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upTravellingCup" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plTravellingCup" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnTravellingCupTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="travCupDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="travCupGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="travCupCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="travCupEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="travCupPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="travCupSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="travCupLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="travCupMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>

                <div id="VolleyballSummerSports" class="tab-pane fade">
                    <h1 class="text-center">Volleyball / SummerSports</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upVolleyballSummerSports" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plVolleyballSummerSports" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnVolleyballSummerSportsTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="sumSportDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="sumSportGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="sumSportCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sumSportEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sumSportPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sumSportSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="sumSportLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="sumSportMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="WalkingGroup" class="tab-pane fade">
                    <h1 class="text-center">Walking Group</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upWalkingGroup" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plWalkingGroup" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnWalkingGroupTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="walkGrpDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="walkGrpGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="walkGrpCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="walkGrpEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="walkGrpPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="walkGrpSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="walkGrpLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="walkGrpMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="WednesdayLeisureGroup" class="tab-pane fade">
                    <h1 class="text-center">Wednesday Leisure Group</h1>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="upWednesdayLeisureGroup" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="plWednesdayLeisureGroup" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnWednesdayLeisureGroupTab" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">General Description</h3>
                            <hr />
                            <p id="wedsGrpDesc" runat="server"></p>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Goals</h3>
                            <hr />
                            <p id="wedsGrpGoal" runat="server" class="paragraph"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Program Information</h3>
                            <hr style="margin-bottom: 5px;" />
                            <table class="table table-condensed text-center">
                                <tr>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5><b>Coordinator</b></h5>
                                    </td>
                                    <td style="border-top: none; padding-top: 0;">
                                        <h5 id="wedsGrpCoord" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Email</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="wedsGrpEmail" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Phone</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="wedsGrpPhone" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Schedule</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="wedsGrpSched" runat="server"></h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5><b>Location</b></h5>
                                    </td>
                                    <td>
                                        <h5 id="wedsGrpLocale" runat="server"></h5>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <h3 class="text-center">Location</h3>
                            <hr style="margin-bottom: 10px;" />
                            <iframe id="wedsGrpMap" runat="server" width="100%" height="300px" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div id="Overview" class="tab-pane fade">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <h1 class="text-center">Schedule Overview</h1>
                            <hr />
                            <div class="row" runat="server" id="overviewDiv">
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnOverviewTab" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            $("#ddlSelectYourProgram").change(function () {
                var index = parseInt($("#ddlSelectYourProgram").val());
                var btnClientID = "";

                switch (index) {
                    case 1:
                        btnClientID = "<%= btnCollectiveKitchenTab.ClientID %>";
                        break;
                    case 2:
                        btnClientID = "<%= btnComputerTutoringTab.ClientID %>";
                        break;
                    case 3:
                        btnClientID = "<%= btnCottageStudioTab.ClientID %>";
                        break;
                    case 4:
                        btnClientID = "<%= btnFridaySocialGroupTab.ClientID %>";
                        break;
                    case 5:
                        btnClientID = "<%= btnGamingGroupTab.ClientID %>";
                        break;
                    case 6:
                        btnClientID = "<%= btnMovieGroupTab.ClientID %>";
                        break;
                    case 7:
                        btnClientID = "<%= btnSweetDonationsGroupTab.ClientID %>";
                        break;
                    case 8:
                        btnClientID = "<%= btnTravellingCupTab.ClientID %>";
                        break;
                    case 9:
                        btnClientID = "<%= btnVolleyballSummerSportsTab.ClientID %>";
                        break;
                    case 10:
                        btnClientID = "<%= btnWalkingGroupTab.ClientID %>";
                        break;
                    case 11:
                        btnClientID = "<%= btnWednesdayLeisureGroupTab.ClientID %>";
                        break;
                    default:
                        btnClientID = "<%= btnOverviewTab.ClientID %>";
                        break;
                }

                document.getElementById(btnClientID).click();
            });
        })
    </script>
</asp:Content>
