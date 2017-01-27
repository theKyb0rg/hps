<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="ImageManager.aspx.cs" Inherits="HPFS.ImageManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Image Manager</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <script type="text/javascript">
        function handleFileSelect(input) {
            document.getElementById("ImageHolder").innerHTML = "";
            document.getElementById("ImageHolder").style.paddingTop = 15;
            

            if (!document.getElementById("Body_NewImage").files.length == 0) {
                document.getElementById("ImageHolder").innerHTML = "<h4>Image Preview:</h4>";
                document.getElementById("ImageHolder").style.paddingTop = 0;
            }


            $(input.files).each(function () {
                var reader = new FileReader();
                reader.readAsDataURL(this);
                reader.onload = function (e) {
                    $("#ImageHolder").append("<img class='img-thumbnail' style='height:100px; width:100px;' src='" + e.target.result + "'>");
                }
            });
        }

    </script>
    <h1 class="text-center">Image Manager</h1>
    <hr />
    <div class="row">
        <div class="col-xs-2"></div>
        <div class="col-xs-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>Upload New File</h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label ID="lblFileName" runat="server" Text="File Name:"></asp:Label>
                                <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label ID="lblFilePermissions" runat="server" Text="Permissions:"></asp:Label>
                                <asp:DropDownList ID="ddlFileRole" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="-1">Select who has access to this (these) file(s)...</asp:ListItem>
                                    <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                                    <asp:ListItem Value="Client">Client</asp:ListItem>
                                    <asp:ListItem Value="Board Member">Board Member</asp:ListItem>
                                    <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-xs-12" style="padding-bottom:0;">
                            <div class="form-group text-center">
                                <asp:FileUpload ID="NewImage" onChange="handleFileSelect(this)" AllowMultiple="true" runat="server" CssClass="btn btn-default" Style="margin: 0 auto;" />
                                <div id="ImageHolder">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-2"></div>
    </div>
</asp:Content>
