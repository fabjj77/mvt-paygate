using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using BankNet.Entity;
using BankNet.Core;
using BankNet.Data;

namespace Web
{
    /// <summary>
    /// Summary description for ServicesPayCard
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Services : System.Web.Services.WebService
    {
        public AuthenticationHeader AuthHeader;

        private bool CheckAuth()
        {
            if(AuthHeader==null) return false;
            string user = Config.UserServices;
            string pass = Config.PassServices;
            if (AuthHeader.Username == user && AuthHeader.Password == pass)
                return true;
            return false;
        }

        [WebMethod(Description = "Lấy list thông tin AnVienCard (PayCard) - status=0: ok| =1: no ok| 2: all"), SoapHeader("AuthHeader")]
        public List<PayCardInfo> GetListAnVienCard(DateTime date1, DateTime date2, int status)
        {
            if (!CheckAuth()) return null;
            try
            {
                return PayCardData.instance.GetListExport(date1, date2, status);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        [WebMethod(Description = "Lấy list thông tin thẻ cào điện thoại - status=0: ok | =1: no ok| 2: all"), SoapHeader("AuthHeader")]
        public List<GateCardInfo> GetListMobileCard(DateTime date1, DateTime date2, int status)
        {
            if (!CheckAuth()) return null;
            try
            {
                return GateCardData.instance.GetListExport(date1, date2, status);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        [WebMethod(Description = "Lấy list thông tin submit voucher - status=0: ok | =1: no ok| 2: all"), SoapHeader("AuthHeader")]
        public List<SubmitVoucherInfo> GetListSubmitVoucher(DateTime date1, DateTime date2, int status)
        {
            if (!CheckAuth()) return null;
            try
            {
                return SubmitVoucherData.instance.GetListExport(date1, date2, status);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public class AuthenticationHeader : SoapHeader
        {
            public string Username;
            public string Password;
        }
    }
}
