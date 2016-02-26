<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="aditem.aspx.cs" Inherits="yeuthietkeweb.cms.pages.aditem" %>

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
            <h1 class="page-header">Hình ảnh</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">
                Lưu</asp:LinkButton>
            <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"
                CssClass="btn btn-default btn-success btn-sm" CausesValidation="false">Xóa</asp:LinkButton>
            <a href="aditem_list.aspx" class="btn btn-default btn-success btn-sm">Quay lại</a>
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
                        <label>Mã</label>
                        <input type="text" name="txtCode" id="txtCode" runat="server" class="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập mã"
                            Text="Vui lòng nhập mã" ControlToValidate="txtCode" CssClass="errormes"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Mô tả</label>
                        <textarea id="txtDesc" runat="server" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Liên kết</label>
                        <input type="text" name="txtUrl" id="txtUrl" runat="server" class="form-control"/>
                        <asp:DropDownList ID="ddlTarget" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Loại</label>
                        <asp:RadioButtonList ID="rblBannerType" runat="server" RepeatColumns="2">
                            <asp:ListItem Selected="True" Text="Hình" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Flash" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>Vị trí</label>
                        <asp:RadioButtonList ID="rblAdPos" runat="server" RepeatColumns="5">
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>Chiều rộng</label>
                        <input type="text" name="txtWidth" id="txtWidth" runat="server" onblur="this.value=formatNumeric(this.value);"
                            maxlength="4" class="form-control" value="200" />
                    </div>
                    <div class="form-group">
                        <label>Chiều cao</label>
                        <input type="text" name="txtHeight" id="txtHeight" runat="server" onblur="this.value=formatNumeric(this.value);"
                            maxlength="4" class="form-control" value="100" />
                        <asp:RangeValidator ID="rvHeight" runat="server" ControlToValidate="txtHeight" Type="Integer"
                            ErrorMessage="Chiều cao phải lớn hơn 0" MaximumValue="1000" MinimumValue="1"
                            SetFocusOnError="True" CssClass="errormes"></asp:RangeValidator>
                    </div>
                    <div class="form-group">
                        <label>Thứ tự</label>
                        <input type="text" name="txtOrder" id="txtOrder" runat="server" onblur="this.value=formatNumeric(this.value);"
                            maxlength="4" class="form-control" value="1" />
                    </div>
                    <%--<div class="form-group">
                        <label>Ngày bắt đầu</label>
                        <uc1:pickerAndCalendar ID="ucFromDate" runat="server" />                        
                    </div>
                    <div class="form-group">
                        <label>Ngày kết thúc</label>
                        <uc1:pickerAndCalendar ID="ucToDate" runat="server" />
                    </div>--%>
                    <div class="form-group" style="display:none">
                        <label>Ngôn ngữ</label>
                        <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="5">
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>Lượt Click</label>
                        <asp:Label ID="lblCount" runat="server" EnableViewState="false"></asp:Label>
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

