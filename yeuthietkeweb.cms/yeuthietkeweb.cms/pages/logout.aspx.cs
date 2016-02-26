using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yeuthietkeweb.cms.pages
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"].Expires = DateTime.Now.AddDays(-10);
            Response.Redirect("login.aspx");
        }
    }
}