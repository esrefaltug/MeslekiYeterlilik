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
    public class BelgeTipiController : Controller
    {
        /*
        private readonly IBelgeTipiService _categoryService;

        public BelgeTipiController(IBelgeTipiService categoryService)
        {
            _categoryService = categoryService;
        }
        */
        private readonly ILogger<BelgeTipiController> _logger;

        public BelgeTipiController(ILogger<BelgeTipiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<BelgeTipi> Get()
        {
            DataTable dataTable = new DataTable();

            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "SELECT * FROM BelgeTipleri";

            using (var da = new SqlDataAdapter(query, connString))
            {
                da.Fill(dataTable);
            }

            List<BelgeTipi> belgeTipiList = new List<BelgeTipi>();

            foreach (DataRow dr in dataTable.Rows)
            {
                BelgeTipi belgetipi = new BelgeTipi();
                {
                    belgetipi.sReferansKodu = dr["sReferansKodu"].ToString();
                    belgetipi.sBelgeAdi = dr["sBelgeAdi"].ToString();
                    belgetipi.dYayimlanmaTarihi = Convert.ToDateTime(dr["dYayimlanmaTarihi"]);
                    //belgetipi.nRevizyonNumarasi = Convert.ToInt16(dr["nRevizyonNumarasi"];
                    int strTempRevizyonNumarasi = Convert.ToInt16(dr["nRevizyonNumarasi"]);
                    belgetipi.sRevizyonNumarasi = String.Format("{0:D2}", strTempRevizyonNumarasi);
                    belgetipi.dRevizyonTarihi = Convert.ToDateTime(dr["dRevizyonTarihi"]);
                    //belgetipi.pBelgeler_rowid = Convert.ToInt16(dr["pBelgeler_rowid"]);
                };
                belgeTipiList.Add(belgetipi);
            }
            return belgeTipiList;
        }

        [HttpDelete]
        public string Delete(string referansKodu)
        {
            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "DELETE FROM BelgeTipleri WHERE sReferansKodu = '" + referansKodu + "';";
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

        [HttpPost]
        public string Post(string sReferansKodu, string sBelgeAdi, DateTime dYayimlanmaTarihi, Int16 nRevizyonNumarasi, DateTime dRevizyonTarihi)
        {
            string strYayimlanmaTarihi = dYayimlanmaTarihi.ToString();
            string strRevizyonNumarasi = nRevizyonNumarasi.ToString();
            string strRevizyonTarihi = dRevizyonTarihi.ToString();

            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "INSERT INTO BelgeTipleri (sReferansKodu, sBelgeAdi, dYayimlanmaTarihi, nRevizyonNumarasi, dRevizyonTarihi) VALUES ('"
                + sReferansKodu + "', '" + sBelgeAdi + "', '" + strYayimlanmaTarihi + "', '" + strRevizyonNumarasi + "', '" + strRevizyonTarihi + "')";
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
        public string Put(int rowId, string sReferansKodu, string sBelgeAdi, DateTime dYayimlanmaTarihi, Int16 nRevizyonNumarasi, DateTime dRevizyonTarihi)
        {
            string strrowId = rowId.ToString();
            string strYayimlanmaTarihi = dYayimlanmaTarihi.ToString();
            string strRevizyonNumarasi = nRevizyonNumarasi.ToString();
            string strRevizyonTarihi = dRevizyonTarihi.ToString();

            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "UPDATE BelgeTipleri SET " +
            "sReferansKodu = '" + sReferansKodu + "', sBelgeAdi = '" + sBelgeAdi + "', dYayimlanmaTarihi = '" + strYayimlanmaTarihi + "', nRevizyonNumarasi = '" + strRevizyonNumarasi + 
            "', dRevizyonTarihi = '" + strRevizyonTarihi + "' WHERE pBelgeTipleri_rowid = " + strrowId;

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