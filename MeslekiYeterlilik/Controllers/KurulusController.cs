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
    public class KurulusController : Controller
    {
        private readonly ILogger<KurulusController> _logger;

        public KurulusController(ILogger<KurulusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Kurulus> Liste()
        {
            DataTable dataTable = new DataTable();

            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "SELECT * FROM Kuruluslar";

            using (var da = new SqlDataAdapter(query, connString))
            {
                da.Fill(dataTable);
            }

            List<Kurulus> belgeTipiList = new List<Kurulus>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Kurulus kurulus = new Kurulus();
                {
                    //kurulus.pSirketler_rowid = Convert.ToInt16(dr["pSirketler_rowid"]);
                    kurulus.sKurulusKodu = dr["sKurulusKodu"].ToString();
                    kurulus.sKurulusAdi = dr["sKurulusAdi"].ToString();
                };
                belgeTipiList.Add(kurulus);
            }

            return belgeTipiList;
        }

        [HttpDelete]
        public string Delete(string kurulusKodu)
        {
            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "DELETE FROM Kuruluslar WHERE sKurulusKodu = '" + kurulusKodu + "';";
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
        public string Post(string sKurulusKodu, string sKurulusAdi)
        {          
            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "INSERT INTO Kuruluslar (sKurulusKodu, sKurulusAdi) VALUES ('"
                + sKurulusKodu + "', '" + sKurulusAdi + "')";
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
        public string Put(int rowId, string sKurulusKodu, string sKurulusAdi)
        {
            string strrowId = rowId.ToString();
            string connString = "Server = localhost\\SQLEXPRESS; Database = MeslekiYeterlilik; Trusted_Connection = True; MultipleActiveResultSets = true";
            string query = "UPDATE Kuruluslar SET " +
            "sKurulusKodu = '" + sKurulusKodu + "', sKurulusAdi = '" + sKurulusAdi +
            "' WHERE pKuruluslar_rowid = " + strrowId;

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
