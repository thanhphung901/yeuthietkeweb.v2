<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="order_list.aspx.cs" Inherits="yeuthietkeweb.cms.pages.order_list" EnableEventValidation="false"%>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- DataTables CSS -->
    <link href="../bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css" rel="stylesheet">
    <style>
        #tblFilter td
        {
            padding:5px;
        }
    </style>
    <script language="javascript">
				<!--
                function ToggleAll(e, action) {
                    if (e.checked) {
                        CheckAll();
                    }
                    else {
                        ClearAll();
                    }
                }

                function CheckAll() {
                    var ml = document.forms[0];
                    var len = ml.elements.length;
                    for (var i = 1; i < len; i++) {
                        var e = ml.elements[i];

                        if (e.name.toString().indexOf("chkSelect") > 0)
                            e.checked = true;
                    }
                    ml.MainContent_GridItemList_toggleSelect.checked = true;
                }

                function ClearAll() {
                    var ml = document.forms[0];
                    var len = ml.elements.length;
                    for (var i = 1; i < len; i++) {
                        var e = ml.elements[i];
                        if (e.name.toString().indexOf("chkSelect") > 0)
                            e.checked = false;
                    }
                    ml.MainContent_GridItemList_toggleSelect.checked = false;
                }

                function selectChange() {
                    var radioButtons = document.getElementsByName("rblType");
                    for (var x = 0; x < radioButtons.length; x++) {
                        if (radioButtons[x].checked) {
                            if (radioButtons[x].value == 1)
                            { CheckAll(); }
                        }
                    }

                }
				    
				// -->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Danh sách đơn hàng</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">Danh sách đơn hàng</div>
                <div class="panel-body">
                    <div class="dataTable_wrapper">
                        <div class="form-group">
                            <table id="tblFilter">
                                <tr>
                                    <td>
                                        <input name="txtKeyword" type="text" id="txtKeyword" style="width: 250px" runat="server" placeholder=" Từ khóa tìm kiếm"/>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="250px" Height="26px">
                                        <asp:ListItem Value="99" Text="Tất cả"></asp:ListItem>
						                    <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
						                    <asp:ListItem Value="1" Text="Liên hệ khách hàng"></asp:ListItem>
						                    <asp:ListItem Value="2" Text="Giao hàng"></asp:ListItem>
						                    <asp:ListItem Value="3" Text="Thành công"></asp:ListItem>
						                    <asp:ListItem Value="4" Text="Thất bại"></asp:ListItem>
					                    </asp:DropDownList>
                                    </td>
                                    <%--<td>
                                        <uc1:pickerAndCalendar ID="ucFromDate" runat="server" />
                                        <uc1:pickerAndCalendar ID="ucToDate" runat="server" />
                                    </td>--%>
                                    <td>
                                        <asp:LinkButton ID="lbtSearch" runat="server" OnClick="lbtSearch_Click" CssClass="btn btn-default btn-success btn-sm">Tìm kiếm </asp:LinkButton>
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
                                    <th class="center">
                                        <input type="checkbox" id="toggleSelect" runat="server" onclick="javascript: ToggleAll(this,0);"
                                            name="toggleSign">
                                    </th>
                                    <th>Mã</th>
                                    <th>Khách hàng</th>
                                    <th>Điện thoại</th>
                                    <th>Địa chỉ</th>
                                    <th>Ngày đặt hàng</th>
                                    <th>Tổng tiền</th>
                                    <th>Trạng thái</th>
                                    <th>Thanh toán</th>
                                    <th>Ghi chú</th>
                                    <th class="center">Xóa</th>
                                </tr>
                            </thead>
                            <tbody>                                
                                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td class="center">
                                                <%# getOrder() %>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ORDER_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td class="center">
                                                <input id="chkSelect" type="checkbox" name="chkSelect" runat="server" style="border-top-style: none;
                                                    border-right-style: none; border-left-style: none; border-bottom-style: none">
                                            </td>
                                            <td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "ORDER_ID")) %>'>
                                                    <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%>
                                                </a>
                                            </td>
                                            <td>
                                                <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "ORDER_ID")) %>'>
                                                    <%# DataBinder.Eval(Container.DataItem, "ORDER_NAME")%>
                                                </a>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_PHONE")%>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_ADDRESS")%>
                                            </td>
                                            <td>
                                                <%# getPublishDate(DataBinder.Eval(Container.DataItem, "ORDER_PUBLISHDATE"))%>
                                            </td>
                                            <td>
                                                <%# GetMoney(DataBinder.Eval(Container.DataItem, "ORDER_TOTAL_ALL"))%>
                                            </td>
                                            <td>
                                                <%# getOrderStatus(DataBinder.Eval(Container.DataItem, "ORDER_STATUS"))%>
                                            </td>
                                            <td>
                                                <%# getOrderPayment(DataBinder.Eval(Container.DataItem, "ORDER_PAYMENT"))%>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_FIELD1")%>
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

    <<!-- Page-Level Demo Scripts - Tables - Use for reference -->
     <script>
         $(document).ready(function () {
             $('#dataTables-example').DataTable({
                 responsive: true,
                 aoColumnDefs: [
                  {
                      bSortable: false,
                      aTargets: [1]
                  },
                  {
                      bSortable: false,
                      aTargets: [8]
                  }
                ]
             });
         });
    </script>
</asp:Content>
