using MsSqlCreateTableWithDapper.Attributes;

namespace MsSqlCreateTableWithDapper.Model
{
    public class Users : BaseEntity
    {

        public string Name { get; set; }
        public string SurName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [CustomColumn(AllowNull = false)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }


        [DbIgnoreColumn(DbIgnore = true)]
        public int  NameSurname{ get; set; }
    }
}
