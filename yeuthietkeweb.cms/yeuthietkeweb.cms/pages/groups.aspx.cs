using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vpro.functions;
using System.Web.UI.HtmlControls;

namespace yeuthietkeweb.cms.pages
{
    public partial class groups : System.Web.UI.Page
    {
        #region Declare

        private int m_group_id = 0;
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

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_group_id = Utils.CIntDef(Request["group_id"]);

            if (m_group_id == 0)
            {
                lbtDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                getInfo();
                LoadCat();
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
            SaveInfo("groups.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_GROUPs
                              where g.GROUP_ID == m_group_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtCode.Value = G_info.ToList()[0].GROUP_CODE;
                    txtName.Value = G_info.ToList()[0].GROUP_NAME;
                    //chkAdmin.Checked = Utils.CBoolDef(G_info.ToList()[0].GROUP_ADMIN);
                    rblType.SelectedValue = Utils.CStrDef(G_info.ToList()[0].GROUP_TYPE);
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
                string Code = txtCode.Value;
                string Name = txtName.Value;
                int Admin = Utils.CIntDef(rblType.SelectedValue);

                if (m_group_id == 0)
                {
                    //insert
                    ESHOP_GROUP g_insert = new ESHOP_GROUP();
                    g_insert.GROUP_CODE = Code;
                    g_insert.GROUP_NAME = Name;
                    g_insert.GROUP_TYPE = Admin;

                    DB.ESHOP_GROUPs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    var _new = DB.GetTable<ESHOP_GROUP>().OrderByDescending(g => g.GROUP_ID).Take(1);

                    SaveGroupCat(_new.Single().GROUP_ID);

                    DB.SubmitChanges();

                    strLink = string.IsNullOrEmpty(strLink) ? "group_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_GROUP>().Where(g => g.GROUP_ID == m_group_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().GROUP_CODE = Code;
                        g_update.Single().GROUP_NAME = Name;
                        g_update.Single().GROUP_TYPE = Admin;

                        SaveGroupCat(m_group_id);

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "group_list.aspx" : strLink;
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

        private void SaveGroupCat(int GroupId)
        {
            try
            {
                int i = 0;
                HtmlInputCheckBox check = new HtmlInputCheckBox();

                var gcdel = (from gp in DB.ESHOP_GROUP_CATs
                             where gp.GROUP_ID == GroupId
                             select gp);
                DB.ESHOP_GROUP_CATs.DeleteAllOnSubmit(gcdel);

                foreach (RepeaterItem item in rptList.Items)
                {
                    check = new HtmlInputCheckBox();
                    check = (HtmlInputCheckBox)item.FindControl("chkSelect");
                    Label lblID = (Label)item.FindControl("lblID");

                    if (check.Checked)
                    {
                        int _Id = Utils.CIntDef(lblID.Text, 0);
                        ESHOP_GROUP_CAT grinsert = new ESHOP_GROUP_CAT();
                        grinsert.CAT_ID = _Id;
                        grinsert.GROUP_ID = GroupId;

                        DB.ESHOP_GROUP_CATs.InsertOnSubmit(grinsert);
                    }

                    i++;
                }

                //DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_GROUP>().Where(g => g.GROUP_ID == m_group_id);

                DB.ESHOP_GROUPs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("group_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object GroupId)
        {
            return "groups.aspx?group_id=" + Utils.CStrDef(GroupId);
        }

        private void LoadCat()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_CATEGORies
                               where g.CAT_RANK > 0
                               select new
                               {
                                   g.CAT_ID,
                                   g.CAT_PARENT_ID,
                                   g.CAT_RANK,
                                   CAT_NAME = (string.IsNullOrEmpty(g.CAT_CODE) ? g.CAT_NAME : g.CAT_NAME + "(" + g.CAT_CODE + ")")
                               });

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    Session["CatGroupList"] = DataUtil.LINQToDataTable(AllList);
                    DataTable tbl = Session["CatGroupList"] as DataTable;

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    rptList.DataSource = CatTable;
                    rptList.DataBind();
                }


                //var allCat=DB.GetTable<ESHOP_CATEGORy>();

                //rptList.DataSource = allCat;
                //rptList.DataBind();

                //Session["CatGroupList"] = allCat;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public bool CheckCat(object CatId)
        {
            try
            {
                int Per_Id = Utils.CIntDef(CatId);

                var per = DB.GetTable<ESHOP_GROUP_CAT>().Where(gp => gp.GROUP_ID == m_group_id && gp.CAT_ID == Per_Id);
                if (per.ToList().Count > 0)
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