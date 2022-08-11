using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace MeslekiYeterlilik
{
    public class ProjectionConnection
    {
        public static SqlConnection conn = null;

        public void Connection_Today()
        {
            conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=MeslekiYeterlilik;Trusted_Connection=True;MultipleActiveResultSets=true");
            conn.Open();
        }
    }
}
