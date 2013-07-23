using System;

namespace BankNet.Entity
{
    [Serializable]
    public class SubmitVoucherInfo
    {
        public int id { get; set; }
        public string GatePayId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string TransId { get; set; }
        public string returnCode { get; set; }
        public string responseData { get; set; }
        public string returnCodeDescription { get; set; }
        public string signature { get; set; }
        public DateTime CreateDate { get; set; }

        public SubmitVoucherInfo()
        {
            id = 0;
            UserId = "";
            GatePayId = "";
            Amount = 0;
            TransId = "";
            returnCode = "";
            responseData = "";
            returnCodeDescription = "";
            signature = "";
            CreateDate = DateTime.Now;
        }
    }
}
