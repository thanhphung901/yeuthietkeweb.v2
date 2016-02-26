<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="news_images.aspx.cs" Inherits="yeuthietkeweb.cms.pages.news_images" %>
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
            <h1 class="page-header">Album hình</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"
                CssClass="btn btn-default btn-success btn-sm" CausesValidation="false">Xóa</asp:LinkButton>
            <asp:LinkButton ID="Lnupload" runat="server" CssClass="btn btn-default btn-success btn-sm" onclick="Lnupload_Click">Lưu</asp:LinkButton>
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
                    Thông tin chung
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Mô tả</label>
                        <input type="text" name="txtTitle" id="txtTitle" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Thứ tự</label>
                        <input type="text" name="txtOrder" id="txtOrder" runat="server" onkeyup="this.value=formatNumeric(this.value);" onblur="this.value=formatNumeric(this.value);"
                            maxlength="4" class="form-control" value="1" />
                    </div>
                    <div class="form-group" id="trUpload1" runat="server">
                        <label>Hình</label>
                        <asp:FileUpload ID="FileUpload1" runat="server" class="multi" multiple="true" />
                    </div>
                    <div class="form-group" id="trImage1" runat="server">
                        <label>Hình</label>
                        <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/icon_delete.png"
                            BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Xóa file đính kèm">
                        </asp:ImageButton>
                        <asp:HyperLink runat="server" ID="hplImage1" Target="_blank"></asp:HyperLink><br />
                        <img id="Image1" runat="server" />
                    </div>
                </div>
                <div class="panel-body">
                    <div class="dataTable_wrapper">
                        <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th class="center" style="width:100px">STT</th>
                                    <th class="center">Hình</th>
                                    <th>Mô tả</th>
                                    <%--<th>#</th>--%>
                                    <th class="center">Xóa</th>
                                </tr>
                            </thead>
                            <tbody>                                
                                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td class="center">
                                                <asp:Label ID="lblSTT" runat="server" EnableViewState="False" Text='<%# getOrder() %>'></asp:Label>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("NEWS_IMG_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td class="center">
                                                <%--<a href='<%# getLink(DataBinder.Eval(Container.DataItem, "NEWS_IMG_ID")) %>'>--%>
                                                    <%# getImage(DataBinder.Eval(Container.DataItem, "NEWS_IMG_ID"), DataBinder.Eval(Container.DataItem, "NEWS_IMG_IMAGE1")) %>
                                                <%--</a>--%>
                                            </td>
                                            <td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "NEWS_IMG_ID")) %>'>
                                                    <%#DataBinder.Eval(Container.DataItem, "NEWS_IMG_DESC") %>
                                                </a>
                                            </td>
                                            <%--<td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "NEWS_IMG_ID")) %>'>Chỉnh sửa</a>
                                            </td>--%>
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
                responsive: true,
                aoColumnDefs: [
                  {
                      bSortable: false,
                      aTargets: [3]
                  }
                ]
            });
        });
    </script>
</asp:Content>
