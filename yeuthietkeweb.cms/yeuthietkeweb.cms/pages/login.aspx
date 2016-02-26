<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="yeuthietkeweb.cms.pages.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
    <title>CMS V1.0</title>

    <!-- Bootstrap Core CSS -->
    <link href="../bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="../bower_components/metisMenu/dist/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="../bower_components/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Thông tin đăng nhập</h3>
                    </div>
                    <div class="panel-body">
                            <fieldset>
                                <div class="form-group">
                                    <input name="txtUN" type="text" class="form-control" id="txtUN" runat="server" placeholder="Tên đăng nhập" autofocus/>
                                </div>
                                <div class="form-group">
                                    <input name="txtPW" type="password" class="form-control" id="txtPW" runat="server" placeholder="Mật khẩu"/>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <input id="chkRemember" runat="server" name="remember" type="checkbox">Nhớ tên đăng nhập
                                    </label>
                                </div>
                                <!-- Change this to a button or input when using this as a form -->                   <div class="form-group">
                                    <label><asp:Literal ID="lbMessage" runat="server" Text=""></asp:Literal></label>
                                </div>        
                                <asp:LinkButton ID="lbtLogin" runat="server" 
                                    CssClass="btn btn-lg btn-success btn-block" Text="Đăng nhập" 
                                    onclick="lbtLogin_Click">
                                </asp:LinkButton>
                            </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="../bower_components/jquery/dist/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../bower_components/metisMenu/dist/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>

    <script language="javascript">
        function setfocus() {
            var txtUN = document.getElementById("txtUN");
            txtUN.focus();
        }

    </script>
    </form>
</body>
</html>
