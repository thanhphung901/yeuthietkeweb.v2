<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="config_meta.aspx.cs" Inherits="yeuthietkeweb.cms.pages.config_meta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Karpach.WebControls" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- DataTables CSS -->
    <link href="../bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css" rel="stylesheet">
    <style>
        #tblFilter td {padding:5px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Cấu hình Meta trang chủ</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">
                Lưu</asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Thông tin chung
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Title</label>
                        <input type="text" name="txtSeoTitle" id="txtSeoTitle" runat="server" class="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập Seo Title"
                            Text="Vui lòng nhập Seo Title" ControlToValidate="txtSeoTitle" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Title(Tiếng anh)</label>
                        <input type="text" name="txtSeoTitleEn" id="txtSeoTitleEn" runat="server" class="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Vui lòng nhập Seo Title tiếng anh"
                            Text="Vui lòng nhập Seo Title tiếng anh" ControlToValidate="txtSeoTitleEn" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Meta Description</label>
                        <textarea id="txtSeoDesc" runat="server" class="form-control"></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập Meta Description"
                            Text="Vui lòng nhập Meta Description" ControlToValidate="txtSeoDesc" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Meta Description(Tiếng anh)</label>
                        <textarea id="txtSeoDescEn" runat="server" class="form-control"></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Vui lòng nhập Meta Description tiếng anh"
                            Text="Vui lòng nhập Meta Description tiếng anh" ControlToValidate="txtSeoDescEn"
                            CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Meta Keyword</label>
                        <textarea id="txtSeoKeyword" runat="server" class="form-control"></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập Meta Keyword"
                            Text="Vui lòng nhập Meta Keyword" ControlToValidate="txtSeoKeyword" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Meta Keyword(Tiếng anh)</label>
                        <textarea id="txtSeoKeywordEn" runat="server" class="form-control"></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Vui lòng nhập Meta Keyword tiếng anh"
                            Text="Vui lòng nhập Meta Keyword tiếng anh" ControlToValidate="txtSeoKeywordEn"
                            CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group" id="trUpload" runat="server">
                        <label>Favicon</label>
                        <input id="fileImage1" type="file" name="fileImage1" size="50" runat="server" class="form-control">
                    </div>
                    <div class="form-group" id="trFile" runat="server">
                        <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/icon_delete.png"
                            BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Xóa file đính kèm">
                        </asp:ImageButton>
                        <asp:HyperLink runat="server" ID="hplFile" Target="_blank"></asp:HyperLink><br />
                        <asp:Literal EnableViewState="false" runat="server" ID="ltrImage"></asp:Literal>
                    </div>
                    <div class="form-group">
                        <label>Chọn màu hover</label>
                        <cc1:ColorPicker ID="ColorPicker1" runat="server"/>
                    </div>
                    <div class="form-group" id="trUploadBG" runat="server">
                        <label>Background</label>
                        <input id="fileImageBG" type="file" name="fileImageBG" size="50" runat="server" class="form-control">
                    </div>
                    <div class="form-group" id="trFileBG" runat="server">
                        <asp:ImageButton ID="btnDeleteBG" runat="server" ImageUrl="../images/icon_delete.png"
                            BorderWidth="0" Width="13px" CausesValidation="false" ToolTip="Xóa file đính kèm" OnClick="btnDeleteBG_Click">
                        </asp:ImageButton>
                        <asp:HyperLink runat="server" ID="hplFileBG" Target="_blank"></asp:HyperLink><br />
                        <asp:Literal EnableViewState="false" runat="server" ID="ltrImageBG"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
<!-- DataTables JavaScript -->
    <script src="../bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="../bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"></script>

    <!-- Page-Level Demo Scripts - Tables - Use for reference -->
    <script>
        $(document).ready(function () {
            $('#dataTables-example').DataTable({
                "sDom": '<"top">rt<"bottom"><"clear">',
                responsive: true
            });
        });
    </script>
</asp:Content>
