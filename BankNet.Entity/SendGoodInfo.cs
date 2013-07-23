using System;

namespace BankNet.Entity
{
	[Serializable]
	public class SendGoodInfo
	{
		public int id { get; set; }
		public string FunctionName { get; set; }
		public string UserId { get; set; }
		public string vouchers { get; set; }
		public string Merchant_trans_id { get; set; }
		public string Merchant_code { get; set; }
		public string Country_code { get; set; }
		public string Good_code { get; set; }
		public string Xml_description { get; set; }
		public string Net_cost { get; set; }
		public string Ship_fee { get; set; }
		public string Tax { get; set; }
		public string Url_success { get; set; }
		public string Url_fail { get; set; }
		public string Trans_key { get; set; }
		public string selected_bank { get; set; }
		public string service_code { get; set; }
		public string OutString { get; set; }
		public string ResultId { get; set; }
		public string Trans_Id { get; set; }
		public DateTime CreateDate { get; set; }
		
	}
}
