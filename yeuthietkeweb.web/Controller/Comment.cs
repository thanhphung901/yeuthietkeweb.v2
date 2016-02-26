using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Controller
{
    public class Comment
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        public bool Addcomment(string desc, int news_id)
        {
            ESHOP_NEWS_COMMENT cm = new ESHOP_NEWS_COMMENT();
            cm.NEWS_ID = news_id;
            cm.COMMENT_CONTENT = desc;
            cm.COMMENT_PUBLISHDATE = DateTime.Now;
            db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(cm);
            db.SubmitChanges();
            return true;
        }
        public List<ESHOP_NEWS_COMMENT> Load_comment(string _url)
        {
            try
            {
                var show = (from a in db.ESHOP_NEWs
                            join b in db.ESHOP_NEWS_COMMENTs on a.NEWS_ID equals b.NEWS_ID
                            where a.NEWS_SEO_URL == _url
                            select b).OrderByDescending(n => n.COMMENT_PUBLISHDATE).ToList();
                return show;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
