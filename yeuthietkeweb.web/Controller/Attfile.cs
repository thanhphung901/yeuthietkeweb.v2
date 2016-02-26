using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class Attfile
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        public List<Attfile_entity> Load_att(string _news_seo)
        {
            List<Attfile_entity> l = new List<Attfile_entity>();
            var list = (from a in db.ESHOP_EXT_FILEs
                        join b in db.ESHOP_NEWS_ATTs on a.EXT_FILE_ID equals b.EXT_ID
                        where b.ESHOP_NEW.NEWS_SEO_URL==_news_seo
                        select new
                        {
                            EXT_FILE_IMAGE = a.EXT_FILE_IMAGE,
                            NEWS_ATT_FILE = b.NEWS_ATT_FILE,
                            NEWS_ATT_URL = b.NEWS_ATT_URL,
                            NEWS_ATT_NAME = b.NEWS_ATT_NAME,
                            EXT_ID = b.EXT_ID,
                            NEWS_ID = b.NEWS_ID
                        });
            foreach (var i in list)
            {
                Attfile_entity att = new Attfile_entity();
                att.EXT_FILE_IMAGE = i.EXT_FILE_IMAGE;
                att.NEWS_ATT_FILE = i.NEWS_ATT_FILE;
                att.NEWS_ATT_URL = i.NEWS_ATT_URL;
                att.NEWS_ATT_NAME = i.NEWS_ATT_NAME;
                att.EXT_ID = Utils.CIntDef(i.EXT_ID);
                att.NEWS_ID = Utils.CIntDef(i.NEWS_ID);
                l.Add(att);
            }
            return l;
        }
        public IQueryable<Attfile_entity> Loadatt_file(int _newsID)
        {
            try
            {
                List<Attfile_entity> l=new List<Attfile_entity>();
                var list = (from a in db.ESHOP_EXT_FILEs
                            join b in db.ESHOP_NEWS_ATTs on a.EXT_FILE_ID equals b.EXT_ID
                            where b.NEWS_ID == _newsID
                            select new
                            {
                                EXT_FILE_IMAGE = a.EXT_FILE_IMAGE,
                                NEWS_ATT_FILE = b.NEWS_ATT_FILE,
                                NEWS_ATT_URL = b.NEWS_ATT_URL,
                                NEWS_ATT_NAME = b.NEWS_ATT_NAME,
                                EXT_ID = b.EXT_ID,
                                NEWS_ID = b.NEWS_ID
                            });
                foreach (var i in list)
                {
                    Attfile_entity att = new Attfile_entity();
                    att.EXT_FILE_IMAGE = i.EXT_FILE_IMAGE;
                    att.NEWS_ATT_FILE = i.NEWS_ATT_FILE;
                    att.NEWS_ATT_URL = i.NEWS_ATT_URL;
                    att.NEWS_ATT_NAME = i.NEWS_ATT_NAME;
                    att.EXT_ID =Utils.CIntDef(i.EXT_ID);
                    att.NEWS_ID = Utils.CIntDef(i.NEWS_ID);
                    l.Add(att);
                }
                return l.AsQueryable();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public string BindAttItems(object News_Id, object Ext_Id, object Att_Name, object Att_Url, object Att_File, object Ext_Image)
        {
            try
            {
                string url = "";
                string strResult = "";

                if (!string.IsNullOrEmpty(Utils.CStrDef(Att_Url)))
                {
                    url = Utils.CStrDef(Att_Url);
                }
                else if (!string.IsNullOrEmpty(Utils.CStrDef(Att_File)))
                {
                    url = PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(Att_File);
                }
                else
                {
                    return "";
                }


                if (!string.IsNullOrEmpty(Utils.CStrDef(Ext_Image)))
                    strResult += "<img src='" + PathFiles.GetPathExt(Utils.CIntDef(Ext_Id)) + Utils.CStrDef(Ext_Image) + "' width='24px' style='margin-right:10px;height:19px' />";

                strResult += "<a href='" + url + "' target='_blank' title='" + Utils.CStrDef(Att_Name) + "'>" + (string.IsNullOrEmpty(Utils.CStrDef(Att_Name)) ? Utils.CStrDef(Att_File) : Utils.CStrDef(Att_Name)) + "</a>";


                return strResult;

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
    }
}
