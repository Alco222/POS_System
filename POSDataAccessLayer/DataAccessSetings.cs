using System;
using System.Configuration;

namespace POSDataAccessLayer
{
    public class DataAccessSetings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
    }
}
