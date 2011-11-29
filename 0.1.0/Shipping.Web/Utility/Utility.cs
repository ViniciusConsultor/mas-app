using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Shipping.Web.Utility
{
    public static class Utility
    {
        public static Guid NewUUID() {
            return Guid.NewGuid();
        }

        public static string GetStringParameter(object par1)
        {
            PropertyInfo[] fields = par1.GetType().GetProperties(); 
            string result = "";

            foreach (var field in fields)
            {
                string name = field.Name;
                result += "@" + name + ", ";
            }

            result = result.Substring(0, result.Length - 2);
            return result;
        }
    }
}
