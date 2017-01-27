<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="ProgramManager.aspx.cs" Inherits="HPFS.Pages.ProgramManager" ValidateRequest="False" maintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="insideHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <%--    <script>
        function RefreshUpdatePanel() {
            __doPostBack('<%= txtProgramSchedule.ClientID %>', '');
        };
    </script>--%>
    <h1 class="text-center">Program Manager</h1>
    <hr />
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-10 col-xs-offset-1 col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4">
                <div class="form-group">
                    <asp:Label ID="lblSelectProgramTab" runat="server" Text="Name:" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlSelectProgramTab" runat="server" CssClass="form-control" EnableViewState="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectProgramTab_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Text="Select a Program" Value=""></asp:ListItem>
                        <asp:ListItem Text="Collective Kitchen" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Computer Tutoring" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Cottage Studio" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Friday Social Group" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Gaming Group" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Movie Group" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Sweet Donations Group" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Travelling Cup" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Volleyball / Summer Sports" Value="9"></asp:ListItem>
                        <asp:ListItem Text="Walking Group" Value="10"></asp:ListItem>
                        <asp:ListItem Text="Wednesday Leisure Group" Value="11"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lblProgramDescription" runat="server" Text="Description:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="121"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lblProgramGoals" runat="server" Text="Goal:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramGoals" runat="server" CssClass="form-control" TextMode="MultiLine" Height="121"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lblProgramSchedule" runat="server" Text="Schedule:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramSchedule" runat="server" CssClass="form-control" TextMode="MultiLine" Height="121" AutoPostBack="False" MaxLength="250"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lblProgramEmail" runat="server" Text="Email:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramEmail" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lblProgramPhone" runat="server" Text="Phone:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramPhone" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lblProgramCoordinator" runat="server" Text="Coordinator:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramCoordinator" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lblProgramLocation" runat="server" Text="Location:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramLocation" runat="server" CssClass="form-control" MaxLength="75"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12">
                <div class="form-group">
                    <asp:Label ID="lblProgramMap" runat="server" Text="Map Url:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtProgramMap" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="text-center">
                    <h4 style="text-decoration: underline">How to Add a New Google Map:</h4>
                    <p>Go to <a href="https://maps.google.ca/" target="_blank">Google Maps.</a></p>
                    <p>Search for the area you wish to display.</p>
                    <p>Click the Share button.</p>
                    <p>Select Embed map.</p>
                    <p>Copy and Replace the link in the Map Url Textbox.</p>
                </div>
            </div>
            <div class="col-xs-12 text-center">
                <br />
                <asp:Label ID="lblProgramDataError" CssClass="text-danger" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblProgramDataSuccess" CssClass="text-success" runat="server" Text=""></asp:Label>
                <hr />
                <asp:Button ID="btnSaveProgramData" runat="server" CssClass="btn btn-default" Text="Save" Width="85" OnClick="btnSaveProgramData_Click" CausesValidation="False" EnableViewState="False" ValidateRequestMode="Inherit" />
                <asp:Button ID="btnCancelProgramData" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancelProgramData_Click" EnableViewState="False" CausesValidation="False" />
            </div>
        </div>
    </div>
</asp:Content>
