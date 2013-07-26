using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using BankNet.Entity;

namespace Web.Helper
{
    public class XMLReader
    {
        public static string ReadResultVocher(string sXML)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create(new StringReader(sXML));
            xDoc.Load(reader);

            XmlNodeList subnum = xDoc.GetElementsByTagName("expirationdate");
            string sRead = subnum[0].InnerText;
            return sRead;
        }

        public static CustomerGateInfo ReadInfo(string sResult)
        {
            CustomerGateInfo info = new CustomerGateInfo();
            XmlDocument xDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create(new StringReader(sResult));
            xDoc.Load(reader);

            XmlNodeList subnum = xDoc.GetElementsByTagName("subnum");
            string sRead = subnum[0].InnerText;
            info.subnum = sRead;

            XmlNodeList contactname = xDoc.GetElementsByTagName("contactname");
            sRead = contactname[0].InnerText;
            info.contactname = sRead;

            XmlNodeList contactaddress = xDoc.GetElementsByTagName("contactaddress");
            sRead = contactaddress[0].InnerText;
            info.contactaddress = sRead;

            XmlNodeList contactphone = xDoc.GetElementsByTagName("contactphone");
            sRead = contactphone[0].InnerText;
            info.contactphone = sRead;

            XmlNodeList contactemail = xDoc.GetElementsByTagName("contactemail");
            sRead = contactemail[0].InnerText;
            info.contactemail = sRead;

            XmlNodeList taxnumber = xDoc.GetElementsByTagName("taxnumber");
            sRead = taxnumber[0].InnerText;
            info.taxnumber = sRead;

            XmlNodeList idnumber = xDoc.GetElementsByTagName("idnumber");
            sRead = idnumber[0].InnerText;
            info.idnumber = sRead;

            XmlNodeList servicename = xDoc.GetElementsByTagName("servicename");
            sRead = servicename[0].InnerText;
            info.servicename = sRead;

            XmlNodeList subtypename = xDoc.GetElementsByTagName("subtypename");
            sRead = subtypename[0].InnerText;
            info.subtypename = sRead;

            XmlNodeList billschemaname = xDoc.GetElementsByTagName("billschemaname");
            sRead = billschemaname[0].InnerText;
            info.billschemaname = sRead;

            XmlNodeList substatusname = xDoc.GetElementsByTagName("substatusname");
            sRead = substatusname[0].InnerText;
            info.substatusname = sRead;

            XmlNodeList institemserialnum1 = xDoc.GetElementsByTagName("institemserialnum1");
            sRead = institemserialnum1[0].InnerText;
            info.institemserialnum1 = sRead;

            XmlNodeList institemserialnum2 = xDoc.GetElementsByTagName("institemserialnum2");
            sRead = institemserialnum2[0].InnerText;
            info.institemserialnum2 = sRead;

            XmlNodeList expirationdate = xDoc.GetElementsByTagName("expirationdate");
            sRead = expirationdate[0].InnerText;
            info.expirationdate = sRead;

            //vouchers
            List<voucher> oList = new List<voucher>();
            XmlNodeList vouchers = xDoc.GetElementsByTagName("voucher");
            for (int i = 0; i < vouchers.Count; i++)
            {
                voucher item = new voucher();

                XmlNodeList voucher = vouchers[i].ChildNodes;
                item.vouchervalue = voucher[0].InnerText;//vouchervalue
                item.duration = int.Parse(voucher[1].InnerText);//duration
                item.vouchername = voucher[2].InnerText;//vouchername
                item.durationuomaltcode = voucher[3].InnerText;//durationuomaltcode
                item.voucherdesc = voucher[4].InnerText;//durationuomaltcode
                oList.Add(item);
            }
            info.vouchers = oList;
            return info;
        }
    }
}