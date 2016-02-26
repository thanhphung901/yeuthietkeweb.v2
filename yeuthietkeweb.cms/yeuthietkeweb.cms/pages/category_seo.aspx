<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="category_seo.aspx.cs" Inherits="yeuthietkeweb.cms.pages.category_seo"   ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">Seo chuyên mục</h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-12 panel-body">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">Lưu</asp:LinkButton>
        <asp:HyperLink ID="Hyperback" runat="server" CssClass="btn btn-default btn-success btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Quay lại</asp:HyperLink>
        <asp:Literal ID="lbMessage" runat="server" Text=""></asp:Literal>        
    </div>
</div>
<div class="row">
    <div class="col-lg-12" id="seo">
        <div class="panel panel-default">                             
            <div class="panel-heading">
                Seo chuyên mục
            </div>
            <div class="panel-body">
                <textarea id="mrk" cols="20" rows="15" class="form-control" runat="server"></textarea>
            </div>
        </div>
    </div>   
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
<%--    <script src="../js/jquery.min.1.7.2.js" type="text/javascript"></script>
    <script src="../js/JqueryCollapse/jquery.collapse.js" type="text/javascript"></script>--%>
    <script src="../tinymce/js/tinymce/tinymce.min.js"></script>
    <script src="../js/TinymiceEditor.js" type="text/javascript"></script>
</asp:Content>