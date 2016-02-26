using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using vpro.functions;
using System.Web.UI;
using System.Web;
using Model;
namespace Controller
{
    public class Config
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        //Config meta
        public List<ESHOP_CONFIG> Config_meta()
        {
            try
            {
                var list = db.ESHOP_CONFIGs.OrderBy(c => c.CONFIG_ID).Take(1).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string Show_File_HTML(string HtmlFile, string path)
        {
            string pathFile;
            string strHTMLContent;
            string _result = string.Empty;
            pathFile = HttpContext.Current.Server.MapPath(path + HtmlFile);

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                _result = strHTMLContent;
            }
            return _result;
        }
        //Insert contact
        public bool Insert_contact(string name, string email, string title, string content, string address, string phone)
        {
            ESHOP_CONTACT add = new ESHOP_CONTACT
            {
                CONTACT_NAME = name,
                CONTACT_EMAIL = email,
                CONTACT_TITLE = title,
                CONTACT_CONTENT = content,
                CONTACT_PUBLISHDATE = DateTime.Now,
                CONTACT_ADDRESS = address,
                CONTACT_PHONE = phone,
                CONTACT_TYPE = 0
            };
            db.ESHOP_CONTACTs.InsertOnSubmit(add);
            db.SubmitChanges();
            return true;
        }
        //Config mail
        public List<ESHOP_EMAIL> Getemail(int stt)
        {
            //stt=2 contact
            var _ccMail = db.GetTable<ESHOP_EMAIL>().Where(c => c.EMAIL_STT == stt).ToList();
            return _ccMail;
        }
        //Counter
        public List<ESHOP_HITCOUNTER> counter()
        {
            var list = db.ESHOP_HITCOUNTERs.ToList();
            return list;
        }
        public void Addvister()
        {
            var _hitTotal = db.GetTable<ESHOP_CONFIG>();
            if (_hitTotal.ToList().Count > 0)
            {
                _hitTotal.ToList()[0].CONFIG_HITCOUNTER = _hitTotal.ToList()[0].CONFIG_HITCOUNTER + 1;
                db.SubmitChanges();
            }
            var list = db.GetTable<ESHOP_HITCOUNTER>().Where(a => (a.HIT_DATE.Value.Date - DateTime.Now.Date).Days == 0).ToList();

            if (list != null && list.Count > 0)
            {

                list[0].HIT_VALUE += 1;
                db.SubmitChanges();
            }
            else
            {
                ESHOP_HITCOUNTER eshop = new ESHOP_HITCOUNTER();
                eshop.HIT_VALUE = 1;
                eshop.HIT_DATE = DateTime.Now;
                db.ESHOP_HITCOUNTERs.InsertOnSubmit(eshop);
                db.SubmitChanges();
            }
        }
        public void Deletebasket(Guid _guid)
        {
            var _bas = from a in db.ESHOP_BASKETs
                       where a.CUSTOMER_OID == _guid
                       select a;

            db.ESHOP_BASKETs.DeleteAllOnSubmit(_bas);

            db.SubmitChanges();
        }
    }
}
