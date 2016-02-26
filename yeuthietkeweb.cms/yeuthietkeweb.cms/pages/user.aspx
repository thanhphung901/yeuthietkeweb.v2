<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="yeuthietkeweb.cms.pages.user" %>
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
            <h1 class="page-header">Người dùng</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">
                Lưu</asp:LinkButton>
            <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"
                CssClass="btn btn-default btn-success btn-sm" CausesValidation="false">Xóa</asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Người dùng
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Họ và tên</label>
                        <input id="txtFullName" runat="server" class="form-control"/>
                    </div>
                    <div class="form-group">
                        <label>Tên đăng nhập</label>
                        <input type="text" name="txtUN" id="txtUN" runat="server" class="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên nhóm"
                            Text="Vui lòng nhập tên nhóm" ControlToValidate="txtUN" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Mật khẩu</label>
                        <input type="password" name="txtPass" id="txtPass" runat="server" class="form-control" autocomplete="off"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập mật khẩu"
                            Text="Vui lòng nhập mật khẩu" ControlToValidate="txtPass" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Nhập lại mật khẩu</label>
                        <input type="password" name="txtRePass" id="txtRePass" runat="server" class="form-control" autocomplete="off" />
                        <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txtPass"
                            ControlToCompare="txtRePass" Operator="Equal" Type="String" ErrorMessage="Mật khẩu nhập không đúng!"
                            CssClass="errormes" />
                    </div>
                    <div class="form-group">
                        <label>Nhóm</label>
                        <asp:DropDownList ID="ddlGroup" runat="server" DataValueField="GROUP_ID" DataTextField="GROUP_NAME">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Active</label>
                        <asp:RadioButtonList ID="rblActive" runat="server" RepeatColumns="2">
                            <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
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