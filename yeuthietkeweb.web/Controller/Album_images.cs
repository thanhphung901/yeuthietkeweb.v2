using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class Album_images
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        //Load tat ca img theo chuyen muc
        public List<Entity_img> Load_album_img(int _Catid)
        {
            try
            {
                List<Entity_img> l = new List<Entity_img>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            join d in db.ESHOP_NEWS_IMAGEs on a.NEWS_ID equals d.NEWS_ID
                            where (c.CAT_ID == _Catid || c.CAT_PARENT_PATH.Contains(_Catid.ToString()))
                            select new { b.NEWS_TITLE, d.NEWS_IMG_ID, d.NEWS_IMG_IMAGE1, d.NEWS_ID, c.CAT_NAME }).Distinct().OrderByDescending(n => n.NEWS_IMG_ID).ToList();
                foreach (var i in list)
                {
                    Entity_img img = new Entity_img();
                    img.NEWS_TITLE = i.NEWS_TITLE;
                    img.NEWS_IMG_ID = i.NEWS_IMG_ID;
                    img.NEWS_IMG_IMAGE1 = i.NEWS_IMG_IMAGE1;
                    img.NEWS_ID =Utils.CIntDef(i.NEWS_ID);
                    img.CAT_NAME = i.CAT_NAME;
                    l.Add(img);
                }
                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Getsotin(int catid)
        {
            int sotin = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == catid).ToList().Count > 0 ? Utils.CIntDef(db.ESHOP_CATEGORies.Where(n => n.CAT_ID == catid).First().CAT_PAGEITEM) : 0;
            return sotin;
        }
    }
}
