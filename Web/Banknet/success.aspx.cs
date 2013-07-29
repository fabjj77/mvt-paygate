using System;
using System.Threading;
using BankNet.Core;
using BankNet.Core.Provider;
using BankNet.Data;
using BankNet.Entity;
using Web.Ajax;
using Web.Helper;
//using Web.WsPaymentTest;
using Web.WsPayment;

namespace Web.Banknet
{
    public partial class success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Good_Code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(Good_Code)) Response.Redirect("/Banknet/", true);

            CacheInfo oCache = (CacheInfo)CacheProvider.Get(string.Format(KeyCache.KeyUserBanknet, Good_Code));
            if (oCache == null) Response.Redirect("/Banknet/", true);

            string sRedirection = "/Banknet/";

            SubmitVoucherInfo oSVInfo = new SubmitVoucherInfo()
            {
                GatePayId = Config.ClientIdBanknet,
                UserId = oCache.User.subnum,
                Amount = int.Parse(oCache.Voucher.vouchervalue),
                CreateDate = DateTime.Now,
                TransId = Good_Code
            };
            try
            {
                WSClient wsclient = new WSClient();
                var cred = new credential { clientId = Config.ClientIdBanknet };
                var wsResult = wsclient.submitVoucher(cred, oSVInfo.UserId, oSVInfo.Amount.ToString(), oSVInfo.TransId);


                oSVInfo.returnCode = wsResult.returnCode;
                oSVInfo.returnCodeDescription = wsResult.returnCodeDescription;
                string sResultDate = XMLReader.ReadResultVocher(wsResult.responseData);//dt
                oSVInfo.responseData = sResultDate;
                oSVInfo.signature = wsResult.signature;

                if (oSVInfo.returnCode == "")
                {
                    Session[Config.GetSessionsResultDate] = sResultDate;//ss

                    //confirm
                    ThreadStart newThread = delegate { Confirm(oCache.Merchant_trans_id,oCache.sTrans_Id); };
                    Thread myThread = new Thread(newThread);
                    myThread.Start();

                    sRedirection="/Banknet/#" + Good_Code + "|T";
                }
                else
                {
                    Session[Config.GetSessionsResultFail] = wsResult.returnCodeDescription;//ss
                    sRedirection="/Banknet/#" + Good_Code + "|F|Y";
                }
            }
            catch (Exception ex)
            {
                //log error
                Session[Config.GetSessionsResultFail] = ex.Message;
                oSVInfo.returnCode = ex.GetHashCode().ToString();
                oSVInfo.returnCodeDescription = ex.Message;
                sRedirection="/Banknet/#" + Good_Code + "|F|Y";
            }
            finally
            {
                SubmitVoucherData.instance.Add(oSVInfo);
                Response.Redirect(sRedirection);
            }
        }

        private void Confirm(string Merchant_trans_id, string sTrans_Id)
        {
            ConfirmTransactionResultInfo oConfirmInfo = new ConfirmTransactionResultInfo()
            {
                CreateDate = DateTime.Now
            };
            try
            {
                string sConfirm = BanknetHelper.ConfirmTransactionResult(Merchant_trans_id,sTrans_Id, "0", ref oConfirmInfo);// quá lâu
                oConfirmInfo.ResultId = BanknetHelper.getCodeResult(sConfirm);
                oConfirmInfo.OutString = sConfirm;

                //if (oConfirmInfo.ResultId == "00")
                //{
                //    //ok
                //}
                //else
                //{
                //    //Response.Redirect("/Banknet/#" + Good_Code + "|F|N");
                //}
            }
            catch (Exception ex)
            {
                oConfirmInfo.ResultId = ex.GetHashCode().ToString();
                oConfirmInfo.OutString = ex.Message;
                //Response.Redirect("/Banknet/#" + Good_Code + "|F|N");
                //throw;
            }
            finally
            {
                ConfirmTransactionResultData.instance.Add(oConfirmInfo);
            }
        }
    }
}