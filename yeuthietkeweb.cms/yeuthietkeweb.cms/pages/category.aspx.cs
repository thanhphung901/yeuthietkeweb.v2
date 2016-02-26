using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;
using System.Data;

namespace yeuthietkeweb.cms.pages
{
    public partial class category : System.Web.UI.Page
    {
        #region Declare

        private int m_cat_id = 0;
        int _count = 0;
        dbShopDataContext DB = new dbShopDataContext();
        string m_pathFile = "";
        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_cat_id = Utils.CIntDef(Request["cat_id"]);

            if (m_cat_id == 0)
            {
                lbtDelete.Visible = false;
                trImage1.Visible = false;
                trImage2.Visible = false;
                Hyperseo_cate.Visible = false;
            }
            else
            {
                Hyperseo_cate.HRef = "category_seo.aspx?cat_id=" + m_cat_id;
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
            if (!string.IsNullOrEmpty(txtCode.Value))
            {
                txtSeoUrl.Value = txtSeoUrl.Value + "-" + txtCode.Value;
            }
            if (CheckExitsLink(txtSeoUrl.Value))
                lblError.Text = "Đã tồn tại Seo Url, vui lòng nhập Seo Url khác cho chuyên mục.";
            else
                SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Value))
            {
                txtSeoUrl.Value = txtSeoUrl.Value + "-" + txtCode.Value;
            }
            if (CheckExitsLink(txtSeoUrl.Value))
                lblError.Text = "Đã tồn tại Seo Url, vui lòng nhập Seo Url khác cho chuyên mục.";
            else
                SaveInfo("category.aspx");
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
                var n_info = DB.GetTable<ESHOP_CATEGORy>().Where(n => n.CAT_ID == m_cat_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].CAT_IMAGE1))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathCategory(m_cat_id) + n_info.ToList()[0].CAT_IMAGE1);
                        n_info.ToList()[0].CAT_IMAGE1 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "category.aspx?cat_id=" + m_cat_id;
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

        protected void btnDelete2_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_CATEGORy>().Where(n => n.CAT_ID == m_cat_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].CAT_IMAGE2))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathCategory(m_cat_id) + n_info.ToList()[0].CAT_IMAGE2);
                        n_info.ToList()[0].CAT_IMAGE2 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "category.aspx?cat_id=" + m_cat_id;
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

        protected void btnDelete3_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_CATEGORy>().Where(n => n.CAT_ID == m_cat_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].CAT_IMAGE3))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathCategory(m_cat_id) + n_info.ToList()[0].CAT_IMAGE3);
                        n_info.ToList()[0].CAT_IMAGE3 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "category.aspx?cat_id=" + m_cat_id;
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

        #region My functions

        private void LoadCategoryParent()
        {
            try
            {
                var CatList = (
                                from t2 in DB.ESHOP_CATEGORies
                                select new
                                {
                                    CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
                                    CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
                                    CAT_PARENT_ID = t2.CAT_PARENT_ID,
                                    CAT_RANK = t2.CAT_RANK

                                }
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    ddlCategory.DataSource = dsCat.Tables[0];
                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataBind();

                }
                else
                {
                    DataTable dt = new DataTable("Newtable");

                    dt.Columns.Add(new DataColumn("CAT_ID"));
                    dt.Columns.Add(new DataColumn("CAT_NAME"));

                    DataRow row = dt.NewRow();
                    row["CAT_ID"] = 0;
                    row["CAT_NAME"] = "--------Root--------";
                    dt.Rows.Add(row);

                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();



                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void getInfo()
        {
            try
            {
                LoadCategoryParent();
                Components.CpanelUtils.createItemTarget(ref ddlTarget);
                Components.CpanelUtils.createItemLanguage(ref rblLanguage);

                var G_info = (from c in DB.ESHOP_CATEGORies
                              where c.CAT_ID == m_cat_id
                              select c
                            );

                if (G_info.ToList().Count > 0)
                {
                    ddlCategory.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_PARENT_ID);
                    txtCode.Value = G_info.ToList()[0].CAT_CODE;
                    txtName.Value = G_info.ToList()[0].CAT_NAME;
                    txtDesc.Value = G_info.ToList()[0].CAT_DESC;
                    txtUrl.Value = G_info.ToList()[0].CAT_URL;
                    ddlTarget.SelectedValue = G_info.ToList()[0].CAT_TARGET;
                    rblStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_STATUS);
                    //rblAccess.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_ACCESS);
                    rblPos.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_POSITION);
                    rblShowFooter.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_SHOWFOOTER);
                    rblShowItems.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_SHOWITEM);
                    rblCatPeriod.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_PERIOD);
                    rblLanguage.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_LANGUAGE);
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].CAT_ORDER);
                    txtOrderPeriod.Value = Utils.CStrDef(G_info.ToList()[0].CAT_PERIOD_ORDER);
                    txtpageItem.Value = Utils.CStrDef(G_info.ToList()[0].CAT_PAGEITEM);
                    //lblCount.Text = Utils.CStrDef(G_info.ToList()[0].CAT_COUNT);
                    rblCatType.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_TYPE);

                    //seo
                    txtSeoTitle.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_TITLE);
                    txtSeoKeyword.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_KEYWORD);
                    txtSeoDescription.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_DESC);
                    txtSeoUrl.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_URL);

                    //english language
                    //txtNameEn.Value = G_info.ToList()[0].CAT_NAME_EN;
                    //txtCodeEn.Value = G_info.ToList()[0].CAT_CODE_EN;
                    //txtDescEn.Value = G_info.ToList()[0].CAT_DESC_EN;

                    //txtSeoTitleEn.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_TITLE_EN);
                    //txtSeoKeywordEn.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_KEYWORD_EN);
                    //txtSeoDescriptionEn.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_DESC_EN);
                    //txtSeoUrlEn.Value = Utils.CStrDef(G_info.ToList()[0].CAT_SEO_URL_EN);


                    //image 1
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].CAT_IMAGE1))
                    {
                        trUploadImage1.Visible = false;
                        trImage1.Visible = true;
                        Image1.Src = PathFiles.GetPathCategory(m_cat_id) + G_info.ToList()[0].CAT_IMAGE1;
                        hplImage1.NavigateUrl = PathFiles.GetPathCategory(m_cat_id) + G_info.ToList()[0].CAT_IMAGE1;
                        hplImage1.Text = G_info.ToList()[0].CAT_IMAGE1;
                    }
                    else
                    {
                        trUploadImage1.Visible = true;
                        trImage1.Visible = false;
                    }

                    //image 2
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].CAT_IMAGE2))
                    {
                        trUploadImage2.Visible = false;
                        trImage2.Visible = true;
                        Image2.Src = PathFiles.GetPathCategory(m_cat_id) + G_info.ToList()[0].CAT_IMAGE2;
                        hplImage2.NavigateUrl = PathFiles.GetPathCategory(m_cat_id) + G_info.ToList()[0].CAT_IMAGE2;
                        hplImage2.Text = G_info.ToList()[0].CAT_IMAGE2;
                    }
                    else
                    {
                        trUploadImage2.Visible = true;
                        trImage2.Visible = false;
                    }

                    ////image 3
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].CAT_IMAGE3))
                    //{
                    //    trUploadImage3.Visible = false;
                    //    trImage3.Visible = true;
                    //    Image3.Src = PathFiles.GetPathCategory(m_cat_id) + G_info.ToList()[0].CAT_IMAGE3;
                    //    hplImage3.NavigateUrl = PathFiles.GetPathCategory(m_cat_id) + G_info.ToList()[0].CAT_IMAGE3;
                    //    hplImage3.Text = G_info.ToList()[0].CAT_IMAGE3;
                    //}
                    //else
                    //{
                    //    trUploadImage3.Visible = true;
                    //    trImage3.Visible = false;
                    //}

                }
                else
                {
                    //trUploadImage1.Visible = true;
                    //trImage1.Visible = false;
                    //trUploadImage2.Visible = true;
                    //trImage2.Visible = false;
                    //trUploadImage3.Visible = true;
                    //trImage3.Visible = false;
                    LoadCategoryParent();
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
                string Cat_Code = txtCode.Value;
                string Cat_Name = txtName.Value;
                string Cat_Desc = txtDesc.Value;
                string Cat_Url = txtUrl.Value;
                string Cat_Seo_Url = txtSeoUrl.Value;
                string Cat_Seo_Tittle = txtSeoTitle.Value;
                string Cat_Seo_Keyword = txtSeoKeyword.Value;
                string Cat_Seo_Description = txtSeoDescription.Value;
                string Cat_Target = ddlTarget.SelectedValue;

                int Cat_Parent_Id = Utils.CIntDef(ddlCategory.SelectedValue, 0);
                int Cat_Status = Utils.CIntDef(rblStatus.SelectedValue);
                //int Cat_Access = Utils.CIntDef(rblAccess.SelectedValue);
                int Cat_Pos = Utils.CIntDef(rblPos.SelectedValue);
                int Cat_Type = Utils.CIntDef(rblCatType.SelectedValue);
                int Cat_Footer = Utils.CIntDef(rblShowFooter.SelectedValue);
                int Cat_ShowItems = Utils.CIntDef(rblShowItems.SelectedValue);
                int Cat_Period = Utils.CIntDef(rblCatPeriod.SelectedValue);
                int Cat_Language = Utils.CIntDef(rblLanguage.SelectedValue);
                int Cat_Order = Utils.CIntDef(txtOrder.Value);
                int Cat_Order_Period = Utils.CIntDef(txtOrderPeriod.Value);
                int Cat_PageItem = Utils.CIntDef(txtpageItem.Value);
                string Cat_Parent_Path = "0";
                int Cat_Rank = 1;

                //english language
                //string Cat_Code_En = txtCodeEn.Value;
                //string Cat_Name_En = txtNameEn.Value;
                //string Cat_Desc_En = txtDescEn.Value;
                //string Cat_Seo_Url_En = txtSeoUrlEn.Value;
                //string Cat_Seo_Tittle_En = txtSeoTitleEn.Value;
                //string Cat_Seo_Keyword_En = txtSeoKeywordEn.Value;
                //string Cat_Seo_Description_En = txtSeoDescriptionEn.Value;


                if (Cat_Parent_Id > 0)
                {
                    var CatParent = DB.GetTable<ESHOP_CATEGORy>().Where(c => c.CAT_ID == Cat_Parent_Id);

                    Cat_Parent_Path = CatParent.Single().CAT_PARENT_PATH + "," + Utils.CStrDef(Cat_Parent_Id);
                    Cat_Rank = Utils.CIntDef(CatParent.Single().CAT_RANK) + 1;
                }

                //get image1
                string Cat_Image1;

                if (trUploadImage1.Visible == true)
                {
                    if (fileImage1.PostedFile != null)
                    {
                        Cat_Image1 = Path.GetFileName(fileImage1.PostedFile.FileName);
                    }
                    else
                    {
                        Cat_Image1 = "";
                    }
                }
                else
                {
                    Cat_Image1 = hplImage1.Text;
                }

                //get image2
                string Cat_Image2;

                if (trUploadImage2.Visible == true)
                {
                    if (fileImage2.PostedFile != null)
                    {
                        Cat_Image2 = Path.GetFileName(fileImage2.PostedFile.FileName);
                    }
                    else
                    {
                        Cat_Image2 = "";
                    }
                }
                else
                {
                    Cat_Image2 = hplImage2.Text;
                }

                ////get image3
                //string Cat_Image3;

                //if (trUploadImage3.Visible == true)
                //{
                //    if (fileImage3.PostedFile != null)
                //    {
                //        Cat_Image3 = Path.GetFileName(fileImage3.PostedFile.FileName);
                //    }
                //    else
                //    {
                //        Cat_Image3 = "";
                //    }
                //}
                //else
                //{
                //    Cat_Image3 = hplImage3.Text;
                //}

                if (m_cat_id == 0)
                {
                    //insert

                    ESHOP_CATEGORy cat_insert = new ESHOP_CATEGORy();

                    cat_insert.CAT_CODE = Cat_Code;
                    cat_insert.CAT_NAME = Cat_Name;
                    cat_insert.CAT_DESC = Cat_Desc;
                    cat_insert.CAT_URL = Cat_Url;
                    cat_insert.CAT_SEO_URL = Cat_Seo_Url;
                    cat_insert.CAT_SEO_TITLE = Cat_Seo_Tittle;
                    cat_insert.CAT_SEO_KEYWORD = Cat_Seo_Keyword;
                    cat_insert.CAT_SEO_DESC = Cat_Seo_Description;
                    cat_insert.CAT_TARGET = Cat_Target;

                    cat_insert.CAT_PARENT_ID = Cat_Parent_Id;
                    cat_insert.CAT_STATUS = Cat_Status;
                    //cat_insert.CAT_ACCESS = Cat_Access;
                    cat_insert.CAT_TYPE = Cat_Type;
                    cat_insert.CAT_POSITION = Cat_Pos;
                    cat_insert.CAT_SHOWFOOTER = Cat_Footer;
                    cat_insert.CAT_SHOWITEM = Cat_ShowItems;
                    cat_insert.CAT_PERIOD = Cat_Period;
                    cat_insert.CAT_LANGUAGE = Cat_Language;
                    cat_insert.CAT_ORDER = Cat_Order;
                    cat_insert.CAT_PERIOD_ORDER = Cat_Order_Period;
                    cat_insert.CAT_PAGEITEM = Cat_PageItem;

                    cat_insert.CAT_PARENT_PATH = Cat_Parent_Path;
                    cat_insert.CAT_RANK = Cat_Rank;

                    cat_insert.CAT_IMAGE1 = Cat_Image1;
                    cat_insert.CAT_IMAGE2 = Cat_Image2;
                    //cat_insert.CAT_IMAGE3 = Cat_Image3;

                    //cat_insert.CAT_CODE_EN = Cat_Code_En;
                    //cat_insert.CAT_NAME_EN = Cat_Name_En;
                    //cat_insert.CAT_DESC_EN = Cat_Desc_En;

                    //cat_insert.CAT_SEO_URL_EN = Cat_Seo_Url_En;
                    //cat_insert.CAT_SEO_TITLE_EN = Cat_Seo_Tittle_En;
                    //cat_insert.CAT_SEO_KEYWORD_EN = Cat_Seo_Keyword_En;
                    //cat_insert.CAT_SEO_DESC_EN = Cat_Seo_Description_En;


                    DB.ESHOP_CATEGORies.InsertOnSubmit(cat_insert);
                    DB.SubmitChanges();

                    var _cat = DB.GetTable<ESHOP_CATEGORy>().OrderByDescending(g => g.CAT_ID).Take(1);

                    m_cat_id = _cat.Single().CAT_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "category_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == m_cat_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.Single().CAT_CODE = Cat_Code;
                        c_update.Single().CAT_NAME = Cat_Name;
                        c_update.Single().CAT_DESC = Cat_Desc;
                        c_update.Single().CAT_URL = Cat_Url;
                        c_update.Single().CAT_SEO_URL = Cat_Seo_Url;
                        c_update.Single().CAT_SEO_TITLE = Cat_Seo_Tittle;
                        c_update.Single().CAT_SEO_KEYWORD = Cat_Seo_Keyword;
                        c_update.Single().CAT_SEO_DESC = Cat_Seo_Description;
                        c_update.Single().CAT_TARGET = Cat_Target;

                        c_update.Single().CAT_PARENT_ID = Cat_Parent_Id;
                        c_update.Single().CAT_STATUS = Cat_Status;
                        //c_update.Single().CAT_ACCESS = Cat_Access;
                        c_update.Single().CAT_TYPE = Cat_Type;
                        c_update.Single().CAT_POSITION = Cat_Pos;
                        c_update.Single().CAT_SHOWFOOTER = Cat_Footer;
                        c_update.Single().CAT_SHOWITEM = Cat_ShowItems;
                        c_update.Single().CAT_LANGUAGE = Cat_Language;
                        c_update.Single().CAT_ORDER = Cat_Order;
                        c_update.Single().CAT_PAGEITEM = Cat_PageItem;
                        c_update.Single().CAT_PERIOD = Cat_Period;
                        c_update.Single().CAT_PERIOD_ORDER = Cat_Order_Period;

                        c_update.Single().CAT_PARENT_PATH = Cat_Parent_Path;
                        c_update.Single().CAT_RANK = Cat_Rank;

                        c_update.Single().CAT_IMAGE1 = Cat_Image1;
                        c_update.Single().CAT_IMAGE2 = Cat_Image2;
                        //c_update.Single().CAT_IMAGE3 = Cat_Image3;

                        //c_update.Single().CAT_CODE_EN = Cat_Code_En;
                        //c_update.Single().CAT_NAME_EN = Cat_Name_En;
                        //c_update.Single().CAT_DESC_EN = Cat_Desc_En;
                        //c_update.Single().CAT_SEO_URL_EN = Cat_Seo_Url_En;
                        //c_update.Single().CAT_SEO_TITLE_EN = Cat_Seo_Tittle_En;
                        //c_update.Single().CAT_SEO_KEYWORD_EN = Cat_Seo_Keyword_En;
                        //c_update.Single().CAT_SEO_DESC_EN = Cat_Seo_Description_En;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "category_list.aspx" : strLink;
                    }
                }

                //update images 1
                if (trUploadImage1.Visible)
                {
                    if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
                    {
                        string pathfile = Server.MapPath("/data/categories/" + m_cat_id);
                        string fullpathfile = pathfile + "/" + Cat_Image1;

                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }

                        fileImage1.PostedFile.SaveAs(fullpathfile);
                    }

                }

                //update images 2
                if (trUploadImage2.Visible)
                {
                    if (!string.IsNullOrEmpty(fileImage2.PostedFile.FileName))
                    {
                        string pathfile = Server.MapPath("/data/categories/" + m_cat_id);
                        string fullpathfile = pathfile + "/" + Cat_Image2;

                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }

                        fileImage2.PostedFile.SaveAs(fullpathfile);
                    }

                }

                //if (trUploadImage3.Visible)
                //{
                //    if (!string.IsNullOrEmpty(fileImage3.PostedFile.FileName))
                //    {
                //        string pathfile = Server.MapPath("../data/categories/" + m_cat_id);
                //        string fullpathfile = pathfile + "/" + Cat_Image3;

                //        if (!Directory.Exists(pathfile))
                //        {
                //            Directory.CreateDirectory(pathfile);
                //        }

                //        fileImage3.PostedFile.SaveAs(fullpathfile);
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
                var G_info = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == m_cat_id);

                DB.ESHOP_CATEGORies.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathCategory(m_cat_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                Response.Redirect("category_list.aspx");

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

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object GroupId)
        {
            return "category.aspx?cat_id=" + Utils.CStrDef(GroupId);
        }

        private bool CheckExitsLink(string strLink)
        {
            try
            {
                var exits = (from c in DB.ESHOP_CATEGORies where c.CAT_SEO_URL == strLink && c.CAT_ID != m_cat_id select c);

                if (exits.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;

            }

        }

        #endregion
        
    }
}