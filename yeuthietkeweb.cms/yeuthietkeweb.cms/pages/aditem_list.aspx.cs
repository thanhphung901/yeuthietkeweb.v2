using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System;
using System.Linq;

namespace yeuthietkeweb.cms.pages
{
    public partial class aditem_list : System.Web.UI.Page
    {
        #region Declare

        private int _count = 0;
        dbShopDataContext DB = new dbShopDataContext();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SearchResult();
            }

        }

        #endregion


        #region My Function

        public string getImage(object obj_id, object obj_image1)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_id)) && Utils.CIntDef(obj_id) > 0)
                return "<img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_image1) + "' width='150px' border='0' />";
            else
                return "";
        }
        public string getPosition(object obj_position)
        {
            string str = "";
            switch (Utils.CIntDef(obj_position))
            {
                case 0: str = "Slideshow";
                    break;
                case 1: str = "Góc Phải";
                    break;
                case 2: str = "Hổ trợ đặt hàng";
                    break;
            }
            return str;
        }


        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "aditem.aspx?ad_id=" + obj_id;
        }

        public string getLinkImage(object obj_id, object obj_file)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_file)) && Utils.CIntDef(obj_id) > 0)
            {
                return "<a href='" + PathFiles.GetPathAdItems(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_file) + "' target='_blank'>" + Utils.CStrDef(obj_file) + "</a>";
            }

            return "";
        }

        private void SearchResult()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_AD_ITEMs
                               orderby g.AD_ITEM_PUBLISHDATE descending, g.AD_ITEM_ORDER descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["AdItemList"] = DataUtil.LINQToDataTable(AllList);

                rptList.DataSource = AllList;
                rptList.DataBind();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void DeleteInfo(int ad_id)
        {
            string strLink = "";
            try
            {
                string Banner_File = "";

                var G_info = DB.GetTable<ESHOP_AD_ITEM>().Where(g => g.AD_ITEM_ID == ad_id);

                if (G_info.ToList().Count > 0)
                    Banner_File = Utils.CStrDef(G_info.ToList()[0].AD_ITEM_ID);

                DB.ESHOP_AD_ITEMs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete file
                if (!string.IsNullOrEmpty(Banner_File))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathAdItems(ad_id) + Banner_File);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }

                strLink = "aditem_list.aspx";

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

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        #endregion

        #region Button Envents

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;

            HtmlInputCheckBox check = new HtmlInputCheckBox();

            int[] items = new int[rptList.Items.Count];

            try
            {
                foreach (RepeaterItem item in rptList.Items)
                {
                    check = new HtmlInputCheckBox();
                    check = (HtmlInputCheckBox)item.FindControl("chkSelect");
                    Label lblID = (Label)item.FindControl("lblID");

                    if (check.Checked)
                    {
                        int catId = Utils.CIntDef(lblID.Text, 0);
                        items[j] = catId;
                        try
                        {
                            //delete folder
                            string fullpath = Server.MapPath(PathFiles.GetPathNews(items[j]));
                            if (Directory.Exists(fullpath))
                            {
                                DeleteAllFilesInFolder(fullpath);
                                Directory.Delete(fullpath);
                            }
                        }
                        catch (Exception)
                        { }
                        j++;
                    }

                    i++;
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_AD_ITEM>().Where(g => items.Contains(g.AD_ITEM_ID));

                DB.ESHOP_AD_ITEMs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                items = null;
                SearchResult();
            }

        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtnDel = (LinkButton)e.Item.FindControl("lnkbtnDel");
            Label lblID = (Label)e.Item.FindControl("lblID");

            int BannerId = Utils.CIntDef(lblID.Text, 0);
            if (lnkbtnDel.CommandName == "Delete" && BannerId > 0)
            {
                DeleteInfo(BannerId);
            }
        }

        #endregion
    }
}