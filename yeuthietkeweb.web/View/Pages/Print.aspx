<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="yeuthietkeweb.vi_vn.PrintPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<link href="/Resources/Styles/template.css" rel="stylesheet" type="text/css" />
<asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>


    <script type="text/javascript">

        function SetBackgroundTagBody() {
            document.getElementsByTagName("body")[0].style.background = "none";

        }
</script>
</head>
<body onload="SetBackgroundTagBody()">
<form id="form1" runat="server">
  <div id="printing">
    <div id="header">
      <asp:Repeater ID="Rplogo" runat="server">
        <ItemTemplate> <%# Getbanner(Eval("BANNER_TYPE"),Eval("BANNER_FIELD1"), Eval("BANNER_ID"), Eval("BANNER_FILE"))%> </ItemTemplate>
      </asp:Repeater>
    </div>
    <div style="clear:both"></div>
    <div id="function_print">
      <div class="fl"> <a href="JavaScript:window.print()">
        <asp:Image ID="Image2" Width="20px" runat="server"
                            ImageUrl="/Resources/Images/btn-print.png" />
        In trang</a></div>
      <div class="fr">
        <asp:Label ID="lbDate" runat="server" Text=""></asp:Label>
      </div>
    </div>
    <div style="clear:both"></div>
    <div id="main">
      <h2 class="h2Title">
        <asp:Label ID="lbTitle" runat="server" Text=""></asp:Label>
      </h2>
      <div id="detail_news">
        <asp:Literal ID="ltrHtml" runat="server"></asp:Literal>
      </div>
    </div>
    <p align="center">Copyright 2015 by dichvuviettel.com.vn - <a href="http://www.yeuthietkeweb.com" target="_blank" tabindex="0"> Thiết kế website</a> bởi <a href="http://yeuthietkeweb.com" title="Website yêu thiết kế web" target="_blank" tabindex="0"> Yêu thiết kế web</a></p>
  </div>
</form>
</body>
</html>
