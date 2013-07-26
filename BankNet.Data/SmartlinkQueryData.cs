using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BankNet.Entity;
using BankNet.Core;

namespace BankNet.Data
{
	public class SmartlinkQueryData
    {
		private static SmartlinkQueryData _instance;
        public static SmartlinkQueryData instance
        {
            get { return _instance ?? (_instance = new SmartlinkQueryData()); }
        }
		
        public int Add(SmartlinkQueryInfo info)
        {
			SqlParameter[] param = {
                                       new SqlParameter("@vpc_Version", info.vpc_Version),
			                           new SqlParameter("@vpc_Command", info.vpc_Command),
			                           new SqlParameter("@vpc_AccessCode", info.vpc_AccessCode),
			                           new SqlParameter("@vpc_Merchant", info.vpc_Merchant),
			                           new SqlParameter("@vpc_MerchTxnRef", info.vpc_MerchTxnRef),
			                           new SqlParameter("@vpc_SecureHash", info.vpc_SecureHash),
			                           new SqlParameter("@vpc_DRExists", info.vpc_DRExists),
			                           new SqlParameter("@vpc_FoundMultipleDRs", info.vpc_FoundMultipleDRs),
			                           new SqlParameter("@vpc_TxnResponseCode", info.vpc_TxnResponseCode),
			                           new SqlParameter("@vpc_Message", info.vpc_Message),
			                           new SqlParameter("@CreateDate", info.CreateDate)			
		   };
            return int.Parse(DataHelper.ExecuteScalar(Config.ConnectString, "usp_SmartlinkQuery_Add", param).ToString());           
        }
        
        public List<SmartlinkQueryInfo> GetList(int pageIndex, int pageSize, out int total)
        {
            List<SmartlinkQueryInfo> list = null;
            var t = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@totalrow",DbType.Int32){Direction = ParameterDirection.Output}
                                   };
            SqlCommand comx;
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SmartlinkQuery_GetList", param, out comx);
            if (r != null)
            {
                list = new List<SmartlinkQueryInfo>();
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
		
		private SmartlinkQueryInfo FillData(IDataReader r)
        {
            SmartlinkQueryInfo info = new SmartlinkQueryInfo();
            if (!r.IsDBNull(r.GetOrdinal("id"))) info.id = r.GetInt32(r.GetOrdinal("id"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Version"))) info.vpc_Version = r.GetString(r.GetOrdinal("vpc_Version"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Command"))) info.vpc_Command = r.GetString(r.GetOrdinal("vpc_Command"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_AccessCode"))) info.vpc_AccessCode = r.GetString(r.GetOrdinal("vpc_AccessCode"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Merchant"))) info.vpc_Merchant = r.GetString(r.GetOrdinal("vpc_Merchant"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_MerchTxnRef"))) info.vpc_MerchTxnRef = r.GetString(r.GetOrdinal("vpc_MerchTxnRef"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_SecureHash"))) info.vpc_SecureHash = r.GetString(r.GetOrdinal("vpc_SecureHash"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_DRExists"))) info.vpc_DRExists = (Object)r["vpc_DRExists"];
			if (!r.IsDBNull(r.GetOrdinal("vpc_FoundMultipleDRs"))) info.vpc_FoundMultipleDRs = (Object)r["vpc_FoundMultipleDRs"];
			if (!r.IsDBNull(r.GetOrdinal("vpc_TxnResponseCode"))) info.vpc_TxnResponseCode = r.GetString(r.GetOrdinal("vpc_TxnResponseCode"));
			if (!r.IsDBNull(r.GetOrdinal("vpc_Message"))) info.vpc_Message = r.GetString(r.GetOrdinal("vpc_Message"));
			if (!r.IsDBNull(r.GetOrdinal("CreateDate"))) info.CreateDate = r.GetDateTime(r.GetOrdinal("CreateDate"));
			
            return info;
        }
    }
}
