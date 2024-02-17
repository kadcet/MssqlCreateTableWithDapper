using MsSqlCreateTableWithDapper.Attributes;

namespace MsSqlCreateTableWithDapper.Model
{
    public class BaseEntity
    {
        [CustomColumn(IsPrimaryKey = true, AllowNull = false)]
        public int Id { get; set; }

        [CustomColumn(AllowNull = false)]
        public int CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        [CustomColumn(AllowNull = false)]
        public int UpdatedBy { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        [CustomColumn(AllowNull = false)]
        public int DeletedBy { get; set; }

        public DateTime DeletedDateTime { get; set; }

        [CustomColumn(AllowNull = false)]
        public int  Status { get; set; }
    }
}
