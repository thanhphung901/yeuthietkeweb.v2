<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="config_email.aspx.cs" Inherits="yeuthietkeweb.cms.pages.config_email" %>
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
            <h1 class="page-header">Cấu hình Email</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">
                Lưu</asp:LinkButton>
            <a href="config_email_list.aspx" class="btn btn-default btn-success btn-sm">Quay lại</a>
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
                        <label>STT</label>
                        <input type="text" name="txtSTT" id="txtSTT" runat="server" class="form-control"
                            readonly="readonly" />
                    </div>
                    <div class="form-group">
                        <label>Mô tả</label>
                        <input type="text" name="txtDesc" id="txtDesc" runat="server" class="form-control"
                            readonly="readonly" />
                    </div>
                    <div class="form-group">
                        <label>Gửi đến(To)</label>
                        <input type="text" name="txtTo" id="txtTo" runat="server" class="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập Email To"
                            Text="Vui lòng nhập Email To" ControlToValidate="txtTo" CssClass="errormes"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="txtTo" ErrorMessage="Invalid Email Format" CssClass="required"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label>Đồng gửi đến(Cc)</label>
                        <input type="text" name="txtCc" id="txtCc" runat="server" class="form-control" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="txtCc" ErrorMessage="Invalid Email Format" CssClass="required"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label>Gửi bản sao(Bcc)</label>
                        <input type="text" name="txtBcc" id="txtBcc" runat="server" class="form-control" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="txtBcc" ErrorMessage="Invalid Email Format" CssClass="required"></asp:RegularExpressionValidator>
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
