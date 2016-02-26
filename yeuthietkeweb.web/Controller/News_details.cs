using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Web;
using System.Web.UI;
using vpro.functions;
using System.IO;

namespace Controller
{
    public class News_details
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        //Load details
        public List<ESHOP_NEW> Load_details(string _sNews_seo_url)
        {
            try
            {
                var list = db.ESHOP_NEWs.Where(n => n.NEWS_SEO_URL == _sNews_seo_url).ToList();
                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Showfile html
        public string Showfilehtm(string _sCat_Seo_Url, string _sNews_seo_url)
        {
            try
            {
                string _result = string.Empty;
                int _newsID=0;
                if (!string.IsNullOrEmpty(_sCat_Seo_Url))
                {
                    var _vNewsID = db.GetTable<ESHOP_NEWS_CAT>().Where(a => a.ESHOP_CATEGORy.CAT_SEO_URL == _sCat_Seo_Url);
                    _newsID = _vNewsID.ToList().Count > 0 ? Utils.CIntDef(_vNewsID.ToList()[0].NEWS_ID) : 0;
                    
                }
                else
                {
                    var getid = db.ESHOP_NEWs.Where(n => n.NEWS_SEO_URL == _sNews_seo_url).ToList();
                    _newsID = getid.Count > 0 ? Utils.CIntDef(getid[0].NEWS_ID) : 0;
                }

                string pathFile;
                string strHTMLContent;

                if (_newsID > 0)
                {

                    var newsInfo = db.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == _newsID);

                    if (newsInfo.ToList().Count > 0)
                    {

                        pathFile =HttpContext.Current.Server.MapPath(PathFiles.GetPathNews(_newsID) + "/" + newsInfo.ToList()[0].NEWS_FILEHTML);

                        if ((File.Exists(pathFile)))
                        {
                            StreamReader objNewsReader;
                            objNewsReader = new StreamReader(pathFile);
                            strHTMLContent = objNewsReader.ReadToEnd();
                            objNewsReader.Close();
                            _result = strHTMLContent;
                        }
                        
                    }
                    
                }
                return _result;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        // Load other news
        public List<News_details_entity> Load_othernews(string _sNews_Seo_Url)
        {
            try
            {
                List<News_details_entity> l = new List<News_details_entity>();
                if (_sNews_Seo_Url != "")
                {
                    var s = (from c in db.ESHOP_NEWS_CATs
                             join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                             join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                             where a.NEWS_SEO_URL == _sNews_Seo_Url
                             select new { b.CAT_ID}).ToList();
                    var _tinTucKhac = (from c in db.ESHOP_NEWS_CATs
                                       join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                                       join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                                       where a.NEWS_SEO_URL != _sNews_Seo_Url && b.CAT_ID == s[0].CAT_ID
                                       select new { a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE3,a.NEWS_PRICE1, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD, a.NEWS_PUBLISHDATE, c.ESHOP_CATEGORy.CAT_SEO_URL }).OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).Take(9).Distinct();
                    if (_tinTucKhac.ToList().Count > 0)
                    {
                        foreach (var i in _tinTucKhac)
                        {
                            News_details_entity pro = new News_details_entity();
                            pro.NEWS_ID = i.NEWS_ID;
                            pro.NEWS_TITLE = i.NEWS_TITLE;
                            pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                            pro.NEWS_DESC = i.NEWS_DESC;
                            pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                            pro.NEWS_URL = i.NEWS_URL;
                            pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                            pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                            pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                            pro.CAT_SEO_URL = i.CAT_SEO_URL;
                            l.Add(pro);
                        }
                    }
                  
                }
                return l;
               
            }
            catch (Exception ex)
            {

                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        //Get viewmore
        public string Get_ViewMore(int _newsID)
        {
            try
            {
                string _result = string.Empty;
                var _vViewmore = from a in db.ESHOP_NEWS_CATs
                                 join b in db.ESHOP_CATEGORies on a.CAT_ID equals b.CAT_ID
                                 where a.NEWS_ID == _newsID
                                 select b;
                if (_vViewmore.ToList().Count > 0)
                {
                    _result = string.IsNullOrEmpty(Utils.CStrDef(_vViewmore.First().CAT_URL)) ? "/" + _vViewmore.First().CAT_SEO_URL + ".html" : _vViewmore.First().CAT_URL;
                }
                return _result;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
       //Insert comment news detail
        public int Getid(string _sCat_Seo_Url, string _sNews_seo_url)
        {
            int _News_id = 0;
            if (!string.IsNullOrEmpty(_sCat_Seo_Url))
            {
                var list = (from a in db.ESHOP_CATEGORies
                            join b in db.ESHOP_NEWS_CATs on a.CAT_ID equals b.CAT_ID
                            select new { b.NEWS_ID }).ToList();
                if (list.Count > 0)
                {
                    _News_id = Utils.CIntDef(list[0].NEWS_ID);
                }
            }
            else
            {
                var list = db.ESHOP_NEWs.Where(n => n.NEWS_SEO_URL == _sNews_seo_url).Select(n => new { n.NEWS_ID}).ToList();
                if (list.Count > 0)
                {
                    _News_id = Utils.CIntDef(list[0].NEWS_ID);
                }
            }
            return _News_id;
        }
        public bool Inser_comment_News(string _sCat_Seo_Url, string _sNews_seo_url, string name, string email, string content)
        {
            int _Newsid = Getid(_sCat_Seo_Url, _sNews_seo_url);
            ESHOP_NEWS_COMMENT cm = new ESHOP_NEWS_COMMENT();
            cm.NEWS_ID = _Newsid;
            cm.COMMENT_CHECK = 0;
            cm.COMMENT_STATUS = 0;
            cm.COMMENT_NAME = name;
            cm.COMMENT_EMAIL = email;
            cm.COMMENT_CONTENT = content;
            cm.COMMENT_PUBLISHDATE = DateTime.Now;
            db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(cm);
            db.SubmitChanges();
            return true; 
        }
        //Load title categories
        public class Entity_title
        {
            public string Cat_name { get;set;}
            public string News_title { get; set; }
        }
        public List<Entity_title> gettitle(string _sCatSeoUrl, string _sNews_Seo_Url)
        {
            try
            {
                List<Entity_title> l = new List<Entity_title>();
                if (_sCatSeoUrl != "")
                {
                    var list = (from a in db.ESHOP_NEWS_CATs
                                join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                                join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                                where c.CAT_SEO_URL == _sCatSeoUrl
                                select new { b.NEWS_TITLE, c.CAT_NAME }).ToList();
                    foreach (var i in list)
                    {
                        Entity_title t = new Entity_title();
                        t.Cat_name = i.CAT_NAME;
                        t.News_title = i.NEWS_TITLE;
                        l.Add(t);
                    }
                }
                else
                {
                    var list = (from a in db.ESHOP_NEWS_CATs
                                join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                                join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                                where b.NEWS_SEO_URL == _sNews_Seo_Url
                                select new { b.NEWS_TITLE, c.CAT_NAME }).ToList();
                    foreach (var i in list)
                    {
                        Entity_title t = new Entity_title();
                        t.Cat_name = i.CAT_NAME;
                        t.News_title = i.NEWS_TITLE;
                        l.Add(t);
                    }
                }
                return l;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get _News_seo_url neu hien thi tuc thoi
        public string Get_News_seo_url(string _sCatSeoUrl)
        {
            try
            {
                var list = (from a in db.ESHOP_NEWs
                            join b in db.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                            where b.ESHOP_CATEGORy.CAT_SEO_URL == _sCatSeoUrl
                            select new { a.NEWS_SEO_URL }).ToList();
                if (list.Count > 0)
                {
                    return list[0].NEWS_SEO_URL;
                }
                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
