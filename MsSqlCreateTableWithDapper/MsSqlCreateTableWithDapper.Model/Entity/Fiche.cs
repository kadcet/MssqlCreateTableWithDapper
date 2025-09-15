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
    public class Fiche : BaseEntity
    {
        public FicheType FicheType { get; set; }


        public DateTime FicheDate { get; set; }

        public string FicheNumber { get; set; }

        [DbIgnoreColumn]
        public Firm Firm { get; set; }

        [ForeignKey("Firm", "Id")]
        public int FirmId { get; set; }

        [DbIgnoreColumn]
        public CompanyCard CompanyCard { get; set; }

        [ForeignKey("CompanyCard", "Id")]
        public int CompanyCardId { get; set; }

        public string Information { get; set; }

        [DbIgnoreColumn]
        public List<FicheDetail> FicheDetail { get; set; }



    }
}
