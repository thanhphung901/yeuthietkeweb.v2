using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using yeuthietkeweb.cms.Components;

namespace yeuthietkeweb.cms.pages
{
    public partial class config_meta : System.Web.UI.Page
    {
        #region Declare

        dbShopDataContext DB = new dbShopDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            trFile.Visible = false;

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

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].CONFIG_FAVICON))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathConfigs() + n_info.ToList()[0].CONFIG_FAVICON);
                        n_info.ToList()[0].CONFIG_FAVICON = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "config_meta.aspx";
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

        protected void btnDeleteBG_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].CONFIG_FIELD1))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathConfigs() + n_info.ToList()[0].CONFIG_FIELD1);
                        n_info.ToList()[0].CONFIG_FIELD1 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "config_meta.aspx";
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
                var G_info = DB.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

                if (G_info.ToList().Count > 0)
                {
                    txtSeoTitle.Value = G_info.ToList()[0].CONFIG_TITLE;
                    txtSeoDesc.Value = G_info.ToList()[0].CONFIG_DESCRIPTION;
                    txtSeoKeyword.Value = G_info.ToList()[0].CONFIG_KEYWORD;

                    txtSeoTitleEn.Value = G_info.ToList()[0].CONFIG_TITLE_EN;
                    txtSeoDescEn.Value = G_info.ToList()[0].CONFIG_DESCRIPTION_EN;
                    txtSeoKeywordEn.Value = G_info.ToList()[0].CONFIG_KEYWORD_EN;
                    ColorPicker1.Color = G_info.ToList()[0].CONFIG_FIELD2;
                    //image
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].CONFIG_FAVICON))
                    {
                        trUpload.Visible = false;
                        trFile.Visible = true;
                        hplFile.NavigateUrl = PathFiles.GetPathConfigs() + G_info.ToList()[0].CONFIG_FAVICON;
                        hplFile.Text = G_info.ToList()[0].CONFIG_FAVICON;

                        ltrImage.Text = "<img src='" + PathFiles.GetPathConfigs() + G_info.ToList()[0].CONFIG_FAVICON + "' border='0'>";
                    }
                    else
                    {
                        trUpload.Visible = true;
                        trFile.Visible = false;
                    }
                    //imageBG
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].CONFIG_FIELD1))
                    {
                        trUploadBG.Visible = false;
                        trFileBG.Visible = true;
                        hplFileBG.NavigateUrl = PathFiles.GetPathConfigs() + G_info.ToList()[0].CONFIG_FIELD1;
                        hplFileBG.Text = G_info.ToList()[0].CONFIG_FIELD1;

                        ltrImageBG.Text = "<img src='" + PathFiles.GetPathConfigs() + G_info.ToList()[0].CONFIG_FIELD1 + "' border='0' width='400px'>";
                    }
                    else
                    {
                        trUploadBG.Visible = true;
                        trFileBG.Visible = false;
                    }
                }
                else
                {
                    trUpload.Visible = true;
                    trFile.Visible = false;
                    trUploadBG.Visible = true;
                    trFileBG.Visible = false;
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo()
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

                //get image
                string BG_File;

                if (trUploadBG.Visible == true)
                {
                    if (fileImageBG.PostedFile != null)
                    {
                        BG_File = Path.GetFileName(fileImageBG.PostedFile.FileName);
                    }
                    else
                    {
                        BG_File = "";
                    }
                }
                else
                {
                    BG_File = hplFileBG.Text;
                }

                var G_info = DB.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

                if (G_info.ToList().Count > 0)
                {
                    G_info.Single().CONFIG_TITLE = txtSeoTitle.Value;
                    G_info.Single().CONFIG_DESCRIPTION = txtSeoDesc.Value;
                    G_info.Single().CONFIG_KEYWORD = txtSeoKeyword.Value;

                    G_info.Single().CONFIG_TITLE_EN = txtSeoTitleEn.Value;
                    G_info.Single().CONFIG_DESCRIPTION_EN = txtSeoDescEn.Value;
                    G_info.Single().CONFIG_KEYWORD_EN = txtSeoKeywordEn.Value;

                    G_info.Single().CONFIG_FAVICON = Banner_File;
                    G_info.Single().CONFIG_FIELD1 = BG_File;
                    G_info.Single().CONFIG_FIELD2 = ColorPicker1.Color;
                    DB.SubmitChanges();
                }
                else
                {
                    //insert
                    ESHOP_CONFIG config_insert = new ESHOP_CONFIG();

                    config_insert.CONFIG_TITLE = txtSeoTitle.Value;
                    config_insert.CONFIG_DESCRIPTION = txtSeoDesc.Value;
                    config_insert.CONFIG_KEYWORD = txtSeoKeyword.Value;

                    config_insert.CONFIG_TITLE_EN = txtSeoTitleEn.Value;
                    config_insert.CONFIG_DESCRIPTION_EN = txtSeoDescEn.Value;
                    config_insert.CONFIG_KEYWORD_EN = txtSeoKeywordEn.Value;
                    config_insert.CONFIG_FAVICON = Banner_File;
                    config_insert.CONFIG_FIELD1 = BG_File;
                    config_insert.CONFIG_FIELD1 = ColorPicker1.Color;

                    DB.ESHOP_CONFIGs.InsertOnSubmit(config_insert);
                    DB.SubmitChanges();
                }

                //update images
                if (trUpload.Visible)
                {
                    if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
                    {
                        string pathfile = Server.MapPath("/data/configs");
                        string fullpathfile = pathfile + "/" + Banner_File;

                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }

                        fileImage1.PostedFile.SaveAs(fullpathfile);
                    }
                }

                //update imagesBG
                if (trUploadBG.Visible)
                {
                    //if (!string.IsNullOrEmpty(fileImageBG.PostedFile.FileName))
                    //{
                    //    string pathfile = Server.MapPath("/data/configs");
                    //    string fullpathfile = pathfile + "/" + BG_File;

                    //    if (!Directory.Exists(pathfile))
                    //    {
                    //        Directory.CreateDirectory(pathfile);
                    //    }

                    //    fileImageBG.PostedFile.SaveAs(fullpathfile);
                    //}
                    CpanelUtils.ImageResize(Server.MapPath("/data/configs/"), fileImageBG.Value, 1366, fileImageBG.PostedFile.InputStream);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                getInfo();
            }
        }

        #endregion
    }
}