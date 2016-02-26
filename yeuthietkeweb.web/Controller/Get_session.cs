using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Controller
{
    public class Get_session
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        public void LoadCatInfo(string _catSeoUrl)
        {
            try
            {
                
                var cats = db.GetTable<ESHOP_CATEGORy>().Where(c => c.CAT_SEO_URL == _catSeoUrl);

                if (cats.ToList().Count > 0)
                {

                    #region Bind Cat Info

                    HttpContext.Current.Session["Cat_access"] = cats.ToList()[0].CAT_ACCESS;
                    HttpContext.Current.Session["Cat_code"] = cats.ToList()[0].CAT_CODE;
                    HttpContext.Current.Session["Cat_count"] = cats.ToList()[0].CAT_COUNT;
                    HttpContext.Current.Session["Cat_field1"] = cats.ToList()[0].CAT_FIELD1;
                    HttpContext.Current.Session["Cat_field2"] = cats.ToList()[0].CAT_FIELD2;
                    HttpContext.Current.Session["Cat_field3"] = cats.ToList()[0].CAT_FIELD3;
                    HttpContext.Current.Session["Cat_field4"] = cats.ToList()[0].CAT_FIELD4;
                    HttpContext.Current.Session["Cat_field5"] = cats.ToList()[0].CAT_FIELD5;
                    HttpContext.Current.Session["Cat_history"] = cats.ToList()[0].CAT_HISTORY;
                    HttpContext.Current.Session["Cat_id"] = cats.ToList()[0].CAT_ID;
                    HttpContext.Current.Session["Cat_image1"] = cats.ToList()[0].CAT_IMAGE1;
                    HttpContext.Current.Session["Cat_image2"] = cats.ToList()[0].CAT_IMAGE2;
                    HttpContext.Current.Session["Cat_language"] = cats.ToList()[0].CAT_LANGUAGE;
                    HttpContext.Current.Session["Cat_name"] = cats.ToList()[0].CAT_NAME;
                    HttpContext.Current.Session["Cat_order"] = cats.ToList()[0].CAT_ORDER;
                    HttpContext.Current.Session["Cat_pageitem"] = cats.ToList()[0].CAT_PAGEITEM;
                    HttpContext.Current.Session["Cat_parent_id"] = cats.ToList()[0].CAT_PARENT_ID;
                    HttpContext.Current.Session["Cat_parent_path"] = cats.ToList()[0].CAT_PARENT_PATH;
                    HttpContext.Current.Session["Cat_period"] = cats.ToList()[0].CAT_PERIOD;
                    HttpContext.Current.Session["Cat_period_order"] = cats.ToList()[0].CAT_PERIOD_ORDER;
                    HttpContext.Current.Session["Cat_position"] = cats.ToList()[0].CAT_POSITION;
                    HttpContext.Current.Session["Cat_rank"] = cats.ToList()[0].CAT_RANK;
                    HttpContext.Current.Session["Cat_rowitem"] = cats.ToList()[0].CAT_ROWITEM;
                    HttpContext.Current.Session["Cat_seo_desc"] = cats.ToList()[0].CAT_SEO_DESC;
                    HttpContext.Current.Session["Cat_seo_keyword"] = cats.ToList()[0].CAT_SEO_KEYWORD;
                    HttpContext.Current.Session["Cat_seo_title"] = cats.ToList()[0].CAT_SEO_TITLE;
                    HttpContext.Current.Session["Cat_seo_url"] = cats.ToList()[0].CAT_SEO_URL;
                    HttpContext.Current.Session["Cat_showfooter"] = cats.ToList()[0].CAT_SHOWFOOTER;
                    HttpContext.Current.Session["Cat_showitem"] = cats.ToList()[0].CAT_SHOWITEM;
                    HttpContext.Current.Session["Cat_status"] = cats.ToList()[0].CAT_STATUS;
                    HttpContext.Current.Session["Cat_target"] = cats.ToList()[0].CAT_TARGET;
                    HttpContext.Current.Session["Cat_type"] = cats.ToList()[0].CAT_TYPE;
                    HttpContext.Current.Session["Cat_url"] = cats.ToList()[0].CAT_URL;

                    #endregion

                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public void LoadNewsInfo(string News_Seo_Url)
        {
            try
            {

                var news = db.GetTable<ESHOP_NEW>().Where(c => c.NEWS_SEO_URL == News_Seo_Url);

                if (news.ToList().Count > 0)
                {

                    #region Bind News Info
                    HttpContext.Current.Session["News_id"] = news.ToList()[0].NEWS_ID;
                    HttpContext.Current.Session["News_title"] = news.ToList()[0].NEWS_TITLE;
                    HttpContext.Current.Session["News_desc"] = news.ToList()[0].NEWS_DESC;
                    HttpContext.Current.Session["News_seo_keyword"] = news.ToList()[0].NEWS_SEO_KEYWORD;
                    HttpContext.Current.Session["News_seo_desc"] = news.ToList()[0].NEWS_SEO_DESC;
                    HttpContext.Current.Session["News_seo_url"] = news.ToList()[0].NEWS_SEO_URL;
                    HttpContext.Current.Session["News_seo_title"] = news.ToList()[0].NEWS_SEO_TITLE;
                    HttpContext.Current.Session["News_filehtml"] = news.ToList()[0].NEWS_FILEHTML;
                    HttpContext.Current.Session["News_publishdate"] = news.ToList()[0].NEWS_PUBLISHDATE;
                    HttpContext.Current.Session["News_url"] = news.ToList()[0].NEWS_URL;
                    HttpContext.Current.Session["News_target"] = news.ToList()[0].NEWS_TARGET;
                    HttpContext.Current.Session["News_showtype"] = news.ToList()[0].NEWS_SHOWTYPE;
                    HttpContext.Current.Session["News_showindetail"] = news.ToList()[0].NEWS_SHOWINDETAIL;
                    HttpContext.Current.Session["News_feedbacktype"] = news.ToList()[0].NEWS_FEEDBACKTYPE;
                    HttpContext.Current.Session["News_type"] = news.ToList()[0].NEWS_TYPE;
                    HttpContext.Current.Session["News_language"] = news.ToList()[0].NEWS_LANGUAGE;
                    HttpContext.Current.Session["News_count"] = news.ToList()[0].NEWS_COUNT;
                    HttpContext.Current.Session["News_order"] = news.ToList()[0].NEWS_ORDER;
                    HttpContext.Current.Session["News_image1"] = news.ToList()[0].NEWS_IMAGE1;
                    HttpContext.Current.Session["News_image2"] = news.ToList()[0].NEWS_IMAGE2;
                    HttpContext.Current.Session["News_image3"] = news.ToList()[0].NEWS_IMAGE3;
                    HttpContext.Current.Session["News_image4"] = news.ToList()[0].NEWS_IMAGE4;
                    HttpContext.Current.Session["News_image5"] = news.ToList()[0].NEWS_IMAGE5;
                    HttpContext.Current.Session["News_field1"] = news.ToList()[0].NEWS_FIELD1;
                    HttpContext.Current.Session["News_field2"] = news.ToList()[0].NEWS_FIELD2;
                    HttpContext.Current.Session["News_field3"] = news.ToList()[0].NEWS_FIELD3;
                    HttpContext.Current.Session["News_field4"] = news.ToList()[0].NEWS_FIELD4;
                    HttpContext.Current.Session["News_field5"] = news.ToList()[0].NEWS_FIELD5;

                    if (Utils.CIntDef(HttpContext.Current.Session["Cat_id"]) == 0)
                    {
                        HttpContext.Current.Session["Cat_id"] = news.ToList()[0].ESHOP_NEWS_CATs.ToList()[0].CAT_ID;
                        HttpContext.Current.Session["Cat_name"] = news.ToList()[0].ESHOP_NEWS_CATs.ToList()[0].ESHOP_CATEGORy.CAT_NAME;
                        HttpContext.Current.Session["Cat_target"] = news.ToList()[0].ESHOP_NEWS_CATs.ToList()[0].ESHOP_CATEGORy.CAT_TARGET;
                        HttpContext.Current.Session["Cat_url"] = news.ToList()[0].ESHOP_NEWS_CATs.ToList()[0].ESHOP_CATEGORy.CAT_URL;
                        HttpContext.Current.Session["Cat_seo_url"] = news.ToList()[0].ESHOP_NEWS_CATs.ToList()[0].ESHOP_CATEGORy.CAT_SEO_URL;
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public int Getcat_type(string News_Seo_Url)
        {
            int _type=0;
            var list = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        where b.NEWS_SEO_URL == News_Seo_Url
                        select new { a.ESHOP_CATEGORy.CAT_TYPE }).ToList();
            if (list.Count > 0)
            {
                _type =Utils.CIntDef(list[0].CAT_TYPE);
            }
            return _type;
        }
    }
}
