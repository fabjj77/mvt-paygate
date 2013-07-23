using System.Configuration;

namespace BankNet.Core
{
    public class Config
    {
        private static string _connectionString;
        public static string ConnectString
        {
            get
            {
                return _connectionString ?? (_connectionString = ConfigurationManager.AppSettings["Connectiondb"]);
            }
        }

        private static string _UserServices;
        public static string UserServices
        {
            get
            {
                return _UserServices ?? (_UserServices = ConfigurationManager.AppSettings["UserServices"]);
            }
        }

        private static string _PassServices;
        public static string PassServices
        {
            get
            {
                return _PassServices ?? (_PassServices = ConfigurationManager.AppSettings["PassServices"]);
            }
        }

        private static string _UserServicesGate;
        public static string UserServicesGate
        {
            get
            {
                return _UserServicesGate ?? (_UserServicesGate = ConfigurationManager.AppSettings["UserServicesGate"]);
            }
        }

        private static string _PassServicesGate;
        public static string PassServicesGate
        {
            get
            {
                return _PassServicesGate ?? (_PassServicesGate = ConfigurationManager.AppSettings["PassServicesGate"]);
            }
        }

        private static string _ClientIdBanknet;
        public static string ClientIdBanknet
        {
            get
            {
                return _ClientIdBanknet ?? (_ClientIdBanknet = ConfigurationManager.AppSettings["ClientIdBanknet"]);
            }
        }

        //ClientIdSmartLink
        private static string _ClientIdSmartLink;
        public static string ClientIdSmartLink
        {
            get
            {
                return _ClientIdSmartLink ?? (_ClientIdSmartLink = ConfigurationManager.AppSettings["ClientIdSmartLink"]);
            }
        }

        //ClientIdPayoo
        private static string _ClientIdPayoo;
        public static string ClientIdPayoo
        {
            get
            {
                return _ClientIdPayoo ?? (_ClientIdPayoo = ConfigurationManager.AppSettings["ClientIdPayoo"]);
            }
        }

        //ClientIdM-Pay
        private static string _ClientIdMPay;
        public static string ClientIdMPay
        {
            get
            {
                return _ClientIdMPay ?? (_ClientIdMPay = ConfigurationManager.AppSettings["ClientIdMPay"]);
            }
        }

        public static string clientIdGatePay
        {
            get { return ClientIdVIETTEL; }
        }

        private static string _ClientIdVIETTEL;
        public static string ClientIdVIETTEL
        {
            get
            {
                return _ClientIdVIETTEL ?? (_ClientIdVIETTEL = ConfigurationManager.AppSettings["ClientIdVIETTEL"]);
            }
        }

        private static string _ClientIdFPT;
        public static string ClientIdFPT
        {
            get
            {
                return _ClientIdFPT ?? (_ClientIdFPT = ConfigurationManager.AppSettings["ClientIdFPT"]);
            }
        }
        
        private static string _GatePayMaxLenInput;
        public static string GatePayMaxLenInput
        {
            get
            {
                return _GatePayMaxLenInput ?? (_GatePayMaxLenInput = ConfigurationManager.AppSettings["GatePayMaxLenInput"]);
            }
        }

        //Banknet
        private static string _MerchantTransId;
        public static string MerchantTransId
        {
            get
            {
                return _MerchantTransId ?? (_MerchantTransId = ConfigurationManager.AppSettings["MerchantTransId"]);
            }
        }

        private static string _MerchantCode;
        public static string MerchantCode
        {
            get
            {
                return _MerchantCode ?? (_MerchantCode = ConfigurationManager.AppSettings["MerchantCode"]);
            }
        }

        private static string _CountryCode;
        public static string CountryCode
        {
            get
            {
                return _CountryCode ?? (_CountryCode = ConfigurationManager.AppSettings["CountryCode"]);
            }
        }

        private static string _MerchantTransKey;
        public static string MerchantTransKey
        {
            get
            {
                return _MerchantTransKey ?? (_MerchantTransKey = ConfigurationManager.AppSettings["MerchantTransKey"]);
            }
        }

        private static string _Desc;
        public static string Desc
        {
            get
            {
                return _Desc ?? (_Desc = ConfigurationManager.AppSettings["Desc"]);
            }
        }

        //Session
        public static string GetSessionCode { get { return "Code_Captcha"; } }
        public static string GetSessionUser { get { return "User_BankNet"; } }
        public static string GetSessionsResultDate { get { return "User_BankNet_Date"; } }
        public static string GetSessionsResultFail { get { return "User_BankNet_Fail"; } }
    }
}
