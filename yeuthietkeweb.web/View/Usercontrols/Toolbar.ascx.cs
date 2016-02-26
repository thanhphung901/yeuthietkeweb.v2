using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using GiaNguyen.Components;
using Controller;
using System.Net.Mail;
using System.Drawing;


namespace GiaNguyen.UIs
{
    public partial class Toolbar : System.Web.UI.UserControl
    {
        #region Declare

        public clsFormat _clsFormat = new clsFormat();
        News_details ndetail = new News_details();
        Config cf = new Config();
        clsFormat fm = new clsFormat();
        string _sNews_Seo_Url = string.Empty;
        SendMail send = new SendMail();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _sNews_Seo_Url = Utils.CStrDef(Request.QueryString["purl"]);

            }
        }
        #region Button Handler
        protected void lbtSendEmail_Click(object sender, EventArgs e)
        {
            string strSecView = LookCookie().ToLower();
            string strSecurity = Send_txtCapcha.Value.ToString().ToLower();
            if (strSecurity != strSecView)
            {
                Response.Write("<script>alert('Nhập mã bảo mật sai!');</script>");
                return;
            }
            //if (this.Send_txtCapcha.Value != this.Session["CaptchaImageText"].ToString())
            //{
            //    Send_lblResult.ForeColor = Color.Red;
            //    Send_lblResult.Text = "Mã bảo vệ không đúng.";
            //    //mp1.Show();
            //}
            //else
            //{
                string strEmailBody = "";
                //string url = string.IsNullOrEmpty(Utils.CStrDef(Session["News_url"])) ? "/tin-tuc/" + Utils.CStrDef(Session["News_seo_url"]) + ".html" : Utils.CStrDef(Session["News_url"]);

                strEmailBody = "<html><body>";
                //strEmailBody += "Chào  " !<br>";
                strEmailBody += Send_txtFullname.Value + " (<a href='mailto:" + Send_txtEmail.Value + "'>" + Send_txtEmail.Value + "</a>) gửi cho bạn thông tin này.<br/>Với lời nhắn : <br/>" + Send_txtContent.Value + "<br/>Click vào đường link bên dưới để xem nội dung chi tiết.<br>";
                //strEmailBody += "<a href='" + url + "'>" + url + "</a>";
                strEmailBody += "<a href='" + Request.ServerVariables["HTTP_REFERER"] + "'>" + Request.ServerVariables["HTTP_REFERER"] + "</a>";
                strEmailBody += "</body></html>";

                send.SendEmailSMTP("Vui lòng ghé thăm website ", Send_txtEmailTo.Value, Send_txtEmailCC.Value, "", strEmailBody, true, false);
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]))
                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"]);
            //}
        }
        private void SendEmailToFriend()
        {
            try
            {
                string strEmailSubject = "";
                string strEmailBody = "";
                //string url = string.IsNullOrEmpty(Utils.CStrDef(Session["News_url"])) ? "/tin-tuc/" + Utils.CStrDef(Session["News_seo_url"]) + ".html" : Utils.CStrDef(Session["News_url"]);

                strEmailSubject = "Vui lòng ghé thăm website";
                strEmailBody = "<html><body>";
                //strEmailBody += "Chào  " !<br>";
                strEmailBody += Send_txtFullname.Value + " (<a href='mailto:" + Send_txtEmail.Value + "'>" + Send_txtEmail.Value + "</a>) gửi cho bạn thông tin này.<br/>Với lời nhắn : <br/>" + Send_txtContent.Value + "<br/>Click vào đường link bên dưới để xem nội dung chi tiết.<br>";
                //strEmailBody += "<a href='" + url + "'>" + url + "</a>";
                strEmailBody += "<a href='" + Request.ServerVariables["HTTP_REFERER"] + "'>" + Request.ServerVariables["HTTP_REFERER"] + "</a>";
                strEmailBody += "</body></html>";

                clsMail.SendMailNet(Send_txtEmailTo.Value, Send_txtEmailCC.Value, "", strEmailSubject, strEmailBody, true, true);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion

        #region FeedBack

        protected void lbtFeedback_Click(object sender, EventArgs e)
        {

            string strSecView = LookCookie().ToLower();
            string strSecurity = txtCapcha.Value.ToString().ToLower();
            if (strSecurity != strSecView)
            {
                Response.Write("<script>alert('Nhập mã bảo mật sai!');</script>");
                return;
            }
            //if (this.txtCapcha.Value != this.Session["CaptchaImageText"].ToString())
            //{
            //    lbResult.ForeColor = Color.Red;
            //    lbResult.Text = "Mã bảo vệ không đúng.";
            //    //mp1.Show();
            //}
            //else
            //{
                string _sCat_Seo_Url = Utils.CStrDef(Request.QueryString["curl"]);
                string _sNews_Seo_Url = Utils.CStrDef(Request.QueryString["purl"]);
                ndetail.Inser_comment_News(_sCat_Seo_Url, _sNews_Seo_Url, full_name.Value, txtemail.Value, message.Value);
                SendEmailFeedback();
                ClearFeedbackForm();
                string url = Request.RawUrl;

                if (!string.IsNullOrEmpty(url))
                    Response.Redirect(url);
            //}
        }

        private void ClearFeedbackForm()
        {
            full_name.Value = "";
            txtemail.Value = "";
            message.Value = "";
            txtCapcha.Value = "";
        }
        public bool Checkcaturl(string _sCat_Seo_Url)
        {

            if (!string.IsNullOrEmpty(_sCat_Seo_Url))
            {
                return true;

            }
            return false;
        }
        //private int Getnewsid()
        //{
        //    int _newsID = Utils.CIntDef(Session["news_id"]);
        //    int _CatID = Utils.CIntDef(Session["cat_id"]);
        //    var _vNewsID = db.GetTable<ESHOP_NEWS_CAT>().Where(a => a.CAT_ID == _CatID);
        //    _newsID = _vNewsID.ToList().Count > 0 ? Utils.CIntDef(_vNewsID.ToList()[0].NEWS_ID) : 0;
        //    return _newsID;
        //}
        //private void InsertFeedback(string _sCat_Seo_Url)
        //{
        //    try
        //    {
        //        ESHOP_NEWS_COMMENT _comment = new ESHOP_NEWS_COMMENT();

        //        _comment.COMMENT_NAME = full_name.Value;
        //        _comment.COMMENT_EMAIL = txtemail.Value;
        //        _comment.COMMENT_STATUS = 0;
        //        _comment.COMMENT_CHECK = 0;
        //        _comment.COMMENT_CONTENT = message.Value;
        //        //_comment.NEWS_ID = _iNewsID;
        //        int _newsID = Utils.CIntDef(Session["news_id"]);
        //        if (Checkcaturl(_sCat_Seo_Url))
        //        {
        //            _newsID = Getnewsid();
        //        }
        //        _comment.NEWS_ID = _newsID;

        //        db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(_comment);
        //        db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //}

        private void SendEmailFeedback()
        {
            try
            {
                var _FeedEmail = cf.Getemail(1);

                if (_FeedEmail.ToList().Count > 0)
                {
                    string strEmailSubject = "";
                    string strEmailBody = "";

                    strEmailSubject += "Thông tin phản hồi từ website " + Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]);
                    strEmailBody += "<html><body>";
                    strEmailBody += "<br>Thông tin phản hồi: <br>";
                    strEmailBody += "Ngày tháng: " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "<br>";
                    strEmailBody += "Phản hồi về: " + Session["News_title"] + "<br>";
                    strEmailBody += "Người gởi: " + full_name.Value + "<br>";
                    strEmailBody += "Email: <a href='mailto:" + txtemail.Value + "'>" + txtemail.Value + "</a><br>";
                    strEmailBody += "Nội dung: " + message.Value;
                    strEmailBody += "</body></html>";

                    send.SendEmailSMTP(strEmailSubject, _FeedEmail.ToList()[0].EMAIL_TO, _FeedEmail.ToList()[0].EMAIL_CC, _FeedEmail.ToList()[0].EMAIL_BCC, strEmailBody, true, false);
                    //SendEmailSMTP(strEmailSubject, txtemail.Value, "", "", strEmailBody, true, false);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        //private void LoadFeedbackList()
        //{
        //    try
        //    {
        //        var _FeedbackList = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(c => c.COMMENT_STATUS == 1).OrderByDescending(m => m.COMMENT_PUBLISHDATE);

        //        if (+_FeedbackList.ToList().Count > 0)
        //        {
        //            rptFeedbackList.Visible = true;
        //            rptFeedbackList.DataSource = _FeedbackList;
        //            rptFeedbackList.DataBind();
        //        }
        //        else
        //            rptFeedbackList.Visible = false;
        //        box_comment.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //}
        /// <summary>
        /// file up-----------------------------------------------------------------------------------------
        /// </summary>
        private void BuildAttFiles()
        {
            //try
            //{
            //    int News_Id = Utils.CIntDef(Session["News_id"]);

            //    var _AttList = (from n in db.ESHOP_NEWS_ATTs
            //                    join nc in db.ESHOP_EXT_FILEs on n.EXT_ID equals nc.EXT_FILE_ID
            //                    where n.NEWS_ID == News_Id
            //                    orderby n.NEWS_ATT_ORDER descending, n.NEWS_ATT_NAME
            //                    select new
            //                    {
            //                        n.EXT_ID,
            //                        n.NEWS_ID,
            //                        n.NEWS_ATT_URL,
            //                        n.NEWS_ATT_FILE,
            //                        n.NEWS_ATT_NAME,
            //                        nc.EXT_FILE_IMAGE
            //                    });

            //    if (_AttList.ToList().Count > 0)
            //    {
            //        rptNewsAtt.Visible = true;
            //        rptNewsAtt.DataSource = _AttList;
            //        rptNewsAtt.DataBind();
            //    }
            //    else
            //    {
            //        rptNewsAtt.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    clsVproErrorHandler.HandlerError(ex);
            //}

        }

        public string BindAttItems(object News_Id, object Ext_Id, object Att_Name, object Att_Url, object Att_File, object Ext_Image)
        {
            try
            {
                string url = "";
                string strResult = "";

                if (!string.IsNullOrEmpty(Utils.CStrDef(Att_Url)))
                {
                    url = Utils.CStrDef(Att_Url);
                }
                else if (!string.IsNullOrEmpty(Utils.CStrDef(Att_File)))
                {
                    url = PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(Att_File);
                }
                else
                {
                    return "";
                }

                strResult += "<li>";

                if (!string.IsNullOrEmpty(Utils.CStrDef(Ext_Image)))
                    strResult += "<img src='" + PathFiles.GetPathExt(Utils.CIntDef(Ext_Id)) + Utils.CStrDef(Ext_Image) + "' width='24px' style='float:left; margin-right:10px;' />";

                strResult += "<a href='" + url + "' target='_blank' title='" + Utils.CStrDef(Att_Name) + "'>" + (string.IsNullOrEmpty(Utils.CStrDef(Att_Name)) ? Utils.CStrDef(Att_File) : Utils.CStrDef(Att_Name)) + "</a>";
                strResult += "</li>";

                return strResult;

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetLink(object News_Url, object News_Seo_Url, int Type)
        {
            try
            {
                string _sType = Type == 0 ? "tin-tuc" : "san-pham";
                return string.IsNullOrEmpty(Utils.CStrDef(News_Url)) ? "/" + _sType + "/" + Utils.CStrDef(News_Seo_Url) + ".html" : Utils.CStrDef(News_Url);
            }
            catch (Exception ex)
            {
                vpro.functions.clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        #endregion

        public string querys()
        {
            return LookCookie();
        }

        private string LookCookie()
        {
            HttpCookie Cookie = new HttpCookie("slmsrcd1");
            Cookie = Request.Cookies["slmsrcd1"];
            string strUser = "";
            if (Cookie != null && Cookie.Value != "" &&
                 Cookie.Value != null)
            {
                strUser = Cookie.Value.ToString();
            }
            return strUser;
        }
    }
}