using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace yeuthietkeweb.cms.pages
{
    public partial class config_hitcouter : System.Web.UI.Page
    {
        #region Declare

        dbShopDataContext DB = new dbShopDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

                if (G_info.ToList().Count > 0)
                {
                    txtHitcouter.Value = Utils.CStrDef(G_info.ToList()[0].CONFIG_HITCOUNTER);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

                if (G_info.ToList().Count > 0)
                {
                    G_info.Single().CONFIG_HITCOUNTER = Utils.CIntDef(txtHitcouter.Value);

                    DB.SubmitChanges();
                }
                else
                { //insert
                    ESHOP_CONFIG config_insert = new ESHOP_CONFIG();

                    config_insert.CONFIG_HITCOUNTER = Utils.CIntDef(txtHitcouter.Value);

                    DB.ESHOP_CONFIGs.InsertOnSubmit(config_insert);
                    DB.SubmitChanges();
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion
    }
}