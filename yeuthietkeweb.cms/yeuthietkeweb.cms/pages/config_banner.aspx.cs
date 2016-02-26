using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;

namespace yeuthietkeweb.cms.pages
{
    public partial class config_banner : System.Web.UI.Page
    {
        #region Declare

        private int m_banner_id = 0;
        private int _count = 0;
        dbShopDataContext DB = new dbShopDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_banner_id = Utils.CIntDef(Request["banner_id"]);

            if (m_banner_id == 0)
            {
                lbtDelete.Visible = false;
                trFile.Visible = false;
            }

            if (!IsPostBack)
            {
                getInfo();

                SearchResult();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Value))
                lblError.Visible = true;
            else
                SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Value))
                lblError.Visible = true;
            else
                SaveInfo("config_banner.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo(m_banner_id);
        }

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_BANNER>().Where(n => n.BANNER_ID == m_banner_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].BANNER_FILE))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathBanner(m_banner_id) + n_info.ToList()[0].BANNER_FILE);
                        n_info.ToList()[0].BANNER_FILE = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "config_banner.aspx?banner_id=" + m_banner_id;
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
                Components.CpanelUtils.createItemLanguage(ref rblLanguage);

                var G_info = (from g in DB.ESHOP_BANNERs
                              where g.BANNER_ID == m_banner_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtTitle.Value = G_info.ToList()[0].BANNER_DESC;
                    rblLanguage.SelectedValue = Utils.CStrDef(G_info.ToList()[0].BANNER_LANGUAGE);
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].BANNER_ORDER);
                    rblBannerType.SelectedValue = Utils.CStrDef(G_info.ToList()[0].BANNER_TYPE);

                    rblLogoBanner.SelectedValue = Utils.CStrDef(G_info.ToList()[0].BANNER_FIELD1);

                    //image
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].BANNER_FILE))
                    {
                        trUpload.Visible = false;
                        trFile.Visible = true;
                        hplFile.NavigateUrl = PathFiles.GetPathBanner(m_banner_id) + G_info.ToList()[0].BANNER_FILE;
                        hplFile.Text = G_info.ToList()[0].BANNER_FILE;

                        if (G_info.ToList()[0].BANNER_TYPE == 0)
                        {
                            ltrImage.Text = "<img src='" + PathFiles.GetPathBanner(m_banner_id) + G_info.ToList()[0].BANNER_FILE + "' border='0'>";
                        }
                        else
                        {
                            ltrImage.Text += "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' width='1000px' height='150' id='ShockwaveFlash1' >";
                            ltrImage.Text += "<param name='movie' value='" + PathFiles.GetPathBanner(m_banner_id) + G_info.ToList()[0].BANNER_FILE + "'>";
                            ltrImage.Text += "<param name='Menu' value='0'>";
                            ltrImage.Text += "<param name='quality' value='high'>";
                            ltrImage.Text += "<param name='wmode' value='transparent'>";
                            ltrImage.Text += "<embed width='1000' height='150' src='" + PathFiles.GetPathBanner(m_banner_id) + G_info.ToList()[0].BANNER_FILE + "' wmode='transparent' ></object>";
                        }
                    }
                    else
                    {
                        trUpload.Visible = true;
                        trFile.Visible = false;
                    }
                }
                else
                {
                    trUpload.Visible = true;
                    trFile.Visible = false;
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo(string strLink = "")
        {
            try
            {

                //get image
                string Banner_File;

                if (trUpload.Visible == true)
                {
                    if (fileImage1.PostedFile != null)
                    {
                        Banner_File = Path.GetFileName(fileImage1.PostedFile.FileName);
                    }
                    else
                    {
                        Banner_File = "";
                    }
                }
                else
                {
                    Banner_File = hplFile.Text;
                }

                if (m_banner_id == 0)
                {
                    //insert
                    ESHOP_BANNER g_insert = new ESHOP_BANNER();
                    g_insert.BANNER_DESC = txtTitle.Value;
                    g_insert.BANNER_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);
                    g_insert.BANNER_TYPE = Utils.CIntDef(rblBannerType.SelectedValue);
                    g_insert.BANNER_ORDER = Utils.CIntDef(txtOrder.Value);
                    g_insert.BANNER_FILE = Banner_File;
                    g_insert.BANNER_FIELD1 = Utils.CStrDef(rblLogoBanner.SelectedValue);

                    DB.ESHOP_BANNERs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    //get new id
                    var _new = DB.GetTable<ESHOP_BANNER>().OrderByDescending(g => g.BANNER_ID).Take(1);

                    m_banner_id = _new.Single().BANNER_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "config_banner.aspx?banner_id=" + m_banner_id : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_BANNER>().Where(g => g.BANNER_ID == m_banner_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().BANNER_DESC = txtTitle.Value;
                        g_update.Single().BANNER_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);
                        g_update.Single().BANNER_TYPE = Utils.CIntDef(rblBannerType.SelectedValue);
                        g_update.Single().BANNER_ORDER = Utils.CIntDef(txtOrder.Value);
                        g_update.Single().BANNER_FILE = Banner_File;
                        g_update.Single().BANNER_FIELD1 = Utils.CStrDef(rblLogoBanner.SelectedValue);

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "config_banner.aspx?banner_id=" + m_banner_id : strLink;
                    }
                }

                //update images
                if (trUpload.Visible)
                {
                    if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
                    {
                        string pathfile = Server.MapPath(PathFiles.GetPathConfigs() + m_banner_id);
                        string fullpathfile = pathfile + "/" + Banner_File;

                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }

                        fileImage1.PostedFile.SaveAs(fullpathfile);
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
                { Response.Redirect(strLink); }
            }
        }

        private void DeleteInfo(int banner_id)
        {
            string strLink = "";
            try
            {
                string Banner_File = "";

                var G_info = DB.GetTable<ESHOP_BANNER>().Where(g => g.BANNER_ID == banner_id);

                if (G_info.ToList().Count > 0)
                    Banner_File = Utils.CStrDef(G_info.ToList()[0].BANNER_FILE);

                DB.ESHOP_BANNERs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete file
                if (!string.IsNullOrEmpty(Banner_File))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathBanner(banner_id) + Banner_File);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }

                strLink = "config_banner.aspx";

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
            return "config_banner.aspx?banner_id=" + obj_id;
        }

        public string getLinkImage(object obj_id, object obj_file)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_file)) && Utils.CIntDef(obj_id) > 0)
            {
                return "<a href='" + PathFiles.GetPathBanner(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_file) + "' target='_blank'>" + Utils.CStrDef(obj_file) + "</a>";
            }

            return "";
        }

        private void SearchResult()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_BANNERs
                               orderby g.BANNER_PUBLISHDATE descending, g.BANNER_ORDER descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["BannerList"] = DataUtil.LINQToDataTable(AllList);

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
            DeleteInfo(_id);
        }

        #endregion

        #region Grid Events
        protected void rptList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Label lblID = (Label)e.Item.FindControl("lblID");

            int _id = Utils.CIntDef(lblID.Text, 0);
            if (((LinkButton)e.CommandSource).CommandName == "Delete")
            {
                EventDelete(_id);
            }
        }

        #endregion
    }
}