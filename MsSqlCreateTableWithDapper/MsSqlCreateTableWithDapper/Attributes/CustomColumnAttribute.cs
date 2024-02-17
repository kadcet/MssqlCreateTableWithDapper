using System.ComponentModel.DataAnnotations.Schema;

namespace MsSqlCreateTableWithDapper.Attributes
{
    public class CustomColumnAttribute : ColumnAttribute
    {
        public bool IsPrimaryKey { get; set; } = false;
        public int Length { get; set; } = 250;
        public bool AllowNull { get; set; } = true;

    }
}
