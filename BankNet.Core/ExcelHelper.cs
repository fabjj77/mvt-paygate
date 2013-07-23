using System.IO;
using System.Xml;
using System.Xml.XPath;
using OfficeOpenXml;

namespace BankNet.Core
{
    public class ExcelHelper
    {
        public static int GetRowCount(ExcelWorksheet worksheet)
        {
            var count = 0;
            try
            {
                XPathNavigator nav = worksheet.WorksheetXml.CreateNavigator();
                XPathExpression exp = nav.Compile("//*[name()='row']/@r");
                exp.AddSort("../@r", XmlSortOrder.Descending, XmlCaseOrder.None, "", XmlDataType.Number);
                XmlNode node = nav.SelectSingleNode(exp).UnderlyingObject as XmlNode;
                int.TryParse(node.InnerText, out count);
            }
            catch { }
            return count;
        }

        public static int GetColumnCount(ExcelWorksheet worksheet)
        {
            var count = 0;
            try
            {
                XPathNavigator nav = worksheet.WorksheetXml.CreateNavigator();
                XPathExpression exp = nav.Compile("//*[name()='c']/@colNumber");
                exp.AddSort("../@colNumber", XmlSortOrder.Descending, XmlCaseOrder.None, "", XmlDataType.Number);
                XmlNode node = nav.SelectSingleNode(exp).UnderlyingObject as XmlNode;
                int.TryParse(node.InnerText, out count);
            }
            catch { }
            return count;
        }

        public static ExcelPackage OpenFromFile(string absolutePath)
        {
            var file = new FileInfo(absolutePath);
            return new ExcelPackage(file);
        }
    }
}
