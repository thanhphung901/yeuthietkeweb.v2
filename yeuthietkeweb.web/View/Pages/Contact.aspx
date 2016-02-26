<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpages/Default.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="yeuthietkeweb.Contact" %>

<%@ Register src="../Usercontrols/ToolsRight.ascx" tagname="ToolsRight" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<nav class="breadcrumb">
  <div class="container">
    <ul>
      <li><a href="/">Trang chủ</a></li>
      <li><a>Liên hệ</a></li>
    </ul>
  </div>
</nav>
<!-- InstanceEndEditable -->
<main class="main" role="main">
 <div class="container">
  <div class="main-content"><!-- InstanceBeginEditable name="maincontent" -->
      <div class="iblock">
        <p class="tt-main"><span>bản đồ</span></p>
        <div class="inner-iblock">
        <div class="col12">
            <map class="map">
                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
            </map>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        <div class="col9 col9-md">
        <p class="tt-main"><span>Liên hệ</span></p>
        <form class="form-ct">
            <input type="text" id="Txtname" placeholder="Họ tên" class="txt" runat="server"/>
            <asp:RequiredFieldValidator ID="rfvHoVaTen" runat="server" ErrorMessage="Xin nhập họ và tên!"
                ControlToValidate="Txtname" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
            <input type="text" id="txtPhone" placeholder="Phone" class="txt" runat="server"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Xin nhập số điện thoại!" 
                ControlToValidate="txtPhone" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
            <input type="text" id="txtEmail" placeholder="Email" class="txt" runat="server"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Xin nhập số điện thoại!" 
                ControlToValidate="txtEmail" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="E-mail định dạng chưa đúng!" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None"
                ForeColor="Red" ValidationGroup="G40"></asp:RegularExpressionValidator>
            <textarea placeholder="Message" id="txtContent" class="txt" runat="server" rows="4" style="height:60px;"></textarea>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Xin nhập lời nhắn!"
                ControlToValidate="txtContent" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
            <p class="send">
              <input type="text" class="txt captcha" id="txtCapcha" placeholder="Nhập mã an toàn" runat="server"/>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa nhập mã bảo vệ!"
                    ControlToValidate="txtCapcha" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <script type="text/javascript"  language="Javascript">
                    function Catpc() {
                        var img = document.getElementById("icp");
                        img.src = "/Pages/captchr.ashx?query=" + Math.random();
                    }
                </script>  
                <img id="icp" src='/Pages/captchr.ashx?query=<%= querys() %>' alt="Mã an toàn"/>
                <a href="javascript:void(0)" onclick="javascript:Catpc();"><img title="Refresh" src="/Resources/Images/reloadpaf.png" /></a>
                <asp:LinkButton ID="Lbthanhtoan" runat="server" OnClick="Lbthanhtoan_Click" ValidationGroup="G40" CssClass="btn-send btn-b">Gửi tin</asp:LinkButton>
            </p>
          </form>
        
        </div>
          
        </div>
      </div>
      <!--end block--> 
      <!-- InstanceEndEditable --> </div>
  <!--end main content-->
  <div class="aside side-right"> <!-- InstanceBeginEditable name="side" -->
    <uc1:ToolsRight ID="ToolsRight1" runat="server" />
  </div>
  <!--side-right--> 
  
 </div>
</main>
 <div style="text-align: center">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True" ShowSummary="False" ValidationGroup="G40" />
</div>
</asp:Content>
