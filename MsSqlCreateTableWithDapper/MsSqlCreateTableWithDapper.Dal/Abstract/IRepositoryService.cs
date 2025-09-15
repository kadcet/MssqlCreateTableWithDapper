using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Dal.Abstract
{
    public interface IRepositoryService
    {
        //GET With Filter  return : List<T>
        //GET With Filter  return : List<T>
        // GETFirstOrDefault with filter return : T
        // GETFirstOrDefault with filter return : T

        // Insert return int id sini göndür
        // Update return int kaç kayıt gncellendi(etkilendi) ise döndürsün


        public List<T> Get<T>(string sqlcmd, object prm);
        public List<T> Get<T>(string sqlcmd);

        public T GetFirstOrDefault<T>(string sqlcmd, object prm);
        public T GetFirstOrDefault<T>(string sqlcmd);

        public int Insert<T>(T entity);
        public int Update<T>(T entity, string updateOnlyThisFields);
        public int Update<T>(T entity);

        public int Delete<T>(T entity);




    }
}
