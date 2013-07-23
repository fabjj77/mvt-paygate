using System;

namespace BankNet.Entity
{
	[Serializable]
	public class GateCardInfo
	{
		public int id { get; set; }
		public string UserId { get; set; }
		public string TransId { get; set; }
		public string ServiceID { get; set; }
		public string SerialsId { get; set; }
		public string CardId { get; set; }
		public string ResultId { get; set; }
		public int Amount { get; set; }
		public string Msg { get; set; }
		public DateTime CreateDate { get; set; }
		
        public GateCardInfo()
        {
            id = 0;
            UserId = "";
            TransId = "";
            ServiceID = "";
            SerialsId = "";
            CardId = "";
            ResultId = "";
            Amount = 0;
            Msg = "";
            CreateDate = DateTime.Now;
        }
	}
}
