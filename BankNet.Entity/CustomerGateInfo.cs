using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankNet.Entity
{
    [Serializable]
    public class CustomerGateInfo
    {
        public string subnum { get; set; }
        public string contactname { get; set; }
        public string contactaddress { get; set; }
        public string contactphone { get; set; }
        public string contactemail { get; set; }
        public string taxnumber { get; set; }
        public string idnumber { get; set; }
        public string servicename { get; set; }
        public string subtypename { get; set; }
        public string billschemaname { get; set; }
        public string substatusname { get; set; }
        public string institemserialnum1 { get; set; }
        public string institemserialnum2 { get; set; }
        public string expirationdate { get; set; }
        public List<voucher> vouchers { get; set; }

        public CustomerGateInfo()
        {
            subnum = "";
            contactname = "";
            contactaddress = "";
            contactphone = "";
            contactemail = "";
            taxnumber = "";
            idnumber = "";
            servicename = "";
            subtypename = "";
            billschemaname = "";
            substatusname = "";
            institemserialnum1 = "";
            institemserialnum2 = "";
            expirationdate = "";
            vouchers = new List<voucher>();
        }
    }
}
