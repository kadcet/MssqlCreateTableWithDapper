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
    public class Invoice : BaseEntity
    {
        [ForeignKey("Fiche", "Id")]
        public int FicheId { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNumber { get; set; }

        [DbIgnoreColumn]
        public Firm Firm { get; set; }

        [ForeignKey("Firm", "Id")]
        public int FirmId { get; set; }

        [DbIgnoreColumn]
        public CompanyCard CompanyCard { get; set; }

        [ForeignKey("CompanyCard", "Id")]
        public int CompanyCardId { get; set; }

        [DbIgnoreColumn]
        public List<InvoiceDetail> InvoiceDetail { get; set; }

        public decimal NetAmount { get; set; }  // matrah

        public int TaxRate { get; set; }  // vergi oranı

        public decimal TaxAmount { get; set; } // vergi tutarı

        public decimal GrossAmount { get; set; } // kdv dahil tutar

        public string Information { get; set; }




    }
}
