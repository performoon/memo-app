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
