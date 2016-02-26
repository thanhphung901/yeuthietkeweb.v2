<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="yeuthietkeweb.cms.pages.category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
    new jQueryCollapse($("#seo"), {
        query: 'div h2'
    });

    function ParseText(objsent) {
        ParseUrl(objsent, document.getElementById('ContentMain_txtSeoUrl'));
        document.getElementById('ContentMain_txtSeoTitle').value = objsent.value;
        document.getElementById('ContentMain_txtSeoKeyword').value = objsent.value;
        //document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
    }
    function ParseTextEn(objsent) {
        ParseUrl(objsent, document.getElementById('ContentMain_txtSeoUrlEn'));
        document.getElementById('ContentMain_txtSeoTitleEn').value = objsent.value;
        document.getElementById('ContentMain_txtSeoKeywordEn').value = objsent.value;
        //document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
    }
    function ParseDesc(objsent) {
        document.getElementById('ContentMain_txtSeoDescription').value = objsent.value;
    }
    function ParseDescEn(objsent) {
        document.getElementById('ContentMain_txtSeoDescriptionEn').value = objsent.value;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">Chuyên mục</h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-12 panel-body">
        <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"
                                            CssClass="btn btn-default btn-success btn-sm" CausesValidation="false">Xóa</asp:LinkButton>
        <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click" CssClass="btn btn-default btn-success btn-sm">
            Lưu và thêm mới
        </asp:LinkButton>
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" CssClass="btn btn-default btn-success btn-sm">Lưu</asp:LinkButton>
        <a href="category_list.aspx" class="btn btn-default btn-success btn-sm">
            Quay lại
        </a>
        <a href="#" id="Hyperseo_cate" runat="server" class="btn btn-default btn-success btn-sm"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Seo chuyên mục </a>
        <asp:Literal ID="lbMessage" runat="server" Text=""></asp:Literal>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True" ShowSummary="False" ValidationGroup="g1" />
    </div>
</div>
<div class="row">
    <div class="col-lg-6" id="seo">
        <div class="panel panel-default">
            <div class="panel-heading">
                Thông tin chung
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                </div>
                <div class="form-group">
                    <label>Cấp chuyên mục</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Mã</label>
                    <input type="text" name="txtCode" id="txtCode" runat="server" class="form-control"/>
                </div>
                <div class="form-group">
                    <label>Tên chuyên mục</label>
                    <input type="text" name="txtName" id="txtName" runat="server" class="form-control"
                        onkeyup="ParseText(this);" onblur="ParseText(this);" />
                </div>
                <div class="form-group">
                    <label>Mô tả</label>
                    <textarea id="txtDesc" runat="server" onkeyup="ParseDesc(this);" class="form-control" rows="6"
                    onblur="ParseDesc(this);"></textarea>
                </div>                                
                <div class="form-group">
                    <label>Liên kết</label>
                    <table style="width: 100%">
                        <tr>
                            <td><input type="text" name="txtUrl" id="txtUrl" runat="server" class="form-control" /></td>
                            <td style="width: 120px;"><asp:DropDownList ID="ddlTarget" runat="server" class="form-control" style="width: 120px;">
                            </asp:DropDownList></td>
                        </tr>
                    </table>
                </div>
                <div class="form-group">
                    <label>Số tin / 1 trang</label>
                    <input type="text" name="txtpageItem" id="txtpageItem" runat="server" onblur="this.value=formatNumeric(this.value);"
                        maxlength="4" value="12" class="form-control"/>
                </div>
                <div class="form-group">
                    <label>Thứ tự</label>
                    <input type="text" name="txtOrder" id="txtOrder" runat="server" onblur="this.value=formatNumeric(this.value);"
                        maxlength="4" value="1" onkeyup="this.value=formatNumeric(this.value);" class="form-control"/>
                </div>
                <div class="form-group">
                    <label>Thứ tự trang chủ</label>
                    <input type="text" name="txtOrderPeriod" id="txtOrderPeriod" runat="server" onblur="this.value=formatNumeric(this.value);"
                        maxlength="4" value="1" onkeyup="this.value=formatNumeric(this.value);" class="form-control"/>
                </div>
            </div>
        </div>
        <div class="panel panel-default">                             
            <div class="panel-heading">
                Tags seo
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <label>SEO Title</label>
                    <input type="text" name="txtSeoTitle" id="txtSeoTitle" runat="server" class="form-control"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập SEO Title"
                        Text="Vui lòng nhập Seo Title" ControlToValidate="txtSeoTitle" CssClass="errormes"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>SEO URL</label>
                    <input type="text" name="txtSeoUrl" id="txtSeoUrl" runat="server" class="form-control"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập SEO URL"
                        Text="Vui lòng nhập Seo URL" ControlToValidate="txtSeoUrl" CssClass="errormes"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>SEO Keyword</label>
                    <textarea id="txtSeoKeyword" runat="server" class="form-control"></textarea>
                </div>
                <div class="form-group">
                    <label>SEO Description</label>
                    <textarea id="txtSeoDescription" runat="server" rows="6" class="form-control"></textarea>
                </div>
            </div>
        </div>        
    </div>
    <div class="col-lg-6">   
        <div class="panel panel-default">                             
            <div class="panel-heading">
                Thông tin hiển thị
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <label>Hiển thị</label>
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatColumns="2">
                        <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group" style="display:none;">
                    <label>Hiển thị trang chủ</label>
                    <asp:RadioButtonList ID="rblCatPeriod" runat="server" RepeatColumns="2">
                        <asp:ListItem Selected="True" Text="Không" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Có" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group">
                    <label>Kiểu hiển thị</label>
                    <asp:RadioButtonList ID="rblCatType" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="Tin tức" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Album" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group">
                    <label>Vị trí</label>
                    <asp:RadioButtonList ID="rblPos" runat="server" RepeatColumns="4">
                        <asp:ListItem Text="Menu" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Khác" Value="20"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group" style="display:none;">
                    <label>Hiển thị Footer</label>
                    <asp:RadioButtonList ID="rblShowFooter" runat="server" RepeatColumns="2">
                        <asp:ListItem Selected="True" Text="Không" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Có" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group">
                    <label>Hiển thị tức thời</label>
                    <asp:RadioButtonList ID="rblShowItems" runat="server" RepeatColumns="2">
                        <asp:ListItem Selected="True" Text="Không" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Có" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group" style="display:none">
                    <label>Ngôn ngữ</label>
                    <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="2">
                    </asp:RadioButtonList>
                </div>                            
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Hình ảnh
            </div>
            <div class="panel-body">            
                <div class="form-group" id="trUploadImage1" runat="server">
                    <label>Icon</label>
                    <input id="fileImage1" type="file" name="fileImage1" size="50" runat="server" class="form-control"/>
                </div>
                <div class="form-group" id="trImage1" runat="server">
                    <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/icon_delete.png"
                        BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Xóa icon này">
                    </asp:ImageButton>
                    <asp:HyperLink runat="server" ID="hplImage1" Target="_blank"></asp:HyperLink><br />
                    <img id="Image1" runat="server" />
                </div>
                <div class="form-group" id="trUploadImage2" runat="server">
                    <label>Ảnh đại diện</label>
                    <input id="fileImage2" type="file" name="fileImage1" size="50" runat="server" class="form-control"/>
                </div>
                <div class="form-group" id="trImage2" runat="server">
                    <asp:ImageButton ID="btnDelete2" runat="server" ImageUrl="../images/icon_delete.png"
                        BorderWidth="0" Width="13px" OnClick="btnDelete2_Click" ToolTip="Xóa hình đại diện này">
                    </asp:ImageButton>
                    <asp:HyperLink runat="server" ID="hplImage2" Target="_blank"></asp:HyperLink><br />
                    <img id="Image2" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
<%--    <script src="../js/jquery.min.1.7.2.js" type="text/javascript"></script>
    <script src="../js/JqueryCollapse/jquery.collapse.js" type="text/javascript"></script>--%>
</asp:Content>