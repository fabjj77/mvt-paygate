using System;
using BankNet.Core.Provider;
using BankNet.Data;
using BankNet.Entity;
using Web.Ajax;
using Web.Helper;

namespace Web.Banknet
{
    public partial class fail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Good_Code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(Good_Code)) Response.Redirect("/Banknet/", true);

            CacheInfo oCache = (CacheInfo)CacheProvider.Get(string.Format(KeyCache.KeyUserBanknet, Good_Code));
            if (oCache == null) Response.Redirect("/Banknet/", true);
            
            QuerryBillStatusInfo oStatus = new QuerryBillStatusInfo()
            {
                CreateDate = DateTime.Now
            };
            try
            {
                string sStatus = BanknetHelper.QuerryBillStatus(oCache.sTrans_Id, ref oStatus);

                oStatus.ResultId = BanknetHelper.getCodeResult(sStatus);
                oStatus.OutString = sStatus;
            }
            catch (Exception ex)
            {
                oStatus.ResultId = ex.GetHashCode().ToString();
                oStatus.OutString = ex.Message;
                //throw;
            }
            finally
            {
                CacheProvider.Remove(string.Format(KeyCache.KeyUserBanknet, Good_Code));
                QuerryBillStatusData.instance.Add(oStatus);
                Response.Redirect("/Banknet/#" + Good_Code + "|F");
            }
        }
    }
}