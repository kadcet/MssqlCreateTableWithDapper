using MsSqlCreateTableWithDapper.Bll.Absract;
using MsSqlCreateTableWithDapper.Dal.Helper;
using MsSqlCreateTableWithDapper.Model.MsSqlCreateTableWithDapperModel.Abstract;
using MsSqlCreateTableWithDapper.Model.MsSqlCreateTableWithDapperModel.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Bll.Concrete
{
    public class AdminService : IAdminService
    {
        public IResult MigrateDatabase()
        {
            try
            {
                var mgr = new MigrationHelper();
                mgr.MigrateDB();

                return new SuccessResult("Tablolar Oluşturuldu Foreign keyler bağlandı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.ToString());
            }


        }


    }
}
