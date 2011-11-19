using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        #region IConfigurationManager Members

        public string GetString(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public bool GetBoolean(string key)
        {
            return bool.Parse(GetString(key));
        }

        public int GetInteger(string key)
        {
            return int.Parse(GetString(key));
        }

        public List<string> GetList(string key)
        {
            return new List<string>(GetString(key).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ConvertAll(item => item.Trim());
        }

        #endregion
    }
}
