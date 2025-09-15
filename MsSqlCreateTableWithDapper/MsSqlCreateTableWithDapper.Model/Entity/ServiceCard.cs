using MsSqlCreateTableWithDapper.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class ServiceCard : BaseEntity
    {
        public string ServiceCode { get; set; }

        public string ServiceName { get; set; }
    }
}
