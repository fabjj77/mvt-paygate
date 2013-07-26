using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;

namespace BankNet.Core
{
    public class Utility
    {
        private static string GetIp(HttpContext context)
        {
            string s = "";
            string ipList = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                s = ipList.Split(',')[0];
            }
            else
            {
                s = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            return s;
        }

        public static bool isOnlyNumber(string s)
        {
            Regex isNumber = new Regex(@"^\d+$");
            return isNumber.IsMatch(s);
        }

        public static bool isText(string s)
        {
            Regex isNumber = new Regex(@"^[0-9A-Za-z]+$");
            return isNumber.IsMatch(s);
        }

        public static bool IsInt(string Value)
        {
            try
            {
                Convert.ToInt32(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static string StripWordFomat(string text)
        {
            Regex rScript = new Regex(@"<!--(.|\n)*?-->", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (rScript.IsMatch(text))
                return rScript.Replace(text, String.Empty);
            return text;
        }
        public static string EncryptMd5(string YourString)
        {
            byte[] MyByte = new byte[16];
            MyByte = MD5.Create().ComputeHash(Encoding.Default.GetBytes(YourString));
            string MyEncryptString = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                MyEncryptString += MyByte[i].ToString("x2");
            }
            return MyEncryptString;

        }
        public static string EncryptMd5OldViOlympic(string YourString)
        {
            byte[] index = new byte[YourString.Length];
            for (int i = 0; i < YourString.Length; i++)
            {
                index[i] = System.Convert.ToByte(YourString[i]);

            }
            byte[] MyByte = new byte[16];
            MyByte = System.Security.Cryptography.MD5.Create().ComputeHash(index);
            string MyEncryptString = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                MyEncryptString += System.Convert.ToString(MyByte[i], 16);
            }
            return MyEncryptString;
        }

        public static bool EmailValidate(string email)
        {
            Regex emailReg = new Regex(@"([a-zA-Z_0-9.-]+\@[a-zA-Z_0-9.-]+\.\w+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return emailReg.IsMatch(email);
        }

        public static bool UserValidate(string username)
        {
            Regex uNameReg = new Regex("[^a-zA-Z0-9_]");
            return uNameReg.IsMatch(username);
        }

        public static void DeleteFile(string FileName)
        {
            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
            }
            catch
            {
            }
        }

        public static string UploadFile(HtmlInputFile clientFile, string FolderToUp, bool AutoGenerateName, bool Overwrite, string LimitExtension)
        {
            if ((!(clientFile == null)) && (!(clientFile.PostedFile == null)) && (!(clientFile.PostedFile.FileName == null)) && clientFile.PostedFile.FileName != "")
            {
                try
                {
                    HttpPostedFile postedFile = clientFile.PostedFile;
                    //string ContentTypeFile = postedFile.ContentType;
                    string sFolder = FolderToUp;
                    if (!(postedFile == null && postedFile.ContentLength != 0))
                    {
                        //Check exist folder
                        try
                        {
                            if (Directory.Exists(sFolder) == false)
                            {
                                Directory.CreateDirectory(sFolder);
                            }
                        }
                        catch
                        {
                            return "";
                        }

                        //Check validate file extension
                        string FileExtension = Path.GetExtension(postedFile.FileName.ToLower());
                        LimitExtension = LimitExtension.ToLower();
                        if (LimitExtension.IndexOf("*.*") == -1 && LimitExtension.IndexOf(FileExtension) == -1) return "";

                        //Generate file name and check overwrite
                        string FileName = Path.GetFileName(postedFile.FileName);
                        string sFileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                        string vFileName = FileName;

                        if (AutoGenerateName)
                        {
                            FileName = FileName.Substring(FileName.LastIndexOf("."));
                            vFileName = FileName.Insert(FileName.LastIndexOf("."), "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss"));

                        }
                        else
                        {
                            if ((!Overwrite) && (File.Exists(FolderToUp + "/" + FileName)))
                            {
                                return "";
                            }
                        }
                        vFileName = vFileName.Replace(" ", string.Empty);
                        string tFileName = FolderToUp + "/" + vFileName;
                        if (File.Exists(tFileName))
                        {
                            File.SetAttributes(tFileName, FileAttributes.Normal);
                            File.Delete(tFileName);
                        }

                        postedFile.SaveAs(tFileName);
                        return FolderToUp + "/" + vFileName;
                    }
                    else return "";
                }
                catch
                {
                    return "";
                }
            }
            else return "";
        }

        public static string UperFirstWord(string pin)
        {
            pin = pin.Trim();//Loại bỏ khoảng trắng hai đầu
            pin = pin.ToLower();//Viết thường toàn bộ
            StringBuilder result = new StringBuilder();
            if (pin.Length > 0)
            {
                result.Append(pin.Substring(0, 1).ToUpper());
                if (pin.Length > 1)
                    result.Append(pin.Substring(1, pin.Length - 1));
            }
            return result.ToString();
        }

        public static string UperEachWord(string pin)
        {
            pin = pin.Trim();//Loại bỏ khoảng trắng hai đầu
            pin = Format(pin);//Loại bỏ 2 khoảng trắng liên tiếp

            StringBuilder result = new StringBuilder();
            string[] words = pin.Split(new char[] { ' ' });
            foreach (string s in words)
            {
                if (result.Length > 0)
                    result.Append(" ");
                result.Append(UperFirstWord(s));
            }
            return result.ToString();
        }

        public static string Format(string pin)
        {
            string result = pin.Trim();//xóa bỏ khoảng trắng 2 đầu
            while (result.IndexOf("  ") > -1)//các từ cách nhau một dấu cách duy nhất
            {
                result = result.Replace("  ", " ");
            }
            return result;
        }
        public static string SQLInjection(string str)
        {
            str = str.ToLower();
            str = str.Replace("+", "");
            str = str.Replace("'", "''");
            str = str.Replace("union", "");
            str = str.Replace("select", "");
            str = str.Replace("insert", "");
            str = str.Replace("update", "");
            str = str.Replace("delete", "");
            str = str.Replace("drop", "");
            str = str.Replace("information_schemal", "");
            str = str.Replace("*", "");
            str = str.Replace("%", "");
            return str;
        }

        public static bool CheckLinkImage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentType.StartsWith("image"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }


        public static string StringForNull(object x)
        {
            if (x is DBNull)
            {
                return "";
            }
            if (x == null)
            {
                return "";
            }
            return x.ToString().Trim();
        }

        public static Boolean isDate(string sDate)
        {
            bool b = true;
            try
            {
                DateTime.Parse(sDate);
            }
            catch (Exception)
            {
                b = false;
            }
            return b;
        }

        public static int IntegerForNull(object x)
        {
            if ((x == null)) return 0;
            if (x is DBNull) return 0;
            if (!IsNumeric(x)) return 0;
            if (!checkFormatInt(x.ToString())) return 0;
            return Convert.ToInt32(x);
        }

        public static double DoubleForNull(object x)
        {
            if ((x == null))
            {
                return 0;
            }
            else if (ReferenceEquals(x, DBNull.Value))
            {
                return 0;
            }
            else if (!IsNumeric(x))
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(x);
            }
        }

        public static Boolean IsNumeric(Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { }
            return false;
        }

        public static string GetCharFormatNum()
        {
            string s = string.Format("{0:N0}", 1000);//return 1.000 or 1,000
            return s.Substring(1, 1);
        }

        public static double StringToDouble(string s)
        {
            s = s.Replace(GetCharFormatNum(), "");
            if ((s == ""))
            {
                return 0;
            }

            else if (!IsNumeric(s))
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(s);
            }
        }

        public static int StringToInt(string s)
        {
            s = s.Replace(GetCharFormatNum(), "");
            if ((s == ""))
            {
                return 0;
            }

            else if (!IsNumeric(s))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(s);
            }

        }

        public static Boolean TryDateDMY(string sDate, out DateTime oDate)
        {
            oDate = new DateTime();
            if (string.IsNullOrEmpty(sDate)) return false;
            if (!checkFormatDate(sDate)) return false;
            string[] arr = sDate.Split('/');
            if (arr.Length != 3) return false;
            int iDay = int.Parse(arr[0]);
            int iMonth = int.Parse(arr[1]);
            int iYear = int.Parse(arr[2]);
            if (iDay < 0 || iDay > 31 || iMonth < 0 || iMonth > 12 || iYear < 1735 || iYear > 2500) return false;
            if (iDay > GetMaxDay(iMonth, iYear)) return false;
            oDate = new DateTime(iYear,iMonth,iDay);
            return true;
        }

        public static bool checkDateDMY(string sDate)
        {
            if (string.IsNullOrEmpty(sDate)) return false;
            if(!checkFormatDate(sDate)) return false;
            string[] arr = sDate.Split('/');
            if (arr.Length != 3) return false;
            int iDay = int.Parse(arr[0]);
            int iMonth = int.Parse(arr[1]);
            int iYear = int.Parse(arr[2]);
            if (iDay < 0 || iDay > 31 || iMonth < 0 || iMonth > 12 || iYear < 1735 || iYear > 2500) return false;
            if (iDay > GetMaxDay(iMonth, iYear)) return false;
            return true;
        }

        private static int GetMaxDay(int iMonth,int iYear)
        {
            switch (iMonth)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if (iYear % 4 == 0)
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }
                default:
                    return 0;
            }
        }

        private static bool checkFormatDate(string s)
        {
            if (s == null) return false;
            Regex isNumber = new Regex(@"^\d{2}\/\d{2}\/\d{4}$");
            Match m = isNumber.Match(s);
            return m.Success;
        }

        public static bool checkFormatInt(object x)
        {
            if (x == null) return true;
            if (x is DBNull) return true;
            string s = x.ToString();
            Regex isNumber = new Regex(@"^(-)?\d+$");
            Match m = isNumber.Match(s);
            return m.Success;
        }
    }
}
