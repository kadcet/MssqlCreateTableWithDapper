using Dapper;
using Microsoft.Data.SqlClient;
using MsSqlCreateTableWithDapper.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Dal.Helper
{
    public class MigrationHelper
    {
        public void MigrateDB()
        {

            string nspace = "MsSqlCreateTableWithDapper.Model.Entity";
            var ttt = AppDomain.CurrentDomain.GetAssemblies().Where(c => c.FullName.Contains("MsSqlCreateTableWithDapper.Model")).SelectMany(t => t.GetTypes()).Where(x => x.IsClass && x.Namespace == nspace).ToList();



            using var cnn = new SqlConnection(CnnStr.MssqlConnectionString);

            var hlp = new MssqlSqlBuilder();

            foreach (var type in ttt)
            {
                #region Gelen tabloyu db de kontrol et yoksa oluştur

                int? tableExist = cnn.QueryFirstOrDefault<int?>($"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{type.Name}'");
                if (!tableExist.HasValue || tableExist.Value == 0)
                {

                    string createTableQuery = hlp.GenerateCreateTableStatement(type);
                    cnn.Execute(createTableQuery);
                }

                #endregion Gelen tabloyu db de kontrol et yoksa oluştur
            }


            foreach (var type in ttt)
            {
                string createTableForeignKey = hlp.GenerateCreateForeignKey(type);
                if (!string.IsNullOrEmpty(createTableForeignKey))
                {
                    try
                    {
                        cnn.Execute(createTableForeignKey);
                    }
                    catch (Exception)
                    {

                    }


                }

            }


        }
    }
}
