using MsSqlCreateTableWithDapper.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class StockCard : BaseEntity
    {

        public string StockName { get; set; }

        public string StockCode { get; set; }
    }
}
