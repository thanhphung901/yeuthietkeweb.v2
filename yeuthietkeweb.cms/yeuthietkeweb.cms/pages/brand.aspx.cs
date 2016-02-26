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
    public partial class brand : System.Web.UI.Page
    {
        #region Declare

        private int brandId = 0;
        int _count = 0;
        dbShopDataContext DB = new dbShopDataContext();
        string m_pathFile = "";
        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            brandId = Utils.CIntDef(Request["id"]);

            if (brandId == 0)
            {
                lbtDelete.Visible = false;
                trImage1.Visible = false;
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
            SaveInfo("brand.aspx");
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
                var n_info = DB.GetTable<ESHOP_BRAND>().Where(n => n.ID == brandId);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].IMAGE1))
                    {
                        string imagePath = Server.MapPath("/data/brand/" + brandId + "/" + n_info.ToList()[0].IMAGE1);
                        n_info.ToList()[0].IMAGE1 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "brand.aspx?id=" + brandId;
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

        private void getInfo()
        {
            try
            {

                var G_info = (from c in DB.ESHOP_BRANDs
                              where c.ID == brandId
                              select c
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].NAME;
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].ORDERBY);
                    rblActive.SelectedValue = Utils.CStrDef(G_info.ToList()[0].ISACTIVE);                   


                    //image 1
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].IMAGE1))
                    {
                        trUploadImage1.Visible = false;
                        trImage1.Visible = true;
                        Image1.Src = "/data/brand/" + brandId + "/" + G_info.ToList()[0].IMAGE1;
                        hplImage1.NavigateUrl = "/data/brand/" + brandId + "/" + G_info.ToList()[0].IMAGE1;
                        hplImage1.Text = G_info.ToList()[0].IMAGE1;
                    }
                    else
                    {
                        trUploadImage1.Visible = true;
                        trImage1.Visible = false;
                    }

                    ////image 2
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].CAT_IMAGE2))
                    //{
                    //    trUploadImage2.Visible = false;
                    //    trImage2.Visible = true;
                    //    Image2.Src = PathFiles.GetPathCategory(brandId) + G_info.ToList()[0].CAT_IMAGE2;
                    //    hplImage2.NavigateUrl = PathFiles.GetPathCategory(brandId) + G_info.ToList()[0].CAT_IMAGE2;
                    //    hplImage2.Text = G_info.ToList()[0].CAT_IMAGE2;
                    //}
                    //else
                    //{
                    //    trUploadImage2.Visible = true;
                    //    trImage2.Visible = false;
                    //}

                    ////image 3
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].CAT_IMAGE3))
                    //{
                    //    trUploadImage3.Visible = false;
                    //    trImage3.Visible = true;
                    //    Image3.Src = PathFiles.GetPathCategory(brandId) + G_info.ToList()[0].CAT_IMAGE3;
                    //    hplImage3.NavigateUrl = PathFiles.GetPathCategory(brandId) + G_info.ToList()[0].CAT_IMAGE3;
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
                string IMAGE1;

                if (trUploadImage1.Visible == true)
                {
                    if (fileImage1.PostedFile != null)
                    {
                        IMAGE1 = Path.GetFileName(fileImage1.PostedFile.FileName);
                    }
                    else
                    {
                        IMAGE1 = "";
                    }
                }
                else
                {
                    IMAGE1 = hplImage1.Text;
                }

                ////get image2
                //string Cat_Image2;

                //if (trUploadImage2.Visible == true)
                //{
                //    if (fileImage2.PostedFile != null)
                //    {
                //        Cat_Image2 = Path.GetFileName(fileImage2.PostedFile.FileName);
                //    }
                //    else
                //    {
                //        Cat_Image2 = "";
                //    }
                //}
                //else
                //{
                //    Cat_Image2 = hplImage2.Text;
                //}

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

                if (brandId == 0)
                {
                    //insert

                    ESHOP_BRAND cat_insert = new ESHOP_BRAND();

                    cat_insert.NAME = txtName.Value;
                    cat_insert.ORDERBY = Utils.CIntDef(txtOrder.Value);
                    cat_insert.ISACTIVE = Utils.CIntDef(rblActive.SelectedValue);


                    cat_insert.IMAGE1 = IMAGE1;
                    //cat_insert.CAT_IMAGE2 = Cat_Image2;
                    //cat_insert.CAT_IMAGE3 = Cat_Image3;


                    DB.ESHOP_BRANDs.InsertOnSubmit(cat_insert);
                    DB.SubmitChanges();

                    brandId = cat_insert.ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "brand_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<ESHOP_BRAND>().Where(g => g.ID == brandId);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.ToList()[0].NAME = txtName.Value;
                        c_update.ToList()[0].ORDERBY = Utils.CIntDef(txtOrder.Value);
                        c_update.ToList()[0].ISACTIVE = Utils.CIntDef(rblActive.SelectedValue);


                        c_update.Single().IMAGE1 = IMAGE1;
                        //c_update.Single().CAT_IMAGE2 = Cat_Image2;
                        //c_update.Single().CAT_IMAGE3 = Cat_Image3;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "brand_list.aspx" : strLink;
                    }
                }

                //update images 1
                if (trUploadImage1.Visible)
                {
                    if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
                    {
                        string pathfile = Server.MapPath("/data/brand/" + brandId);
                        string fullpathfile = pathfile + "/" + IMAGE1;

                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }

                        fileImage1.PostedFile.SaveAs(fullpathfile);
                    }

                }

                ////update images 2
                //if (trUploadImage2.Visible)
                //{
                //    if (!string.IsNullOrEmpty(fileImage2.PostedFile.FileName))
                //    {
                //        string pathfile = Server.MapPath("../data/brand/" + brandId);
                //        string fullpathfile = pathfile + "/" + Cat_Image2;

                //        if (!Directory.Exists(pathfile))
                //        {
                //            Directory.CreateDirectory(pathfile);
                //        }

                //        fileImage2.PostedFile.SaveAs(fullpathfile);
                //    }

                //}

                //if (trUploadImage3.Visible)
                //{
                //    if (!string.IsNullOrEmpty(fileImage3.PostedFile.FileName))
                //    {
                //        string pathfile = Server.MapPath("../data/brand/" + brandId);
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
                var G_info = DB.GetTable<ESHOP_BRAND>().Where(g => g.ID == brandId);

                DB.ESHOP_BRANDs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath("/data/brand/" + brandId);
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                Response.Redirect("brand_list.aspx");

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
            return "brand.aspx?id=" + Utils.CStrDef(GroupId);
        }

        #endregion
        
    }
}