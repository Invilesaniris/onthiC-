using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phan3Bai1
{
    public class DatabaseHelper
    {
        private static string connectionString = @"Data Source=LAPTOP-GVG5O4TT\SQLEXPRESS; Initial Catalog=quanlynhankhau; Integrated Security=true";

        public static DataTable executeQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }


    }
}
