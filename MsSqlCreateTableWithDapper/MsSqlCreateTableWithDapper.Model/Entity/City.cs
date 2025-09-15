using MsSqlCreateTableWithDapper.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class City : BaseEntity
    {
        public string CityName { get; set; }

        public string Slug { get; set; }

    }
}
