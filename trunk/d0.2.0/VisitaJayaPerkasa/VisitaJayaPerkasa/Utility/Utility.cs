using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Utility.Log;
using System.Globalization;

namespace VisitaJayaPerkasa.Utility
{
    public class Utility
    {
        public static string DisplayNullValues(object text) {
            if (text == null)
                return "-";
            else
                return text.ToString();
        }


        public static bool IsDBNull(object obj) {
            if (obj == DBNull.Value)
                return true;
            else
                return false;
        }

        public static DateTime DefaultDateTime() {
            string date = "02-01-1990";
            DateTime dt = Convert.ToDateTime(date);

            return dt;
        }

        
        public static DateTime ConvertStringToDate(string dateTime) {
            DateTime DateTime = DefaultDateTime();

            try {
                DateTime = Convert.ToDateTime(dateTime);
            }
            catch (Exception e) {
                Logging.Error("Utility.cs - ConvertStringToDate() " + e.Message);
            }

            return DateTime;
        }


        public static string GetDateOnly(string date) {
            string[] tempSplit = date.Split(' ');

            return tempSplit[0];
        }


        public static string ConvertDateToString(DateTime dateTime) {
            string value = null;
            
            try
            {
                value = string.Format("{0:MM/dd/yyyy}", dateTime);
            }
            catch (Exception e) {
                Logging.Error("Utility.cs - ConvertDateToString() " + e.Message);
            }

            return value;
        }


        public static string ChangeDateMMDD(string date) {
            string temp = date;
            string[] tempSplit = date.Split('/');

            temp = tempSplit[1] + "/" + tempSplit[0] + "/" + tempSplit[2];

            return temp;
        }


        public static Guid ConvertToUUID(string Id) {
            Guid ID = Guid.Empty;

            try
            {
                ID = Guid.Parse(Id);
            }
            catch (Exception e) {
                Logging.Error("Utility.cs - ConvertToUUI " + e.Message);
            }

            return ID;
        }

        public static decimal ConvertStringToDecimal(string text) {
            decimal dec = -1;
            
            try
            {
                dec = Decimal.Parse(text);
            }
            catch (Exception e) {
                Logging.Error("Utility.cs - ConvertStringToDecimal " + e.Message);
            }

            return dec;
        }

        public static bool IsObjectNullOrEmpty(object obj) {
            if (obj == null)
                return true;
            else
                return false;
        }

        public static bool IsStringNullorEmpty(string text) {
            if (text != null)
            {
                if (text.Trim().Length > 0)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        public static string MD5(string password)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(password);
            string ret = "";
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
            }
            catch
            {
                Logging.Error("MD5");
            }


            return ret;
        }
    }
}
