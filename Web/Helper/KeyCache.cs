using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helper
{
    public class KeyCache
    {
        public static string KeyUserBanknet
        {
            get { return "Banknet_{0}"; }
        }

        public static string KeyUserSmartlink
        {
            get { return "Smartlink_{0}"; }
        }

        public static string KeySesionAVCard
        {
            get { return "AnVienCard"; }
        }

        public static string KeySesionMobileCard
        {
            get { return "MobileCard"; }
        }

        public static string KeySesionBanknet
        {
            get { return "Banknet"; }
        }

        public static string KeySesionSmartlink
        {
            get { return "Banknet"; }
        }
    }
}