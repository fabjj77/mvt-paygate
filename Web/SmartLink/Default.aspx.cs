using System;
using BankNet.Core;

namespace Web.SmartLink
{
    public partial class Default : System.Web.UI.Page
    {
        protected string sDate = "";
        protected string sErr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var ss1 = Session[Config.GetSessionsResultDate];
                if (ss1 != null)
                {
                    sDate = ss1.ToString();
                }

                var ss2 = Session[Config.GetSessionsResultFail];
                if (ss2 != null)
                {
                    sErr = ss2.ToString();
                }
            }

            Session.Clear();
            Session.Abandon();
        }
    }
}