using System;

namespace BankNet.Entity
{
	[Serializable]
	public class SmartlinkRedirectUrlInfo
	{
		public int id { get; set; }
		public string vpc_Version { get; set; }
		public string vpc_Locale { get; set; }
		public string vpc_Command { get; set; }
		public string vpc_Merchant { get; set; }
		public string vpc_AccessCode { get; set; }
		public string vpc_MerchTxnRef { get; set; }
		public string vpc_Amount { get; set; }
		public string vpc_CurrencyCode { get; set; }
		public string vpc_OrderInfo { get; set; }
		public string vpc_ReturnURL { get; set; }
		public string vpc_BackURL { get; set; }
		public string vpc_TicketNo { get; set; }
		public string vpc_SecureHash { get; set; }
		public DateTime CreateDate { get; set; }
		
	}
}
