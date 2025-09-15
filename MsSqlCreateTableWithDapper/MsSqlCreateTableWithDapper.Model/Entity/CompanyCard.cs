using MsSqlCreateTableWithDapper.Model.Abstract;
using MsSqlCreateTableWithDapper.Model.Attributes;
using MsSqlCreateTableWithDapper.Model.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class CompanyCard : BaseEntity
    {
        public string CompanyName { get; set; }

        public CompanyType CompanyType { get; set; }

        [CustomColumn(Length = 11)]
        public string TaxNumber { get; set; }

        public string TaxOffice { get; set; }

        [ForeignKey("City", "Id")]
        public int CityId { get; set; }
        [DbIgnoreColumn]
        public City City { get; set; }

        [ForeignKey("Town", "Id")]
        public int TownId { get; set; }
        [DbIgnoreColumn]
        public Town Town { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressText { get; set; }

        public string PostCode { get; set; }



    }
}
