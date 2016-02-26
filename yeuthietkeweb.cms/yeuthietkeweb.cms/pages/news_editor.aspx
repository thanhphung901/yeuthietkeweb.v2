<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="news_editor.aspx.cs" Inherits="yeuthietkeweb.cms.pages.news_editor"  ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Soạn tin</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:Button ID="lbtSave" runat="server" Text="Lưu" CssClass="btn btn-default btn-success btn-sm"  onclick="lbtSave_Click" />
            <a href="#" id="hplBack" runat="server" class="btn btn-default btn-success btn-sm">Quay lại</a>
        </div>
        <div class="col-lg-12 panel-body">
            <a href="#" id="hplCatNews" runat="server" class="btn btn-default btn-sm">Chọn chuyên mục </a>
            <a href="#" id="hplEditorHTMl" runat="server" class="btn btn-default btn-sm">Soạn  tin HTML </a>
            <a href="#" id="hplNewsAtt" runat="server" class="btn btn-default btn-sm">File  đính kèm </a>
            <a href="#" id="hplAlbum" runat="server" class="btn btn-default btn-sm">Album hình </a>
            <a href="#" id="hplComment" runat="server" class="btn btn-default btn-sm">Thông tin phản hồi</a>                
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Nội dung chi tiết tin
                </div>                
                <div class="panel-body">
                    <div class="form-group">
                        <textarea id="mrk" cols="20" rows="10" class="mrk" style="height: 500px;" runat="server"></textarea>
                    </div>    
                    <div class="form-group">
                        <asp:FileUpload ID="FileUpload1" runat="server" class="multi" multiple="true" Width="300px" />
                        <asp:Button ID="Btupmulti" runat="server" Text="Upload" OnClick="Btupmulti_Click" Width="100px"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="../tinymce/js/tinymce/tinymce.min.js"></script>
    <script src="../js/TinymiceEditor.js" type="text/javascript"></script>
</asp:Content>
