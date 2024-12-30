using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phan1Bai4
{
    class LoaiPhong
    {
        public String TenLoai { get;}
        public double Gia { get;}
        public LoaiPhong(string ten, double gia)
        {
            TenLoai = ten;
            Gia = gia;
        }

    }

    class PhieuThuePhong
    {
        public String TenKhach { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public LoaiPhong LoaiPhongThue { get; set; }
        public PhieuThuePhong(string khach, DateTime start, DateTime end, LoaiPhong lp)
        {
            this.TenKhach = khach;
            this.NgayBatDau = start;
            this.NgayKetThuc = end;
            this.LoaiPhongThue = lp;
        }

        public double TinhTien()
        {
            int numberOfDay = (NgayKetThuc - NgayBatDau).Days;
            double ans = numberOfDay * LoaiPhongThue.Gia;
            if (numberOfDay > 6)
                ans = ans * 10 %;
            return ans;
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {


        }
    }
}
