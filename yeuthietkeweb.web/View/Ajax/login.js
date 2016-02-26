
function login() {
    $('.login_div').removeClass("hidden");

}
function ajaxlogin() {
    var email = $('#ContentRight_txtEmail').val();
    var pass = $('#ContentRight_Txtpass').val();
    var email_regex = /^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/;
    if (pass == "" || email == "") {
        $('.errors').html("Tên đăng nhập hoặc mật khẩu trống");
        if (email == "") {
            $('#ContentRight_txtEmail').focus();
        } else {
            $('#ContentRight_Txtpass').focus();
        }
        return false;
    }

//    else if (!email_regex.test(email)) {
//        $('.errors').html("Email định dạng không đúng");
//        $('#header1_login1_txtEmail').focus();
//        return false;
    //}
    else {
        $('#loading-errors').html("<img src='../Resources/Images/loader-bar.gif' alt=''/>").fadeIn('slow');
        $.ajax({
            type: "POST",
            url: "../Resources/Ajax-customer.aspx/login",
            data: "{email:'" + email + "',pass:'" + pass + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (e) {
                $('#loading-errors').fadeOut(1500);
                setTimeout(function () { show_result(e) }, 1500);
            }
        });
    }
}
function show_result(e) {
    if (e.d == "errors") {
        $('.errors').html("Tên đăng nhập hoặc mật khẩu không chính xác");
    }
    else {
        $('.popup-close').css("display", "none");
        var login_response = '<div id="logged_in">' +
	                     '<div style="width: 350px; float: left; margin-left: 70px;">' +
	                     '<div style="width: 40px; float: left;">' +
	                     '<img alt="" style="margin: 10px 0px 10px 0px;" align="middle" src="../Resources/images/ajax-loader.gif"/>' +
	                     '</div>' +
	                     '<div style="margin: 20px 0px 0px 10px; float: right; width: 300px;">' +
	                     "Đăng nhập thành công ......<p>Chờ trong giây lát để chuyển hướng .........! </p></div></div>";
        $('.popup-bg').html(login_response); // Refers to 'status'
        setTimeout('goBack()', 2000);
    }
}
function goBack() {
    var url = document.URL;
    document.location = "" + url + "";
}
