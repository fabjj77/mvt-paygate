using System;
using BankNet.Core;
using BankNet.Core.Provider;
using BankNet.Data;
using BankNet.Entity;
using Web.Ajax;
using Web.Helper;

namespace Web.SmartLink
{
    public partial class fail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sTranId = Request.QueryString["code"];
            if (string.IsNullOrEmpty(sTranId)) Response.Redirect("/SmartLink/", true);

            CacheInfo oCache = (CacheInfo)CacheProvider.Get(string.Format(KeyCache.KeyUserSmartlink, sTranId));
            if (oCache == null) Response.Redirect("/SmartLink/", true);
            
            SmartlinkQueryInfo oQueryInfo = new SmartlinkQueryInfo()
            {
                CreateDate = DateTime.Now
            };
            try
            {
                string sStatus = SmartLinkHelper.GetQuery(sTranId, ref oQueryInfo);
                if(!string.IsNullOrEmpty(sStatus))
                {
                    String[] arr = sStatus.Split('&');
                    foreach (String item in arr)
                    {
                        String[] temp = item.Split('=');
                        if ("vpc_DRExists".ToUpper().Equals(temp[0].ToUpper()))
                        {
                            oQueryInfo.vpc_DRExists = temp[1];
                        }
                        if ("vpc_FoundMultipleDRs".ToUpper().Equals(temp[0].ToUpper()))
                        {
                            oQueryInfo.vpc_FoundMultipleDRs = temp[1];
                        }
                        if ("vpc_Message".ToUpper().Equals(temp[0].ToUpper()))
                        {
                            oQueryInfo.vpc_Message = temp[1];
                        }
                        if ("vpc_SecureHash".ToUpper().Equals(temp[0].ToUpper()))
                        {
                            oQueryInfo.vpc_SecureHash = temp[1];
                        }
                        if ("vpc_TxnResponseCode".ToUpper().Equals(temp[0].ToUpper()))
                        {
                            oQueryInfo.vpc_TxnResponseCode = temp[1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oQueryInfo.vpc_TxnResponseCode = ex.GetHashCode().ToString();
                oQueryInfo.vpc_Message = ex.Message;
                //throw;
            }
            finally
            {
                CacheProvider.Remove(string.Format(KeyCache.KeyUserSmartlink, sTranId));
                SmartlinkQueryData.instance.Add(oQueryInfo);
                //Session[Config.GetSessionsResultFail] =oQueryInfo.vpc_TxnResponseCode==null?"Giao dịch bị hủy bỏ": SmartLinkHelper.getResponseDescription(oQueryInfo.vpc_TxnResponseCode);
                Session[Config.GetSessionsResultFail] = "Giao dịch không thành công";
                Response.Redirect("/SmartLink/#" + sTranId + "|F", false);
            }
        }
    }
}