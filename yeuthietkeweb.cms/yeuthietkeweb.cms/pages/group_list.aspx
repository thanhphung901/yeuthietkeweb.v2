<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="group_list.aspx.cs" Inherits="yeuthietkeweb.cms.pages.group_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- DataTables CSS -->
    <link href="../bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css" rel="stylesheet">
    <style>
        #tblFilter td
        {
            padding:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Danh sách nhóm người dùng</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Danh sách nhóm người dùng
                </div>
                <div class="panel-body">
                    <div class="dataTable_wrapper">
                        <div class="form-group">
                            <table id="tblFilter">
                                <tr>
                                    <td>
                                        <a href="groups.aspx" class="btn btn-default btn-success btn-sm">Thêm mới</a>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtSave" runat="server" CssClass="btn btn-default btn-success btn-sm" OnClick="lbtSave_Click">Chỉnh sửa</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtDelete" runat="server" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');" CssClass="btn btn-default btn-success btn-sm"
                                            CausesValidation="false" OnClick="lbtDelete_Click">Xóa</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th class="center">STT</th>
                                    <th class="center">#</th>
                                    <th>Mã nhóm</th>
                                    <th>Tên nhóm</th>
                                    <th>#</th>
                                    <th>Xóa</th>
                                </tr>
                            </thead>
                            <tbody>                                
                                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td class="center">
                                                <%# getOrder() %>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("GROUP_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td class="center">
                                                <input id="chkSelect" type="checkbox" name="chkSelect" runat="server" style="border-top-style: none;
                                                    border-right-style: none; border-left-style: none; border-bottom-style: none">
                                            </td>
                                            <td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "GROUP_ID")) %>'>
                                                    <%# DataBinder.Eval(Container.DataItem, "GROUP_CODE")%>
                                                </a>
                                            </td>
                                            <td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "GROUP_ID")) %>'>
                                                    <%# DataBinder.Eval(Container.DataItem, "GROUP_NAME")%>
                                                </a>
                                            </td>
                                            <td class="center">
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "GROUP_ID")) %>'>Chỉnh sửa</a>
                                            </td>
                                            <td class="center">
                                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" OnClientClick="return confirm('Bạn có chắc chắn xóa?');">
                                                    <img src="../images/icon_delete.png" title="Xóa" border="0">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>    
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
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
                 responsive: true
             });
         });
    </script>
</asp:Content>