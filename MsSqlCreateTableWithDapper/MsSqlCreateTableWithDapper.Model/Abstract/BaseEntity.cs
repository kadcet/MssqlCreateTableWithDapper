using MsSqlCreateTableWithDapper.Model.Attributes;
using MsSqlCreateTableWithDapper.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Abstract
{
    public abstract class BaseEntity
    {
        [CustomColumn(IsPrimaryKey = true)]
        public int Id { get; set; }

       
        public int CreatedBy { get; set; }

       
        public DateTime CreatedDateTime { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        public int DeletedBy { get; set; }

        public DateTime DeletedDateTime { get; set; }

        [CustomColumn(AllowNull = false)]
        public RecordStatus RecordStatus { get; set; }
    }
}
