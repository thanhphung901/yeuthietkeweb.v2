using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Controller
{
    public class Register_email
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        private bool Checkemail(string email)
        {
            try
            {
                var list = db.ESHOP_MAIL_RECIVEs.Where(n => n.MAIL_NAME == email).ToList();
                if (list.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Add_email(string email)
        {
            if (Checkemail(email))
            {
                return false;
            }
            else
            {
                ESHOP_MAIL_RECIVE mail = new ESHOP_MAIL_RECIVE();
                mail.MAIL_NAME = email;
                mail.MAIL_ACTIVE = 1;
                db.ESHOP_MAIL_RECIVEs.InsertOnSubmit(mail);
                db.SubmitChanges();
                return true;
            }
        }
    }
}
