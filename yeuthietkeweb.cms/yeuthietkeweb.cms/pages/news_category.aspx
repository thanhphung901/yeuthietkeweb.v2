<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="news_category.aspx.cs" Inherits="yeuthietkeweb.cms.pages.news_category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <h1 class="page-header">Soạn tin</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12 panel-body">
            <asp:Button ID="lbtSave" runat="server" Text="Lưu" CssClass="btn btn-default btn-success btn-sm"  onclick="lbtSave_Click" />
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
                <div class="panel-body">
                    <div class="dataTable_wrapper">
                        <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th class="center">STT</th>
                                    <th class="center">
                                        <input type="checkbox" id="toggleSelect" runat="server" onclick="javascript: ToggleAll(this,0);"
                                            name="toggleSign">
                                    </th>
                                    <th>Chuyên mục</th>
                                </tr>
                            </thead>
                            <tbody>                                
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td class="center">
                                                <%# getOrder() %>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("CAT_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td class="center">
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
    <script src="../bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="../bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"></script>

    <!-- Page-Level Demo Scripts - Tables - Use for reference -->
     <script>
         $(document).ready(function () {
             $('#dataTables-example').DataTable({
                 responsive: true,
                 "paging": false,
                 "ordering": false,
                 "info": false
             });
         });
    </script>
</asp:Content>
