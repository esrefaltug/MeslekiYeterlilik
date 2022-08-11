namespace MeslekiYeterlilik.Domain.Models
{
    public class Belge
    {
        public int pPersonelId { get; set; }
        public int nBelgeTipi { get; set; }
        //public BelgeTipi oBelgeTipi { get; set; }
        public int nBelgeNumarasi { get; set; }
        public DateTime dBelgeDuzenlenmeTarihi { get; set; }
        public DateTime dBelgeGecerlilikTarihi { get; set; }
        public int nKurulus { get; set; }
        public DateTime dtEklenmeTarihi { get; set; }
        public DateTime dtSonGuncellenmeTarihi { get; set; }
        public int pBelgeler_rowid { get; set; }
    }
}
