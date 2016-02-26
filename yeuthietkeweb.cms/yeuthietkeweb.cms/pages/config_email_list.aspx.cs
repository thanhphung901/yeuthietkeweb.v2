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
    public partial class config_email_list : System.Web.UI.Page
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
            return "config_email.aspx?email_id=" + Utils.CStrDef(obj_id);
        }

        private void SearchResult()
        {
            try
            {
                var AllList = DB.ESHOP_EMAILs.ToList();


                if (AllList.ToList().Count > 0)
                    Session["EmailList"] = DataUtil.LINQToDataTable(AllList);

                rptList.DataSource = AllList;
                rptList.DataBind();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
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
                        int _Id = Utils.CIntDef(lblID.Text, 0);
                        items[j] = _Id;
                        j++;
                    }

                    i++;
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_EMAIL>().Where(g => items.Contains(g.EMAIL_ID));

                DB.ESHOP_EMAILs.DeleteAllOnSubmit(g_delete);
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

        #endregion
    }
}