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
    public partial class online : System.Web.UI.Page
    {
        #region Declare

        private int m_online_id = 0;
        dbShopDataContext DB = new dbShopDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_online_id = Utils.CIntDef(Request["online_id"]);

            if (m_online_id == 0)
            {
                lbtDelete.Visible = false;
                // trImage1.Visible = false;
            }

            if (!IsPostBack)
            {
                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo("online.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_ONLINE>().Where(n => n.ONLINE_ID == m_online_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].ONLINE_IMAGE))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathOnline(m_online_id) + n_info.ToList()[0].ONLINE_IMAGE);
                        n_info.ToList()[0].ONLINE_IMAGE = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "online.aspx?online_id=" + m_online_id;
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

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {

                Components.CpanelUtils.createItemLanguage(ref rblLanguage);

                var G_info = (from g in DB.ESHOP_ONLINEs
                              where g.ONLINE_ID == m_online_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtSkype.Value = G_info.ToList()[0].ONLINE_FIELD1;
                    txtName.Value = G_info.ToList()[0].ONLINE_NICKNAME;
                    txtDesc.Value = Utils.CStrDef(System.Web.HttpUtility.HtmlDecode(G_info.ToList()[0].ONLINE_DESC.Replace("<br>", "\r\n").Replace(" ", "&nbsp;")), "");
                    //txtDescEn.Value = Utils.CStrDef(System.Web.HttpUtility.HtmlDecode(G_info.ToList()[0].ONLINE_DESC_EN.Replace("<br>", "\r\n").Replace(" ", "&nbsp;")), "");
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].ONLINE_ORDER);
                    rblType.SelectedValue = Utils.CStrDef(G_info.ToList()[0].ONLINE_TYPE);
                    rblLanguage.SelectedValue = Utils.CStrDef(G_info.ToList()[0].ONLINE_LANGUAGE);


                    //image 1
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].ONLINE_IMAGE))
                    //{
                    //    trUploadImage1.Visible = false;
                    //    trImage1.Visible = true;
                    //    Image1.Src = PathFiles.GetPathOnline(m_online_id) + G_info.ToList()[0].ONLINE_IMAGE;
                    //    hplImage1.NavigateUrl = PathFiles.GetPathOnline(m_online_id) + G_info.ToList()[0].ONLINE_IMAGE;
                    //    hplImage1.Text = G_info.ToList()[0].ONLINE_IMAGE;
                    //}
                    //else
                    //{
                    //    trUploadImage1.Visible = true;
                    //    trImage1.Visible = false;
                    //}
                }
                else
                {
                    //trUploadImage1.Visible = true;
                    //trImage1.Visible = false;
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

                //get image1
                string Online_Image1;

                //if (trUploadImage1.Visible == true)
                //{
                //    if (fileImage1.PostedFile != null)
                //    {
                //        Online_Image1 = Path.GetFileName(fileImage1.PostedFile.FileName);
                //    }
                //    else
                //    {
                //        Online_Image1 = "";
                //    }
                //}
                //else
                //{
                //    Online_Image1 = hplImage1.Text;
                //}


                if (m_online_id == 0)
                {
                    //insert
                    ESHOP_ONLINE g_insert = new ESHOP_ONLINE();
                    g_insert.ONLINE_FIELD1 = txtSkype.Value;
                    g_insert.ONLINE_NICKNAME = txtName.Value;
                    g_insert.ONLINE_DESC = Utils.CStrDef(System.Web.HttpUtility.HtmlDecode(txtDesc.Value.Replace("\r\n", "<br>").Replace(" ", "&nbsp;")), "");
                    //g_insert.ONLINE_DESC_EN = Utils.CStrDef(System.Web.HttpUtility.HtmlDecode(txtDescEn.Value.Replace("\r\n", "<br>").Replace(" ", "&nbsp;")), "");
                    g_insert.ONLINE_ORDER = Utils.CIntDef(txtOrder.Value);
                    g_insert.ONLINE_TYPE = Utils.CIntDef(rblType.SelectedValue);
                    g_insert.ONLINE_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);
                    //g_insert.ONLINE_IMAGE = Online_Image1;

                    DB.ESHOP_ONLINEs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    var _online = DB.GetTable<ESHOP_ONLINE>().OrderByDescending(g => g.ONLINE_ID).Take(1);

                    m_online_id = _online.Single().ONLINE_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "online_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_ONLINE>().Where(g => g.ONLINE_ID == m_online_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().ONLINE_FIELD1 = txtSkype.Value;
                        g_update.Single().ONLINE_NICKNAME = txtName.Value;
                        //g_update.Single().ONLINE_FIELD1 = txtSkype.Value;
                        //g_update.Single().ONLINE_FIELD2 = txtHotline.Value;
                        g_update.Single().ONLINE_DESC = Utils.CStrDef(System.Web.HttpUtility.HtmlDecode(txtDesc.Value.Replace("\r\n", "<br>").Replace(" ", "&nbsp;")), "");
                        //g_update.Single().ONLINE_DESC_EN = Utils.CStrDef(System.Web.HttpUtility.HtmlDecode(txtDescEn.Value.Replace("\r\n", "<br>").Replace(" ", "&nbsp;")), "");
                        g_update.Single().ONLINE_ORDER = Utils.CIntDef(txtOrder.Value);
                        g_update.Single().ONLINE_TYPE = Utils.CIntDef(rblType.SelectedValue);
                        g_update.Single().ONLINE_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);
                        // g_update.Single().ONLINE_IMAGE = Online_Image1;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "online_list.aspx" : strLink;
                    }
                }

                //update images 1
                //if (trUploadImage1.Visible)
                //{
                //    if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
                //    {
                //        string pathfile = Server.MapPath("../data/onlines/" + m_online_id);
                //        string fullpathfile = pathfile + "/" + Online_Image1;

                //        if (!Directory.Exists(pathfile))
                //        {
                //            Directory.CreateDirectory(pathfile);
                //        }

                //        fileImage1.PostedFile.SaveAs(fullpathfile);
                //    }

                //}

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

        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_ONLINE>().Where(g => g.ONLINE_ID == m_online_id);

                DB.ESHOP_ONLINEs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathOnline(m_online_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                Response.Redirect("online_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        #endregion
    }
}