using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankNet.Entity
{
    [Serializable]
    public class voucher
    {
        public string vouchervalue { get; set; }
        public int duration { get; set; }
        public string vouchername { get; set; }
        public string durationuomaltcode { get; set; }
        public string voucherdesc { get; set; }

        public voucher()
        {
            vouchervalue = "";
            duration = 0;
            vouchername = "";
            durationuomaltcode = "";
            voucherdesc = "";
        }
    }
}
