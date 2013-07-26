using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using BankNet.Core;
using BankNet.Data;
using BankNet.Entity;
using Web.WsNapTheAvg;//bản thật
//using Web.WsNapTheAvgSandbox;//bản test

namespace Web.Ajax
{
    /// <summary>
    /// Summary description for PaycardCmd
    /// </summary>
    public class PaycardCmd : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (!Security.AllowCall(context)) return;
            context.Response.ContentType = "application/json";
            
                string sUserId = context.Request["UserId"];
                string sCardId = context.Request["CardId"];
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

                if (string.IsNullOrEmpty(sUserId) || string.IsNullOrEmpty(sCardId))
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 4, "Chưa nhập đủ thông tin"));
                    return;
                }

                if (sCardId.Trim().Length != 12)
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 5, "Số thẻ phải là 12 ký tự"));
                    return;
                }

                if (sUserId.Trim().Length != 12 && sUserId.Trim().Length != 14)
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 6, "Số thuê bao phải là 12 hoặc 14 ký tự"));
                    return;
                }

                if (!Utility.isOnlyNumber(sUserId) || !Utility.isOnlyNumber(sCardId))
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 7, "Số hợp đồng và số thẻ phải là kiểu số"));
                    return;
                }

                PayCardInfo info = new PayCardInfo()
                {
                    UserId = sUserId,
                    CardId = sCardId,
                    CreateDate = DateTime.Now
                };

            try
            {
                //call services
                NapClient napClient = new NapClient();
                string sTrans_id = sUserId + sCardId + string.Format("{0:yyMMddHHmmss}",DateTime.Now);
                var oResult = napClient.submitVoucherByScratchcard(sUserId, sCardId,sTrans_id);
                
                if (oResult != null)
                {
                    info.ResulFull = oResult.responseData ?? "";
                    info.ResulId = oResult.returnCode;
                    info.Msg = oResult.returnCodeDescription ?? "";

                    if (info.ResulId == "" && info.ResulFull!="")
                    {
                        context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 0, ReadResultVocher(oResult.responseData)));
                    }
                    else
                    {
                        context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 8, info.Msg));
                    }
                }
                else
                {
                    context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", 9, "SERVER đang bận"));
                }
            }
            catch (Exception e)
            {
                context.Response.Write(string.Format("{{\"error\":{0},\"msg\":\"{1}\"}}", e.GetHashCode(), e.Message));
                //throw;
            }
            finally
            {
                //insert log to database
                PayCardData.instance.Add(info);
                context.Session.Remove(Config.GetSessionCode);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}