using MsSqlCreateTableWithDapper.Model.MsSqlCreateTableWithDapperModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Bll.Absract
{
    public interface IAdminService
    {

        public IResult MigrateDatabase();



    }
}
