using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BankNet.Entity;
using BankNet.Core;

namespace BankNet.Data
{
    public class SubmitVoucherData
    {
        private static SubmitVoucherData _instance;
        public static SubmitVoucherData instance
        {
            get { return _instance ?? (_instance = new SubmitVoucherData()); }
        }

        public int Add(SubmitVoucherInfo info)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@GatePayId", info.GatePayId),
                                       new SqlParameter("@UserId", info.UserId),
                                       new SqlParameter("@Amount", info.Amount),
                                       new SqlParameter("@TransId", info.TransId),
			                           new SqlParameter("@returnCode", info.returnCode),
			                           new SqlParameter("@responseData", info.responseData),
			                           new SqlParameter("@returnCodeDescription", info.returnCodeDescription),
			                           new SqlParameter("@signature", info.signature),
			                           new SqlParameter("@CreateDate", info.CreateDate)			
		   };
            return int.Parse(DataHelper.ExecuteScalar(Config.ConnectString, "usp_SubmitVoucher_Add", param).ToString());
        }

        public SubmitVoucherInfo GetInfo(int id)
        {
            SubmitVoucherInfo info = null;
            SqlParameter[] param = {
									   new SqlParameter("@id", id)
								   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SubmitVoucher_GetById", param);
            if (r != null)
            {
                info = new SubmitVoucherInfo();
                while (r.Read())
                {
                    info = FillData(r);
                }
                r.Close();
                r.Dispose();
            }
            return info;
        }

        public List<SubmitVoucherInfo> GetListExport(DateTime date1, DateTime date2, int status)
        {
            List<SubmitVoucherInfo> list = null;
            SqlParameter[] param = {
                                       new SqlParameter("@Date1",date1),
                                       new SqlParameter("@Date2",date2),
                                       new SqlParameter("@Status",status)
                                   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SubmitVoucher_GetListExport", param);
            if (r != null)
            {
                list = new List<SubmitVoucherInfo>();
                while (r.Read())
                {
                    list.Add(FillData(r));
                }
                r.Close();
                r.Dispose();
            }

            return list;
        }


        private SubmitVoucherInfo FillData(IDataReader r)
        {
            SubmitVoucherInfo info = new SubmitVoucherInfo();
            if (!r.IsDBNull(r.GetOrdinal("id"))) info.id = r.GetInt32(r.GetOrdinal("id"));
            if (!r.IsDBNull(r.GetOrdinal("UserId"))) info.UserId = r.GetString(r.GetOrdinal("UserId"));
            if (!r.IsDBNull(r.GetOrdinal("Amount"))) info.Amount = r.GetInt32(r.GetOrdinal("Amount"));
            if (!r.IsDBNull(r.GetOrdinal("TransId"))) info.TransId = r.GetString(r.GetOrdinal("TransId"));
            if (!r.IsDBNull(r.GetOrdinal("returnCode"))) info.returnCode = r.GetString(r.GetOrdinal("returnCode"));
            if (!r.IsDBNull(r.GetOrdinal("responseData"))) info.responseData = r.GetString(r.GetOrdinal("responseData"));
            if (!r.IsDBNull(r.GetOrdinal("returnCodeDescription"))) info.returnCodeDescription = r.GetString(r.GetOrdinal("returnCodeDescription"));
            if (!r.IsDBNull(r.GetOrdinal("signature"))) info.signature = r.GetString(r.GetOrdinal("signature"));
            if (!r.IsDBNull(r.GetOrdinal("CreateDate"))) info.CreateDate = r.GetDateTime(r.GetOrdinal("CreateDate"));

            return info;
        }
    }
}
