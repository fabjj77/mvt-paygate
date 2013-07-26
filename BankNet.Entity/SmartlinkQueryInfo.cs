using System;

namespace BankNet.Entity
{
	[Serializable]
	public class SmartlinkQueryInfo
	{
		public int id { get; set; }
		public string vpc_Version { get; set; }
		public string vpc_Command { get; set; }
		public string vpc_AccessCode { get; set; }
		public string vpc_Merchant { get; set; }
		public string vpc_MerchTxnRef { get; set; }
		public string vpc_SecureHash { get; set; }
		public object vpc_DRExists { get; set; }
		public object vpc_FoundMultipleDRs { get; set; }
		public string vpc_TxnResponseCode { get; set; }
		public string vpc_Message { get; set; }
		public DateTime CreateDate { get; set; }
		
	}
}
