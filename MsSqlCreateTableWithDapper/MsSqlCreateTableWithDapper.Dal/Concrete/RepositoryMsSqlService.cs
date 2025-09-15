using Dapper;
using Microsoft.Data.SqlClient;
using MsSqlCreateTableWithDapper.Dal.Abstract;
using MsSqlCreateTableWithDapper.Dal.Helper;
using MsSqlCreateTableWithDapper.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Dal.Concrete
{
    public class RepositoryMsSqlService : IRepositoryService
    {

        public List<T> Get<T>(string sqlcmd, object prm)
        {
            using (var cnn = new SqlConnection(CnnStr.MssqlConnectionString))
            {
                return cnn.Query<T>(sqlcmd, prm, commandTimeout: 90).ToList();
            }
        }

        public List<T> Get<T>(string sqlcmd)
        {
            using var cnn = new SqlConnection(CnnStr.MssqlConnectionString);
            return cnn.Query<T>(sqlcmd, commandTimeout: 90).ToList();
        }

        public T GetFirstOrDefault<T>(string sqlcmd, object prm)
        {
            using var cnn = new SqlConnection(CnnStr.MssqlConnectionString);
            return cnn.QueryFirstOrDefault<T>(sqlcmd, prm, commandTimeout: 90);
        }

        public T GetFirstOrDefault<T>(string sqlcmd)
        {
            using (var cnn = new SqlConnection(CnnStr.MssqlConnectionString))
            {
                cnn.Open();
                return cnn.QueryFirstOrDefault<T>(sqlcmd, commandTimeout: 90);
            }

        }

        public int Insert<T>(T entity)
        {

            using var cnn = new SqlConnection(CnnStr.MssqlConnectionString);
            var sqlBuilder = new MssqlSqlBuilder();

            #region Gelen tabloyu db de kontrol et yoksa oluştur

            int? tableExist = GetFirstOrDefault<int?>($"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{typeof(T).Name}'");
            if (!tableExist.HasValue || tableExist.Value == 0)
            {

                string createTableQuery = sqlBuilder.GenerateCreateTableStatement(typeof(T));
                cnn.Execute(createTableQuery);
            }

            #endregion Gelen tabloyu db de kontrol et yoksa oluştur


            var sql = sqlBuilder.GenerateInsertSqlStatement(typeof(T));
            return cnn.QueryFirstOrDefault<int>(sql, entity); // yeni kaydın id si  dönüyor

        }

        public int Update<T>(T entity, string updateOnlyThisFields)
        {
            using var cnn = new SqlConnection(CnnStr.MssqlConnectionString);
            var sqlBuilder = new MssqlSqlBuilder();
            var sql = sqlBuilder.GenerateUpdateSqlStatement(typeof(T), updateOnlyThisFields);
            return cnn.Execute(sql, entity); // Etkilenen kayıt sayısı döner

        }

        public int Update<T>(T entity)
        {

            return Update(entity, string.Empty);
        }

        public int Delete<T>(T entity)
        {
            // Id :
            // Status= Active / Deleted
            // DeletedBy : 
            // DeletedTime : 

            Type t = entity.GetType();
            foreach (var propInfo in t.GetProperties())
            {
                if (propInfo.Name != "Status") continue;
                propInfo.SetValue(entity, RecordStatus.Deleted, null);
            }
            return Update(entity, "Status,DeletedBy,DeletedTime");
        }

    }
}
