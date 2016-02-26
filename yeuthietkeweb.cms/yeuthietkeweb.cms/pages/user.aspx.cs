using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace yeuthietkeweb.cms.pages
{
    public partial class user : System.Web.UI.Page
    {
        #region Declare

        private int m_user_id = 0;
        //int _count = 0;
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

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_user_id = Utils.CIntDef(Request["user_id"]);

            if (m_user_id == 0)
            {
                lbtDelete.Visible = false;
            }
            else
            { RequiredFieldValidator2.Enabled = false; }

            if (!IsPostBack)
            {
                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            if (CheckExits(txtUN.Value))
                lblError.Text = "Đã tồn tại Tên đăng nhập, vui lòng nhập Tên đăng nhập khác.";
            else
                SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            if (CheckExits(txtUN.Value))
                lblError.Text = "Đã tồn tại Tên đăng nhập, vui lòng nhập Tên đăng nhập khác.";
            else
                SaveInfo("user.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                LoadGroup();

                var G_info = (from g in DB.ESHOP_USERs
                              where g.USER_ID == m_user_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtFullName.Value = G_info.ToList()[0].USER_NAME;
                    txtUN.Value = G_info.ToList()[0].USER_UN;
                    ddlGroup.SelectedValue = Utils.CStrDef(G_info.ToList()[0].GROUP_ID);
                    rblActive.SelectedValue = Utils.CStrDef(G_info.ToList()[0].USER_TYPE);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo(string strLink = "")
        {
            try
            {
                string SALT = "";
                string USER_PW = "";
                if (!string.IsNullOrEmpty(txtPass.Value))
                {
                    SALT = Common.CreateSalt();
                    USER_PW = Common.Encrypt(txtPass.Value, SALT);
                }

                if (m_user_id == 0)
                {
                    //insert
                    ESHOP_USER g_insert = new ESHOP_USER();
                    g_insert.USER_NAME = txtFullName.Value;
                    g_insert.USER_UN = txtUN.Value;
                    g_insert.USER_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                    g_insert.GROUP_ID = Utils.CIntDef(ddlGroup.SelectedValue);
                    g_insert.USER_PW = USER_PW;
                    g_insert.SALT = SALT;

                    DB.ESHOP_USERs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    strLink = string.IsNullOrEmpty(strLink) ? "user_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_USER>().Where(g => g.USER_ID == m_user_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().USER_NAME = txtFullName.Value;
                        g_update.Single().USER_UN = txtUN.Value;
                        g_update.Single().USER_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        g_update.Single().GROUP_ID = Utils.CIntDef(ddlGroup.SelectedValue);

                        if (!string.IsNullOrEmpty(USER_PW))
                        {
                            g_update.Single().USER_PW = USER_PW;
                            g_update.Single().SALT = SALT;
                        }

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "user_list.aspx" : strLink;
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
                { Response.Redirect(strLink); }
            }
        }

        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_USER>().Where(g => g.USER_ID == m_user_id);

                DB.ESHOP_USERs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("user_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void LoadGroup()
        {
            try
            {
                var allGroup = DB.GetTable<ESHOP_GROUP>().OrderBy(g => g.GROUP_NAME);

                ddlGroup.DataSource = allGroup;
                ddlGroup.DataTextField = "GROUP_NAME";
                ddlGroup.DataValueField = "GROUP_ID";
                ddlGroup.DataBind();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private bool CheckExits(string strUN)
        {
            try
            {
                if (m_user_id == 0)
                {
                    var exits = (from c in DB.ESHOP_USERs where c.USER_UN == strUN select c);

                    if (exits.ToList().Count > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;

            }

        }

        #endregion
    }
}