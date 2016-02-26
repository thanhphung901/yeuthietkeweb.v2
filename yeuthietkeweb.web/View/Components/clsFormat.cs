using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Globalization;
namespace GiaNguyen.Components
{
    public class clsFormat
    {

        public string FormatMoney(object Expression)
        {
            try
            {
                string Money = String.Format("{0:0,0 VNĐ}", Expression);
                return Money.Replace(",", ".");
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }

        // Chuyển định dạng chuỗi ngày thành định dạng ngày
        public DateTime StrDateToDate(string dateInput, string formatInput)
        {
            var formatInfo = new DateTimeFormatInfo { ShortDatePattern = formatInput };
            return DateTime.Parse(dateInput, formatInfo);
        }

        // Chuyển định dạng ngày thành định dạng chuỗi ngày
        public string DateToStrDate(object dateInput, string formatOutput)
        {
            //return dateInput.ToString(formatOutput);
            return string.Format(formatOutput, dateInput);
        }

        // Chuyển định dạng chuỗi ngày thành định dạng chuỗi ngày
        public string StrDateToStrDate(string dateInput, string formatInput, string formatOutput)
        {
            var date = StrDateToDate(dateInput, formatInput);
            return DateToStrDate(date, formatOutput);
        }


        /// <summary>
        /// Cắt chuỗi url chuyển về Cat_seo_url
        /// </summary>
        /// <param name="s">đường dẫn url</param>
        /// <returns>Cat_seo_url</returns>
        public string CatChuoiURL(string s)
        {
            string[] sep = { "/" };
            string[] sep1 = { " " };
            string[] t1 = s.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            for (int i = 1; i < t1.Length; i++)
            {
                string[] t2 = t1[i].Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                if (t2.Length > 0)
                {
                    if (res.Length > 0)
                    {
                        res += "//";
                    }
                    res += t2[0];
                }
            }
            return res.Substring(0, res.Length - 5);
        }


        /// <summary>
        /// Chuyển chuỗi kiểu số thành chuỗi kiểu chữ
        /// </summary>
        /// <param name="str">mảng chứa đường dẫn kiểu số</param>
        /// <returns>đường dẫn kiểu chữ</returns>
        //public string Convert_Name(string[] str)
        //{
        //    string s = "";

        //    try
        //    {
        //        int _value = 0;

        //        for (int i = 1; i < str.Count(); i++)
        //        {
        //            _value = Utils.CIntDef(str[i]);

        //            var rausach = from r in db.ESHOP_CATEGORies
        //                          where r.CAT_ID == _value
        //                          select r.CAT_NAME;
        //            s += rausach.ToList()[0] + " > ";
        //        }
        //        return s;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //        return "";
        //    }
        //}

        /// <summary>
        /// Mã hóa mật khẩu
        /// </summary>
        /// <param name="sPassword">Mật khẩu</param>
        /// <returns>Mật khẩu đã mã hóa</returns>
        public string MaHoaMatKhau(string Password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedDataBytes = md5Hasher.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(Password));
            string EncryptPass = Convert.ToBase64String(hashedDataBytes);
            return EncryptPass;
        }

        public string TaoChuoiTuDong(int dodai)
        {
            string _allowedChars = "abcdefghijk0123456789mnopqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[dodai];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < dodai; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static string ClearUnicode(string SourceString)
        {

            SourceString = Regex.Replace(SourceString, "[ÂĂÀÁẠẢÃÂẦẤẬẨẪẰẮẶẲẴàáạảãâầấậẩẫăằắặẳẵ]", "a");
            SourceString = Regex.Replace(SourceString, "[ÈÉẸẺẼÊỀẾỆỂỄèéẹẻẽêềếệểễ]", "e");
            SourceString = Regex.Replace(SourceString, "[IÌÍỈĨỊìíịỉĩ]", "i");
            SourceString = Regex.Replace(SourceString, "[ÒÓỌỎÕÔỒỐỔỖỘƠỜỚỞỠỢòóọỏõôồốộổỗơờớợởỡ]", "o");
            SourceString = Regex.Replace(SourceString, "[ÙÚỦŨỤƯỪỨỬỮỰùúụủũưừứựửữ]", "u");
            SourceString = Regex.Replace(SourceString, "[ỲÝỶỸỴỳýỵỷỹ]", "y");
            SourceString = Regex.Replace(SourceString, "[đĐ]", "d");

            return SourceString;
        }

        private static string GetFirstCharacter(string s)
        {
            string[] chuoi_cat = s.Split(' ');
            string _sResult = "";
            for (int i = 0; i <= chuoi_cat.Length - 1; i++)
            {
                _sResult += chuoi_cat[i].Substring(0, 1).ToLower();
            }
            return _sResult;
        }

        private string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
        private string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
        // Hàm đọc số thành chữ
        public string DocTienBangChu(string GiaTien, string strTail)
        {
            if (GiaTien.Contains("."))
            {
                string[] s = GiaTien.Split('.');
                if (s.ToList().Count > 1)
                {
                    long SoTien = Utils.CLngDef(s[0]);
                    int lan, i;
                    long so;
                    string KetQua = "", tmp = "";
                    int[] ViTri = new int[6];
                    if (SoTien < 0) return "Số tiền âm !";
                    if (SoTien == 0) return "Không đồng !";
                    if (SoTien > 0)
                    {
                        so = SoTien;
                    }
                    else
                    {
                        so = -SoTien;
                    }
                    //Kiểm tra số quá lớn
                    if (SoTien > 8999999999999999)
                    {
                        SoTien = 0;
                        return "";
                    }
                    ViTri[5] = (int)(so / 1000000000000000);
                    so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
                    ViTri[4] = (int)(so / 1000000000000);
                    so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
                    ViTri[3] = (int)(so / 1000000000);
                    so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
                    ViTri[2] = (int)(so / 1000000);
                    ViTri[1] = (int)((so % 1000000) / 1000);
                    ViTri[0] = (int)(so % 1000);
                    if (ViTri[5] > 0)
                    {
                        lan = 5;
                    }
                    else if (ViTri[4] > 0)
                    {
                        lan = 4;
                    }
                    else if (ViTri[3] > 0)
                    {
                        lan = 3;
                    }
                    else if (ViTri[2] > 0)
                    {
                        lan = 2;
                    }
                    else if (ViTri[1] > 0)
                    {
                        lan = 1;
                    }
                    else
                    {
                        lan = 0;
                    }
                    for (i = lan; i >= 0; i--)
                    {
                        tmp = DocSo3ChuSo(ViTri[i]);
                        KetQua += tmp;
                        if (ViTri[i] != 0) KetQua += Tien[i];
                        if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
                    }
                    if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
                    string ThapPhan = DocSo3ChuSo(Utils.CIntDef(s[1]));
                    KetQua = KetQua.Trim() + " phẩy " + ThapPhan + strTail;
                    return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
                }
                else
                {
                    return "Không";
                }
            }
            else
            {
                long SoTien = Utils.CLngDef(GiaTien);
                int lan, i;
                long so;
                string KetQua = "", tmp = "";
                int[] ViTri = new int[6];
                if (SoTien < 0) return "Số tiền âm !";
                if (SoTien == 0) return "Không đồng !";
                if (SoTien > 0)
                {
                    so = SoTien;
                }
                else
                {
                    so = -SoTien;
                }
                //Kiểm tra số quá lớn
                if (SoTien > 8999999999999999)
                {
                    SoTien = 0;
                    return "";
                }
                ViTri[5] = (int)(so / 1000000000000000);
                so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
                ViTri[4] = (int)(so / 1000000000000);
                so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
                ViTri[3] = (int)(so / 1000000000);
                so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
                ViTri[2] = (int)(so / 1000000);
                ViTri[1] = (int)((so % 1000000) / 1000);
                ViTri[0] = (int)(so % 1000);
                if (ViTri[5] > 0)
                {
                    lan = 5;
                }
                else if (ViTri[4] > 0)
                {
                    lan = 4;
                }
                else if (ViTri[3] > 0)
                {
                    lan = 3;
                }
                else if (ViTri[2] > 0)
                {
                    lan = 2;
                }
                else if (ViTri[1] > 0)
                {
                    lan = 1;
                }
                else
                {
                    lan = 0;
                }
                for (i = lan; i >= 0; i--)
                {
                    tmp = DocSo3ChuSo(ViTri[i]);
                    KetQua += tmp;
                    if (ViTri[i] != 0) KetQua += Tien[i];
                    if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
                }
                if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
                KetQua = KetQua.Trim() + strTail;
                return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
            }
        }

        // Hàm đọc số có 3 chữ số
        private string DocSo3ChuSo(int baso)
        {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int)(baso / 100);
            chuc = (int)((baso % 100) / 10);
            donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0)
            {
                KetQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                KetQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh";
            }
            if (chuc == 1) KetQua += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        KetQua += " mốt";
                    }
                    else
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    else
                    {
                        KetQua += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
            }
            return KetQua;
        }

        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static string Encrypt(string cleanString, string salt)
        {
            System.Text.Encoding encoding;
            byte[] clearBytes = null;
            byte[] hashedBytes = null;
            encoding = System.Text.Encoding.GetEncoding("unicode");
            clearBytes = encoding.GetBytes(salt.ToLower().Trim() + cleanString.Trim());
            hashedBytes = MD5hash(clearBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public static string CreateSalt()
        {
            byte[] bytSalt = new byte[9];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytSalt);
            return Convert.ToBase64String(bytSalt);
        }

        public static byte[] MD5hash(byte[] data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            return result;
        }

        public static string getOnClickScript(string ctrlName)
        {
            string strScript = "";
            strScript = "if(event.which || event.keyCode || event.charCode){if ((event.charCode == 13) || (event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + ctrlName + "').focus();return false;}} else {return true}; ";
            return strScript;
        }

        public static string getSubmitScript(string btnName)
        {
            string strScript = "";
            strScript = "if(event.which || event.keyCode || event.charCode){if ((event.charCode == 13) || (event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnName + "').click();return false;}} else {return true}; ";
            return strScript;
        }
        public void Show(string message)
        {
            string cleanMessage = message.Replace("'", "\'");
            Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
            }
        }
       
    }
}