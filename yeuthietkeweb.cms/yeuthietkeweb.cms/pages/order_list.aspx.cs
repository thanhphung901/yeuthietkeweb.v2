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
    public partial class order_list : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        int m_cus_id = 0;
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
            m_cus_id = Utils.CIntDef(Request["cus_id"]);

            if (!IsPostBack)
            {

                //ucFromDate.returnDate = DateTime.Now.Add(new TimeSpan(-30, 0, 0, 0));
                //ucToDate.returnDate = DateTime.Now;

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
                string keyword = txtKeyword.Value;
                //DateTime fromDate = ucFromDate.returnDate;
                //DateTime toDate = new DateTime(ucToDate.returnDate.Year, ucToDate.returnDate.Month, ucToDate.returnDate.Day, 23, 59, 59);

                int _status = Utils.CIntDef(ddlStatus.SelectedValue);
                if (_status != 99)
                {
                    var AllList = (from o in DB.ESHOP_ORDERs
                                   join o_i in DB.ESHOP_ORDER_ITEMs on o.ORDER_ID equals o_i.ORDER_ID
                                   where ("" == keyword || (o.ORDER_CODE).Contains(keyword) || (o.ORDER_FIELD1).Contains(keyword))
                                   //&& o.ORDER_PUBLISHDATE <= toDate && o.ORDER_PUBLISHDATE >= fromDate
                                   && o.ORDER_STATUS == _status
                                   orderby o.ORDER_PUBLISHDATE descending
                                   select o).Distinct().OrderByDescending(n => n.ORDER_ID);

                    if (AllList.ToList().Count > 0)
                        Session["OrderList"] = DataUtil.LINQToDataTable(AllList);

                    rptList.DataSource = AllList;
                    rptList.DataBind();
                }
                else
                {
                    var AllList = (from o in DB.ESHOP_ORDERs
                                   join o_i in DB.ESHOP_ORDER_ITEMs on o.ORDER_ID equals o_i.ORDER_ID
                                   where ("" == keyword || (o.ORDER_CODE).Contains(keyword) || (o.ORDER_FIELD1).Contains(keyword))
                                   //&& o.ORDER_PUBLISHDATE <= toDate && o.ORDER_PUBLISHDATE >= fromDate
                                   orderby o.ORDER_PUBLISHDATE descending
                                   select o).Distinct().OrderByDescending(n => n.ORDER_ID);


                    if (AllList.ToList().Count > 0)
                        Session["OrderList"] = DataUtil.LINQToDataTable(AllList);

                    rptList.DataSource = AllList;
                    rptList.DataBind();
                }
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
                var g_delete = DB.GetTable<ESHOP_ORDER>().Where(g => g.ORDER_ID == _id);

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("order_list.aspx");
            }
        }

        public string getPublishDate(object obj_date)
        {
            return string.Format("{0:dd-MM-yyyy}", obj_date);
        }

        public string getLink(object obj_id)
        {
            return "order.aspx?order_id=" + Utils.CStrDef(obj_id);
        }

        public string getOrderStatus(object obj_status)
        {
            switch (Utils.CIntDef(obj_status))
            {
                case 0:
                    return "<font color='#FF0000'>Chưa xử lý</font>";
                case 1:
                    return "<font color='#0c5cd4'>Đang xử lý</font>";
                case 2:
                    return "<font color='#0c5cd4'>Đã xác nhận</font>";
                case 3:
                    return "<font color='#0c5cd4'>Đang giao hàng</font>";
                case 4:
                    return "<font color='#529214'>Giao hàng thành công</font>";
                case 5:
                    return "<font color='#c4670c'>Hủy đơn hàng</font>";
                default:
                    return "Chưa xử lý";
            }
        }

        public string getOrderPayment(object obj_payment)
        {
            switch (Utils.CIntDef(obj_payment))
            {
                case 1:
                    return "<font color='#0c5cd4'>Thanh toán bằng tiền mặt</font>";
                case 2:
                    return "<font color='#529214'>Thanh toán tại ngân hàng</font>";
                default:
                    return "Khác";
            }
        }

        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#} đ", obj_value).Replace(',', '.');
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
                        int _Id = Utils.CIntDef(lblID.Text, 0);
                        items[j] = _Id;
                        j++;
                    }

                    i++;
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_ORDER>().Where(g => items.Contains(g.ORDER_ID));

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(g_delete);
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
            LinkButton lnkbtnDel = (LinkButton)e.Item.FindControl("lnkbtnDel");
            Label lblID = (Label)e.Item.FindControl("lblID");
            int _id = Utils.CIntDef(lblID.Text, 0);
            if (((LinkButton)e.CommandSource).CommandName == "Delete")
            {
                EventDelete(_id);
            }
        }

        protected void rptList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((((e.Item.ItemType == ListItemType.Item) | (e.Item.ItemType == ListItemType.AlternatingItem)) | (e.Item.ItemType == ListItemType.SelectedItem)))
            {
                e.Item.Cells[11].Attributes.Add("onClick", "return confirm('Bạn có chắc chắn xóa?');");
            }
        }
        public IQueryable Load_Products(int _id)
        {
            try
            {
                var list = (from o in DB.ESHOP_ORDER_ITEMs
                            where o.ORDER_ID == _id
                            orderby o.ESHOP_NEW.NEWS_TITLE descending
                            select new
                            {
                                o.ITEM_ID,
                                o.ESHOP_NEW.NEWS_TITLE,
                                o.ITEM_QUANTITY,
                                o.ITEM_PRICE,
                                o.ITEM_SUBTOTAL,
                                o.ITEM_FIELD1,
                                o.ITEM_FIELD2
                            });
                return list;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        public List<ESHOP_ORDER_ITEM> Load_DonHang(int id)
        {
            var AllList = DB.ESHOP_ORDER_ITEMs.Where(n => n.ORDER_ID == id).ToList();
            return AllList;
        }
        #endregion
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}