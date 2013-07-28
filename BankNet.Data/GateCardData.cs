using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BankNet.Entity;
using BankNet.Core;

namespace BankNet.Data
{
    public class GateCardData
    {
        private static GateCardData _instance;
        public static GateCardData instance
        {
            get { return _instance ?? (_instance = new GateCardData()); }
        }

        public int Add(GateCardInfo info)
        {
            SqlParameter[] param = {
			    new SqlParameter("@UserId", info.UserId),
			new SqlParameter("@TransId", info.TransId),
			new SqlParameter("@ServiceID", info.ServiceID),
			new SqlParameter("@SerialsId", info.SerialsId),
			new SqlParameter("@CardId", info.CardId),
			new SqlParameter("@ResultId", info.ResultId),
			new SqlParameter("@Amount", info.Amount),
			new SqlParameter("@Msg", info.Msg),
			new SqlParameter("@CreateDate", info.CreateDate)			
		   };
            return int.Parse(DataHelper.ExecuteScalar(Config.ConnectString, "usp_GateCard_Add", param).ToString());
        }

        //public int Update(GateCardInfo info)
        //{
        //    SqlParameter[] param = {
        //                               new SqlParameter("@id", info.id)
        //    ,new SqlParameter("@UserId", info.UserId),
        //    new SqlParameter("@TransId", info.TransId),
        //    new SqlParameter("@ServiceID", info.ServiceID),
        //    new SqlParameter("@SerialsId", info.SerialsId),
        //    new SqlParameter("@CardId", info.CardId),
        //    new SqlParameter("@ResultId", info.ResultId),
        //    new SqlParameter("@Amount", info.Amount),
        //    new SqlParameter("@Msg", info.Msg),
        //    new SqlParameter("@CreateDate", info.CreateDate)			
        //                           };
        //    return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_GateCard_Update", param);
        //}


        //public int Delete(int id)
        //{
        //    SqlParameter[] param = {
        //                               new SqlParameter("@id", id)
			
        //                           };
        //    return DataHelper.ExecuteNonQuery(Config.ConnectString, "usp_GateCard_Delete", param);
        //}

        public GateCardInfo GetInfo(int id)
        {
            GateCardInfo info = null;
            SqlParameter[] param = {
									   new SqlParameter("@id", id)
			
								   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_GateCard_GetById", param);
            if (r != null)
            {
                info = new GateCardInfo();
                while (r.Read())
                {
                    info = FillData(r);
                }
                r.Close();
                r.Dispose();
            }
            return info;
        }

        //public List<GateCardInfo> GetAll()
        //{
        //    List<GateCardInfo> list = null;
        //    var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_GateCard_GetAll");
        //    if (r != null)
        //    {
        //        list = new List<GateCardInfo>();
        //        while (r.Read())
        //        {
        //            list.Add(FillData(r));
        //        }
        //        r.Close();
        //        r.Dispose();
        //    }

        //    return list;
        //}

        //public List<GateCardInfo> GetList(int pageIndex, int pageSize, out int total)
        //{
        //    List<GateCardInfo> list = null;
        //    var t = 0;
        //    SqlParameter[] param = {
        //                               new SqlParameter("@pageIndex",pageIndex),
        //                               new SqlParameter("@pageSize",pageSize),
        //                               new SqlParameter("@totalrow",DbType.Int32){Direction = ParameterDirection.Output}
        //                           };
        //    SqlCommand comx;
        //    var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_GateCard_GetList", param, out comx);
        //    if (r != null)
        //    {
        //        list = new List<GateCardInfo>();
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

        public List<GateCardInfo> GetListExport(DateTime date1, DateTime date2,string sType,  int status)
        {
            List<GateCardInfo> list = null;
            SqlParameter[] param = {
                                       new SqlParameter("@Date1",date1),
                                       new SqlParameter("@Date2",date2),
                                       new SqlParameter("@ServiceID",sType), 
                                       new SqlParameter("@Status",status)
                                   };
            var r = DataHelper.ExecuteReader(Config.ConnectString, "usp_GateCard_GetListExport", param);
            if (r != null)
            {
                list = new List<GateCardInfo>();
                while (r.Read())
                {
                    list.Add(FillData(r));
                }
                r.Close();
                r.Dispose();
            }

            return list;
        }

        private GateCardInfo FillData(IDataReader r)
        {
            GateCardInfo info = new GateCardInfo();
            if (!r.IsDBNull(r.GetOrdinal("id"))) info.id = r.GetInt32(r.GetOrdinal("id"));
            if (!r.IsDBNull(r.GetOrdinal("UserId"))) info.UserId = r.GetString(r.GetOrdinal("UserId"));
            if (!r.IsDBNull(r.GetOrdinal("TransId"))) info.TransId = r.GetString(r.GetOrdinal("TransId"));
            if (!r.IsDBNull(r.GetOrdinal("ServiceID"))) info.ServiceID = r.GetString(r.GetOrdinal("ServiceID"));
            if (!r.IsDBNull(r.GetOrdinal("SerialsId"))) info.SerialsId = r.GetString(r.GetOrdinal("SerialsId"));
            if (!r.IsDBNull(r.GetOrdinal("CardId"))) info.CardId = r.GetString(r.GetOrdinal("CardId"));
            if (!r.IsDBNull(r.GetOrdinal("ResultId"))) info.ResultId = r.GetString(r.GetOrdinal("ResultId"));
            if (!r.IsDBNull(r.GetOrdinal("Amount"))) info.Amount = r.GetInt32(r.GetOrdinal("Amount"));
            if (!r.IsDBNull(r.GetOrdinal("Msg"))) info.Msg = r.GetString(r.GetOrdinal("Msg"));
            if (!r.IsDBNull(r.GetOrdinal("CreateDate"))) info.CreateDate = r.GetDateTime(r.GetOrdinal("CreateDate"));

            return info;
        }
    }
}
