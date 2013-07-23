using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using BankNet.Core;
using BankNet.Core.Provider;
using BankNet.Data;
using BankNet.Entity;
using Web.Helper;
using Web.WsPaymentTest;

namespace Web.Ajax
{
    /// <summary>
    /// Summary description for BanknetCmd
    /// </summary>
    public class BanknetCmd : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (!AllowCall(context)) return;
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
            var cred = new credential { clientId = Config.ClientIdBanknet };
            var result = client.getVoucherPaymentInfo(cred, sUserId);

            if (result.returnCode != "")
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", result.returnCode, result.returnCodeDescription));
                return;
            }
            else
            {
                CustomerGateInfo info = ReadInfo(result.responseData);
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
            
            string Good_Code =  string.Format("{0:yyMMddHHmmssfff}",DateTime.Now) + oVoucher.duration;
            Good_Code = Security.GetMD5Hash(Good_Code);
            Good_Code = "THAV-" + Good_Code.Substring(0,15).ToUpper();

            string sSuccess =context.Request.Url.Scheme + "://" + context.Request.Url.Authority + "/Banknet/success.aspx?code=" +Good_Code;
            string sFail = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + "/Banknet/fail.aspx?code=" + Good_Code;

            SendGoodInfo sendInfo = new SendGoodInfo()
                                        {
                                            UserId = oUser.subnum,
                                            vouchers = oVoucher.vouchervalue + " - " + oVoucher.vouchername + " - " + oVoucher.duration + " " + oVoucher.durationuomaltcode,
                                            Good_code = Good_Code,
                                            Xml_description = Config.Desc,
                                            Url_success = sSuccess,
                                            Url_fail = sFail,
                                            CreateDate = DateTime.Now
                                        };

            try
            {
                string s = BanknetHelper.Send_GoodInfo(Good_Code, Config.Desc, vouchers, "0", "0", sSuccess, sFail, ref sendInfo);

                if (BanknetHelper.getCodeResult(s) != "010")
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 9, "Kết nối với cổng thanh toán Banknet gặp sự cố"));
                    return;
                }

                string sUrl = "";
                string sTrans_Id = "";
                int iResult = BanknetHelper.GetUrlRedirection(s, out sUrl);
                if (iResult != 0)
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 10, "Kết nối với cổng thanh toán Banknet gặp sự cố"));
                    return;
                }
                sTrans_Id = BanknetHelper.getTransIdFromUrl(sUrl);
                sendInfo.Trans_Id = sTrans_Id;
                //save cached
                CacheInfo oCacheInfo = new CacheInfo()
                {
                    Voucher = oVoucher,
                    sTrans_Id =sTrans_Id,
                    Good_Code = Good_Code,
                    User = oUser
                };

                CacheProvider.AddWithTimeOut(string.Format(KeyCache.KeyUserBanknet, Good_Code), oCacheInfo, 720);
                //
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 0, sUrl));
                return;
            }
            catch (Exception ex)
            {
                sendInfo.ResultId = ex.GetHashCode().ToString();
                sendInfo.OutString = ex.Message;
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", sendInfo.ResultId, sendInfo.OutString));
            }
            finally
            {
                SendGoodData.instance.Add(sendInfo);
            }
        }

        private CustomerGateInfo ReadInfo(string sResult)
        {
            CustomerGateInfo info = new CustomerGateInfo();
            XmlDocument xDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create(new StringReader(sResult));
            xDoc.Load(reader);

            XmlNodeList subnum = xDoc.GetElementsByTagName("subnum");
            string sRead = subnum[0].InnerText;
            info.subnum = sRead;

            XmlNodeList contactname = xDoc.GetElementsByTagName("contactname");
            sRead = contactname[0].InnerText;
            info.contactname = sRead;

            XmlNodeList contactaddress = xDoc.GetElementsByTagName("contactaddress");
            sRead = contactaddress[0].InnerText;
            info.contactaddress = sRead;

            XmlNodeList contactphone = xDoc.GetElementsByTagName("contactphone");
            sRead = contactphone[0].InnerText;
            info.contactphone = sRead;

            XmlNodeList contactemail = xDoc.GetElementsByTagName("contactemail");
            sRead = contactemail[0].InnerText;
            info.contactemail = sRead;

            XmlNodeList taxnumber = xDoc.GetElementsByTagName("taxnumber");
            sRead = taxnumber[0].InnerText;
            info.taxnumber = sRead;

            XmlNodeList idnumber = xDoc.GetElementsByTagName("idnumber");
            sRead = idnumber[0].InnerText;
            info.idnumber = sRead;

            XmlNodeList servicename = xDoc.GetElementsByTagName("servicename");
            sRead = servicename[0].InnerText;
            info.servicename = sRead;

            XmlNodeList subtypename = xDoc.GetElementsByTagName("subtypename");
            sRead = subtypename[0].InnerText;
            info.subtypename = sRead;

            XmlNodeList billschemaname = xDoc.GetElementsByTagName("billschemaname");
            sRead = billschemaname[0].InnerText;
            info.billschemaname = sRead;

            XmlNodeList substatusname = xDoc.GetElementsByTagName("substatusname");
            sRead = substatusname[0].InnerText;
            info.substatusname = sRead;

            XmlNodeList institemserialnum1 = xDoc.GetElementsByTagName("institemserialnum1");
            sRead = institemserialnum1[0].InnerText;
            info.institemserialnum1 = sRead;

            XmlNodeList institemserialnum2 = xDoc.GetElementsByTagName("institemserialnum2");
            sRead = institemserialnum2[0].InnerText;
            info.institemserialnum2 = sRead;

            XmlNodeList expirationdate = xDoc.GetElementsByTagName("expirationdate");
            sRead = expirationdate[0].InnerText;
            info.expirationdate = sRead;

            //vouchers
            List<voucher> oList = new List<voucher>();
            XmlNodeList vouchers = xDoc.GetElementsByTagName("voucher");
            for (int i = 0; i < vouchers.Count; i++)
            {
                voucher item = new voucher();

                XmlNodeList voucher = vouchers[i].ChildNodes;
                item.vouchervalue = voucher[0].InnerText;//vouchervalue
                item.duration = int.Parse(voucher[1].InnerText);//duration
                item.vouchername = voucher[2].InnerText;//vouchername
                item.durationuomaltcode = voucher[3].InnerText;//durationuomaltcode
                item.voucherdesc = voucher[4].InnerText;//durationuomaltcode
                oList.Add(item);
            }
            info.vouchers = oList;
            return info;
        }
        
        private bool AllowCall(HttpContext context)
        {
            string ServerLocal = context.Request.Url.Authority;
            string ServerRefeffer = "";
            if (context.Request.UrlReferrer != null) ServerRefeffer = context.Request.UrlReferrer.Authority;
            return (ServerLocal == ServerRefeffer);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    [Serializable]
    public class CacheInfo
    {
        public CustomerGateInfo User { get; set; }
        public voucher Voucher { get; set; }
        public string Good_Code { get; set; }
        public string sTrans_Id { get; set; }
    }
}