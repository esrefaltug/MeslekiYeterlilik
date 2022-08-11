
namespace MeslekiYeterlilik.Domain.Models
{
    public class BelgeTipi 
    {
        public string sReferansKodu { get; set; }
        public string sBelgeAdi { get; set; }
        public DateTime dYayimlanmaTarihi { get; set; }
        public Int16? nRevizyonNumarasi { get; set; }
        public DateTime dRevizyonTarihi { get; set; }
        public Int16 pBelgeler_rowid { get; set; }
        public string sRevizyonNumarasi { get; set; }
        
    }
}