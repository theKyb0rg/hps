<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="FileManager.aspx.cs" Inherits="HPFS.FileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - File Manager</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <script src="/Scripts/FileManager.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1 class="text-center">File Manager</h1>
    <hr />
    <div class="row">
        <div class="col-xs-12 col-sm-6 text-center" runat="server" id="divSearchFilesInfo">
            <blockquote>To search for files, click the <b>'Search Files'</b> button.</blockquote>
            <a href="#Search" class="btn btn-default btn-block" data-toggle="collapse">Search Files</a>
        </div>
        <div class="col-xs-12 col-sm-6 text-center" runat="server" id="divCreateFolderInfo">
            <blockquote>To create a new folder, click the <b>'Create Folder'</b> button.</blockquote>
            <a href="#CreateNewFolder" class="btn btn-default btn-block" data-toggle="collapse">Create Folder</a>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-6" runat="server" id="divSearchFiles">
            <div id="Search" class="collapse">
                <hr />
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading text-center">
                                <h4>Search Files</h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12 col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblSearchFileName" runat="server" Text="File Name:"></asp:Label>
                                            <asp:TextBox ID="txtSearchFileName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblSearchFileType" runat="server" Text="Type:"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchFileType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="-1">Select a file type...</asp:ListItem>
                                                <asp:ListItem Value="doc">.doc</asp:ListItem>
                                                <asp:ListItem Value="jpg">.jpg</asp:ListItem>
                                                <asp:ListItem Value="png">.png</asp:ListItem>
                                                <asp:ListItem Value="mp3">.mp3</asp:ListItem>
                                                <asp:ListItem Value="pdf">.pdf</asp:ListItem>
                                                <asp:ListItem Value="rtf">.rtf</asp:ListItem>
                                                <asp:ListItem Value="txt">.txt</asp:ListItem>
                                                <asp:ListItem Value="zip">.zip</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblSearchStartDate" runat="server" Text="Upload Start Date:"></asp:Label>
                                            <asp:TextBox ID="txtSearchStartDate" runat="server" CssClass="form-control" TextMode="Date" PlaceHolder="mm/dd/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblSearchEndDate" runat="server" Text="Upload End Date:"></asp:Label>
                                            <asp:TextBox ID="txtSearchEndDate" runat="server" CssClass="form-control" TextMode="Date" PlaceHolder="mm/dd/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblSearchFolders" runat="server" Text="Folder:"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchFolders" runat="server" CssClass="form-control" AppendDataBoundItems="True"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:Label ID="lblSearchFolderErrors" runat="server" CssClass="text-danger" Text=""></asp:Label>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group text-center">
                                            <asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6" runat="server" id="divCreateFolder">
            <div id="CreateNewFolder" class="collapse">
                <hr />
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading text-center">
                                <h4>Create New Folder</h4>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblFolderName" runat="server" Text="Folder Name:"></asp:Label>
                                            <asp:TextBox ID="txtFolderName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblFolderPermissions" runat="server" Text="Permissions:"></asp:Label>
                                            <br />
                                            <asp:CheckBoxList ID="chkFolderPermissions" runat="server" RepeatColumns="2" CssClass="table table-condensed"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group text-center">
                                            <asp:Button ID="btnCreateFolder" CssClass="btn btn-default" runat="server" Text="Create" OnClick="btnCreateFolder_Click" />
                                            <asp:Label ID="lblFolderErrors" CssClass="text-danger" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <asp:Panel ID="pnlSearchResults" runat="server" Visible="false" EnableViewState="False">
        <hr />
        <div class="row">
            <div class="col-xs-12">
                <h2 class="text-center">Search Results</h2>
                <div style="overflow: auto; max-height: 400px;">
                    <asp:Table ID="tblFiles" CssClass="table table-condensed sortable" runat="server"></asp:Table>
                    <div class="text-center">
                        <asp:Label ID="lblNoResults" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <hr />
    <div class="row">
        <div class="text-center">
            <h1>Folders</h1>
            <asp:PlaceHolder ID="plPermissionLevel" runat="server"></asp:PlaceHolder>
            <br />
            <asp:Label ID="lblCRUDMessage" runat="server" Text="" EnableViewState="False"></asp:Label>
        </div>
        <hr />
        <asp:PlaceHolder ID="plFolders" runat="server"></asp:PlaceHolder>
    </div>

    <%--UPLOAD FILE MODAL--%>
    <div class="modal fade" id="mdlUploadFile" tabindex="-1" role="dialog" aria-labelledby="mdlUploadFile-label" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="mdlUploadFile-label">Upload New File</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label ID="lblUploadFileFolder" runat="server" Text="Folder:"></asp:Label>
                                <asp:DropDownList ID="ddlUploadFileFolder" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group text-center">
                                <asp:FileUpload ID="NewFile" AllowMultiple="true" runat="server" CssClass="btn btn-default" Style="display: inline-block;" />
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-default" Style="display: inline-block;" Text="Upload" OnClick="btnUpload_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <%--VIEW FILES MODAL PLACEHOLDER FOR ALL THE MODALS--%>
    <asp:PlaceHolder ID="plViewModals" runat="server"></asp:PlaceHolder>
</asp:Content>
