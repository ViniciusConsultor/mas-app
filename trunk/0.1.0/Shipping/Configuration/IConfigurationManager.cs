using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Configuration
{
    public interface IConfigurationManager
    {
        string GetString(string key);

        bool GetBoolean(string key);

        int GetInteger(string key);

        List<string> GetList(string key);
    }
}
