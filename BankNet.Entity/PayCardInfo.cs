using System;

namespace BankNet.Entity
{
    [Serializable]
    public class PayCardInfo
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string CardId { get; set; }
        public string ResulId { get; set; }
        public string Msg { get; set; }
        public string ResulFull { get; set; }
        public DateTime CreateDate { get; set; }

        public PayCardInfo()
        {
            ID = 0;
            UserId = "";
            CardId = "";
            ResulId = "";
            Msg = "";
            ResulFull = "";
            CreateDate = DateTime.Now;
        }
    }
}
