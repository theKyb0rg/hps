<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="HPFS.UserManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - User Manager</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <script src="/Scripts/UserManager.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1 class="text-center">User Manager</h1>
    <hr />
    <%--CREATE USER FORM--%>
    <div class="row">
        <div class="col-xs-12 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Create New User</h4>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblUserFirstName" runat="server" Text="First Name:"></asp:Label>
                            <asp:TextBox ID="txtUserFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblUserLastName" runat="server" Text="Last Name:"></asp:Label>
                            <asp:TextBox ID="txtUserLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblPasswordConfirm" runat="server" Text="Confirm Password:"></asp:Label>
                            <asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="form-group">
                            <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                <asp:ListItem Value="-1">Select a Role...</asp:ListItem>
                                <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                                <asp:ListItem Value="Client">Client</asp:ListItem>
                                <asp:ListItem Value="Board Member">Board Member</asp:ListItem>
                                <asp:ListItem Value="Family Association">Family Association</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <hr />
                    <div class="col-xs-12">
                        <div class="text-center">
                            <asp:Button ID="btnCreateUser" runat="server" CssClass="btn btn-default text-center" Text="Create" OnClick="btnCreateUser_Click" />
                            <br />
                            <br />
                            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Search Users</h4>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblSearchUserName" runat="server" Text="User Name:"></asp:Label>
                            <asp:TextBox ID="txtSearchUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblSearchEmail" runat="server" Text="Email:"></asp:Label>
                            <asp:TextBox ID="txtSearchEmail" runat="server" CssClass="form-control" PlaceHolder="johnsmith@domain.com"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblSearchFirstName" runat="server" Text="First Name:"></asp:Label>
                            <asp:TextBox ID="txtSearchFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblSearchLastName" runat="server" Text="Last Name:"></asp:Label>
                            <asp:TextBox ID="txtSearchLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblSearchStartDate" runat="server" Text="Creation Start Date:"></asp:Label>
                            <asp:TextBox ID="txtSearchStartDate" runat="server" CssClass="form-control" TextMode="Date" PlaceHolder="mm/dd/yyyy"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblSearchEndDate" runat="server" Text="Creation End Date:"></asp:Label>
                            <asp:TextBox ID="txtSearchEndDate" runat="server" CssClass="form-control" TextMode="Date" PlaceHolder="mm/dd/yyyy"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="form-group">
                            <asp:Label ID="lblSearchUserRole" runat="server" Text="Role:"></asp:Label>
                            <asp:DropDownList ID="ddlSearchUserRole" runat="server" CssClass="form-control">
                                <asp:ListItem Value="-1">All Roles</asp:ListItem>
                                <asp:ListItem Value="Administrator">&nbsp;Administrator</asp:ListItem>
                                <asp:ListItem Value="Client">&nbsp;Client</asp:ListItem>
                                <asp:ListItem Value="Board Member">&nbsp;Board Member</asp:ListItem>
                                <asp:ListItem Value="Family Association">&nbsp;Family Association</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <asp:Label ID="lblUserSearchErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </div>
                    <div class="clearfix"></div>
                    <hr />
                    <div class="col-xs-12">
                        <div class="text-center">
                            <asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlSearchResults" runat="server" Visible="false" EnableViewState="false">
        <div class="row">
            <div class="col-xs-12">
                <div class="col-xs-12 text-center">
                    <h2>Search Results</h2>
                    <hr />
                    <div style="overflow: auto; max-height: 400px;">
                        <asp:Table ID="tblUsers" CssClass="table table-condensed sortable" runat="server"></asp:Table>
                        <asp:Label ID="lblNoResults" runat="server" CssClass="text-center" Text="" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
