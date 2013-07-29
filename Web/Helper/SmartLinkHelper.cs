using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using BankNet.Core;
using BankNet.Entity;

namespace Web.Helper
{
    [Serializable]
    public class SmartLinkHelper
    {
        private static String secureSecret = Config.SecureSecret;

        public static string getRedirectUrl(string sAmount,ref  SmartlinkRedirectUrlInfo oRedirectUrlInfo)
        {
            var context = HttpContext.Current;
            string sTranId = string.Format("T{0:yyMMddHHmmssfff}",DateTime.Now);
            string sSuccess = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + "/SmartLink/success.aspx";
            string sFail = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + "/SmartLink/fail.aspx?code=" + sTranId;
            string sIP = GetFullIp(context);

            Hashtable hash = new Hashtable();
            hash.Add("vpc_Version", Config.vpc_Version);
            hash.Add("vpc_Locale", Config.vpc_Locale);
            hash.Add("vpc_Command", Config.vpc_Command);
            hash.Add("vpc_Merchant", Config.vpc_Merchant);
            hash.Add("vpc_AccessCode", Config.vpc_AccessCode);
            hash.Add("vpc_MerchTxnRef", sTranId);
            hash.Add("vpc_Amount", string.Format(Config.vpc_Amount,sAmount));
            hash.Add("vpc_Currency", Config.vpc_Currency);
            hash.Add("vpc_OrderInfo", Config.vpc_OrderInfo);
            hash.Add("vpc_ReturnURL", sSuccess);
            hash.Add("vpc_BackURL", sFail);
            hash.Add("vpc_TicketNo", sIP);

            //
            oRedirectUrlInfo.vpc_Version = Config.vpc_Version;
            oRedirectUrlInfo.vpc_Locale = Config.vpc_Locale;
            oRedirectUrlInfo.vpc_Command = Config.vpc_Command;
            oRedirectUrlInfo.vpc_Merchant = Config.vpc_Merchant;
            oRedirectUrlInfo.vpc_AccessCode = Config.vpc_AccessCode;
            oRedirectUrlInfo.vpc_MerchTxnRef = sTranId;
            oRedirectUrlInfo.vpc_Amount = string.Format(Config.vpc_Amount,sAmount);
            oRedirectUrlInfo.vpc_CurrencyCode = Config.vpc_Currency;
            oRedirectUrlInfo.vpc_OrderInfo = Config.vpc_OrderInfo;
            oRedirectUrlInfo.vpc_ReturnURL =sSuccess ;
            oRedirectUrlInfo.vpc_BackURL = sFail;
            oRedirectUrlInfo.vpc_TicketNo =sIP;
            //

            return getRedirectUrl(hash, Config.VirtualPaymentClientUrl);
        }
        
        private static string GetFullIp(HttpContext context)
        {
            string s = "";
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            s = Convert.ToString(ipEntry.AddressList[1]);
            s += " - " + Convert.ToString(ipEntry.HostName);

            string ipList = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                s += " - " + ipList.Split(',')[0];
            }
            else
            {
                s += " - " + context.Request.ServerVariables["REMOTE_ADDR"];
            }
            return s;
        }

        public static String getRedirectUrl(Hashtable parameters,string sUrl)
        {
            String vpcUrl =sUrl + "?";
            String md5HashData = secureSecret;

            ArrayList keys = new ArrayList(parameters.Keys);
            Object[] keyArray = keys.ToArray();
            Array.Sort(keyArray, StringComparer.Ordinal);
            int appendFlag = 0;
            foreach (Object obj in keyArray)
            {
                String key = (String)obj;
                String value = (String)parameters[key];
                if (value.Length > 0)
                {
                    if (appendFlag == 0)
                    {
                        vpcUrl += HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value);
                        appendFlag = 1;
                    }
                    else
                    {
                        vpcUrl += "&" + HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value);
                    }
                    md5HashData += value;
                }
            }

            Byte[] originalBytes;
            StringBuilder sb = new StringBuilder();
            MD5 md5 = new MD5CryptoServiceProvider();
            originalBytes = UTF8Encoding.UTF8.GetBytes(md5HashData);

            foreach (Byte b in md5.ComputeHash(originalBytes))
                sb.Append(b.ToString("x2").ToUpper());

            string checksum = sb.ToString();
            vpcUrl += "&vpc_SecureHash=" + checksum;
            return vpcUrl;
        }

        public static bool checkSum(Hashtable parameters, out bool isEmptysecureSecret)
        {
            String md5HashData = secureSecret;
            if (secureSecret.Length > 0)
            {
                isEmptysecureSecret = false;//add

                ArrayList keys = new ArrayList(parameters.Keys);
                Object[] keyArray = keys.ToArray();
                Array.Sort(keyArray, StringComparer.Ordinal);
                foreach (String key in keyArray)
                {
                    String value = (String)parameters[key];
                    if (!key.Equals("vpc_SecureHash") && value.Length > 0)
                    {
                        md5HashData += HttpUtility.UrlDecode(value);
                    }
                }

                Byte[] originalBytes;
                StringBuilder sb = new StringBuilder();
                MD5 md5 = new MD5CryptoServiceProvider();
                originalBytes = UTF8Encoding.UTF8.GetBytes(md5HashData);

                foreach (Byte b in md5.ComputeHash(originalBytes))
                    sb.Append(b.ToString("x2").ToUpper());

                String checksum = sb.ToString();
                String secureHash = (String)parameters["vpc_SecureHash"];
                if (checksum.ToUpper().Equals(secureHash))
                {
                    return true;
                }
            }
            else
            {
                isEmptysecureSecret = true;
            }
            return false;

        }

        public static string GetQuery(string sTransId,ref SmartlinkQueryInfo oQueryInfo)
        {
            Hashtable hash = new Hashtable();
            hash.Add("vpc_Version", Config.vpc_Version);
            hash.Add("vpc_Command", "queryDr");
            hash.Add("vpc_Merchant", Config.vpc_Merchant);
            hash.Add("vpc_AccessCode", Config.vpc_AccessCode);
            hash.Add("vpc_MerchTxnRef", sTransId);

            //
            oQueryInfo.vpc_Version = Config.vpc_Version;
            oQueryInfo.vpc_Command = Config.vpc_Command;
            oQueryInfo.vpc_Merchant = Config.vpc_Merchant;
            oQueryInfo.vpc_AccessCode = Config.vpc_AccessCode;
            oQueryInfo.vpc_MerchTxnRef =sTransId;
            //
            return GetHtmlPage(getRedirectUrl(hash, Config.VirtualPaymentClientQueryUrl));
        }

        private static string GetHtmlPage(string strURL)
        {
            String strResult;
            WebResponse objResponse;
            WebRequest objRequest = HttpWebRequest.Create(strURL);
            objResponse = objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            return strResult;
        }

        public static String getResponseDescription(String vResponseCode)
        {
            String result = "";
            if (!string.IsNullOrEmpty(vResponseCode) && vResponseCode.Length >= 1)
            {
                char input = vResponseCode[0];
                switch (input)
                {
                    case '0': result = "Giao dịch thành công"; break;
                    case '1': result = "Ngân hàng từ chối thanh toán: thẻ/tài khoản bị khóa"; break;
                    case '3': result = "Thẻ hết hạn"; break;
                    case '4': result = "Lỗi người mua hàng: Quá số lần cho phép. (Sai OTP, quá hạn mức trong ngày)"; break;
                    case '5': result = "Không có trả lời của Ngân hàng"; break;
                    case '6': result = "Lỗi giao tiếp với Ngân hàng"; break;
                    case '7': result = "Tài khoản không đủ tiền"; break;
                    case '8': result = "Lỗi checksum dữ liệu"; break;
                    case '9': result = "Kiểu giao dịch không được hỗ trợ"; break;
                    default: result = "Không xác định"; break;
                }
                return result;
            }
            else
            {
                return "Không có giá trị trả về";
            }
        }
    }
}