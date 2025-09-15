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
    public class FicheDetail : BaseEntity
    {
        public TransType TransType { get; set; }



        [ForeignKey("Fiche", "Id")]
        public int FicheId { get; set; }
        [DbIgnoreColumn]
        public Fiche Fiche { get; set; }

        [ForeignKey("WareHouse", "Id")]
        public int WareHouseId { get; set; }
        [DbIgnoreColumn]
        public WareHouse WareHouse { get; set; }

        [ForeignKey("StockCard", "Id")]
        public int StockCardId { get; set; }
        [DbIgnoreColumn]
        public StockCard StockCard { get; set; }

        public int Quantity { get; set; }


    }
}
