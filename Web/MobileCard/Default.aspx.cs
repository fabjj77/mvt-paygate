using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.GateCard
{
    public partial class Default : System.Web.UI.Page
    {
        protected static string sDDLType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDDL();
            }
        }

        private void LoadDDL()
        {
            //DDL_CardType.Items.Add(new ListItem("Thẻ Viettel", "CardInputViettel"));
            //DDL_CardType.Items.Add(new ListItem("Thẻ Mobiphone", "CardInputVMS"));
            //DDL_CardType.Items.Add(new ListItem("Thẻ Vinaphone", "CardInputVNP"));
            //DDL_CardType.Items.Add(new ListItem("Thẻ Gate", "CardInputGate"));

            if(string.IsNullOrEmpty(sDDLType))
            {
                sDDLType = "<select id=\"MainContent_DDL_CardType\" class=\"DDLType\">" +
                           "<option selected=\"selected\" value=\"CardInputViettel\">Thẻ Viettel</option>" +
                           "<option value=\"CardInputVMS\">Thẻ Mobiphone</option>" +
                           "<option value=\"CardInputVNP\">Thẻ Vinaphone</option>" +
                           "<option value=\"CardInputGate\">Thẻ Gate</option>" +
                           "</select>";

            }
        }
    }
}