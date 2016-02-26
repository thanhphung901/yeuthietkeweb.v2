<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Toolbar.ascx.cs" Inherits="GiaNguyen.UIs.Toolbar" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<link href="/Resources/Styles/toolbar.css" rel="stylesheet" type="text/css" />
<cc3:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="hplFeedback"
    CancelControlID="hplClose" BackgroundCssClass="modalBackground" BehaviorID="lbtFeedback">
</cc3:ModalPopupExtender>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none;
    width: 500px">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="close" style="height: 30px; float: left; width: 100%">
                <a href="#" class="buttonE" id="hplClose" runat="server" style="float: right;">
                    <img src="/Resources/Images/p_close.gif" alt="" /></a> <span class="p_title">Gửi Ý Kiến
                        Phản Hồi</span>
            </div>
            <div class="block1">
                <div class="blockcontent1 padding2">
                    <div class="row">
                        <label class="col1 left_col1">
                            Họ tên của bạn:<span class="required">*</span></label>
                        <div class="col1 right_col1">
                            <input name="full_name" id="full_name" type="text" runat="server" class="inputbox" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập họ tên!"
                                Text="Vui lòng nhập họ tên" ControlToValidate="full_name" CssClass="required"
                                ValidationGroup="FeedbackGroup" Display="None"></asp:RequiredFieldValidator></div>
                    </div>
                    <div class="row">
                        <label class="col1 left_col1">
                            Email của bạn:<span class="required">*</span></label>
                        <div class="col1 right_col1">
                            <input name="txtemail" type="text" class="inputbox" runat="server" id="txtemail" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập e-mail!"
                                Text="Vui lòng nhập e-mail!" ControlToValidate="txtemail" CssClass="required" ValidationGroup="FeedbackGroup" Display="None"></asp:RequiredFieldValidator></div>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="txtemail" ErrorMessage="E-mail không hợp lệ!" CssClass="required"
                            ValidationGroup="FeedbackGroup" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="row">
                        <label class="col1 left_col1">
                            Nội dung:<span class="required">*</span></label>
                        <div class="col1 right_col1">
                            <textarea name="message" rows="5" col1s="30" runat="server" id="message"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập nội dung!"
                                Text="Vui lòng nhập nội dung!" ControlToValidate="message" CssClass="required"
                                ValidationGroup="FeedbackGroup" Display="None"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col1 left_col1">
                            Mã an toàn:<span class="required">*</span></label>
                        <div class="col1 right_col1">                            
                            <script type="text/javascript"  language="Javascript">
                                function Catpc() {
                                    var img = document.getElementById("icp");
                                    img.src = "/Resources/captchr.ashx?query=" + Math.random();
                                }
                            </script>                    
                            <img id="icp" align="absmiddle" src='/Resources/captchr.ashx?query=<%= querys() %>' alt="Mã  an toàn" />
                            <a href="javascript:void(0)" onclick="javascript:Catpc();">
                                <img title="Refresh" style="vertical-align: middle;border-width:0px" src="/Resources/images/reloadpaf.png" /></a>
                            <input name="txtCapcha" id="txtCapcha" runat="server" type="text" class="inputbox"
                                style="width: 100px;" />
                            <asp:Label ID="lbResult" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Vui lòng nhập captcha!"
                                Text="Vui lòng nhập captcha!" ControlToValidate="txtCapcha" CssClass="required"
                                ValidationGroup="FeedbackGroup" Display="None"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="buttonE">
                        <asp:LinkButton CssClass="buttonE" ID="lbtFeedback" runat="server" OnClick="lbtFeedback_Click"
                            ValidationGroup="FeedbackGroup"><img src="/Resources/Images/send.png" alt="" width="100%" /></asp:LinkButton>
                        <div class="clearfix">
                        </div>
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="FeedbackGroup" />
                </div>
                <!-- ModalPopupExtender -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="progress" src="/Resources/Images/loading.gif" />
            <p>
                Processing...</p>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Panel>
<!--Gui mail-->
<div style="float: right; margin-right: 10px;">
    <cc3:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="hplSendEmail"
        CancelControlID="hplClose2" BackgroundCssClass="modalBackground" BehaviorID="">
    </cc3:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none;
        width: 500px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="height: 30px; float: left; width: 100%">
                    <a href="#" id="hplClose2" runat="server" style="float: right;">
                        <img src="/Resources/Images/p_close.gif" alt="" /></a> <span class="p_title">Gửi Email
                            Cho Bạn Bè</span>
                </div>
                <div class="block1">
                    <div class="blockcontent1 padding2" style="border: 0;">
                        <div class="row">
                            <div class="col1 left_col1">
                                Họ tên của bạn:<span class="required">*</span></div>
                            <div class="col1 right_col1">
                                <input name="Send_txtFullname" id="Send_txtFullname" type="text" runat="server" class="inputbox" />
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Forecol1or="Red"
                                    runat="server" ErrorMessage="Vui lòng nhập họ tên!" Text="Vui lòng nhập họ tên!"
                                    ControlToValidate="Send_txtFullname" CssClass="required" ValidationGroup="SendEmailGroup2" Display="None"></asp:RequiredFieldValidator></div>
                        </div>
                        <div class="row">
                            <div class="col1 left_col1">
                                Email của bạn:<span class="required">*</span></div>
                            <div class="col1 right_col1">
                                <input name="Send_txtEmail" type="text" class="inputbox" runat="server" id="Send_txtEmail" />
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    Forecol1or="Red" ErrorMessage="Vui lòng nhập e-mail của bạn!" Text="Vui lòng nhập e-mail của bạn!"
                                    ControlToValidate="Send_txtEmail" CssClass="required" ValidationGroup="SendEmailGroup2" Display="None"></asp:RequiredFieldValidator></div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="Send_txtEmail"  ForeColor="Red" ErrorMessage="E-mail của bạn không hợp lệ!"
                                CssClass="required" ValidationGroup="SendEmailGroup2" Display="None"></asp:RegularExpressionValidator>
                        </div>
                        <div class="row">
                            <div class="col1 left_col1">
                                Gửi đến (To):<span class="required">*</span></div>
                            <div class="col1 right_col1">
                                <input name="Send_txtEmailTo" type="text" class="inputbox" runat="server" id="Send_txtEmailTo" />
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Forecol1or="Red" runat="server"
                                    ErrorMessage="Vui lòng nhập e-mail gửi đến!" Text="Vui lòng nhập e-mail gửi đến!"
                                    ControlToValidate="Send_txtEmailTo" CssClass="required" ValidationGroup="SendEmailGroup2" Display="None"></asp:RequiredFieldValidator></div>
                            <asp:RegularExpressionValidator Forecol1or="Red" ID="RegularExpressionValidator2"
                                runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="Send_txtEmailTo" ErrorMessage="E-mail gửi đến không hợp lệ!" CssClass="required"
                                ValidationGroup="SendEmailGroup2" Display="None"></asp:RegularExpressionValidator>
                        </div>
                        <div class="row">
                            <div class="col1 left_col1">
                                Gửi đến (CC):<span class="required"></span></div>
                            <div class="col1 right_col1">
                                <input name="Send_txtEmailCC" type="text" class="inputbox" runat="server" id="Send_txtEmailCC" />
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                    runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ControlToValidate="Send_txtEmailCC" Forecol1or="Red" ErrorMessage="E-mail(CC) không hợp lệ!"
                                    CssClass="required" ValidationGroup="SendEmailGroup2" Display="None"></asp:RegularExpressionValidator>
                            </div>
                            <div class="row">
                                <div class="col1 left_col1">
                                    Nội dung:<span class="required">*</span></div>
                                <div class="col1 right_col1">
                                    <textarea name="Send_txtContent" rows="5" col1s="30" runat="server" id="Send_txtContent"></textarea>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ErrorMessage="Vui lòng nhập nội dung!" Text="Vui lòng nhập nội dung!" Forecol1or="Red"
                                        ControlToValidate="Send_txtContent" CssClass="required" ValidationGroup="SendEmailGroup2" Display="None"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col1 left_col1">
                                    &nbsp;
                                </div>
                                <div class="col1 right_col1">                                    
                                    <script type="text/javascript"  language="Javascript">
                                        function Catpc2() {
                                            var img = document.getElementById("icp2");
                                            img.src = "/Resources/captchr.ashx?query=" + Math.random();
                                        }
                                </script>                    
                                <img id="icp2" align="absmiddle" src='/Resources/captchr.ashx?query=<%= querys() %>' alt="Mã  an toàn" />
                                <a href="javascript:void(0)" onclick="javascript:Catpc2();">
                                    <img title="Refresh" style="vertical-align: middle;border-width:0px" src="/Resources/images/reloadpaf.png" /></a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col1 left_col1">
                                    Mã an toàn:<span class="required">*</span></div>
                                <div class="col1 right_col1">
                                    <input name="Send_txtCapcha" id="Send_txtCapcha" runat="server" type="text" class="inputbox" />
                                    <br />
                                    <asp:Label ID="Send_lblResult" runat="server" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                        ErrorMessage="Vui lòng nhập captcha!" Text="Vui lòng nhập captcha!" Forecol1or="Red"
                                        ControlToValidate="Send_txtCapcha" CssClass="required" ValidationGroup="SendEmailGroup2" Display="None"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="buttonE row" style="margin-bottom: 10px">
                                <asp:LinkButton ID="lbtSendEmail" runat="server" OnClick="lbtSendEmail_Click" ValidationGroup="SendEmailGroup2"><img src="/Resources/Images/send.png" alt="" /></asp:LinkButton>
                                <div class="clearfix">
                                </div>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="SendEmailGroup2" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
            <ProgressTemplate>
                <img alt="progress" src="/Resources/images/loading.gif" />
                <p>
                    Processing...</p>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
</div>