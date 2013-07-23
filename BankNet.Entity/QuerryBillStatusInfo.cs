using System;

namespace BankNet.Entity
{
	[Serializable]
	public class QuerryBillStatusInfo
	{
		public int id { get; set; }
		public string Merchant_trans_id { get; set; }
		public string Trans_id { get; set; }
		public string Merchant_code { get; set; }
		public string Trans_key { get; set; }
		public string ResultId { get; set; }
		public string OutString { get; set; }
		public DateTime CreateDate { get; set; }
    }
}
