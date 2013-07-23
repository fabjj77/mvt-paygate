using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestServices.ServiceBanknet;

namespace TestServices
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceBanknet.PaymentGatewayPortTypeClient client = new PaymentGatewayPortTypeClient();
        }
    }
}