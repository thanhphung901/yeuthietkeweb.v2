<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="groups.aspx.cs" Inherits="yeuthietkeweb.cms.pages.groups" %>
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
            <h1 class="page-header">Nhóm người dùng</h1>
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
                    Nhóm người dùng
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Mã nhóm</label>
                        <input id="txtCode" runat="server" class="form-control"/>
                    </div>
                    <div class="form-group">
                        <label>Tên nhóm</label>
                        <input type="text" name="txtName" id="txtName" runat="server" class="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên nhóm"
                            Text="Vui lòng nhập tên nhóm" ControlToValidate="txtName" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Loại nhóm</label>
                        <asp:RadioButtonList ID="rblType" runat="server" RepeatColumns="5">
                            <asp:ListItem Enabled="true" Selected="False" Text="Administrator" Value="1"></asp:ListItem>
                            <asp:ListItem Enabled="true" Selected="True" Text="Editor" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>SEO Description</label>
                        <textarea id="txtSeoDescription" runat="server" class="form-control"></textarea>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="dataTable_wrapper">
                        <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>#</th>
                                    <th>Phân quyền chuyên mục</th>
                                </tr>
                            </thead>
                            <tbody>                                
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td>
                                                <asp:Label ID="lblSTT" runat="server" EnableViewState="False" Text='<%# getOrder() %>'></asp:Label>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("CAT_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <input id="chkSelect" type="checkbox" name="chkSelect" runat="server" style="border-top-style: none;
                                                    border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                    checked='<%#CheckCat(DataBinder.Eval(Container.DataItem, "CAT_ID")) %>'>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "CAT_NAME")%>
                                            </td>
                                        </tr>    
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
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

