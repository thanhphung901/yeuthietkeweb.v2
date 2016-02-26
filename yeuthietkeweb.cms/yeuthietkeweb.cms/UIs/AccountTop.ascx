<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountTop.ascx.cs" Inherits="yeuthietkeweb.cms.UIs.AccountTop" %>

<ul class="nav navbar-top-links navbar-right">                
    <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
            <i class="fa fa-user fa-fw"></i>  <i class="fa fa-caret-down"></i>
        </a>
        <ul class="dropdown-menu dropdown-user">
            <%--<li><a href="edit_user.aspx"><i class="fa fa-user fa-fw"></i> Tài khoản</a></li>--%>
            <li><a href="user_changepass.aspx"><i class="fa fa-gear fa-fw"></i> Đổi mật khẩu</a></li>
            <li class="divider"></li>
            <li><a href="logout.aspx"><i class="fa fa-sign-out fa-fw"></i> Thoát</a></li>
        </ul>
    </li>
</ul>