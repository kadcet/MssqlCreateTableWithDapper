using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Attributes
{
    public class DbIgnoreColumnAttribute : ColumnAttribute
    {
        public bool DbIgnore { get; set; } = true;
    }
}
