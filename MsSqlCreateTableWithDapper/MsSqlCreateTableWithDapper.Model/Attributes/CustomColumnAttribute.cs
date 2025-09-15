using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Attributes
{
    public class CustomColumnAttribute : ColumnAttribute
    {
        public bool IsPrimaryKey { get; set; } = false;
        public int Length { get; set; } = 250;
        public bool AllowNull { get; set; } = true;

    }
}
