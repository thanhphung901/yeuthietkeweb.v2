using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Web.UI.HtmlControls;

namespace yeuthietkeweb.cms.pages
{
    public partial class contact_list : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
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

        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void SearchResult()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_CONTACTs
                               where g.CONTACT_TYPE != 1
                               orderby g.CONTACT_PUBLISHDATE descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["ContactList"] = DataUtil.LINQToDataTable(AllList);

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
            try
            {

                var g_delete = DB.GetTable<ESHOP_CONTACT>().Where(g => g.CONTACT_ID == _id);

                DB.ESHOP_CONTACTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("contact_list.aspx?");
            }
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
                var g_delete = DB.GetTable<ESHOP_CONTACT>().Where(g => items.Contains(g.CONTACT_ID));

                DB.ESHOP_CONTACTs.DeleteAllOnSubmit(g_delete);
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

        public string getLink(object obj_id)
        {
            return "contact.aspx?contact_id=" + Utils.CStrDef(obj_id);
        }

        #endregion

        #region Grid Events

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtnDel = (LinkButton)e.Item.FindControl("lnkbtnDel");
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