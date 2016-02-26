<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuLeft.ascx.cs" Inherits="yeuthietkeweb.cms.UIs.MenuLeft" %>

<div class="navbar-default sidebar" role="navigation">
    <div class="sidebar-nav navbar-collapse">
        <ul class="nav" id="side-menu">
            <%--<li class="sidebar-search">
                <div class="input-group custom-search-form">
                    <input type="text" class="form-control" placeholder="Search...">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button">
                            <i class="fa fa-search"></i>
                        </button>
                    </span>
                </div>
                <!-- /input-group -->
            </li>--%>
            <li>
                <a href="#"><i class="fa fa-dashboard fa-fw"></i>Chuyên mục<span class="fa arrow"></span></a>
                <ul class="nav nav-second-level">
                    <li><a href="category_list.aspx">Danh sách chuyên mục</a></li>
                    <li><a href="category.aspx">Thêm mới chuyên mục</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-table fa-fw"></i>Bài viết<span class="fa arrow"></span></a>
                <ul class="nav nav-second-level">
                    <li><a href="news_list.aspx">Danh sách bài viết</a></li>
                    <li><a href="news.aspx">Thêm mới bài viết</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-table fa-fw"></i>Hình ảnh<span class="fa arrow"></span></a>
                <ul class="nav nav-second-level">
                    <li><a href="aditem_list.aspx">Danh sách hình ảnh</a></li>
                    <li><a href="aditem.aspx">Thêm mới hình ảnh</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-table fa-fw"></i>Thông tin liên hệ<span class="fa arrow"></span></a>
                <ul class="nav nav-second-level">
                    <li><a href="contact_list.aspx">Danh sách liên hệ</a></li>
                    <li><a href="contact_config.aspx">Cấu hình liên hệ</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-table fa-fw"></i>Hỗ trợ trực tuyến<span class="fa arrow"></span></a>
                <ul class="nav nav-second-level">
                    <li><a href="online_list.aspx">Danh sách HTTT</a></li>
                    <li><a href="online.aspx">Thêm mới HTTT</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-table fa-fw"></i>Quản trị<span class="fa arrow"></span></a>
                <ul class="nav nav-second-level">
                    <li><a href="group_list.aspx">Nhóm quản trị</a></li>
                    <li><a href="groups.aspx">Thêm nhóm quản trị</a></li>
                    <li><a href="user_list.aspx">Người quản trị</a></li>
                    <li><a href="user.aspx">Thêm người quản trị</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-table fa-fw"></i>Cấu hình<span class="fa arrow"></span></a>
                <ul class="nav nav-second-level">
                    <li><a href="config_meta.aspx">Meta</a></li>
                    <li><a href="Config_Hitcouter.aspx">Lượt truy cập</a></li>
                    <li><a href="Config_footer.aspx">Footer</a></li>
                    <li><a href="Config_banner.aspx">Logo/Banner</a></li>
                    <li><a href="config_email_list.aspx">Email</a></li>
                </ul>
            </li>
        </ul>
    </div>
</div>