<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="brand.aspx.cs" Inherits="yeuthietkeweb.cms.pages.brand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">Thương hiệu</h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-12 panel-body">
        <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"
                                            CssClass="btn btn-default btn-success btn-sm" CausesValidation="false">Xóa</asp:LinkButton>
        <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click" CssClass="btn btn-default btn-success btn-sm">
            Lưu và thêm mới
        </asp:LinkButton>
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">Lưu</asp:LinkButton>
        <a href="brand_list.aspx" class="btn btn-default btn-success btn-sm">
            Quay lại
        </a>        
        <asp:Literal ID="lbMessage" runat="server" Text=""></asp:Literal>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True" ShowSummary="False" ValidationGroup="g1" />
    </div>
</div>
<div class="row">
    <div class="col-lg-12" id="seo">
        <div class="panel panel-default">
            <div class="panel-heading">
                Thông tin chung
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                </div>
                <div class="form-group">
                    <label>Tên chuyên mục</label>
                    <input type="text" name="txtName" id="txtName" runat="server" class="form-control"  />
                </div>
                <div class="form-group">
                    <label>Thứ tự</label>
                    <input type="text" name="txtOrder" id="txtOrder" runat="server" onblur="this.value=formatNumeric(this.value);"
                        maxlength="4" value="1" onkeyup="this.value=formatNumeric(this.value);" class="form-control"/>
                </div>
                <div class="form-group">
                    <label>Active</label>
                    <asp:RadioButtonList ID="rblActive" runat="server" RepeatColumns="2">
                        <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group" id="trUploadImage1" runat="server">
                    <label>Logo</label>
                    <input id="fileImage1" type="file" name="fileImage1" size="50" runat="server" class="form-control"/>
                </div>
                <div class="form-group" id="trImage1" runat="server">
                    <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/icon_delete.png"
                        BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Xóa icon này">
                    </asp:ImageButton>
                    <asp:HyperLink runat="server" ID="hplImage1" Target="_blank"></asp:HyperLink><br />
                    <img id="Image1" runat="server" />
                </div>
            </div>
        </div>
                
    </div>    
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
<%--    <script src="../js/jquery.min.1.7.2.js" type="text/javascript"></script>
    <script src="../js/JqueryCollapse/jquery.collapse.js" type="text/javascript"></script>--%>
</asp:Content>