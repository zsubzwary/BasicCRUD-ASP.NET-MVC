using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicFuncWebApp.Helper
{
    public class DBHelper
    {
        public static String getConnectionString(String FileName = "BasicFuncWebAppDB.db")
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;

            string sqlLiteConnectionString = string.Format(
              "data source=\"{0}\"",
              System.IO.Path.Combine(baseFolder, FileName));
            return sqlLiteConnectionString;
        }
    }
}
