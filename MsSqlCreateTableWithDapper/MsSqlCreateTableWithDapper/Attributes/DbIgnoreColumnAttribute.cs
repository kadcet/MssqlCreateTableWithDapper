using System.ComponentModel.DataAnnotations.Schema;

namespace MsSqlCreateTableWithDapper.Attributes
{
    public class DbIgnoreColumnAttribute : ColumnAttribute
    {
        public bool DbIgnore { get; set; }
    }
}
