<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="order.aspx.cs" Inherits="yeuthietkeweb.cms.pages.order" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- DataTables CSS -->
    <link href="../bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css" rel="stylesheet">
    <link href="../Calendar/calendarStyle.css" rel="stylesheet" type="text/css" />
    <style>
        #tblFilter td {padding:5px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Chi tiết đơn hàng</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">
                Lưu</asp:LinkButton>
            <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"
                CssClass="btn btn-default btn-success btn-sm" CausesValidation="false">Xóa</asp:LinkButton>
            <a href="order_list.aspx" class="btn btn-default btn-success btn-sm">Quay lại</a>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Thông tin đơn hàng
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Ngày đặt hàng</label>
                        <input type="text" name="txtOrderDate" id="txtOrderDate" runat="server" class="form-control"
						    readonly="readonly" />
                    </div>
                    <div class="form-group">
                        <label>Mã đơn hàng</label>
                        <input type="text" name="txtOrderCode" id="txtOrderCode" runat="server" class="form-control"
						    readonly="readonly" />
                    </div>
                    <div class="form-group">
                        <label>Trạng thái</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="250px" Height="26px">
						    <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
						    <asp:ListItem Value="1" Text="Đang xử lý"></asp:ListItem>
						    <asp:ListItem Value="2" Text="Đã xác nhận"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Đang giao hàng"></asp:ListItem>
						    <asp:ListItem Value="4" Text="Giao hàng thành công"></asp:ListItem>
						    <asp:ListItem Value="5" Text="Hủy đơn hàng"></asp:ListItem>
					    </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Ghi chú</label>
                        <textarea id="txtOrderDesc" runat="server" class="form-control" readonly="readonly"></textarea>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Thông tin khách hàng
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Tên khách hàng</label>
                        <input type="text" name="txtName" id="txtName" runat="server" class="form-control"
						    readonly="readonly" />
                    </div>
                    <div class="form-group">
                        <label>Email liên hệ</label>
                        <input type="text" name="txtEmail" id="txtEmail" runat="server" class="form-control"
						    readonly="readonly" />
                    </div>
                    <div class="form-group">
                        <label>Địa chỉ</label>
                        <input type="text" name="txtAddress" id="txtAddress" runat="server" class="form-control"
						    readonly="readonly" />
                    </div>
                    <div class="form-group">
                        <label>Điện thoại</label>
                        <input type="text" name="txtPhone" id="txtPhone" runat="server" class="form-control"
						    readonly="readonly" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="dataTable_wrapper">
                        <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Sản phẩm</th>
                                    <th>Đơn giá</th>
                                    <th>Số lượng</th>
                                    <th>Thành tiền</th>
                                </tr>
                            </thead>
                            <tbody>                                
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td>
                                                <asp:Label ID="lblSTT" runat="server" EnableViewState="False" Text='<%# getOrder() %>'></asp:Label>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ITEM_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <%# GetNewsTitle(Eval("NEWS_TITLE"), Eval("ITEM_FIELD1"),Eval("ITEM_FIELD2"))%>
                                            </td>
                                            <td>
                                                <%# GetMoney(DataBinder.Eval(Container.DataItem, "ITEM_PRICE"))%>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "ITEM_QUANTITY")%>
                                            </td>
                                            <td>
                                                <%# GetMoney(DataBinder.Eval(Container.DataItem, "ITEM_SUBTOTAL"))%>
                                            </td>
                                        </tr>    
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="text-align: right;">
                            <asp:Literal ID="lblFreeShip" runat="server"></asp:Literal><br />
                            <asp:Literal ID="lblTotal" runat="server"></asp:Literal>
                        </div>
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

