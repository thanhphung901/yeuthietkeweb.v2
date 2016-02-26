
function FormatNumber(obj) {
    var strvalue;
    if (eval(obj))
        strvalue = eval(obj).value;
    else
        strvalue = obj;
    var num;
    num = strvalue.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    num = Math.floor(num / 100).toString();
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
   num.substring(num.length - (4 * i + 3));
    //return (((sign)?'':'-') + num); 
    eval(obj).value = (((sign) ? '' : '-') + num);
}
function repStr(str) {
    var strResult = "";
    for (i = 0; i < str.length; i++)
        if ((str.charAt(i) != "$") && (str.charAt(i) != ",")) {
            strResult = strResult + str.charAt(i)
        }
    return strResult;
}

function RemoveUnicode(obj) {
    var str;
    if (eval(obj))
        str = eval(obj).value;
    else
        str = obj;
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    /*thay the 1 so ky tu dat biet*/
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|-+|–|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\“|\”|\&|\#|\[|\]|~|$|_/g, " ");
    /**/
    //str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
    str = str.replace(/\s+/g, "-");
    /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-  
    str = str.replace(/^\-+|\-+$/g, "");
    //cắt bỏ ký tự - ở đầu và cuối chuỗi 
    //eval(obj).value = str.toUpperCase();
    return str;
}

function ParseUrl(objsent, objreceive) {
    objreceive.value = RemoveUnicode(objsent);
}

function mask(str, textbox, loc, delim) {
    var locs = loc.split(',');
    for (var i = 0; i <= locs.length; i++) {
        for (var k = 0; k <= str.length; k++) {
            if (k == locs[i]) {
                if (str.substring(k, k + 1) != delim) {
                    str = str.substring(0, k) + delim + str.substring(k, str.length)
                }
            }
        }
    }

    textbox.value = str
}

function repStr(str) {
    var strResult = "";
    for (i = 0; i < str.length; i++)
        if ((str.charAt(i) != "$") && (str.charAt(i) != ",")) {
            strResult = strResult + str.charAt(i)
        }
    return strResult;
}

function number_format(number, decimals, dec_point, thousands_sep) {
    // * example 1: number_format(1234.5678, 2, '.', '');
    // * returns 1: 1234.57

    var n = number, c = isNaN(decimals = Math.abs(decimals)) ? 2 : decimals;
    var d = dec_point == undefined ? "," : dec_point;
    var t = thousands_sep == undefined ? "." : thousands_sep, s = n < 0 ? "-" : "";
    var i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;

    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
}

function auto_currency(id) {
    var variable = document.getElementById(id);
    var new_value = variable.value.replace(/\,/g, "");
    variable.style.textAlign = "right";
    variable.value = digit_grouping(new_value);
}