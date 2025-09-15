using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Dal.Helper
{
    public static class CnnStr
    {
        public static string MssqlConnectionString = "Server=PC-NAME;initial Catalog=DB-NAME;integrated Security=true;TrustServerCertificate=True";
    }
}
