using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

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
    }
}