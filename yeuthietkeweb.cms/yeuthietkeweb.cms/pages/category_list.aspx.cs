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
    public partial class category_list : System.Web.UI.Page
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
            return "category.aspx?cat_id=" + Utils.CStrDef(obj_id);
        }

        private void SearchResult()
        {
            try
            {
                //string keyword = CpanelUtils.ClearUnicode(txtKeyword.Value);

                var AllList = (from g in DB.ESHOP_CATEGORies
                               where g.CAT_RANK > 0
                               select new
                               {
                                   g.CAT_ID,
                                   g.CAT_PARENT_ID,
                                   CAT_NAME = (string.IsNullOrEmpty(g.CAT_CODE) ? g.CAT_NAME : g.CAT_NAME + "(" + g.CAT_CODE + ")"),
                                   g.CAT_POSITION,
                                   g.CAT_LANGUAGE,
                                   g.CAT_ORDER,
                                   g.CAT_PERIOD_ORDER,
                                   g.CAT_RANK
                               });

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    Session["CatList"] = DataUtil.LINQToDataTable(AllList);
                    //DataTable tbl = Session["CatList"] as DataTable;
                    DataTable tbl = DataUtil.LINQToDataTable(AllList);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);
                    if (IsPostBack)
                    {
                        rptList.DataSource = AllList;
                        rptList.DataBind();
                    }
                    else
                    {
                        rptList.DataSource = dsCat.Tables[0];
                        rptList.DataBind();
                    }
                }

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

        public string getPos(object Cat_Pos)
        {
            string str = "";
            switch (Utils.CIntDef(Cat_Pos))
            {
                case 0:
                    str = "Trên";
                    break;
                case 1: str = "Dưới";
                    break;
                case 2: str = "Chính";
                    break;
                case 20: str = "Khác";
                    break;
            }
            return str;
        }

        public string getLanguage(object Cat_Pos)
        {
            return Utils.CIntDef(Cat_Pos) == 1 ? "Việt Nam" : "English";
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
                var g_delete = DB.GetTable<ESHOP_CATEGORy>().Where(g => items.Contains(g.CAT_ID));

                DB.ESHOP_CATEGORies.DeleteAllOnSubmit(g_delete);
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
                    txtOrderPeriod = (HtmlInputText)item.FindControl("txtOrderPeriod");

                    //if (chkSelect.Checked)
                    //{
                        int catId = Utils.CIntDef(lblID.Text, 0);
                        var c_update = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == catId);

                        if (c_update.ToList().Count > 0)
                        {
                            c_update.Single().CAT_ORDER = Utils.CIntDef(txtOrder.Value);
                            c_update.Single().CAT_PERIOD_ORDER = Utils.CIntDef(txtOrderPeriod.Value);

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

            int catId = Utils.CIntDef(lblID.Text, 0);
            if (lnkbtnDel.CommandName == "Delete" && catId > 0)
            {
                //delete 
                var g_delete = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == catId);

                DB.ESHOP_CATEGORies.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
                SearchResult();
            }
        }
    }
}