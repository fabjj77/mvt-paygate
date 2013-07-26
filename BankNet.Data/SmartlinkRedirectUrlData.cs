using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BankNet.Entity;
using BankNet.Core;

namespace BankNet.Data
{
	public class SmartlinkRedirectUrlData
    {
		private static SmartlinkRedirectUrlData _instance;
        public static SmartlinkRedirectUrlData instance
        {
            get { return _instance ?? (_instance = new SmartlinkRedirectUrlData()); }
        }
		
        public int Add(SmartlinkRedirectUrlInfo info)
        {
			SqlParameter[] param = {
			    new SqlParameter("@vpc_Version", info.vpc_Version),
			new SqlParameter("@vpc_Locale", info.vpc_Locale),
			new SqlParameter("@vpc_Command", info.vpc_Command),
			new SqlParameter("@vpc_Merchant", info.vpc_Merchant),
			new SqlParameter("@vpc_AccessCode", info.vpc_AccessCode),
			new SqlParameter("@vpc_MerchTxnRef", info.vpc_MerchTxnRef),
			new SqlParameter("@vpc_Amount", info.vpc_Amount),
			new SqlParameter("@vpc_CurrencyCode", info.vpc_CurrencyCode),
			new SqlParameter("@vpc_OrderInfo", info.vpc_OrderInfo),
			new SqlParameter("@vpc_ReturnURL", info.vpc_ReturnURL),
			new SqlParameter("@vpc_BackURL", info.vpc_BackURL),
			new SqlParameter("@vpc_TicketNo", info.vpc_TicketNo),
			new SqlParameter("@vpc_SecureHash", info.vpc_SecureHash),
			new SqlParameter("@CreateDate", info.CreateDate)			
		   };
            return int.Parse(DataHelper.ExecuteScalar(Config.ConnectString, "usp_SmartlinkRedirectUrl_Add", param).ToString());           
        }
        
        public int Update(SmartlinkRedirectUrlInfo info)
        {
			SqlParameter[] param = {
									   new SqlParameter("@id", info.id)
			,new SqlParameter("@vpc_Version", info.vpc_Version),
			new SqlParameter("@vpc_Locale", info.vpc_Locale),
			new SqlParameter("@vpc_Command", info.vpc_Command),
			new SqlParameter("@vpc_Merchant", info.vpc_Merchant),
			new SqlParameter("@vpc_AccessCode", info.vpc_AccessCode),
			new SqlParameter("@vpc_MerchTxnRef", info.vpc_MerchTxnRef),
			new SqlParameter("@vpc_Amount", info.vpc_Amount),
			new SqlParameter("@vpc_CurrencyCode", info.vpc_CurrencyCode),
			new SqlParameter("@vpc_OrderInfo", info.vpc_OrderInfo),
			new SqlParameter("@vpc_ReturnURL", info.vpc_ReturnURL),
			new SqlParameter("@vpc_BackURL", info.vpc_BackURL),
			new SqlParameter("@vpc_TicketNo", info.vpc_TicketNo),
			new SqlParameter("@vpc_SecureHash", info.vpc_SecureHash),
			new SqlParameter("@CreateDate", info.CreateDate)			
								   };
            return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_SmartlinkRedirectUrl_Update", param);    
        }


        public int Delete(int id)
        {
            SqlParameter[] param = {
									   new SqlParameter("@id", id)
			
								   };
            return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_SmartlinkRedirectUrl_Delete", param);   
        }

        public SmartlinkRedirectUrlInfo GetInfo(int id)
        {
            SmartlinkRedirectUrlInfo info = null;
			SqlParameter[] param = {
									   new SqlParameter("@id", id)
			
								   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SmartlinkRedirectUrl_GetById", param);
			if (r != null)
			{
				info = new SmartlinkRedirectUrlInfo();
				while (r.Read())
				{
					info = FillData(r);
				}
				r.Close();
                r.Dispose();
			}
			return info;
        }
		
		public List<SmartlinkRedirectUrlInfo> GetAll()
        {
            List<SmartlinkRedirectUrlInfo> list = null;
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SmartlinkRedirectUrl_GetAll");
            if (r != null)
            {
                list = new List<SmartlinkRedirectUrlInfo>();
                while (r.Read())
                {
					list.Add(FillData(r));
                }
                r.Close();
                r.Dispose();
            }

            return list;
        }

        public List<SmartlinkRedirectUrlInfo> GetList(int pageIndex, int pageSize, out int total)
        {
            List<SmartlinkRedirectUrlInfo> list = null;
            var t = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@totalrow",DbType.Int32){Direction = ParameterDirection.Output}
                                   };
            SqlCommand comx;
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SmartlinkRedirectUrl_GetList", param, out comx);
            if (r != null)
            {
                list = new List<SmartlinkRedirectUrlInfo>();
                while (r.Read())
                {
					list.Add(FillData(r));
                }
                r.Close();
                r.Dispose();
                t = int.Parse(comx.Parameters[2].Value.ToString());
            }

            total = t;
            return list;
        }
		
		private SmartlinkRedirectUrlInfo FillData(IDataReader r)
        {
            SmartlinkRedirectUrlInfo info = new SmartlinkRedirectUrlInfo();
            if (!r.IsDBNull(r.GetOrdinal("id"))) info.id = r.GetInt32(r.GetOrdinal("id"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Version"))) info.vpc_Version = r.GetString(r.GetOrdinal("vpc_Version"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Locale"))) info.vpc_Locale = r.GetString(r.GetOrdinal("vpc_Locale"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Command"))) info.vpc_Command = r.GetString(r.GetOrdinal("vpc_Command"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Merchant"))) info.vpc_Merchant = r.GetString(r.GetOrdinal("vpc_Merchant"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_AccessCode"))) info.vpc_AccessCode = r.GetString(r.GetOrdinal("vpc_AccessCode"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_MerchTxnRef"))) info.vpc_MerchTxnRef = r.GetString(r.GetOrdinal("vpc_MerchTxnRef"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Amount"))) info.vpc_Amount = r.GetString(r.GetOrdinal("vpc_Amount"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_CurrencyCode"))) info.vpc_CurrencyCode = r.GetString(r.GetOrdinal("vpc_CurrencyCode"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_OrderInfo"))) info.vpc_OrderInfo = r.GetString(r.GetOrdinal("vpc_OrderInfo"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_ReturnURL"))) info.vpc_ReturnURL = r.GetString(r.GetOrdinal("vpc_ReturnURL"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_BackURL"))) info.vpc_BackURL = r.GetString(r.GetOrdinal("vpc_BackURL"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_TicketNo"))) info.vpc_TicketNo = r.GetString(r.GetOrdinal("vpc_TicketNo"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_SecureHash"))) info.vpc_SecureHash = r.GetString(r.GetOrdinal("vpc_SecureHash"));
			if (!r.IsDBNull(r.GetOrdinal("CreateDate"))) info.CreateDate = r.GetDateTime(r.GetOrdinal("CreateDate"));
			
            return info;
        }
    }
}
