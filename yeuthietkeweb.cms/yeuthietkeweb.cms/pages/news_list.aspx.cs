using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;
using yeuthietkeweb.cms.Components;

namespace yeuthietkeweb.cms.pages
{
    public partial class news_list : System.Web.UI.Page
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
                Loadchuyenmuc();
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
            return "news.aspx?news_id=" + Utils.CStrDef(obj_id);
        }
        public string getLink_comment(object obj_id)
        {
            return "news_comment.aspx?news_id=" + Utils.CStrDef(obj_id);
        }
        public int Getcount_comment(object NewsID)
        {
            try
            {
                int _iNewsID = Utils.CIntDef(NewsID);
                var _vComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a => a.NEWS_ID == _iNewsID);
                return _vComment.ToList().Count;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return 0;
            }
        }
        public string getTypeNew(object obj_id)
        {
            return (Utils.CIntDef(obj_id) == 0) ? "Tin tức" : ((Utils.CIntDef(obj_id) == 1) ? "Dịch vụ" : ((Utils.CIntDef(obj_id) == 2) ? "Hình ảnh" : "N/A"));
        }
        public void Loadchuyenmuc()
        {
            try
            {
                var CatList = (
                                from t2 in DB.ESHOP_CATEGORies
                                where t2.CAT_RANK > 0
                                select new
                                {
                                    CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
                                    CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
                                    CAT_NAME_EN = (string.IsNullOrEmpty(t2.CAT_CODE_EN) ? t2.CAT_NAME_EN : t2.CAT_NAME_EN + "(" + t2.CAT_CODE_EN + ")"),
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
                ListItem l = new ListItem("------ Chọn chuyên mục ------", "0", true);
                l.Selected = true;
                ddlCategory.Items.Insert(0, l);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SearchResult()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_NEWs
                               orderby g.NEWS_ID descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["NewsList"] = DataUtil.LINQToDataTable(AllList);

                rptList.DataSource = AllList;
                rptList.DataBind();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public string GetNewsStatus(object NewsID, object NewsTitle)
        {
            try
            {
                int _iNewsID = Utils.CIntDef(NewsID);
                var _vComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a => a.NEWS_ID == _iNewsID && a.COMMENT_CHECK == 0);
                if (_vComment.ToList().Count > 0)
                {
                    return Utils.CStrDef(NewsTitle) + " - <font color='#FF0000'>Có phản hồi mới</font>";
                }
                else
                {
                    return Utils.CStrDef(NewsTitle);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }

        public string getStatus(object obj_status)
        {
            return Utils.CIntDef(obj_status) == 0 ? "Ẩn" : "Hiển thị";
        }

        public string getLanguage(object News_Language)
        {
            return Utils.CIntDef(News_Language) == 1 ? "Việt Nam" : "English";
        }

        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm:ss}", News_PublishDate);
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
                var g_delete = DB.GetTable<ESHOP_NEW>().Where(g => items.Contains(g.NEWS_ID));

                DB.ESHOP_NEWs.DeleteAllOnSubmit(g_delete);
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

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        #endregion

        #region Grid Events

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkbtnDel = (LinkButton)e.Item.FindControl("lnkbtnDel");
            Label lblID = (Label)e.Item.FindControl("lblID");

            int NewsId = Utils.CIntDef(lblID.Text, 0);
            if (lnkbtnDel.CommandName == "Delete" && NewsId > 0)
            {
                //delete 
                var g_delete = DB.GetTable<ESHOP_NEW>().Where(g => g.NEWS_ID == NewsId);

                DB.ESHOP_NEWs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
                SearchResult();
            }
        }

        #endregion

        //gui mail km
        public void SendEmailSMTP(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]));
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = strSubject;

                    string str = body;
                    mail.Body = str;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = isSSL;
                    client.Host = Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailHost"]);
                    client.Port = Utils.CIntDef(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                    client.Credentials = new System.Net.NetworkCredential(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]));

                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void Send_Mail_Content(string MailContent, string Email)
        {
            try
            {
                string strEmailBody = "";
                string url = string.IsNullOrEmpty(Utils.CStrDef(Session["News_url"])) ? "/tin-tuc/" + Utils.CStrDef(Session["News_seo_url"]) + ".aspx" : Utils.CStrDef(Session["News_url"]);


                strEmailBody = "<html><body>";
                strEmailBody += "Click vào những đường link bên dưới để xem nội dung chi tiết.<br />";
                strEmailBody += MailContent;
                strEmailBody += "</body></html>";

                SendEmailSMTP("Vui lòng ghé thăm website myphamhanqoucso1.com", Email, "", "", strEmailBody, true, false);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        protected void lbtnSendMail_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //int j = 0;
            //HtmlInputCheckBox check = new HtmlInputCheckBox();
            //int[] items = new int[rptList.Items.Count];

            //string _sMailContent = string.Empty;

            //try
            //{
            //    foreach (DataGridItem item in rptList.Items)
            //    {
            //        check = new HtmlInputCheckBox();
            //        check = (HtmlInputCheckBox)item.Cells[1].FindControl("chkSelect");

            //        if (check.Checked)
            //        {
            //            items[j] = Utils.CIntDef(rptList.DataKeys[i]);
            //            try
            //            {
            //                //Lấy nội dung mail
            //                var _v = DB.ESHOP_NEWs.Single(a => a.NEWS_ID == items[j]);
            //                if (_v != null)
            //                {
            //                    _v.NEWS_SENDDATE = DateTime.Now;
            //                    DB.SubmitChanges();

            //                    string link = _v.NEWS_TYPE == 0 ? System.Configuration.ConfigurationManager.AppSettings["URLWebsite1"] + "tin-tuc/" + _v.NEWS_SEO_URL + ".aspx" : System.Configuration.ConfigurationManager.AppSettings["URLWebsite1"] + "san-pham/" + _v.NEWS_SEO_URL + ".aspx";
            //                    _sMailContent += "<a href='" + link + "'>" + _v.NEWS_TITLE + "</a><br />";
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                clsVproErrorHandler.HandlerError(ex);
            //            }
            //            j++;
            //        }

            //        i++;
            //    }
            //    //Gửi mail
            //    var _vEmailReceive = DB.GetTable<ESHOP_MAIL_RECIVE>().Where(a => a.MAIL_ACTIVE == 1);
            //    foreach (var item in _vEmailReceive)
            //    {
            //        Send_Mail_Content(_sMailContent, item.MAIL_NAME);
            //    }
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('Thông báo: Tin tức đã được gửi thành công!');document.location='" + ResolveClientUrl("/cpanel/page/news_list.aspx") + "';</script>");
            //}
            //catch (Exception ex)
            //{
            //    clsVproErrorHandler.HandlerError(ex);
            //}
            //finally
            //{
            //    items = null;
            //    SearchResult();
            //}
        }
        protected void Drchuyenmuc_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int id = Utils.CIntDef(ddlCategory.SelectedValue);
                var list = (from a in DB.ESHOP_NEWs
                            join b in DB.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                            join c in DB.ESHOP_CATEGORies on b.CAT_ID equals c.CAT_ID
                            where (c.CAT_ID == id || c.CAT_PARENT_PATH.Contains(id.ToString()))
                            select a).OrderByDescending(n => n.NEWS_PUBLISHDATE).ToList();
                Session["NewsList"] = DataUtil.LINQToDataTable(list);

                rptList.DataSource = list;
                rptList.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void Change_nguon(object sender, EventArgs e)
        {
            int N_ID = Utils.CIntDef(Ddnguon.SelectedValue, -1);
            var s = DB.ESHOP_NEWs.Where(n => n.NEWS_TYPE == N_ID || N_ID == -1).ToList();
            if (s.Count > 0)
                Session["NewsList"] = DataUtil.LINQToDataTable(s);

            rptList.DataSource = s;
            rptList.DataBind();
        }
    }
}