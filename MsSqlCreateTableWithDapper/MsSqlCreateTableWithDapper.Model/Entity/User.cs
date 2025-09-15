using MsSqlCreateTableWithDapper.Model.Abstract;
using MsSqlCreateTableWithDapper.Model.Attributes;
using MsSqlCreateTableWithDapper.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class User : BaseEntity
    {
        public string NameSurname { get; set; }

        [CustomColumn(AllowNull = false)]
        public string UserName { get; set; }

        [CustomColumn(AllowNull = false)]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; } // E K 

        [ForeignKey("City", "Id")]
        public int CityId { get; set; }

        [ForeignKey("Town", "Id")]
        public int TownId { get; set; }

        public string EMail { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressText { get; set; }

        public string PostCode { get; set; }




    }
}
