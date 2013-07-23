using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BankNet.Entity;
using BankNet.Core;

namespace BankNet.Data
{
	public class QuerryBillStatusData
    {
		private static QuerryBillStatusData _instance;
        public static QuerryBillStatusData instance
        {
            get { return _instance ?? (_instance = new QuerryBillStatusData()); }
        }
		
        public int Add(QuerryBillStatusInfo info)
        {
			SqlParameter[] param = {
			    new SqlParameter("@Merchant_trans_id", info.Merchant_trans_id),
			new SqlParameter("@Trans_id", info.Trans_id),
			new SqlParameter("@Merchant_code", info.Merchant_code),
			new SqlParameter("@Trans_key", info.Trans_key),
			new SqlParameter("@ResultId", info.ResultId),
			new SqlParameter("@OutString", info.OutString),
			new SqlParameter("@CreateDate", info.CreateDate)			
		   };
            return int.Parse(DataHelper.ExecuteScalar(Config.ConnectString, "usp_QuerryBillStatus_Add", param).ToString());           
        }
        
        //public int Update(QuerryBillStatusInfo info)
        //{
        //    SqlParameter[] param = {
        //                               new SqlParameter("@id", info.id)
        //    ,new SqlParameter("@Merchant_trans_id", info.Merchant_trans_id),
        //    new SqlParameter("@Trans_id", info.Trans_id),
        //    new SqlParameter("@Merchant_code", info.Merchant_code),
        //    new SqlParameter("@Trans_key", info.Trans_key),
        //    new SqlParameter("@ResultId", info.ResultId),
        //    new SqlParameter("@OutString", info.OutString),
        //    new SqlParameter("@CreateDate", info.CreateDate)			
        //                           };
        //    return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_QuerryBillStatus_Update", param);    
        //}


        //public int Delete(int id)
        //{
        //    SqlParameter[] param = {
        //                               new SqlParameter("@id", id)
			
        //                           };
        //    return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_QuerryBillStatus_Delete", param);   
        //}

        public QuerryBillStatusInfo GetInfo(int id)
        {
            QuerryBillStatusInfo info = null;
			SqlParameter[] param = {
									   new SqlParameter("@id", id)
								   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_QuerryBillStatus_GetById", param);
			if (r != null)
			{
				info = new QuerryBillStatusInfo();
				while (r.Read())
				{
					info = FillData(r);
				}
				r.Close();
                r.Dispose();
			}
			return info;
        }
		
		public List<QuerryBillStatusInfo> GetAll()
        {
            List<QuerryBillStatusInfo> list = null;
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_QuerryBillStatus_GetAll");
            if (r != null)
            {
                list = new List<QuerryBillStatusInfo>();
                while (r.Read())
                {
					list.Add(FillData(r));
                }
                r.Close();
                r.Dispose();
            }

            return list;
        }

        public List<QuerryBillStatusInfo> GetList(int pageIndex, int pageSize, out int total)
        {
            List<QuerryBillStatusInfo> list = null;
            var t = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@totalrow",DbType.Int32){Direction = ParameterDirection.Output}
                                   };
            SqlCommand comx;
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_QuerryBillStatus_GetList", param, out comx);
            if (r != null)
            {
                list = new List<QuerryBillStatusInfo>();
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
		
		private QuerryBillStatusInfo FillData(IDataReader r)
        {
            QuerryBillStatusInfo info = new QuerryBillStatusInfo();
            if (!r.IsDBNull(r.GetOrdinal("id"))) info.id = r.GetInt32(r.GetOrdinal("id"));
			if (!r.IsDBNull(r.GetOrdinal("Merchant_trans_id"))) info.Merchant_trans_id = r.GetString(r.GetOrdinal("Merchant_trans_id"));
			if (!r.IsDBNull(r.GetOrdinal("Trans_id"))) info.Trans_id = r.GetString(r.GetOrdinal("Trans_id"));
			if (!r.IsDBNull(r.GetOrdinal("Merchant_code"))) info.Merchant_code = r.GetString(r.GetOrdinal("Merchant_code"));
			if (!r.IsDBNull(r.GetOrdinal("Trans_key"))) info.Trans_key = r.GetString(r.GetOrdinal("Trans_key"));
			if (!r.IsDBNull(r.GetOrdinal("ResultId"))) info.ResultId = r.GetString(r.GetOrdinal("ResultId"));
			if (!r.IsDBNull(r.GetOrdinal("OutString"))) info.OutString = r.GetString(r.GetOrdinal("OutString"));
			if (!r.IsDBNull(r.GetOrdinal("CreateDate"))) info.CreateDate = r.GetDateTime(r.GetOrdinal("CreateDate"));
			
            return info;
        }
    }
}
