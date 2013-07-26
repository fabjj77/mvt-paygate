using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using BankNet.Core;
using BankNet.Core.Provider;
using BankNet.Data;
using BankNet.Entity;
using Web.Helper;
//using Web.WsPayment;//bản thật
using Web.WsPaymentTest;//bản test

namespace Web.Ajax
{
    /// <summary>
    /// Summary description for SmartlinkCmd
    /// </summary>
    public class SmartlinkCmd : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            if (!Security.AllowCall(context)) return;
            context.Response.ContentType = "application/json";
            try
            {
                string sType = context.Request["Type"];
                string sCaptCha = context.Request["CaptCha"];

                if (string.IsNullOrEmpty(sCaptCha))
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 1, "Chưa nhập captcha"));
                    return;
                }

                if (context.Session[Config.GetSessionCode] == null)
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 2, "Captcha time out"));
                    return;
                }

                if (context.Session[Config.GetSessionCode].ToString().ToUpper() != sCaptCha.ToUpper())
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 3, "Captcha không đúng"));
                    return;
                }

                if (string.IsNullOrEmpty(sType))
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 4, "Yêu cầu không hợp lệ"));
                    return;
                }

                switch (sType)
                {
                    case "GetInfo":
                        GetInfo(context);
                        break;
                    case "SetInfo":
                        SetInfo(context);
                        break;
                    default: break;
                }
            }
            catch (Exception e)
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", e.GetHashCode(), e.Message));
                throw;
            }
            finally
            {
                context.Session.Remove(Config.GetSessionCode);
            }
        }

        private void GetInfo(HttpContext context)
        {
            string sUserId = context.Request["UserId"];

            if (string.IsNullOrEmpty(sUserId))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 5, "Chưa nhập đủ thông tin"));
                return;
            }

            if (sUserId.Trim().Length != 12 && sUserId.Trim().Length != 14)
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 6, "Số thuê bao phải là 12 hoặc 14 ký tự"));
                return;
            }

            if (!Utility.isOnlyNumber(sUserId))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Số hợp đồng phải là kiểu số"));
                return;
            }

            WSClient client = new WSClient();
            var cred = new credential { clientId = Config.ClientIdSmartLink };
            var result = client.getVoucherPaymentInfo(cred, sUserId);

            if (result.returnCode != "")
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", result.returnCode, result.returnCodeDescription));
                return;
            }
            else
            {
                CustomerGateInfo info = XMLReader.ReadInfo(result.responseData);
                if (info != null)
                {
                    
                    string sVoucher = "";
                    if (info.vouchers != null && info.vouchers.Count > 0)
                    {
                        foreach (voucher voucher in info.vouchers)
                        {
                            if (sVoucher == "")
                            {
                                sVoucher += string.Format("{{\"vouchervalue\":\"{0}\",\"duration\":\"{1}\",\"vouchername\":\"{2}\",\"durationuomaltcode\":\"{3}\",\"voucherdesc\":\"{4}\"}}", voucher.vouchervalue, voucher.duration, voucher.vouchername, voucher.durationuomaltcode, voucher.voucherdesc);
                            }
                            else
                            {
                                sVoucher += "," + string.Format("{{\"vouchervalue\":\"{0}\",\"duration\":\"{1}\",\"vouchername\":\"{2}\",\"durationuomaltcode\":\"{3}\",\"voucherdesc\":\"{4}\"}}", voucher.vouchervalue, voucher.duration, voucher.vouchername, voucher.durationuomaltcode, voucher.voucherdesc);
                            }
                        }
                    }

                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\",\"subnum\":\"{2}\",\"contactname\":\"{3}\",\"contactaddress\":\"{4}\"," +
                                                     "\"contactphone\":\"{5}\",\"contactemail\":\"{6}\",\"servicename\":\"{7}\",\"substatusname\":\"{8}\"," +
                                                     "\"expirationdate\":\"{9}\",\"vouchers\":[{10}] }}",
                    0, "load thông tin ok", info.subnum, info.contactname, info.contactaddress, info.contactphone, info.contactemail, info.servicename, info.substatusname, info.expirationdate, sVoucher));
                    context.Session[Config.GetSessionUser] = info;
                }
                else
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 8, "Không tìm thấy thông tin khách hàng"));
                }
            }
        }
        
        private void SetInfo(HttpContext context)
        {
            string vouchers = context.Request["vouchers"];

            if (string.IsNullOrEmpty(vouchers))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 5, "Hãy nhập gói cước"));
                return;
            }

            var oUser = (CustomerGateInfo)context.Session[Config.GetSessionUser];
            if (oUser == null)
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 6, "Hết phiên làm việc, hãy thực hiện lại"));
                return;
            }
            
            if (!Utility.isOnlyNumber(vouchers))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Gói cước không phù hợp"));
                return;
            }

            List<voucher> list = oUser.vouchers;
            voucher oVoucher = null;
            foreach (voucher item in list)
            {
                if(vouchers==item.vouchervalue)
                {
                    oVoucher = item;
                    break;
                }
            }

            if (oVoucher==null)
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 8, "Gói cước không phù hợp"));
                return;
            }

            SmartlinkRedirectUrlInfo oRedirectUrlInfo = new SmartlinkRedirectUrlInfo()
                                        {
                                            CreateDate = DateTime.Now
                                        };

            try
            {
                string sUrl = SmartLinkHelper.getRedirectUrl(vouchers, ref oRedirectUrlInfo);
                
                //save cached
                CacheInfo oCacheInfo = new CacheInfo()
                {
                    Voucher = oVoucher,
                    sTrans_Id = oRedirectUrlInfo.vpc_MerchTxnRef,
                    User = oUser
                };

                CacheProvider.AddWithTimeOut(string.Format(KeyCache.KeyUserSmartlink, oRedirectUrlInfo.vpc_MerchTxnRef), oCacheInfo, 720);
                //
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 0, sUrl));

            }
            catch (Exception ex)
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", ex.GetHashCode().ToString(), ex.Message));
            }
            finally
            {
                SmartlinkRedirectUrlData.instance.Add(oRedirectUrlInfo);
            }
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}