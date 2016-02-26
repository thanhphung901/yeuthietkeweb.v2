<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="user_changepass.aspx.cs" Inherits="yeuthietkeweb.cms.pages.user_changepass" %>
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
            <h1 class="page-header">Đổi mật khẩu</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">Lưu</asp:LinkButton>
            <a href="#" id="hplBack" runat="server" class="btn btn-default btn-success btn-sm">Quay lại</a>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group">
                        <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Mật khẩu cũ</label>
                        <input type="password" name="txtPass" id="txtPass" runat="server" class="form-control" autocomplete="off"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập mật khẩu cũ"
                            Text="Vui lòng nhập mật khẩu cũ" ControlToValidate="txtPass" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Mật khẩu mới</label>
                        <input type="password" name="txtPassNew" id="txtPassNew" runat="server" class="form-control" autocomplete="off"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập mật khẩu mới"
                            Text="Vui lòng nhập mật khẩu mới" ControlToValidate="txtPassNew" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Nhập lại mật khẩu mới</label>
                        <input type="password" name="txtRePass" id="txtRePass" runat="server" class="form-control" autocomplete="off" />
                        <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txtPassNew"
                            ControlToCompare="txtRePass" Operator="Equal" Type="String" ErrorMessage="Mật khẩu mới nhập không đúng!"
                            CssClass="errormes" />
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
</asp:Content>

