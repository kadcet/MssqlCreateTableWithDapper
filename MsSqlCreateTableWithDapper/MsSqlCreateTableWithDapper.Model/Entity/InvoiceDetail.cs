using MsSqlCreateTableWithDapper.Model.Abstract;
using MsSqlCreateTableWithDapper.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Entity
{
    public class InvoiceDetail : BaseEntity
    {
        [ForeignKey("Invoice", "Id")]
        public int InvoiceId { get; set; }

        [ForeignKey("FicheDetail", "Id")]
        public int FicheDetailId { get; set; }

        [ForeignKey("ServiceCard", "Id")]
        public int ServiceCardId { get; set; }

        [ForeignKey("StockCard", "Id")]
        public int StockCardId { get; set; }

        [ForeignKey("WareHouse", "Id")]
        public int WareHouseId { get; set; }

        public double Quantity { get; set; } // miktar


        public decimal UnitPrice { get; set; } // birim fiyat

        public decimal Price { get; set; }  // miktar x birim fiyat = toplam fiyat

    }
}
