using KDABackendLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KDABackendLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }
        public static void InitializeConnections()
        {
            Connection = new SqlConnector();
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}