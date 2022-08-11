using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using MeslekiYeterlilik.Domain.Models;
using MeslekiYeterlilik.Domain.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MeslekiYeterlilik.Controllers
{
    [Route("/api/[controller]")]
    public class BelgeController : Controller
    {
        private readonly ILogger<BelgeController> _logger;

        public BelgeController(ILogger<BelgeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Belge> Get()
        {
            DataTable dataTable = new DataTable();

            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "SELECT * FROM Belgeler";

            using (var da = new SqlDataAdapter(query, connString))
            {
                da.Fill(dataTable);
            }

            List<Belge> belgeList = new List<Belge>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Belge belge = new Belge();
                {
                    //oBelgeTipi YAPILACAK --- SU ANLIK IPTAL EDİLDİ
                    belge.pPersonelId = Convert.ToInt32(dr["pPersonelId"]);
                    belge.nBelgeNumarasi = Convert.ToInt16(dr["nBelgeNumarasi"]);
                    belge.nBelgeTipi = Convert.ToInt16(dr["nBelgeTipi"]);
                    //belge.pBelgeler_rowid = Convert.ToInt32(dr["pBelgeler_rowid"]);
                    belge.dBelgeDuzenlenmeTarihi = Convert.ToDateTime(dr["dBelgeDuzenlenmeTarihi"]);
                    belge.dBelgeGecerlilikTarihi = Convert.ToDateTime(dr["dBelgeGecerlilikTarihi"]);
                    belge.nKurulus = Convert.ToInt16(dr["nKurulus"]);
                    belge.dtEklenmeTarihi = Convert.ToDateTime(dr["dtEklenmeTarihi"]);
                    belge.dtSonGuncellenmeTarihi = Convert.ToDateTime(dr["dtSonGuncellenmeTarihi"]);
                };
                belgeList.Add(belge);
            }
            return belgeList;
        }

        [HttpDelete]
        public string Delete(string belgeNumarasi)
        {
            string[] belgeNumarasiArray = belgeNumarasi.Split('/');
            //return belgeNumarasiArray;
            //belgeNumarasiArray[0] = kurulus
            //belgeNumarasiArray[1] = belge tipi
            //belgeNumarasiArray[2] = revizyon
            //belgeNumarasiArray[3] = ayirt edici kod
            
            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "DELETE Belgeler FROM Belgeler JOIN BelgeTipleri ON Belgeler.nBelgeTipi = BelgeTipleri.pBelgeler_rowid JOIN Kuruluslar ON Belgeler.nKurulus = Kuruluslar.pKuruluslar_rowid WHERE" +
                "((Kuruluslar.sKurulusKodu = '" + belgeNumarasiArray[0].ToString() + "') AND" +
                "(BelgeTipleri.sReferansKodu = '" + belgeNumarasiArray[1].ToString() + "') AND" +
                "(BelgeTipleri.nRevizyonNumarasi = '" + Convert.ToInt16(belgeNumarasiArray[2]) + "') AND" +
                "(Belgeler.nBelgeNumarasi = '" + Convert.ToInt64(belgeNumarasiArray[3]) + "'))";
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                        return "DONE";
                    }
                }
            }
            catch (SystemException ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string Post(Int32 pPersonelId, Int16 nBelgeTipi, int nBelgeNumarasi, DateTime dBelgeDuzenlenmeTarihi, DateTime dBelgeGecerlilikTarihi, int nKurulus, DateTime dtEklenmeTarihi, DateTime dtSonGuncellenmeTarihi)
        {
            string strPersonelId = pPersonelId.ToString();
            string strBelgeTipi = nBelgeTipi.ToString();
            string strBelgeNumarasi = nBelgeNumarasi.ToString();
            string strBelgeDuzenlenmeTarihi = dBelgeDuzenlenmeTarihi.ToString();
            string strBelgeGecerlilikTarihi = dBelgeGecerlilikTarihi.ToString();
            string strKurulus = nKurulus.ToString();
            string strEklenmeTarihi = dtEklenmeTarihi.ToString();
            string strSonGuncellenmeTarihi = dtSonGuncellenmeTarihi.ToString();

            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "INSERT INTO Belgeler (pPersonelId, nBelgeTipi, nBelgeNumarasi, dBelgeDuzenlenmeTarihi, dBelgeGecerlilikTarihi," +
                " nKurulus, dtEklenmeTarihi, dtSonGuncellenmeTarihi) VALUES ('"
                + strPersonelId + "', '" + strBelgeTipi + "', '" + strBelgeNumarasi + "', '" + strBelgeDuzenlenmeTarihi + "', '" + strBelgeGecerlilikTarihi + "', '" + strKurulus + "', '" + strEklenmeTarihi + "', '" + strSonGuncellenmeTarihi + "')";
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                        return "JOB DONE!";
                    }
                }
            }
            catch (SystemException ex)
            {
                return ex.Message;
            }
        }

        [HttpPut]
        public string Put(int rowId, Int32 pPersonelId, Int16 nBelgeTipi, int nBelgeNumarasi, DateTime dBelgeDuzenlenmeTarihi, DateTime dBelgeGecerlilikTarihi, int nKurulus, DateTime dtEklenmeTarihi, DateTime dtSonGuncellenmeTarihi)
        {
            string strrowId = rowId.ToString();
            string strPersonelId = pPersonelId.ToString(); 
            string strBelgeTipi = nBelgeTipi.ToString();
            string strBelgeNumarasi = nBelgeNumarasi.ToString();
            string strBelgeDuzenlenmeTarihi = dBelgeDuzenlenmeTarihi.ToString();
            string strBelgeGecerlilikTarihi = dBelgeGecerlilikTarihi.ToString();
            string strKurulus = nKurulus.ToString();
            string strEklenmeTarihi = dtEklenmeTarihi.ToString();
            string strSonGuncellenmeTarihi = dtSonGuncellenmeTarihi.ToString();

            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "UPDATE Belgeler SET " +
            "pPersonelId = '" + strPersonelId + "', nBelgeTipi = '" + strBelgeTipi + "', nBelgeNumarasi = '" + strBelgeNumarasi + "', dBelgeDuzenlenmeTarihi = '" + strBelgeDuzenlenmeTarihi + 
            "', dBelgeGecerlilikTarihi = '" + strBelgeGecerlilikTarihi + "', nKurulus = '" + strKurulus + "', dtEklenmeTarihi = '" + strEklenmeTarihi + "', dtSonGuncellenmeTarihi = '" + strSonGuncellenmeTarihi +
            "' WHERE pBelgeler_rowid = " + strrowId;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                        return "JOB DONE!";
                    }
                }
            }
            catch (SystemException ex)
            {
                return ex.Message;
            }
        }
    }
}