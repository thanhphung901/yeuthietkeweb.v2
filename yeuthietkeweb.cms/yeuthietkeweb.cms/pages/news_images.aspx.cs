using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using vpro.functions;

namespace yeuthietkeweb.cms.pages
{
    public partial class news_images : System.Web.UI.Page
    {
        #region Declare

        private int m_img_id = 0;
        private int m_news_id = 0;
        private int _count = 0;
        dbShopDataContext DB = new dbShopDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_img_id = Utils.CIntDef(Request["img_id"]);
            m_news_id = Utils.CIntDef(Request["news_id"]);

            hplBack.HRef = "news.aspx?news_id=" + m_news_id;

            if (m_img_id == 0)
            {
                lbtDelete.Visible = false;
                trImage1.Visible = false;
                //trImage2.Visible = false;
            }

            if (!IsPostBack)
            {
                getInfo();

                SearchResult();
            }

            hplCatNews.HRef = "news_category.aspx?news_id=" + m_news_id;
            hplEditorHTMl.HRef = "news_editor.aspx?news_id=" + m_news_id;
            hplNewsAtt.HRef = "news_attachment.aspx?news_id=" + m_news_id; hplNewsAtt.Visible = false;
            hplAlbum.HRef = "news_images.aspx?news_id=" + m_news_id;
            //bplNewsCopy.HRef = "news_copy.aspx?news_id=" + m_news_id;
            hplComment.HRef = "news_comment.aspx?news_id=" + m_news_id; hplComment.Visible = false;
            //hplCatProducts.HRef = "news_news.aspx?news_id=" + m_news_id;

        }

        #endregion

        #region Button Events

        //protected void lbtSave_Click(object sender, EventArgs e)
        //{
        //    SaveInfo();
        //}

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo(m_img_id);
        }

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(n => n.NEWS_IMG_ID == m_img_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].NEWS_IMG_IMAGE1))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + n_info.ToList()[0].NEWS_IMG_IMAGE1);
                        n_info.ToList()[0].NEWS_IMG_IMAGE1 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "news_images.aspx?img_id=" + m_img_id + "&news_id=" + m_news_id;
                    }
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }
        }


        //protected void btnDelete2_Click(object sender, ImageClickEventArgs e)
        //{
        //    string strLink = "";
        //    try
        //    {
        //        var n_info = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(n => n.NEWS_IMG_ID == m_img_id);

        //        if (n_info.ToList().Count > 0)
        //        {
        //            if (!string.IsNullOrEmpty(n_info.ToList()[0].NEWS_IMG_IMAGE2))
        //            {
        //                string imagePath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + n_info.ToList()[0].NEWS_IMG_IMAGE2);
        //                n_info.ToList()[0].NEWS_IMG_IMAGE2 = "";
        //                DB.SubmitChanges();

        //                if (File.Exists(imagePath))
        //                    File.Delete(imagePath);

        //                strLink = "news_images.aspx?img_id=" + m_img_id + "&news_id=" + m_news_id;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //    finally
        //    {
        //        if (!string.IsNullOrEmpty(strLink))
        //            Response.Redirect(strLink);
        //    }
        //}

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {

                var G_info = (from g in DB.ESHOP_NEWS_IMAGEs
                              where g.NEWS_IMG_ID == m_img_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtTitle.Value = G_info.ToList()[0].NEWS_IMG_DESC;
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].NEWS_IMG_ORDER);

                    //image 1
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].NEWS_IMG_IMAGE1))
                    {
                        trUpload1.Visible = false;
                        trImage1.Visible = true;
                        hplImage1.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE1;
                        hplImage1.Text = G_info.ToList()[0].NEWS_IMG_IMAGE1;
                        Image1.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE1;
                        Image1.Width = 300;
                    }
                    else
                    {
                        trUpload1.Visible = true;
                        trImage1.Visible = false;
                    }

                    //image 2
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].NEWS_IMG_IMAGE2))
                    //{
                    //    trUpload2.Visible = false;
                    //    trImage2.Visible = true;
                    //    hplImage2.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE2;
                    //    hplImage2.Text = G_info.ToList()[0].NEWS_IMG_IMAGE2;
                    //    Image2.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE2;
                    //}
                    //else
                    //{
                    //    trUpload2.Visible = true;
                    //    trImage2.Visible = false;
                    //}
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        //private void SaveInfo(string strLink = "")
        //{
        //    try
        //    {

        //        //get image
        //        string Img_Image1, Img_Image2;

        //        if (trUpload1.Visible == true)
        //        {
        //            if (fileImage1.PostedFile != null)
        //            {
        //                Img_Image1 = Path.GetFileName(fileImage1.PostedFile.FileName);
        //            }
        //            else
        //            {
        //                Img_Image1 = "";
        //            }
        //        }
        //        else
        //        {
        //            Img_Image1 = hplImage1.Text;
        //        }

        //        //if (trUpload2.Visible == true)
        //        //{
        //        //    if (fileImage2.PostedFile != null)
        //        //    {
        //        //        Img_Image2 = Path.GetFileName(fileImage2.PostedFile.FileName);
        //        //    }
        //        //    else
        //        //    {
        //        //        Img_Image2 = "";
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    Img_Image2= hplImage2.Text;
        //        //}

        //        if (m_img_id == 0)
        //        {
        //            //insert
        //            ESHOP_NEWS_IMAGE g_insert = new ESHOP_NEWS_IMAGE();

        //            g_insert.NEWS_IMG_DESC = txtTitle.Value;
        //            g_insert.NEWS_IMG_ORDER = Utils.CIntDef(txtOrder.Value);
        //            g_insert.NEWS_ID = m_news_id;
        //            g_insert.NEWS_IMG_IMAGE1 = Img_Image1;
        //            //g_insert.NEWS_IMG_IMAGE2 = Img_Image2;

        //            DB.ESHOP_NEWS_IMAGEs.InsertOnSubmit(g_insert);

        //            DB.SubmitChanges();

        //            //get new id
        //            var _new = DB.GetTable<ESHOP_NEWS_IMAGE>().OrderByDescending(g => g.NEWS_IMG_ID).Take(1);

        //            m_img_id = _new.Single().NEWS_IMG_ID;

        //            strLink = string.IsNullOrEmpty(strLink) ? "news_images.aspx?news_id=" + m_news_id : strLink;
        //        }
        //        else
        //        {
        //            //update
        //            var g_update = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(g => g.NEWS_IMG_ID == m_img_id);

        //            if (g_update.ToList().Count > 0)
        //            {
        //                g_update.Single().NEWS_IMG_DESC = txtTitle.Value;
        //                g_update.Single().NEWS_IMG_ORDER = Utils.CIntDef(txtOrder.Value);
        //                g_update.Single().NEWS_ID = m_news_id;
        //                g_update.Single().NEWS_IMG_IMAGE1 = Img_Image1;
        //                //g_update.Single().NEWS_IMG_IMAGE2 = Img_Image2;

        //                DB.SubmitChanges();

        //                strLink = string.IsNullOrEmpty(strLink) ? "news_images.aspx?news_id=" + m_news_id : strLink;
        //            }
        //        }

        //        //update images
        //        if (trUpload1.Visible)
        //        {
        //            if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
        //            {
        //                string pathfile = Server.MapPath("/data/news/" + m_news_id);
        //                string fullpathfile = pathfile + "/" + Img_Image1;

        //                if (!Directory.Exists(pathfile))
        //                {
        //                    Directory.CreateDirectory(pathfile);
        //                }

        //                fileImage1.PostedFile.SaveAs(fullpathfile);
        //            }

        //        }

        //        //if (trUpload2.Visible)
        //        //{
        //        //    if (!string.IsNullOrEmpty(fileImage2.PostedFile.FileName))
        //        //    {
        //        //        string pathfile = Server.MapPath("../data/news/" + m_news_id);
        //        //        string fullpathfile = pathfile + "/" + Img_Image2;

        //        //        if (!Directory.Exists(pathfile))
        //        //        {
        //        //            Directory.CreateDirectory(pathfile);
        //        //        }

        //        //        fileImage2.PostedFile.SaveAs(fullpathfile);
        //        //    }

        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //    finally
        //    {
        //        if (!string.IsNullOrEmpty(strLink))
        //        { Response.Redirect(strLink); }
        //    }
        //}

        private void DeleteInfo(int img_id)
        {
            string strLink = "";
            try
            {
                string Img_Image1 = "";
                string Img_Image2 = "";

                var G_info = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(g => g.NEWS_IMG_ID == img_id);

                if (G_info.ToList().Count > 0)
                {
                    Img_Image1 = Utils.CStrDef(G_info.ToList()[0].NEWS_IMG_IMAGE1);
                    Img_Image2 = Utils.CStrDef(G_info.ToList()[0].NEWS_IMG_IMAGE2);
                }

                DB.ESHOP_NEWS_IMAGEs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete file
                if (!string.IsNullOrEmpty(Img_Image1))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + Img_Image1);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }
                if (!string.IsNullOrEmpty(Img_Image2))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + Img_Image2);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }

                strLink = "news_images.aspx?news_id=" + m_news_id;

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }

        }

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "news_images.aspx?news_id=" + m_news_id + "&img_id=" + Utils.CStrDef(obj_id);
        }

        public string getLinkImage(object obj_id, object obj_image1)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_id)) && Utils.CIntDef(obj_id) > 0)
                return PathFiles.GetPathNews(m_news_id) + Utils.CStrDef(obj_id);

            return "";
        }

        public string getImage(object obj_id, object obj_image1)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_id)) && Utils.CIntDef(obj_id) > 0)
                return "<img src='" + PathFiles.GetPathNews(m_news_id) + Utils.CStrDef(obj_image1) + "' width='120px' border='0' />";

            return "";
        }

        private void SearchResult()
        {
            try
            {
                var AllList = DB.ESHOP_NEWS_IMAGEs.Where(n => n.NEWS_ID == m_news_id).ToList();


                if (AllList.ToList().Count > 0)
                    Session["ImgList"] = DataUtil.LINQToDataTable(AllList);

                rptList.DataSource = AllList;
                rptList.DataBind();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void EventDelete(int _id)
        {
            int AttId = _id;
            DeleteInfo(AttId);
        }

        #endregion

        #region Grid Events
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblID = (Label)e.Item.FindControl("lblID");
            int _id = Utils.CIntDef(lblID.Text, 0);
            if (((LinkButton)e.CommandSource).CommandName == "Delete")
            {
                EventDelete(_id);
            }
        }

        #endregion

        protected void Lnupload_Click(object sender, EventArgs e)
        {
            string strLink = "";
            try
            {
                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        ESHOP_NEWS_IMAGE g_update = new ESHOP_NEWS_IMAGE();
                        string pathfile = Server.MapPath("/data/news/" + m_news_id);
                        string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }
                        hpf.SaveAs(fullpathfile);
                        g_update.NEWS_IMG_DESC = txtTitle.Value;
                        g_update.NEWS_IMG_ORDER = Utils.CIntDef(txtOrder.Value);
                        g_update.NEWS_ID = m_news_id;
                        g_update.NEWS_IMG_IMAGE1 = hpf.FileName;
                        DB.ESHOP_NEWS_IMAGEs.InsertOnSubmit(g_update);
                        DB.SubmitChanges();

                    }
                }
                SearchResult();
                strLink = "news_images.aspx?news_id=" + m_news_id;

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }
        }
    }
}