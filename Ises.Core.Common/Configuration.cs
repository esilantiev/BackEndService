using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Ises.Core.Common
{
    public class Configuration
    {
        public class DbConnection
        {
            private static readonly Object ThisLock = new Object();
            private static string connectionString;

            public static string ConnectionString
            {
                get
                {
                    lock (ThisLock)
                    {
                        if (String.IsNullOrEmpty(connectionString))
                        {
                            connectionString = ConfigurationManager.ConnectionStrings["IsesDbContext"].ConnectionString;
                            var csb = new SqlConnectionStringBuilder(connectionString);
                            csb.Password = Crypto.Decrypt(csb.Password);
                            csb.UserID = Crypto.Decrypt(csb.UserID);
                            connectionString = csb.ToString();
                        }
                        return connectionString;
                    }

                }
            }
        }

        public class Host
        {
            public static String BackOfficeApiBaseAddress
            {
                get { return ConfigurationUtils.GetAsString("host:backOfficeApiBaseAddress", "http://localhost:8080"); }
            }
        }

        public class Report
        {
            public static int DefaultItemsPerPage
            {
                get { return ConfigurationUtils.GetAsInt("report:defaultItemsPerPage", 10); }
            }
        }

        public class ConfigFile
        {
            public const string SqlErrorCodesFile = "SqlErrorCodes.json";
            public static string Path
            {
                get { return ConfigurationUtils.GetAsString("config:configFilePath", @"c:\Ises\Config\"); }
            }
        }
    }
}
