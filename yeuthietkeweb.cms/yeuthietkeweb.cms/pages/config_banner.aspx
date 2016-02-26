<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="config_banner.aspx.cs" Inherits="yeuthietkeweb.cms.pages.config_banner" %>
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
            <h1 class="page-header">Cấu hình Logo/Banner</h1>
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
                    Thông tin chung
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Mô tả</label>
                        <input type="text" name="txtTitle" id="txtTitle" runat="server" class="form-control"/>
                        <asp:Label ID="lblError" runat="server" CssClass="errormes" Text="Vui lòng nhập mô tả"
                            Visible="false"></asp:Label>
                    </div>
                    <div class="form-group" style="display:none">
                        <label>Ngôn ngữ</label>
                        <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="5">
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>Loại</label>
                        <asp:RadioButtonList ID="rblBannerType" runat="server" RepeatColumns="5">
                            <asp:ListItem Selected="True" Text="Hình" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Flash" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>Logo/Banner</label>
                        <asp:RadioButtonList ID="rblLogoBanner" runat="server" RepeatColumns="5">
                            <asp:ListItem Selected="True" Text="Logo" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Banner" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>Thứ tự</label>
                        <input type="text" name="txtOrder" id="txtOrder" runat="server" onblur="this.value=formatNumeric(this.value);"
                            maxlength="4" class="form-control" value="1" />
                    </div>
                    <div class="form-group" id="trUpload" runat="server">
                        <label>Upload File</label>
                        <input id="fileImage1" type="file" name="fileImage1" size="50" runat="server" class="form-control">
                    </div>
                    <div class="form-group" id="trFile" runat="server">
                        <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/icon_delete.png"
                            BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Xóa file đính kèm">
                        </asp:ImageButton>
                        <asp:HyperLink runat="server" ID="hplFile" Target="_blank"></asp:HyperLink><br />
                        <asp:Literal EnableViewState="false" runat="server" ID="ltrImage"></asp:Literal>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="dataTable_wrapper">
                        <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Mô tả</th>
                                    <th>Banner File</th>
                                    <th>#</th>
                                    <th>Xóa</th>
                                </tr>
                            </thead>
                            <tbody>                                
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td>
                                                <asp:Label ID="lblSTT" runat="server" EnableViewState="False" Text='<%# getOrder() %>'></asp:Label>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("BANNER_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "BANNER_ID")) %>'>
                                                    <%# DataBinder.Eval(Container.DataItem, "BANNER_DESC")%>
                                                </a>
                                            </td>
                                            <td>
                                                <%# getLinkImage(DataBinder.Eval(Container.DataItem, "BANNER_ID"),DataBinder.Eval(Container.DataItem, "BANNER_FILE")) %>
                                            </td>
                                            <td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "BANNER_ID")) %>'>Chỉnh sửa</a>
                                            </td>
                                            <td>
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