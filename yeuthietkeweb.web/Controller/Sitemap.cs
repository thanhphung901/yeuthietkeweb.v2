using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{

    public class Sitemap
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        public List<ESHOP_CATEGORy> Load_sitemap()
        {
            try
            {
                var _Cats = db.GetTable<ESHOP_CATEGORy>().Where(c => c.CAT_STATUS >0).OrderBy(c => c.CAT_NAME).OrderByDescending(c => c.CAT_ORDER);
                return _Cats.ToList();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public List<ESHOP_CATEGORy> BuildSubTree(int Cat_Parent_Id)
        {
            try
            {

                var _Cats = db.GetTable<ESHOP_CATEGORy>().Where(c => c.CAT_STATUS > 0 && c.CAT_PARENT_ID == Cat_Parent_Id).OrderBy(c => c.CAT_NAME).OrderByDescending(c => c.CAT_ORDER);
                return _Cats.ToList();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
    }
}
