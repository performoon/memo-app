using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textRPG.statics
{
    public static class SqlConect
    {
        /* //学校用
        public static SqlConnectionStringBuilder SqlConection()
        {
            return new SqlConnectionStringBuilder()
            {
                DataSource = "localhost\\SQLEXPRESS",
                IntegratedSecurity = false,
                UserID = "admin",
                Password = "qwe123",
                InitialCatalog = "idea"
            };
        }
        */

        // 家用

        public static SqlConnectionStringBuilder SqlConection()
        {
            return new SqlConnectionStringBuilder()
            {
                DataSource = "localhost\\SQLEXPRESS",
                IntegratedSecurity = true,
                InitialCatalog = "idea"
            };
        }
    }
}
