<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="AccountRecovery.aspx.cs" Inherits="HPFS.Pages.AccountRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Account Recovery</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="insideHead" runat="server">
    <link rel="stylesheet" href="../Content/css/pages/AccountRecovery.css" />
    <script src="/Scripts/AccRecovery.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div style="max-height: 400px; margin: 0 auto;">
        <div id="why">
            <h2 class="text-center">Why can't you sign in?</h2>
            <div class="recDiv">
                <hr />
                <br />
                <label class="radRec" for="radPass">
                    <input id="radPass" type="radio" name="radWhy" value="pass" checked="checked" />&nbsp;&nbsp;&nbsp;&nbsp;I forgot my password</label>
                <br />
                <label class="radRec" for="radUser">
                    <input id="radUser" type="radio" name="radWhy" value="user" />&nbsp;&nbsp;&nbsp;&nbsp;I forgot my username</label>
                <br />
                <br />
                <br />
                <div class="text-center">
                    <input id="btnNext" type="button" class="btn btn-default" value="Next" />
                    <asp:Button ID="btnCancel" Width="180" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
        <div id="recPass">
            <div class="recDiv text-center">
                <h2>Recover your account</h2>
                <hr />
                <h4>Enter your phone number or email:</h4>
                <br />
                <h5>We will send you a security code, in order to create a new password you'll need to be able to receive it.</h5>
                <h6><b>Note: </b><i>If you haven't verified your phone with your account you won't be able to receive the code.</i></h6>
                <br />
                <asp:TextBox ID="txtInfo" runat="server" Width="363" CssClass="form-control" Style="margin-left: 10px;" placeholder="Phone or Email"></asp:TextBox><br />
                <br />
                <asp:Button ID="btnSendCode" runat="server" CssClass="btn btn-default" Width="180" Text="Send Code" OnClick="btnSendCode_Click" />
                <asp:Button ID="btnCancelCode" runat="server" CssClass="btn btn-default" Width="180" Text="Cancel" OnClick="btnCancel_Click" />
                <br />
                <br />
                <asp:UpdatePanel ID="upRecPass" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSendCode" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="recUser">
            <div class="recDiv text-center">
                <h2>Recover your account</h2>
                <hr />
                <br />
                <div class="form-group text-left">
                    <asp:Label ID="lblFirst" runat="server" Style="padding-left: 10px;" Text="Enter your first name:"></asp:Label>
                    <asp:TextBox ID="txtFirst" CssClass="form-control" Width="360" runat="server" Style="margin-left: 10px;" placeholder="First Name"></asp:TextBox><br />
                </div>
                <div class="form-group text-left">
                    <asp:Label ID="lblLast" runat="server" Style="padding-left: 10px;" Text="Enter your last name:"></asp:Label>
                    <asp:TextBox ID="txtLast" runat="server" Width="360" CssClass="form-control" Style="margin-left: 10px;" placeholder="Last Name"></asp:TextBox><br />
                </div>
                <br />
                <asp:Button ID="btnUsernameNext" runat="server" Width="180" CssClass="btn btn-default" Text="Next" OnClick="btnUsernameNext_Click" />
                <asp:Button ID="btnUsernameCancel" runat="server" Width="180" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                <br />
                <br />
                <asp:UpdatePanel ID="upRecUsername" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblUsernameErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUsernameNext" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="upRecDisplay" runat="server">
            <ContentTemplate>
                <div id="recDisplay">
                    <div class="recDiv text-center">
                        <h2>Here is your username</h2>
                        <hr />
                        <br />
                        <asp:Label ID="lblUsername" runat="server" Text="" Font-Size="Large" Font-Underline="True"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnLogin" runat="server" Width="360" CssClass="btn btn-default" Text="Next" OnClick="btnCancel_Click" />
                        <br />
                        <br />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnUsernameNext" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="recCode">
            <div class="recDiv text-center">
                <h2>Enter your security code</h2>
                <hr />
                <br />
                <asp:UpdatePanel ID="upRecCode" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblSuccess" runat="server" CssClass="text-success" Text=""></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSendCode" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <br />
                <asp:TextBox ID="txtCode" runat="server" Width="363" CssClass="form-control" Style="margin-left: 10px;" placeholder="Security Code"></asp:TextBox><br />
                <br />
                <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-default" Width="180" Text="Verify" OnClick="btnVerify_Click" />
                <asp:Button ID="btnCancelVerify" runat="server" CssClass="btn btn-default" Width="180" Text="Cancel" OnClick="btnCancel_Click" />
                <br />
                <br />
                <asp:UpdatePanel ID="upRecCode2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblSecErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                        <br />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnVerify" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="recReset">
            <div class="recDiv text-center">
                <h2>Reset your password</h2>
                <hr />
                <asp:UpdatePanel ID="upRecReset" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblSuccessReset" runat="server" CssClass="text-success" Text=""></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnVerify" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <br />
                <div class="form-group text-left">
                    <asp:Label ID="lblPass" runat="server" Text="Enter your new password:"></asp:Label>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" Style="margin-left: 10px;" Width="363" TextMode="Password"></asp:TextBox>
                </div>
                <br />
                <div class="form-group text-left">
                    <asp:Label ID="lblRePass" runat="server" Text="Label">Confirm password:</asp:Label>
                    <asp:TextBox ID="txtRePass" runat="server" CssClass="form-control" Style="margin-left: 10px;" Width="363" TextMode="Password"></asp:TextBox>
                </div>
                <br />
                <asp:Button ID="btnSavePass" runat="server" CssClass="btn btn-default" Width="180" Text="Save" OnClick="btnSavePass_Click" />
                <asp:Button ID="btnCancelPass" runat="server" CssClass="btn btn-default" Width="180" Text="Cancel" OnClick="btnCancel_Click" />
                <br />
                <br />
                <asp:Label ID="lblPassErrors" CssClass="text-danger" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <asp:UpdatePanel ID="upRecComplete" runat="server">
            <ContentTemplate>
                <div id="recComplete">
                    <div class="recDiv text-center">
                        <h2>Your account has been recovered</h2>
                        <hr />
                        <br />
                        <h5>You should be able to sign in to it now using your new password.</h5>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnDone" runat="server" CssClass="btn btn-default" Width="360" Text="Next" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSavePass" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
