using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace yeuthietkeweb.cms.pages
{
    public partial class login : System.Web.UI.Page
    {
        #region Declare

        dbShopDataContext DB = new dbShopDataContext();

        #endregion

        #region Form Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Session.Remove("USER_ID");
                //Session.Remove("USER_UN");
                //Session.Remove("USER_NAME");
                //Session.Remove("GROUP_ID");
                //Session.Remove("GROUP_TYPE");

                txtUN.Attributes.Add("onKeyPress", Common.getSubmitScript(lbtLogin.ClientID));
                txtPW.Attributes.Add("onKeyPress", Common.getSubmitScript(lbtLogin.ClientID));

                var _configs = DB.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

                if (_configs.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                        ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
                }
            }
        }

        #endregion

        #region Button

        protected void lbtLogin_Click(object sender, EventArgs e)
        {
            string strLink = "";

            try
            {
                string strUn = Utils.CStrDef((txtUN.Value), "").ToUpper();
                string strPW = Utils.CStrDef(txtPW.Value, "");

                var login = DB.GetTable<ESHOP_USER>().Where(u => u.USER_UN == strUn);

                if (login.ToList().Count <= 0)
                {
                    lbMessage.Text = "Tài khoản đăng nhập không tồn tại.";
                }
                else
                {
                    //decode pass
                    strPW = Common.Encrypt(strPW, Utils.CStrDef(login.ToList()[0].SALT));
                    //kiem tra lai salt bi null

                    if (login.ToList()[0].USER_PW != strPW)
                    { lbMessage.Text = "Thông tin đăng nhập không hợp lệ."; }
                    else if (login.ToList()[0].USER_ACTIVE == 0)
                    { lbMessage.Text = "Tài khoản chưa được kích hoạt."; }
                    else
                    {
                        Session["USER_ID"] = login.ToList()[0].USER_ID;
                        Session["USER_UN"] = login.ToList()[0].USER_UN;
                        Session["USER_NAME"] = login.ToList()[0].USER_NAME;
                        Session["GROUP_ID"] = login.ToList()[0].GROUP_ID;
                        Session["GROUP_TYPE"] = login.ToList()[0].ESHOP_GROUP.GROUP_TYPE;

                        HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_ID"] = Session["USER_ID"].ToString();
                        HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_UN"] = Session["USER_UN"].ToString();
                        HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_NAME"] = Session["USER_NAME"].ToString();
                        HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_ID"] = Session["GROUP_ID"].ToString();
                        HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_TYPE"] = Session["GROUP_TYPE"].ToString();
                        HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"].Expires = DateTime.Now.AddDays(30);

                        if (login.ToList()[0].ESHOP_GROUP.GROUP_TYPE == 1)
                            strLink = "default.aspx";
                        else if (login.ToList()[0].ESHOP_GROUP.GROUP_TYPE == 2)
                            strLink = "news_list.aspx";
                        else
                            strLink = "login.aspx";
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
                    Response.Redirect(strLink);
            }
        }

        #endregion
    }
}