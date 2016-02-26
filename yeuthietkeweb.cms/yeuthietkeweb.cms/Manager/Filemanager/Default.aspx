<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Testtinymice.Filemanager.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap/js/tiny_mce_popup.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>

    <style type="text/css">
        body
        {
            margin: 10px auto;
        }

        .col-md-3
        {
            width: 25%;
            float: left;
            padding: 10px;
        }

        .img-responsive, .thumbnail > img, .thumbnail a > img, .carousel-inner > .item > img, .carousel-inner > .item > a > img
        {
            height: 150px;
        }
    </style>
    <script type="text/javascript">
        function InsertFile(input, filename) {
            var i = tinyMCEPopup.getWin();
            i.document.getElementById("" + input + "").value = filename;
            tinyMCEPopup.close();
        }
        function DeleteFile(filename, id) {
            $.ajax({
                type: "POST",
                url: "/Cpanel/Manager/Filemanager/Default.aspx/deleteFile",
                data: "{filename:'" + filename + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (e) {
                    if (e.d == "success") {
                        document.getElementById("img_" + id + "").style.display = "none";
                    }
                    else alert("Lỗi");
                }
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <div class="page-header">
                <h1>Upload nhiều file</h1>
               
                    <div class="row">
                        <div class="col-md-3">
                            <asp:FileUpload ID="FileUpload1" runat="server"  multiple="true" Width="150px" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="Btupmulti" runat="server" Text="Upload" OnClick="Btupmulti_Click" class="btn btn-primary" />
                        </div>
                    </div>
               
            </div>

            <div class="row">
                <asp:Repeater ID="Rplistimg" runat="server">
                    <ItemTemplate>
                        <div class="col-md-3" id="img_<%#getCount() %>">
                            <div class="thumbnail">

                                <%# getOnlickdata(Eval("filename")) %>
                                <div class="caption">
                                    <%# getLinkbtn(Eval("filename")) %>
                                </div>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
