using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Controller;
using System.Web.UI.HtmlControls;

namespace yeuthietkeweb.vi_vn
{
    public partial class PrintPage : System.Web.UI.Page
    {
        Config cf = new Config();
        Propertity pro = new Propertity();
        Function fun = new Function();
        News_details ndetail = new News_details();
        int _iNewsID = -1;
        string _sNewsSeoUrl = string.Empty;
        string _sCat_Seo_Url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            _sCat_Seo_Url = Utils.CStrDef(Request.QueryString["curl"]);
            _sNewsSeoUrl = Utils.CStrDef(Request.QueryString["purl"]);
            ShowNewsInfo();
            load_logo();
            Show_File_HTML();

            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }

            HtmlHead header = base.Header;
            header.Title = "In";
        }
        #region Logo

        protected void load_logo()
        {
            var _logoSlogan = pro.Load_logo_and_sologan(2);
            if (_logoSlogan.ToList().Count > 0)
            {
                Rplogo.DataSource = _logoSlogan;
                Rplogo.DataBind();
            }
        }
        public string Getbanner(object Banner_type, object banner_field, object Banner_ID, object Banner_Image)
        {
            return GetImage(Banner_type, banner_field, Banner_ID, Banner_Image);
        }
        public string GetImage(object Banner_type, object banner_field, object Banner_ID, object Banner_Image)
        {
            try
            {
                return fun.Getbanner(Banner_type, banner_field, Banner_ID, Banner_Image);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        #endregion
        private void ShowNewsInfo()
        {
            try
            {
                var _v = ndetail.Load_details(_sNewsSeoUrl);
                if (_v.ToList().Count > 0)
                {
                   lbTitle.Text = Utils.CStrDef(_v.ToList()[0].NEWS_TITLE);
                   lbDate.Text = string.Format("{0: HH:MM - dd/MM/yyyy}", Utils.CDateDef(_v.ToList()[0].NEWS_PUBLISHDATE, DateTime.Now));
                   // lbDesc.Text = Utils.CStrDef(_v.ToList()[0].NEWS_DESC);
                    _iNewsID = Utils.CIntDef(_v.ToList()[0].NEWS_ID);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void Show_File_HTML()
        {
            ltrHtml.Text = ndetail.Showfilehtm(_sCat_Seo_Url, _sNewsSeoUrl);
        }
    }
}