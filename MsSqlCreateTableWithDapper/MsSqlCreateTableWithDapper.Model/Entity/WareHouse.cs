using MsSqlCreateTableWithDapper.Model.Abstract;
using MsSqlCreateTableWithDapper.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class WareHouse : BaseEntity
    {
        public string WareHouseCode { get; set; }

        public string WareHouseName { get; set; }


        [ForeignKey("City", "Id")]
        public int CityId { get; set; }

        [ForeignKey("Town", "Id")]
        public int TownId { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressText { get; set; }

        public string PostCode { get; set; }


    }
}
