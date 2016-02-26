using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class Weblink
    {
        #region Declare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        public List<ESHOP_WEBLINK> Loadweblink()
        {
            try
            {
                var list = db.ESHOP_WEBLINKs.OrderByDescending(o => o.WEBSITE_LINKS_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ESHOP_WEBLINK GetweblinkById(int weblinkId)
        {
            try
            {
                var item = db.ESHOP_WEBLINKs.Single(n => n.WEBSITE_LINKS_ID == weblinkId);
                return item;
            }
            catch
            {
                return null;
            }
        }
    }
}
