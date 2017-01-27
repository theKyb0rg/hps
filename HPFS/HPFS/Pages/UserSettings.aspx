<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="HPFS.Pages.UserSettings" maintainScrollPositionOnPostBack="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - User Settings</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <script src="/Scripts/UserSettings.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1 class="text-center">User Settings</h1>
    <hr />
    <div class="row">
        <div class="col-xs-12 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Contact</h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblFN" runat="server" Text="First Name:"></asp:Label>
                                <asp:TextBox ID="txtFN" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblLN" runat="server" Text="Last Name:"></asp:Label>
                                <asp:TextBox ID="txtLN" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblPhone" runat="server" Text="Phone #:"></asp:Label>
                                <asp:TextBox ID="txtPhone" runat="server" placeHolder="8888888888" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblEmail" runat="server" Text="Email Address:"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" placeHolder="johndoe1@host.com" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblIsMobileVerified" runat="server" Text="Mobile Verified:"></asp:Label>
                                <asp:TextBox ID="txtIsMobileVerified" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblIsEmailVerified" runat="server" Text="Email Verified:"></asp:Label>
                                <asp:TextBox ID="txtIsEmailVerified" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group text-center">
                                <asp:Button ID="btnSaveContact" runat="server" CssClass="btn btn-default" Text="Save" OnClick="btnSaveContact_Click" />
                                <asp:Button ID="btnCancelContact" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancelContact_Click" />
                                <asp:Label ID="lblContactErrors" CssClass="text-danger" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblContactSuccess" CssClass="text-success" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Password</h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label ID="lblOldPass" runat="server" Text="Current Password:"></asp:Label>
                                <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblNewPass" runat="server" Text="New Password:"></asp:Label>
                                <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblRePass" runat="server" Text="Confirm Password:"></asp:Label>
                                <asp:TextBox ID="txtRePass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group text-center">
                                <asp:Button ID="btnSavePass" runat="server" CssClass="btn btn-default" Text="Save" OnClick="btnSavePass_Click" />
                                <asp:Button ID="btnCancelPass" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancelPass_Click" />
                                <asp:Label ID="lblPassErrors" CssClass="text-danger" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblPassSuccess" CssClass="text-success" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>Security</h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12 col-md-6 text-center">
                            <h4 style="padding-left: 0;">Phone Verification</h4>
                            <hr />
                            <asp:TextBox ID="txtVerifyCell" runat="server" placeHolder="Mobile Number" CssClass="form-control"></asp:TextBox>
                            <br />
                            <asp:TextBox ID="txtVerifyCellCode" CssClass="form-control" placeHolder="Mobile Code" runat="server" ReadOnly="True"></asp:TextBox>
                            <br />
                            <asp:Label ID="lblVerifyCell" runat="server" CssClass="text-danger" Text=""></asp:Label>
                            <asp:Label ID="lblVerifyCellConflict" runat="server" CssClass="text-warning" Text=""></asp:Label>
                            <asp:Label ID="lblVerifyCellSuccess" runat="server" CssClass="text-success" Text=""></asp:Label>
                            <br />
                            <hr />
                            <div class="text-center">
                                <asp:Button ID="btnSendCellCode" runat="server" Text="Send Code" CssClass="btn btn-default" OnClick="btnSendCellCode_Click" />
                                <br /><br />
                            </div>
                        </div>
                        <div class="col-xs-12 col-md-6 text-center">
                            <h4 style="padding-left: 0;">Email Verification</h4>
                            <hr />
                            <asp:TextBox ID="txtVerifyEmail" runat="server" CssClass="form-control" placeHolder="Email@domain.com"></asp:TextBox>
                            <br />
                            <asp:TextBox ID="txtVerifyEmailCode" CssClass="form-control" placeHolder="Email Code" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="lblVerifyEmail" runat="server" CssClass="text-danger" Text=""></asp:Label>
                            <asp:Label ID="lblVerifyEmailConflict" runat="server" CssClass="text-warning" Text=""></asp:Label>
                            <asp:Label ID="lblVerifyEmailSuccess" runat="server" CssClass="text-success" Text=""></asp:Label>
                            <br />
                            <hr />
                            <div class="text-center">
                                <asp:Button ID="btnSendEmailCode" runat="server" CssClass="btn btn-default" Text="Send Code" OnClick="btnSendEmailCode_Click"/>
                                <asp:Button ID="btnVerifyEmailCode" runat="server" CssClass="btn btn-default" Text="Verify" OnClick="btnVerifyEmailCode_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
