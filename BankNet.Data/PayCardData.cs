using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BankNet.Entity;
using BankNet.Core;

namespace BankNet.Data
{
    public class PayCardData
    {
        private static PayCardData _instance;
        public static PayCardData instance
        {
            get { return _instance ?? (_instance = new PayCardData()); }
        }

        public int Add(PayCardInfo info)
        {
            SqlParameter[] param = {
			                        new SqlParameter("@UserId", info.UserId),
			                        new SqlParameter("@CardId", info.CardId),
			                        new SqlParameter("@ResulId", info.ResulId),
			                        new SqlParameter("@Msg", info.Msg),
			                        new SqlParameter("@ResulFull", info.ResulFull),
			                        new SqlParameter("@CreateDate", info.CreateDate)			
		                           };
            return int.Parse(DataHelper.ExecuteScalar(Config.ConnectString, "usp_PayCard_Add", param).ToString());
        }

        //public int Update(PayCardInfo info)
        //{
        //    SqlParameter[] param = {
        //                                new SqlParameter("@ID", info.ID),
        //                                new SqlParameter("@UserId", info.UserId),
        //                                new SqlParameter("@CardId", info.CardId),
        //                                new SqlParameter("@ResulId", info.ResulId),
        //                                new SqlParameter("@Msg", info.Msg),
        //                                new SqlParameter("@ResulFull", info.ResulFull),
        //                                new SqlParameter("@CreateDate", info.CreateDate)			
        //                           };
        //    return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_PayCard_Update", param);
        //}


        //public int Delete(int id)
        //{
        //    SqlParameter[] param = {
        //                               new SqlParameter("@ID", id)
        //                           };
        //    return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_PayCard_Delete", param);
        //}

        public PayCardInfo GetInfo(int id)
        {
            PayCardInfo info = null;
            SqlParameter[] param = {
									   new SqlParameter("@ID", id)
								   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_PayCard_GetById", param);
            if (r != null)
            {
                info = new PayCardInfo();
                while (r.Read())
                {
                    info = FillData(r);
                }
                r.Close();
                r.Dispose();
            }
            return info;
        }

        //public List<PayCardInfo> GetAll()
        //{
        //    List<PayCardInfo> list = null;
        //    var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_PayCard_GetAll");
        //    if (r != null)
        //    {
        //        list = new List<PayCardInfo>();
        //        while (r.Read())
        //        {
        //            list.Add(FillData(r));
        //        }
        //        r.Close();
        //        r.Dispose();
        //    }

        //    return list;
        //}

        //public List<PayCardInfo> GetList(int pageIndex, int pageSize, out int total)
        //{
        //    List<PayCardInfo> list = null;
        //    var t = 0;
        //    SqlParameter[] param = {
        //                               new SqlParameter("@pageIndex",pageIndex),
        //                               new SqlParameter("@pageSize",pageSize),
        //                               new SqlParameter("@totalrow",DbType.Int32){Direction = ParameterDirection.Output}
        //                           };
        //    SqlCommand comx;
        //    var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_PayCard_GetList", param, out comx);
        //    if (r != null)
        //    {
        //        list = new List<PayCardInfo>();
        //        while (r.Read())
        //        {
        //            list.Add(FillData(r));
        //        }
        //        r.Close();
        //        r.Dispose();
        //        t = int.Parse(comx.Parameters[2].Value.ToString());
        //    }

        //    total = t;
        //    return list;
        //}

        public List<PayCardInfo> GetListExport(DateTime date1, DateTime date2,int status)
        {
            List<PayCardInfo> list = null;
            SqlParameter[] param = {
                                       new SqlParameter("@Date1",date1),
                                       new SqlParameter("@Date2",date2),
                                       new SqlParameter("@Status",status)
                                   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_PayCard_GetListExport", param);
            if (r != null)
            {
                list = new List<PayCardInfo>();
                while (r.Read())
                {
                    list.Add(FillData(r));
                }
                r.Close();
                r.Dispose();
            }

            return list;
        }

        private PayCardInfo FillData(IDataReader r)
        {
            PayCardInfo info = new PayCardInfo();
            if (!r.IsDBNull(r.GetOrdinal("ID"))) info.ID = r.GetInt32(r.GetOrdinal("ID"));
            if (!r.IsDBNull(r.GetOrdinal("UserId"))) info.UserId = r.GetString(r.GetOrdinal("UserId"));
            if (!r.IsDBNull(r.GetOrdinal("CardId"))) info.CardId = r.GetString(r.GetOrdinal("CardId"));
            if (!r.IsDBNull(r.GetOrdinal("ResulId"))) info.ResulId = r.GetString(r.GetOrdinal("ResulId"));
            if (!r.IsDBNull(r.GetOrdinal("Msg"))) info.Msg = r.GetString(r.GetOrdinal("Msg"));
            if (!r.IsDBNull(r.GetOrdinal("ResulFull"))) info.ResulFull = r.GetString(r.GetOrdinal("ResulFull"));
            if (!r.IsDBNull(r.GetOrdinal("CreateDate"))) info.CreateDate = r.GetDateTime(r.GetOrdinal("CreateDate"));

            return info;
        }
    }
}
