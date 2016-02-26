using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Web.UI.HtmlControls;
using System.IO;

namespace yeuthietkeweb.cms.pages
{
    public partial class online_list : System.Web.UI.Page
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
            return "online.aspx?online_id=" + Utils.CStrDef(obj_id);
        }

        public string getType(object obj_type)
        {
            string str = "";
            switch (Utils.CIntDef(obj_type))
            {
                case 0:
                    str = "Hotline";
                    break;
                case 1:
                    str = "Yahoo";
                    break;
                case 2:
                    str = "Skype";
                    break;
                case 3:
                    str = "Facebook";
                    break;
                case 4:
                    str = "Google+";
                    break;
                case 5:
                    str = "Twitter";
                    break;
                case 6:
                    str = "Youtube";
                    break;
                case 7:
                    str = "Hổ trợ";
                    break;
                case 8:
                    str = "Mail";
                    break;
            }
            return str;
        }

        private void SearchResult()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_ONLINEs select g);


                if (AllList.ToList().Count > 0)
                    Session["OnlineList"] = DataUtil.LINQToDataTable(AllList);

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

                var g_delete = DB.GetTable<ESHOP_ONLINE>().Where(g => g.ONLINE_ID == _id);

                DB.ESHOP_ONLINEs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathOnline(_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("online_list.aspx");
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
                foreach (DataGridItem item in rptList.Items)
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
                var g_delete = DB.GetTable<ESHOP_ONLINE>().Where(g => items.Contains(g.ONLINE_ID));

                DB.ESHOP_ONLINEs.DeleteAllOnSubmit(g_delete);
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

        #region Grid Events
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblID = (Label)e.Item.FindControl("lblID");
            if (((LinkButton)e.CommandSource).CommandName == "Delete")
            {
                int _Id = Utils.CIntDef(lblID.Text, 0);
                EventDelete(_Id);
            }
        }
        #endregion
    }
}