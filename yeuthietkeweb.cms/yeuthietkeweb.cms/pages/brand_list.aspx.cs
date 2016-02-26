using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using yeuthietkeweb.cms.Components;

namespace yeuthietkeweb.cms.pages
{
    public partial class brand_list : System.Web.UI.Page
    {
        #region Declare
        int _count = 0;
        dbShopDataContext DB = new dbShopDataContext();
        #endregion

        #region properties
        public SortDirection sortProperty
        {
            get
            {
                if (ViewState["SortingState"] == null)
                {
                    ViewState["SortingState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortingState"];
            }
            set
            {
                ViewState["SortingState"] = value;
            }
        }

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

        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "brand.aspx?id=" + Utils.CStrDef(obj_id);
        }

        private void SearchResult()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_BRANDs
                               select g);
                rptList.DataSource = AllList;
                rptList.DataBind();

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
                        j++;
                    }

                    i++;
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_BRAND>().Where(g => items.Contains(g.ID));

                DB.ESHOP_BRANDs.DeleteAllOnSubmit(g_delete);
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

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                HtmlInputText txtOrder;
                HtmlInputText txtOrderPeriod;

                foreach (RepeaterItem item in rptList.Items)
                {
                    HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                    Label lblID = (Label)item.FindControl("lblID");
                    txtOrder = (HtmlInputText)item.FindControl("txtOrder");

                    //if (chkSelect.Checked)
                    //{
                        int Id = Utils.CIntDef(lblID.Text, 0);
                        var c_update = DB.GetTable<ESHOP_BRAND>().Where(g => g.ID == Id);

                        if (c_update.ToList().Count > 0)
                        {
                            c_update.Single().ORDERBY = Utils.CIntDef(txtOrder.Value);

                            DB.SubmitChanges();
                        }
                    //}
                    i++;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            { SearchResult(); }
        }

        #endregion

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtnDel = (LinkButton)e.Item.FindControl("lnkbtnDel");
            Label lblID = (Label)e.Item.FindControl("lblID");

            int Id = Utils.CIntDef(lblID.Text, 0);
            if (lnkbtnDel.CommandName == "Delete" && Id > 0)
            {
                //delete 
                var g_delete = DB.GetTable<ESHOP_BRAND>().Where(g => g.ID == Id);

                DB.ESHOP_BRANDs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
                SearchResult();
            }
        }
    }
}