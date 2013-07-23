using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using BankNet.Core;
using BankNet.Data;
using BankNet.Entity;
using Web.GateCardServices;
using Web.WsPaymentTest;

namespace Web.Ajax
{
    /// <summary>
    /// Summary description for GateCardCmd
    /// </summary>
    public class GateCardCmd : IHttpHandler, IRequiresSessionState
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
                    case "GateCard":
                        GateCard(context);
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

        //Test => /Ajax/GateCardCmd.ashx?Type=GetInfo&CaptCha=12345&UserId=70161641854001
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
            var cred = new credential { clientId = Config.clientIdGatePay };
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
                    /*
                    string sVoucher = "";
                    if (info.vouchers != null && info.vouchers.Count > 0)
                    {
                        foreach (voucher voucher in info.vouchers)
                        {
                            if (Utility.isOnlyNumber(voucher.vouchervalue) && int.Parse(voucher.vouchervalue) <= 500000)
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
                    */
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\",\"subnum\":\"{2}\",\"contactname\":\"{3}\",\"contactaddress\":\"{4}\"," +
                                                     "\"contactphone\":\"{5}\",\"contactemail\":\"{6}\",\"servicename\":\"{7}\",\"substatusname\":\"{8}\"," +
                                                     "\"expirationdate\":\"{9}\" }}",
                    0, "load thông tin ok", info.subnum, info.contactname, info.contactaddress, info.contactphone, info.contactemail, info.servicename, info.substatusname, info.expirationdate));
                    context.Session[Config.GetSessionUser] = info;
                }
                else
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 8, "Không tìm thấy thông tin khách hàng"));
                }
            }
        }

        private void GateCard(HttpContext context)
        {
            string CardType = context.Request["CardType"];
            string CardSerials = context.Request["CardSerials"];
            string CardPin = context.Request["CardPin"];

            if (string.IsNullOrEmpty(CardType))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 5, "Loại thẻ không phù hợp"));
                return;
            }

            //CardSerials
            if (string.IsNullOrEmpty(CardSerials))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 6, "chưa nhập số Serials"));
                return;
            }

            //if (CardSerials.Trim().Length != 11)
            //{
            //    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Số thuê bao phải là 11 số"));
            //    return;
            //}

            //if (!Utility.isOnlyNumber(CardSerials))
            //{
            //    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Serials phải là kiểu số"));
            //    return;
            //}

            if (!Utility.isText(CardSerials))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Số Serials không hợp lệ"));
                return;
            }

            //CardPin
            if (string.IsNullOrEmpty(CardPin))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 8, "chưa nhập số thẻ"));
                return;
            }

            //if (CardPin.Trim().Length != 13)
            //{
            //    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Số thẻ phải là 13 số"));
            //    return;
            //}

            //if (!Utility.isOnlyNumber(CardSerials))
            //{
            //    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Số thẻ phải là kiểu số"));
            //    return;
            //}

            if (!Utility.isText(CardSerials))
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 9, "Số thẻ không hợp lệ"));
                return;
            }

            var oUser = (CustomerGateInfo)context.Session[Config.GetSessionUser];

            if (oUser == null)
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 10, "Hết phiên làm việc, hãy thực hiện lại"));
                return;
            }

            CardSerials = CardSerials.ToUpper();
            CardPin = CardPin.ToUpper();

            switch (CardType)
            {
                case "CardInputViettel":
                    GateViettel(context, oUser.subnum, CardSerials, CardPin, CardType);
                    break;
                case "CardInputVMS":
                case "CardInputVNP":
                case "CardInputGate":
                    GateFTP(context, oUser.subnum, CardSerials, CardPin, CardType);
                    break;
            }
        }

        private void GateViettel(HttpContext context, string UserId, string CardSerials, string CardPin, string sType)
        {
            GateCardInfo info = new GateCardInfo()
            {
                UserId = UserId,
                CardId = CardPin,
                SerialsId = CardSerials,
                CreateDate = DateTime.Now,
                ServiceID = sType
            };

            try
            {
                AuthenSoapHeader authen = new AuthenSoapHeader();
                authen.UserName = Config.UserServicesGate;
                authen.Password = Config.PassServicesGate;
                WsGateCardSoapClient client = new WsGateCardSoapClient();
                var result = client.TopupCard(authen, sType, CardSerials, CardPin);

                if (result.ErrorCode == "00")
                {
                    //Giao dịch GateCard thành công
                    info.ResultId = result.ErrorCode;
                    info.Msg = result.ErrorMessage;
                    info.Amount = int.Parse(result.Amount);
                    info.TransId = result.TransId;
                    
                    SubmitVoucherInfo sbInfo = new SubmitVoucherInfo()
                    {
                        GatePayId = Config.ClientIdVIETTEL,
                        UserId = UserId,
                        Amount = info.Amount,
                        CreateDate = DateTime.Now,
                        TransId = info.TransId
                    };
                    try
                    {
                        WSClient wsclient = new WSClient();
                        var cred = new credential { clientId = Config.clientIdGatePay };
                        var wsResult = wsclient.submitVoucher(cred, UserId, result.Amount, info.TransId);

                        sbInfo.returnCode = wsResult.returnCode;
                        sbInfo.returnCodeDescription = wsResult.returnCodeDescription;
                        sbInfo.responseData = wsResult.responseData;
                        sbInfo.signature = wsResult.signature;

                        if (sbInfo.returnCode == "")
                        {
                            string sDate = ReadResultVocher(sbInfo.responseData);
                            context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 0, sDate));
                            return;
                        }
                        else
                        {
                            context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", sbInfo.returnCode, sbInfo.returnCodeDescription));
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        //log error
                        context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", e.GetHashCode(), e.Message));
                        sbInfo.returnCode = e.GetHashCode().ToString();
                        sbInfo.returnCodeDescription = e.Message;
                        return;
                    }
                    finally
                    {
                        SubmitVoucherData.instance.Add(sbInfo);
                    }
                }
                else
                {
                    info.ResultId = result.ErrorCode;
                    info.Msg = result.ErrorMessage;
                    context.Response.Write(string.Format("{{\"error\":\"{0}\",\"msg\":\"{1}\"}}", result.ErrorCode, result.ErrorMessage));
                }
            }
            catch (Exception e)
            {
                //log error
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", e.GetHashCode(), e.Message));
                info.ResultId = e.GetHashCode().ToString();
                info.Msg = e.Message;
                throw;
            }
            finally
            {
                GateCardData.instance.Add(info);
            }
        }

        private void GateFTP(HttpContext context, string UserId, string CardSerials, string CardPin, string sType)
        {
            GateCardInfo info = new GateCardInfo()
            {
                UserId = UserId,
                CardId = CardPin,
                SerialsId = CardSerials,
                CreateDate = DateTime.Now,
                ServiceID = sType
            };

            try
            {
                AuthenSoapHeader authen = new AuthenSoapHeader();
                authen.UserName = Config.UserServicesGate;
                authen.Password = Config.PassServicesGate;
                WsGateCardSoapClient client = new WsGateCardSoapClient();
                var result = client.CardInputSandbox(authen, sType, CardSerials, CardPin);

                if (result.ErrorCode == "00")
                {
                    //Giao dịch GateCard thành công
                    info.ResultId = result.ErrorCode;
                    info.Msg = result.ErrorMessage;
                    info.Amount = int.Parse(result.Amount);
                    info.TransId = result.TransId;

                    SubmitVoucherInfo sbInfo = new SubmitVoucherInfo()
                                                   {
                                                       GatePayId = Config.ClientIdFPT,
                                                       UserId = UserId,
                                                       Amount = info.Amount,
                                                       CreateDate = DateTime.Now,
                                                       TransId = info.TransId
                                                   };
                    try
                    {
                        WSClient wsclient = new WSClient();
                        var cred = new credential { clientId = Config.ClientIdFPT };
                        var wsResult = wsclient.submitVoucher(cred, UserId, result.Amount, info.TransId);

                        sbInfo.returnCode = wsResult.returnCode;
                        sbInfo.returnCodeDescription = wsResult.returnCodeDescription;
                        sbInfo.responseData = wsResult.responseData;
                        sbInfo.signature = wsResult.signature;

                        if(sbInfo.returnCode=="")
                        {
                            string sDate = ReadResultVocher(sbInfo.responseData);
                            context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 0, sDate));
                            return;
                        }
                        else
                        {
                            context.Response.Write(string.Format("{{\"error\":\"{0}\",\"msg\":\"{1}\"}}", sbInfo.returnCode, sbInfo.returnCodeDescription));
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        //log error
                        context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", e.GetHashCode(), e.Message));
                        sbInfo.returnCode = e.GetHashCode().ToString();
                        sbInfo.returnCodeDescription = e.Message;
                        return;
                    }
                    finally
                    {
                        SubmitVoucherData.instance.Add(sbInfo);
                    }
                }
                else
                {
                    info.ResultId = result.ErrorCode;
                    info.Msg = result.ErrorMessage;
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", result.ErrorCode, result.ErrorMessage));
                }
            }
            catch (Exception e)
            {
                //log error
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", e.GetHashCode(), e.Message));
                //if (info.ResultId == "")
                //{
                //    info.ResultId = e.GetHashCode().ToString();
                //    info.Msg = e.Message;
                //}
                return;
            }
            finally
            {
                GateCardData.instance.Add(info);
            }
        }

        private string ReadResultVocher(string sXML)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create(new StringReader(sXML));
            xDoc.Load(reader);

            XmlNodeList subnum = xDoc.GetElementsByTagName("expirationdate");
            string sRead = subnum[0].InnerText;
            return sRead;
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
}