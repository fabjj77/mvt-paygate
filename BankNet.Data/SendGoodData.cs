using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BankNet.Entity;
using BankNet.Core;

namespace BankNet.Data
{
	public class SendGoodData
    {
		private static SendGoodData _instance;
        public static SendGoodData instance
        {
            get { return _instance ?? (_instance = new SendGoodData()); }
        }
		
        public int Add(SendGoodInfo info)
        {
			SqlParameter[] param = {
			    new SqlParameter("@FunctionName", info.FunctionName),
			new SqlParameter("@UserId", info.UserId),
			new SqlParameter("@vouchers", info.vouchers),
			new SqlParameter("@Merchant_trans_id", info.Merchant_trans_id),
			new SqlParameter("@Merchant_code", info.Merchant_code),
			new SqlParameter("@Country_code", info.Country_code),
			new SqlParameter("@Good_code", info.Good_code),
			new SqlParameter("@Xml_description", info.Xml_description),
			new SqlParameter("@Net_cost", info.Net_cost),
			new SqlParameter("@Ship_fee", info.Ship_fee),
			new SqlParameter("@Tax", info.Tax),
			new SqlParameter("@Url_success", info.Url_success),
			new SqlParameter("@Url_fail", info.Url_fail),
			new SqlParameter("@Trans_key", info.Trans_key),
			new SqlParameter("@selected_bank", info.selected_bank),
			new SqlParameter("@service_code", info.service_code),
			new SqlParameter("@OutString", info.OutString),
			new SqlParameter("@ResultId", info.ResultId),
			new SqlParameter("@Trans_Id", info.Trans_Id),
			new SqlParameter("@CreateDate", info.CreateDate)			
		   };
            return int.Parse(DataHelper.ExecuteScalar(Config.ConnectString, "usp_SendGood_Add", param).ToString());           
        }
        
        public int Update(SendGoodInfo info)
        {
			SqlParameter[] param = {
									   new SqlParameter("@id", info.id)
			,new SqlParameter("@FunctionName", info.FunctionName),
			new SqlParameter("@UserId", info.UserId),
			new SqlParameter("@vouchers", info.vouchers),
			new SqlParameter("@Merchant_trans_id", info.Merchant_trans_id),
			new SqlParameter("@Merchant_code", info.Merchant_code),
			new SqlParameter("@Country_code", info.Country_code),
			new SqlParameter("@Good_code", info.Good_code),
			new SqlParameter("@Xml_description", info.Xml_description),
			new SqlParameter("@Net_cost", info.Net_cost),
			new SqlParameter("@Ship_fee", info.Ship_fee),
			new SqlParameter("@Tax", info.Tax),
			new SqlParameter("@Url_success", info.Url_success),
			new SqlParameter("@Url_fail", info.Url_fail),
			new SqlParameter("@Trans_key", info.Trans_key),
			new SqlParameter("@selected_bank", info.selected_bank),
			new SqlParameter("@service_code", info.service_code),
			new SqlParameter("@OutString", info.OutString),
			new SqlParameter("@ResultId", info.ResultId),
			new SqlParameter("@Trans_Id", info.Trans_Id),
			new SqlParameter("@CreateDate", info.CreateDate)			
								   };
            return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_SendGood_Update", param);    
        }


        public int Delete(int id)
        {
            SqlParameter[] param = {
									   new SqlParameter("@id", id)
			
								   };
            return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_SendGood_Delete", param);   
        }

        public SendGoodInfo GetInfo(int id)
        {
            SendGoodInfo info = null;
			SqlParameter[] param = {
									   new SqlParameter("@id", id)
			
								   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SendGood_GetById", param);
			if (r != null)
			{
				info = new SendGoodInfo();
				while (r.Read())
				{
					info = FillData(r);
				}
				r.Close();
                r.Dispose();
			}
			return info;
        }
		
		public List<SendGoodInfo> GetAll()
        {
            List<SendGoodInfo> list = null;
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SendGood_GetAll");
            if (r != null)
            {
                list = new List<SendGoodInfo>();
                while (r.Read())
                {
					list.Add(FillData(r));
                }
                r.Close();
                r.Dispose();
            }

            return list;
        }

        public List<SendGoodInfo> GetList(int pageIndex, int pageSize, out int total)
        {
            List<SendGoodInfo> list = null;
            var t = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@totalrow",DbType.Int32){Direction = ParameterDirection.Output}
                                   };
            SqlCommand comx;
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_SendGood_GetList", param, out comx);
            if (r != null)
            {
                list = new List<SendGoodInfo>();
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
		
		private SendGoodInfo FillData(IDataReader r)
        {
            SendGoodInfo info = new SendGoodInfo();
            if (!r.IsDBNull(r.GetOrdinal("id"))) info.id = r.GetInt32(r.GetOrdinal("id"));
			if (!r.IsDBNull(r.GetOrdinal("FunctionName"))) info.FunctionName = r.GetString(r.GetOrdinal("FunctionName"));
			if (!r.IsDBNull(r.GetOrdinal("UserId"))) info.UserId = r.GetString(r.GetOrdinal("UserId"));
			if (!r.IsDBNull(r.GetOrdinal("vouchers"))) info.vouchers = r.GetString(r.GetOrdinal("vouchers"));
			if (!r.IsDBNull(r.GetOrdinal("Merchant_trans_id"))) info.Merchant_trans_id = r.GetString(r.GetOrdinal("Merchant_trans_id"));
			if (!r.IsDBNull(r.GetOrdinal("Merchant_code"))) info.Merchant_code = r.GetString(r.GetOrdinal("Merchant_code"));
			if (!r.IsDBNull(r.GetOrdinal("Country_code"))) info.Country_code = r.GetString(r.GetOrdinal("Country_code"));
			if (!r.IsDBNull(r.GetOrdinal("Good_code"))) info.Good_code = r.GetString(r.GetOrdinal("Good_code"));
			if (!r.IsDBNull(r.GetOrdinal("Xml_description"))) info.Xml_description = r.GetString(r.GetOrdinal("Xml_description"));
			if (!r.IsDBNull(r.GetOrdinal("Net_cost"))) info.Net_cost = r.GetString(r.GetOrdinal("Net_cost"));
			if (!r.IsDBNull(r.GetOrdinal("Ship_fee"))) info.Ship_fee = r.GetString(r.GetOrdinal("Ship_fee"));
			if (!r.IsDBNull(r.GetOrdinal("Tax"))) info.Tax = r.GetString(r.GetOrdinal("Tax"));
			if (!r.IsDBNull(r.GetOrdinal("Url_success"))) info.Url_success = r.GetString(r.GetOrdinal("Url_success"));
			if (!r.IsDBNull(r.GetOrdinal("Url_fail"))) info.Url_fail = r.GetString(r.GetOrdinal("Url_fail"));
			if (!r.IsDBNull(r.GetOrdinal("Trans_key"))) info.Trans_key = r.GetString(r.GetOrdinal("Trans_key"));
			if (!r.IsDBNull(r.GetOrdinal("selected_bank"))) info.selected_bank = r.GetString(r.GetOrdinal("selected_bank"));
			if (!r.IsDBNull(r.GetOrdinal("service_code"))) info.service_code = r.GetString(r.GetOrdinal("service_code"));
			if (!r.IsDBNull(r.GetOrdinal("OutString"))) info.OutString = r.GetString(r.GetOrdinal("OutString"));
			if (!r.IsDBNull(r.GetOrdinal("ResultId"))) info.ResultId = r.GetString(r.GetOrdinal("ResultId"));
			if (!r.IsDBNull(r.GetOrdinal("Trans_Id"))) info.Trans_Id = r.GetString(r.GetOrdinal("Trans_Id"));
			if (!r.IsDBNull(r.GetOrdinal("CreateDate"))) info.CreateDate = r.GetDateTime(r.GetOrdinal("CreateDate"));
			
            return info;
        }
    }
}
