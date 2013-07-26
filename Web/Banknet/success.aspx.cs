using System;
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

            //send success

            QuerryBillStatusInfo oStatus = new QuerryBillStatusInfo()
                                               {
                                                   CreateDate = DateTime.Now
                                               };
            try
            {
                string sStatus = BanknetHelper.QuerryBillStatus(oCache.sTrans_Id,ref oStatus);

                oStatus.ResultId = BanknetHelper.getCodeResult(sStatus);
                oStatus.OutString = sStatus;

                if (oStatus.ResultId == "00")
                {
                    ConfirmTransactionResultInfo oConfirmInfo = new ConfirmTransactionResultInfo()
                                                                    {
                                                                        CreateDate = DateTime.Now
                                                                    };
                    try
                    {
                        string sConfirm = BanknetHelper.ConfirmTransactionResult(oCache.sTrans_Id, "0", ref oConfirmInfo);
                        oConfirmInfo.ResultId = BanknetHelper.getCodeResult(sConfirm);
                        oConfirmInfo.OutString = sConfirm;
                        
                        if(oConfirmInfo.ResultId=="00")
                        {
                            //submit voucher
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
                                oSVInfo.responseData = wsResult.responseData;
                                oSVInfo.signature = wsResult.signature;

                                if (oSVInfo.returnCode == "")
                                {
                                    string sResultDate = XMLReader.ReadResultVocher(oSVInfo.responseData);//dt
                                    Session[Config.GetSessionsResultDate] = sResultDate;//ss

                                    Response.Redirect("/Banknet/#" + Good_Code + "|T",false);
                                }
                                else
                                {
                                    Session[Config.GetSessionsResultFail] = wsResult.returnCodeDescription;//ss
                                    Response.Redirect("/Banknet/#" + Good_Code + "|F|Y", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                //log error
                                Session[Config.GetSessionsResultFail]=ex.Message;
                                oSVInfo.returnCode = ex.GetHashCode().ToString();
                                oSVInfo.returnCodeDescription = ex.Message;
                                Response.Redirect("/Banknet/#" + Good_Code + "|F|Y");
                            }
                            finally
                            {
                                SubmitVoucherData.instance.Add(oSVInfo);
                            }
                        }
                        else
                        {
                            Response.Redirect("/Banknet/#" + Good_Code + "|F|N");
                        }
                    }
                    catch (Exception ex)
                    {
                        oConfirmInfo.ResultId = ex.GetHashCode().ToString();
                        oConfirmInfo.OutString = ex.Message;
                        Response.Redirect("/Banknet/#" + Good_Code + "|F|N");
                        //throw;
                    }
                    finally
                    {
                        ConfirmTransactionResultData.instance.Add(oConfirmInfo);
                    }
                }
                else
                {
                    Response.Redirect("/Banknet/#" + Good_Code + "|F|N");
                }
            }
            catch (Exception ex)
            {
                oStatus.ResultId = ex.GetHashCode().ToString();
                oStatus.OutString = ex.Message;
                Response.Redirect("/Banknet/#" + Good_Code + "|F|N");
                //throw;
            }
            finally
            {
                CacheProvider.Remove(string.Format(KeyCache.KeyUserBanknet, Good_Code));
                QuerryBillStatusData.instance.Add(oStatus);
            }
        }
    }
}