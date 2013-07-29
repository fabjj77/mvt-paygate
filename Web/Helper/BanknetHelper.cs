using System;
using System.Net.Mime;
using System.Threading;
using BankNet.Core;
using BankNet.Entity;
//using Web.BanknetSandbox;//bản demo
using Web.BanknetServices;

namespace Web.Helper
{
    public class BanknetHelper
    {
        private static string Merchant_code = Config.MerchantCode;// "010035";//010042
        private static string Country_Code = Config.CountryCode;// "+84";
        public static string Merchant_trans_key = Config.MerchantTransKey;//"fb9d207792845d2fac137f4ae0139c84";//26dc6bdb54d04dc20036dad8313ed251

        //private static PaymentGatewayPortTypeClient _client;
        //private static PaymentGatewayPortTypeClient instance
        //{
        //    get { return _client ?? (_client = new PaymentGatewayPortTypeClient("PaymentGatewayHttpSoap11Endpoint")); }
        //}

        private static PaymentGateway _client;
        private static PaymentGateway instance
        {
            get { return _client ?? (_client = new PaymentGateway()); }
        }

        public static string Send_GoodInfo(string Good_code, string XMLDesc, string Net_cost, string Ship_Fee, string Tax, string URLSuccess, string URLFail, ref SendGoodInfo sendInfo)
        {
            string Merchant_trans_id = GetMerchantTransId();
            string TransHash = Security.GetMD5Hash(Merchant_trans_id + Merchant_code + Good_code + Net_cost + Ship_Fee + Tax + Merchant_trans_key);
            //
            sendInfo.FunctionName = "Send_GoodInfo";
            sendInfo.Merchant_code = Merchant_code;
            sendInfo.Merchant_trans_id = Merchant_trans_id;
            sendInfo.Net_cost = Net_cost;
            sendInfo.Ship_fee = Ship_Fee;
            sendInfo.Tax = Tax;
            sendInfo.Country_code = Country_Code;
            sendInfo.Trans_key = TransHash;
            //
            try
            {
                //string s =instance.Send_GoodInfo(Merchant_trans_id, Merchant_code, Country_Code, Good_code, XMLDesc, Net_cost, Ship_Fee, Tax, URLSuccess, URLFail, TransHash);
                string s = instance.Send_GoodInfo(Merchant_trans_id, Merchant_code, Country_Code, Good_code, XMLDesc, Net_cost, Ship_Fee, Tax, URLSuccess, URLFail, TransHash);
                //
                sendInfo.ResultId = getCodeResult(s);
                sendInfo.OutString = s;
                //
                return s;
            }
            catch (Exception ex)
            {
                return ex.Message;
                //throw;
            }
            
        }

        //public static string Send_GoodInfo_Ext(string Good_code, string XMLDesc, string Net_cost, string Ship_Fee, string Tax, string URLSuccess, string URLFail)
        //{
        //    string TransHash = Security.GetMD5Hash(Merchant_trans_id + Merchant_code + Good_code + Net_cost + Ship_Fee + Tax + Merchant_trans_key);
        //    return instance.Send_GoodInfo_Ext(Merchant_trans_id, Merchant_code, Country_Code, Good_code, XMLDesc, Net_cost, Ship_Fee, Tax, URLSuccess, URLFail, TransHash, "970489");
        //}

        //public static string checkStatus(string Trans_id,string Trans_Key)
        //{
        //    //goi ws kiem tra giao dich
        //    string keymd5 = Security.GetMD5Hash(Merchant_trans_id + Trans_id + Merchant_trans_key + Trans_Key);
        //    string result = instance.QuerryBillStatus(Merchant_trans_id, Trans_id, Merchant_trans_key, keymd5);
        //    string[] returnArgs = result.Split('|');
        //    return returnArgs[1];
        //}

        public static string QuerryBillStatus(string Merchant_trans_id, string Trans_id)
        {
            string TransHash = Security.GetMD5Hash(Merchant_trans_id + Trans_id + Merchant_code + Merchant_trans_key);
            return instance.QuerryBillStatus(Merchant_trans_id, Trans_id, Merchant_code, TransHash);
        }

        public static string QuerryBillStatus(string Merchant_trans_id,string Trans_id, ref QuerryBillStatusInfo oStatusInfo)
        {
            string TransHash = Security.GetMD5Hash(Merchant_trans_id + Trans_id + Merchant_code + Merchant_trans_key);

            oStatusInfo.Merchant_trans_id = Merchant_trans_id;
            oStatusInfo.Trans_id = Trans_id;
            oStatusInfo.Merchant_code = Merchant_code;
            oStatusInfo.Trans_key = TransHash;

            return instance.QuerryBillStatus(Merchant_trans_id,Trans_id, Merchant_code, TransHash);
        }

        /// <summary>
        /// Xác nhận thanh toán thành công hay thất bại
        /// </summary>
        /// <param name="Trans_id">Mã giao dịch</param>
        /// <param name="Trans_result">0: thành công | 1: thất bại</param>
        /// <returns></returns>
        public static string ConfirmTransactionResult(string Merchant_trans_id, string Trans_id, string Trans_result)
        {
            string TransHash = Security.GetMD5Hash(Merchant_trans_id + Trans_id + Merchant_code + Trans_result + Merchant_trans_key);
            return instance.ConfirmTransactionResult(Merchant_trans_id, Trans_id, Merchant_code, Trans_result, TransHash);
        }

        public static string ConfirmTransactionResult(string Merchant_trans_id,string Trans_id, string Trans_result, ref ConfirmTransactionResultInfo oConfirmInfo)
        {
            string TransHash = Security.GetMD5Hash(Merchant_trans_id + Trans_id + Merchant_code + Trans_result + Merchant_trans_key);

            oConfirmInfo.Merchant_trans_id = Merchant_trans_id;
            oConfirmInfo.Trans_id = Trans_id;
            oConfirmInfo.Merchant_code = Merchant_code;
            oConfirmInfo.Trans_key = TransHash;
            oConfirmInfo.Trans_result = Trans_result;

            return instance.ConfirmTransactionResult(Merchant_trans_id, Trans_id, Merchant_code,Trans_result,TransHash);
        }

        //public static string confirmTrans(string Merchant_trans_id, string Trans_id, string Merchant_code, string Trans_Key, string Trans_result)
        //{
        //    //goi ws kiem tra giao dich
        //    string key = Security.GetMD5Hash(Merchant_trans_id + Trans_id + Merchant_code + Trans_Key);
        //    string result = instance.ConfirmTransactionResult(Merchant_trans_id, Trans_id, Merchant_code, key, Trans_result);
        //    string[] returnArgs = result.Split('|');
        //    return returnArgs[1];
        //}

        public static string getCodeResult(string s)
        {
            string[] arr = s.Split('|');
            if (arr != null && arr.Length > 0) return arr[0];
            return "";
        }

        public static int GetUrlRedirection(string s, out string UrlOut)
        {
            string[] arr = s.Split('|');
            if (arr == null || arr.Length != 3)
            {
                UrlOut = "chuỗi ko phù hợp";
                return 1;
            }

            int iLeng = int.Parse(arr[1]);
            string s2 = arr[2];
            if(s2.Length<iLeng)
            {
                UrlOut = "chuỗi trả về không phù hợp";
                return 2;
            }
            string url = s2.Substring(0, iLeng);
            string sMd5Get = s2.Substring(iLeng, s2.Length - iLeng);

            string sMd5New = Security.GetMD5Hash("010" + iLeng + url + Merchant_trans_key);
            if (sMd5Get!=sMd5New)
            {
                UrlOut = "chuỗi trả về không phù hợp";
                return 3;
            }

            UrlOut = url;
            return 0;
        }

        public static string getTransIdFromUrl(string url)
        {
            string transId = "";
            int pos = url.IndexOf("Trans_id=");
            if (pos > 0)
            {
                transId = url.Substring(pos + 9);//lấy từ trans_id= đến cuối chuỗi
            }
            else
            {
                transId = "";
            }
            return transId;
        }

        public static string GetMerchantTransId()
        {
            int i = System.Web.HttpContext.Current.Application["count"] == null ? 0 : int.Parse(System.Web.HttpContext.Current.Application["count"].ToString());
            i++;
            i = i > 999999 ? 0 : i;
            System.Web.HttpContext.Current.Application["count"] = i;

            //ThreadStart newThread = delegate { SaveCount(i); };
            //Thread myThread = new Thread(newThread);
            //myThread.Start();

            return i.ToString();
        }

        private static void SaveCount(int i)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath("/count.txt"));
            writer.WriteLine(i);
            writer.Close();
        }

        public static string getTransIdFromUrl_UseServicePMG(string url)
        {
            string transId = "";
            int pos = url.IndexOf("trans_id=");
            if (pos > 0)
            {
                transId = url.Substring(pos + 9);
            }
            else
            {
                transId = "";
            }
            return transId;
        }
    }
}