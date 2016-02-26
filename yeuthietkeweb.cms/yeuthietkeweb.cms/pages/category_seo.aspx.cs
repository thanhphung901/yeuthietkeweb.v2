using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;
using System.Data;

namespace yeuthietkeweb.cms.pages
{
    public partial class category_seo : System.Web.UI.Page
    {
        #region Declare

        private int m_cat_id = 0;
        dbShopDataContext DB = new dbShopDataContext();
        string m_pathFile = "";
        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_cat_id = Utils.CIntDef(Request["cat_id"]);
            Hyperback.NavigateUrl = "category.aspx?cat_id=" + m_cat_id;
            CreateDirectory();
            if (!IsPostBack) showFileHTML();

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveHTMLInfo();
        }
        #endregion
              

        #region seo chuyen muc
        private void showFileHTML()
        {
            string pathFile;
            string strHTMLContent;

            if (m_cat_id > 0)
            {

                var newsInfo = DB.GetTable<ESHOP_CATEGORy>().Where(n => n.CAT_ID == m_cat_id);

                pathFile = Server.MapPath(PathFiles.GetPathCategory(m_cat_id) + "/" + newsInfo.ToList()[0].CAT_FIELD5);

                if ((File.Exists(pathFile)))
                {
                    StreamReader objNewsReader;
                    //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                    objNewsReader = new StreamReader(pathFile);
                    strHTMLContent = objNewsReader.ReadToEnd();
                    objNewsReader.Close();
                    mrk.Value = strHTMLContent;
                }
            }
        }
        private void SaveHTMLInfo()
        {
            try
            {
                if ((m_cat_id > 0))
                {
                    CreateDirectory();

                    string strHTMLFileLocation;
                    string strFileName;
                    string strHTMLContent;
                    StreamWriter fsoFile;

                    strFileName = PathFiles.GetPathCategory(m_cat_id) + m_cat_id.ToString() + "-vi.htm";
                    strHTMLFileLocation = Server.MapPath(strFileName);
                    strHTMLContent = mrk.Value;

                    fsoFile = File.CreateText(strHTMLFileLocation);
                    fsoFile.Write(strHTMLContent);
                    fsoFile.Close();

                    //update to db
                    var n_update = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == m_cat_id);

                    if (n_update.ToList().Count > 0)
                    {
                        n_update.Single().CAT_FIELD5 = m_cat_id.ToString() + "-vi.htm";

                        DB.SubmitChanges();
                    }

                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void CreateDirectory()
        {
            m_pathFile = PathFiles.GetPathCategory(m_cat_id);

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }
            Session["FileManager"] = m_pathFile;
        }
        #endregion
    }
}