using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;

namespace yeuthietkeweb.UIs
{
    public partial class Path : System.Web.UI.UserControl
    {
        #region Decclare
        Propertity per = new Propertity();
        Function fun = new Function();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = per.Getpath();
            if (str.Length > 0)
            {
                int first_index = str.IndexOf(">");
                int second_index = str.IndexOf("<", 19);
                string str1 = str.Substring(first_index + 1, second_index - first_index - 1).Trim();
                string str2 = str1.Substring(0, 1).ToUpper() + str1.Substring(1).ToLower();
                str = str.Replace(str1, str2);
            }
            liPath.Text = str;
        }
    }
}