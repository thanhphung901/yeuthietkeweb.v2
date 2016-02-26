<%@ Page Title="" Language="C#" MasterPageFile="~/master/Site.Master" AutoEventWireup="true" CodeBehind="online.aspx.cs" Inherits="yeuthietkeweb.cms.pages.online" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Hổ trợ trực tuyến</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Hổ trợ trực tuyến
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <form>
                                <div class="form-group">
                                    <label>Mô tả</label>
                                    <textarea id="txtDesc" runat="server" class="form-control"></textarea>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Vui lòng nhập Mô tả"
                                        Text="Vui lòng nhập Mô tả" ControlToValidate="txtDesc" CssClass="errormes"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>Phone/URL</label>
                                    <input type="text" name="txtName" id="txtName" runat="server" class="form-control"/>
                                </div>
                                <div class="form-group" style="display:none">
                                    <label>Nick skype</label>
                                    <input type="text" name="txtName" id="txtSkype" runat="server" class="form-control"/>
                                </div>
                                <div class="form-group">
                                    <label>Loại</label>
                                    <asp:RadioButtonList ID="rblType" runat="server" RepeatColumns="6" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0" Text="Hotline"></asp:ListItem>
                                        <%--<asp:ListItem Value="7" Text="Mua hàng"></asp:ListItem>--%>
                                        <%--<asp:ListItem Value="8" Text="Mail"></asp:ListItem>--%>
                                        <%--<asp:ListItem Value="1" Text="Yahoo"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Skype"></asp:ListItem> --%>                      
                                        <asp:ListItem Value="3" Text="Facebook"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Google+"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Twitter"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Youtube"></asp:ListItem>    
                                        <%--<asp:ListItem Value="9" Text="Zing me"></asp:ListItem>  --%>                
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group">
                                    <label>Thứ tự</label>
                                    <input type="text" name="txtOrder" id="txtOrder" runat="server" onkeyup="this.value=formatNumeric(this.value);"
                                        onblur="this.value=formatNumeric(this.value);" maxlength="4" class="form-control"
                                        value="1" />
                                </div>
                                <div class="form-group" style="display:none">
                                    <label>Ngôn ngữ</label>
                                    <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="2">
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group" style="margin-bottom:0">
                                    <label><asp:Literal ID="lbMessage" runat="server" Text=""></asp:Literal></label>
                                </div>
                                <asp:Button ID="lbtSave" runat="server" Text="Lưu" CssClass="btn btn-default btn-success btn-sm"  ValidationGroup="g1" onclick="lbtSave_Click" />
                                <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click" CssClass="btn btn-default btn-success btn-sm">
                                    Lưu và thêm mới
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"
                                    CssClass="btn btn-default btn-success btn-sm" CausesValidation="false">
                                        Xóa
                                </asp:LinkButton>
                                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default btn-success btn-sm" Text="Trở lại" PostBackUrl="~/pages/online_list.aspx" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True" ShowSummary="False" ValidationGroup="g1" />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>