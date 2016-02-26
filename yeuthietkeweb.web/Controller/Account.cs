using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Web;
using System.Web.UI;
using vpro.functions;

namespace Controller
{
    public class Account
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        public  bool Login(string Email, string MatKhau)
        {
            var dangnhap = from a in db.ESHOP_CUSTOMERs
                           where a.CUSTOMER_EMAIL == Email && a.CUSTOMER_PW == MatKhau
                           select a;
            if (dangnhap.ToList().Count > 0)
            {
                Load_All_Cuss(Email);
                return true;
            }
            else
                return false;
        }
        private void Load_All_Cuss(string email)
        {
            try
            {
                var _cus = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_EMAIL == email);
                if (_cus.ToList().Count > 0)
                {
                    HttpContext.Current.Session["User_Name"] = _cus.ToList()[0].CUSTOMER_FULLNAME;
                    HttpContext.Current.Session["User_ID"] = _cus.ToList()[0].CUSTOMER_ID;
                    HttpContext.Current.Session["User_Phone"] = _cus.ToList()[0].CUSTOMER_PHONE1;
                    HttpContext.Current.Session["User_Email"] = _cus.ToList()[0].CUSTOMER_EMAIL;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public bool Check_email(string _email)
        {
            try
            {
                var _user = db.GetTable<ESHOP_CUSTOMER>().Where(u => u.CUSTOMER_EMAIL == _email.Trim());

                if (_user.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return true;
            }
        }
        public bool Check_username(string username)
        {
            try
            {
                var _user = db.GetTable<ESHOP_CUSTOMER>().Where(u => u.CUSTOMER_UN == username.Trim());

                if (_user.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return true;
            }
        }
        public bool Register(string _sFullName, string _Email, string _Pass, string _phone, string _add)
        {
            if (!Check_email(_Email))
            {
                ESHOP_CUSTOMER user = new ESHOP_CUSTOMER();
                user.CUSTOMER_EMAIL = _Email;
                user.CUSTOMER_PW = _Pass;
                user.CUSTOMER_FULLNAME = _sFullName;
                user.CUSTOMER_PUBLISHDATE = DateTime.Now;
                user.CUSTOMER_PHONE1 = _phone;
                user.CUSTOMER_ADDRESS = _add;
                user.CUSTOMER_SHOWTYPE = 0;
                db.ESHOP_CUSTOMERs.InsertOnSubmit(user);
                db.SubmitChanges();
                Load_All_Cuss(_Email);
                return true;
            }
            return false;
        }

        public List<ESHOP_CONFIG> config()
        {
            try
            {
                var _cus = db.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);
                if (_cus != null)
                    return _cus.ToList();
                return null;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public List<ESHOP_CUSTOMER> loadUser(int id)
        {
            try
            {
                var _cus = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == id);
                if (_cus != null)
                    return _cus.ToList();
                return null;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public bool updateUserInfo(int id, string name, string phone, string add)
        {
            try
            {
                var _vUser = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == id);
                if (_vUser.ToList().Count > 0)
                {
                    foreach (var i in _vUser)
                    {
                        i.CUSTOMER_FULLNAME = name;
                        i.CUSTOMER_PHONE1 = phone;
                        i.CUSTOMER_ADDRESS = add;
                        db.SubmitChanges();
                    }
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;
            }
        }
    }
}
