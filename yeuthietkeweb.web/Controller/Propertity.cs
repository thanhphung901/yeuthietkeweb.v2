using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.Web.UI;

namespace Controller
{
    public class Propertity
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        //Menu
        public List<ESHOP_CATEGORy> Loadmenu(int position, int limit, int rank, int lang, int showfooter)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && (n.CAT_POSITION == position || position == -1) && n.CAT_RANK == rank 
                    && (n.CAT_SHOWFOOTER == 1 || showfooter == -1)
                    && (n.CAT_LANGUAGE == lang || lang == -1)
                    ).OrderByDescending(n => n.CAT_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_CATEGORy> Loadmenu1(int type, int per, int rank, int limit)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_TYPE == type && n.CAT_PERIOD == per && n.CAT_RANK == rank).OrderByDescending(n => n.CAT_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Menu cap 2
        public IQueryable<ESHOP_CATEGORy> Menu2(object catid)
        {
            int id=Utils.CIntDef(catid);
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_PARENT_ID == id).OrderByDescending(n => n.CAT_ORDER);
            return list.ToList().Count>0 ? list:null;
        }
        public List<ESHOP_CATEGORy> Loadmenu3(int type, int rank, int limit)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_TYPE == type && n.CAT_RANK == rank).OrderByDescending(n => n.CAT_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //get shop catalogry by ID
        public ESHOP_CATEGORy GetCatalogryByID(object catid)
        {
            int id = Utils.CIntDef(catid);
            var item = db.ESHOP_CATEGORies.FirstOrDefault(n => n.CAT_ID == id);
            return item;
        }
        //Active menu
        #region Active menu
        public string Get_Cat_Seo_Url(string _seoUrl)
        {
            var rausach = from p in db.ESHOP_CATEGORies
                          where p.CAT_SEO_URL == _seoUrl && p.CAT_STATUS == 1
                          select p;
            int _catID = -1;

            if (rausach.ToList().Count > 0)
            {
                string cat_parent_path = rausach.ToList()[0].CAT_PARENT_PATH;

                string[] str = cat_parent_path.Split(',');

                if (str.Count() > 1)
                {
                    _catID = Utils.CIntDef(str[1]);
                }
                else
                {
                    _catID = Utils.CIntDef(rausach.ToList()[0].CAT_ID);
                }
            }

            else
            {
                var rausach1 = (from nc in db.ESHOP_NEWS_CATs
                                join c in db.ESHOP_CATEGORies on nc.CAT_ID equals c.CAT_ID
                                join n in db.ESHOP_NEWs on nc.NEWS_ID equals n.NEWS_ID
                                where n.NEWS_SEO_URL == _seoUrl && c.CAT_STATUS == 1
                                orderby c.CAT_RANK descending
                                select new
                                {
                                    c.CAT_PARENT_PATH,
                                    c.CAT_NAME,
                                    c.CAT_DESC,
                                    c.CAT_ID
                                }).Take(1);

                if (rausach1.ToList().Count > 0)
                {
                    string cat_parent_path_Max = rausach1.ToList()[0].CAT_PARENT_PATH;

                    string[] str = cat_parent_path_Max.Split(',');
                    if (str.Count() > 1)
                    {
                        _catID = Utils.CIntDef(str[1]);
                    }
                    else
                    {
                        _catID = Utils.CIntDef(rausach1.ToList()[0].CAT_ID);
                    }
                }
            }
            var _cat_Seo_Url = db.GetTable<ESHOP_CATEGORy>().Where(a => a.CAT_ID == _catID && a.CAT_STATUS == 1);
            if (_cat_Seo_Url.ToList().Count > 0)
            {
                string _catSeoUrl = _cat_Seo_Url.ToList()[0].CAT_SEO_URL;
                return _catSeoUrl;
            }
            else
            {
                return null;
            }
        }
        public string GetStyleActive(object Cat_Seo_Url, object Cat_Url)
        {
            try
            {
                if (!string.IsNullOrEmpty(Utils.CStrDef(HttpContext.Current.Request.QueryString["curl"])))
                {
                    string _curl = Utils.CStrDef(HttpContext.Current.Request.QueryString["curl"]);

                    var _cat = db.GetTable<ESHOP_CATEGORy>().Where(a => a.CAT_SEO_URL == _curl && a.CAT_STATUS == 1);
                    if (_cat.ToList().Count > 0)
                    {
                        if (_cat.ToList()[0].CAT_RANK == 1)
                        {
                            if (Utils.CStrDef(HttpContext.Current.Request.QueryString["curl"]) == Utils.CStrDef(Cat_Seo_Url))
                            {
                                return "active";
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            int _catID = -1;
                            string[] str = Utils.CStrDef(_cat.ToList()[0].CAT_PARENT_PATH).Split(',');

                            if (str.Count() > 1)
                            {
                                _catID = Utils.CIntDef(str[1]);

                                var _cat_Seo_Url = db.GetTable<ESHOP_CATEGORy>().Where(a => a.CAT_ID == _catID && a.CAT_STATUS == 1);
                                if (_cat_Seo_Url.ToList().Count > 0)
                                {
                                    string _catSeoUrl = _cat_Seo_Url.ToList()[0].CAT_SEO_URL;

                                    if (_catSeoUrl == Utils.CStrDef(Cat_Seo_Url))
                                    {
                                        return "active";
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //string _seoUrl = fm.CatChuoiURL(Utils.CStrDef(Request.RawUrl));
                    string _seoUrl = Utils.CStrDef(HttpContext.Current.Request.QueryString["purl"]);
                    if (!string.IsNullOrEmpty(_seoUrl))
                    {
                        string _catSeoUrl = Get_Cat_Seo_Url(_seoUrl);
                        if (_catSeoUrl == Utils.CStrDef(Cat_Seo_Url))
                        {
                            return "active";
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (Utils.CStrDef(HttpContext.Current.Request.RawUrl) == Utils.CStrDef(Cat_Url))
                        {
                            return "active";
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                //}
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        #endregion
        //Danh muc menu
        public List<ESHOP_CATEGORy> Load_danhmuc(int type, int rank, int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_TYPE == type && n.CAT_RANK == rank && n.CAT_STATUS == 1
                && (n.CAT_LANGUAGE == lang || lang == -1)).OrderByDescending(n => n.CAT_ORDER).ToList();
            return list;
        }
        public List<ESHOP_CATEGORy> Load_danhmuc_position(int type, int rank,int postion)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_TYPE == type && n.CAT_RANK == rank && n.CAT_STATUS == 1&&n.CAT_POSITION==postion).OrderByDescending(n => n.CAT_ORDER).ToList();
            return list;
        }
        public int getNameCatID(string catSeoUrl)
        {
            int temp = 0;
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == catSeoUrl).ToList();
            if (list.Count > 0)
                temp = list[0].CAT_ID;
            return temp;
        }
        public string getNameCat(int catParentId)
        {
            string temp = "";
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == catParentId).ToList();
            if (list.Count > 0)
                temp = list[0].CAT_NAME;
            return temp;
        }
        public List<ESHOP_CATEGORy> Load_danhmuc_chabyID(int idCat)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_PARENT_ID == idCat).OrderByDescending(n => n.CAT_ORDER).ToList();
            if (list.Count == 0)
            {
                var lTemp = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == idCat).Single();
                if (lTemp != null)
                {
                    list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_PARENT_ID == lTemp.CAT_PARENT_ID).OrderByDescending(n => n.CAT_ORDER).ToList();
                }
            }
            return list;
        }
        public int Getactive_menudanhmuc(string cat_seo, string news_seo)
        {
            int cat_id = 0;
            if (!string.IsNullOrEmpty(cat_seo))
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == cat_seo).ToList();
                if (list.Count > 0)
                {
                    if (list[0].CAT_RANK > 2)
                    {
                        string[] a = list[0].CAT_PARENT_PATH.Split(',');
                        cat_id = Utils.CIntDef(a[2]);
                    }
                    else cat_id = list[0].CAT_ID;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(news_seo))
                {
                    var list = (from a in db.ESHOP_NEWs
                                join b in db.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                                join c in db.ESHOP_CATEGORies on b.CAT_ID equals c.CAT_ID
                                where a.NEWS_SEO_URL == news_seo
                                select new { c.CAT_ID, c.CAT_RANK, c.CAT_PARENT_PATH }).ToList();
                    if (list.Count > 0)
                    {
                        if (list[0].CAT_RANK > 2)
                        {
                            string[] a = list[0].CAT_PARENT_PATH.Split(',');
                            cat_id = Utils.CIntDef(a[2]);
                        }
                        else cat_id = list[0].CAT_ID;
                    }
                }
            }
            return cat_id;
        }
        //Logo-sologan
        public List<ESHOP_BANNER> Load_logo_and_sologan(int limit)
        {
            try
            {
                 var _logoSlogan = (from a in db.ESHOP_BANNERs
                               select a).Take(limit).ToList();
                 return _logoSlogan;
            }
            catch
            {
                return null;
            }
        }

        public List<ESHOP_ONLINE> Load_Online()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Logo or sologan
        public List<ESHOP_BANNER> Load_logo_or_sologan(string field,int limit)
        {
            try
            {
                var list = db.ESHOP_BANNERs.Where(n => n.BANNER_FIELD1 == field).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // Silder
        public List<ESHOP_AD_ITEM> Load_slider(int position, int limit)
        {
            try
            {
                var list = db.ESHOP_AD_ITEMs.Where(n => n.AD_ITEM_POSITION == position).OrderByDescending(n => n.AD_ITEM_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<ESHOP_AD_ITEM> Load_slider(object catid, int position, int limit, int lang)
        {
            try
            {
                var list = (from a in db.ESHOP_AD_ITEM_CATs
                            join b in db.ESHOP_AD_ITEMs on a.AD_ITEM_ID equals b.AD_ITEM_ID
                            where b.AD_ITEM_POSITION == position && b.AD_ITEM_LANGUAGE == lang && (a.CAT_ID == Utils.CIntDef(catid, 0) || catid == null)
                            select b).Distinct().Take(limit).ToList();
                //db.ESHOP_AD_ITEMs.Where(n => n.AD_ITEM_POSITION == position && n.AD_ITEM_LANGUAGE == lang).Take(limit).ToList();
                return list;
            }
            catch
            {
                return null;
            }
        }
        //Holine
        public List<ESHOP_ONLINE> Load_hotline()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n=>n.ONLINE_TYPE==0).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //ho trợ
        public List<ESHOP_ONLINE> Load_hotro()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 7).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //mail
        public List<ESHOP_ONLINE> Load_mail()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 8).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //yahoo
        public List<ESHOP_ONLINE> Load_yahoo()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 1).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //skype
        public List<ESHOP_ONLINE> Load_skype()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 2).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_ONLINE> Load_socialnetwork()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE >= 0 && n.ONLINE_TYPE <= 6).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //face
        public List<ESHOP_ONLINE> Load_facebook()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 3).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }//g+
        public List<ESHOP_ONLINE> Load_google()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 4).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_ONLINE> Load_Twitter()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 5).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //youtude
        public List<ESHOP_ONLINE> Load_youtube()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 6).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //zing
        public List<ESHOP_ONLINE> Load_Zing()
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 9).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        //Video
        public string Load_video()
        {
            string _sResult = "";
            var _vGetVideo = db.GetTable<Product>().Take(1).OrderByDescending(a => a.product_id);

            if (_vGetVideo.ToList().Count > 0)
            {
                _sResult += "<iframe style='display: block; margin-left: auto; margin-right: auto; width:100%;'";
                _sResult += " src='http://www.youtube.com/embed/" + Get_Embed(_vGetVideo.First().product_name) + "?rel=0' frameborder='0' width='100%'></iframe>";
            }
            return _sResult;
        }
        private string Get_Embed(string s)
        {
            try
            {
                return s.Substring(s.Length - 11, 11);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        //Weblink
        public List<ESHOP_WEBLINK> Load_weblink()
        {
            var list = db.ESHOP_WEBLINKs.ToList();
            return list;
        }
        //Path
        #region Path

        /// <summary>
        /// Lấy đường dẫn và ghi chú về sản phẩm
        /// </summary>
        public string Getpath()
        {
            try
            {
                string _result = string.Empty;
                string cat_seo_url = CatChuoiURL(Utils.CStrDef(HttpContext.Current.Request.RawUrl));
                if (cat_seo_url.Contains("html?p"))
                {
                    string[] a = cat_seo_url.Split('?');
                    cat_seo_url = a[0].Substring(0, a[0].Length - 5);
                }
                var rausach = from p in db.ESHOP_CATEGORies
                              where p.CAT_SEO_URL == cat_seo_url && p.CAT_STATUS == 1
                              select p;

                if (rausach.ToList().Count > 0)
                {

                    string cat_parent_path = rausach.ToList()[0].CAT_PARENT_PATH;

                    string[] str = cat_parent_path.Split(',');

                    if (str.Count() > 1)
                    {
                        _result = Convert_Name(str) + "  &raquo;  <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a>";
                    }
                    else
                    {
                        if (rausach.ToList()[0].CAT_SHOWITEM > 0)
                        {
                            _result = "  &raquo; <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a> ";
                        }
                        else
                        {
                            _result = "  &raquo; <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a> ";
                        }
                    }
                }

                else
                {
                    var rausach1 = (from nc in db.ESHOP_NEWS_CATs
                                    join c in db.ESHOP_CATEGORies on nc.CAT_ID equals c.CAT_ID
                                    join n in db.ESHOP_NEWs on nc.NEWS_ID equals n.NEWS_ID
                                    where n.NEWS_SEO_URL == cat_seo_url && c.CAT_STATUS == 1
                                    orderby c.CAT_RANK descending
                                    select c).Take(1);
                    if (rausach1.ToList().Count > 0)
                    {
                        string cat_parent_path_Max = rausach1.ToList()[0].CAT_PARENT_PATH;

                        string[] str = cat_parent_path_Max.Split(',');
                        if (str.Count() > 1)
                        {
                            _result = Convert_Name(str) + "  &raquo; <a href='/" + rausach1.ToList()[0].CAT_SEO_URL + ".html'>" + rausach1.ToList()[0].CAT_NAME + "</a>";
                        }
                        else
                        {
                            if (rausach1.ToList()[0].CAT_SHOWITEM > 0)
                            {
                                _result = "  &raquo; <a href='/" + rausach1.ToList()[0].CAT_SEO_URL + ".html'>" + rausach1.ToList()[0].CAT_NAME + "</a> ";
                            }
                            else
                            {
                                _result = "  &raquo; <a href='/" + rausach1.ToList()[0].CAT_SEO_URL + ".html'>" + rausach1.ToList()[0].CAT_NAME + "</a> ";
                            }
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

        /// <summary>
        /// Chuyển chuỗi kiểu số thành chuỗi kiểu chữ
        /// </summary>
        /// <param name="str">mảng chứa đường dẫn kiểu số</param>
        /// <returns>đường dẫn kiểu chữ</returns>
        public string Convert_Name(string[] str)
        {
            string s = "";

            try
            {
                int _value = 0;

                for (int i = 1; i < str.Count(); i++)
                {
                    _value = Utils.CIntDef(str[i]);

                    var rausach = from r in db.ESHOP_CATEGORies
                                  where r.CAT_ID == _value && r.CAT_STATUS == 1
                                  select r;
                    //s += rausach.ToList()[0] + " > ";
                    s += "  &raquo; <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a> ";
                }
                return s;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        private string CatChuoiURL(string s)
        {
            string[] sep = { "/" };
            string[] sep1 = { " " };
            string[] t1 = s.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            for (int i = (t1.Length>1 ? 1 : 0); i < t1.Length; i++)
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
        #endregion
        //Total product
        public int Total_product()
        {
            try
            {
                var list = db.ESHOP_NEWs.Where(n => n.NEWS_TYPE == 1).ToList();
                return list.Count;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //demluotxem
        public void demluotxem(int _idnews)
        {
            var get = db.GetTable<ESHOP_NEW>().Where(a => a.NEWS_ID == _idnews).ToList();
            if (get.Count > 0)
            {
                get[0].NEWS_COUNT++;
                db.SubmitChanges();
            }
        }

        //brank
        public List<ESHOP_BRAND> Load_Brand()
        {
            var list = db.ESHOP_BRANDs.Where(n => n.ISACTIVE == 1).OrderByDescending(n => n.NAME).OrderByDescending(n => n.ORDERBY).ToList();
            return list;
        }
        //brank
        public ESHOP_BRAND GetBrandById(int id)
        {
            try
            {
                var item = db.ESHOP_BRANDs.Single(n => n.ID == id);
                return item;
            }
            catch
            {
                return null;
            }
        }
    }
}
