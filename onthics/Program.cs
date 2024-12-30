using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onthics
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            string ConnectionString = @"Data Source=LAPTOP-GVG5O4TT\SQLEXPRESS; Initial Catalog=Contacts; Integrated Security=true";
            using(SqlConnection conn=new SqlConnection(ConnectionString))
            {
                conn.Open();
                Console.WriteLine(conn.State);
                Console.WriteLine(conn.Database);

                conn.ChangeDatabase("bt1");
                Console.WriteLine(conn.State);
                Console.WriteLine(conn.Database);
            }




        }
    }
}
