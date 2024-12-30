using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phan2Bai1
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

            PhanSo a = new PhanSo(5); // a = [5/1]
            PhanSo b = new PhanSo(1, 2); // b = [1/2]

            Console.WriteLine("a = {0}, b = {1}", a, b);

            PhanSo c = a + b;
            Console.WriteLine("kq = {0} + {1} = {2}", a, b, c);
            Console.WriteLine("Gia tri phan so a = {0}, b = {1}", (float)a, (float)b);

        }
    }
}
