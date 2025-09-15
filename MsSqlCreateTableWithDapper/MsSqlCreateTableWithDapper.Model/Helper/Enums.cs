using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.Helper
{
    public enum RecordStatus
    {
        Active = 1,
        Deleted = 0
    }

    public enum Gender
    {
        [Description("E")]
        E,
        [Description("K")]
        K
    }

    public enum FirmType
    {
        [Description("Tüzel")]
        Corporate = 1,

        [Description("Şahıs")]
        Individual = 2
    }

    public enum CompanyType
    {
        [Description("Alıcı")]
        Alici = 120,

        [Description("Satıcı")]
        Satici = 320
    }

    public enum InvoiceType
    {
        [Description("Alış Faturası")]
        AlisFaturasi = 1000,

        [Description("Satış Faturası")]
        SatisFaturası = 2000

    }

    public enum FicheType
    {
        [Description("Alış İrsaliyesi")]
        AlisIrsaliye = 500,

        [Description("Satış İrsaliyesi")]
        SatisIrsaliye = 600,

        [Description("Depo Transfer İrsaliyesi")]
        DepoIrsaliye = 700
    }

    public enum TransType
    {
        [Description("Giriş")]
        Giris = 20,

        [Description("Çıkış")]
        Cikis = 30
    }


}
