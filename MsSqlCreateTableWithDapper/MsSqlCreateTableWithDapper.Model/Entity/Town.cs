using MsSqlCreateTableWithDapper.Model.Abstract;
using MsSqlCreateTableWithDapper.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class Town : BaseEntity
    {
        [ForeignKey("City", "Id")]
        public int CityId { get; set; }

        public string TownName { get; set; }

        public string Slug { get; set; }
    }
}
