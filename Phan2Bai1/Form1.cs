using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phan2Bai1
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ph2 b1 ca
            List<MatHang> matHangs = new List<MatHang>() {
                new MatHang("Samsung N148", 5500000),
                new MatHang("Máy bộ", 4500000),
                new MatHang("Ti vi", 2300000),
            };
            cboMatHang.DataSource = matHangs;
            cboMatHang.DisplayMember = "TenHang";

            DateTime currentDate = DateTime.Now.Date;
            datNgayLap.Value = currentDate;

            txtTongTien.ReadOnly = true;
            txtTongTien.Text = "0 Đ";
        }

        class MatHang
        {
            //Luu y uu tien dung Property thay vi dung Attribute
            public string TenHang { get; }
            public double GiaHang { get; }
            public MatHang(string tenHang, double giaHang)
            {
                this.TenHang = tenHang;
                this.GiaHang = giaHang;
            }
            public MatHang() { }
        }

        private void cboMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ph2 b1 cb
            MatHang matHang = cboMatHang.SelectedValue as MatHang;
            txtDonGia.ReadOnly = false; ;
            txtSoLuong.Text = "1";
            txtDonGia.Text=matHang.GiaHang.ToString()+"Đ";
            txtDonGia.ReadOnly = true;

        }

        //ph2 b1 cc
        private bool isHoadonValid(string hoaDon)
        {
            string pattern = @"\AHD-\d{1,}\z";
            Regex reg = new Regex(pattern);
            Match result = reg.Match(hoaDon);
            if (!result.Success)
            {
                return true;
            }
            return false;
        }
        private void txtMaHoaDon_Leave(object sender, EventArgs e)
        {
            if (isHoadonValid(txtMaHoaDon.Text))
            {
                MessageBox.Show("Hoa don phai theo format HD-{so}");
                txtMaHoaDon.Focus();
            }
        }

        private void txtKhachHang_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKhachHang.Text))
            {
                MessageBox.Show("Ten khach khong duoc de trong");
                txtKhachHang.Focus();
            }
        }

        private void datNgayLap_Leave(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime inputDate = datNgayLap.Value.Date;
            if (inputDate > currentDate)
            {
                MessageBox.Show("Ngay lap khong duoc lon hon ngay hien tai");
                datNgayLap.Focus();
            }
        }

        private void txtSoLuong_Leave(object sender, EventArgs e)
        {
            int soLuong;
            bool res=int.TryParse(txtSoLuong.Text, out soLuong);
            if (!res || soLuong<=0)
            {
                MessageBox.Show("So luong khong phu hop");
            }

        }

        //ph2 b1 cd
        private void updateTongTien(double soTien)
        {
            string strTongTien = txtTongTien.Text;
            strTongTien = strTongTien.Remove(strTongTien.Length - 1, 1);
            double intTongTien;
            double.TryParse(strTongTien, out intTongTien);
            intTongTien += soTien;

            txtTongTien.ReadOnly = false;
            txtTongTien.Text = intTongTien.ToString()+"Đ";
            txtTongTien.ReadOnly = true;
        }

        private void btnChonMua_Click(object sender, EventArgs e)
        {
            MatHang selectedMatHang = cboMatHang.SelectedValue as MatHang;
            ListViewItem item = new ListViewItem(selectedMatHang.TenHang);
            int soLuong;
            int.TryParse(txtSoLuong.Text, out soLuong);
            item.SubItems.Add(selectedMatHang.GiaHang.ToString() + "Đ");
            item.SubItems.Add(soLuong.ToString());
            double thanhTien = selectedMatHang.GiaHang * soLuong;
            item.SubItems.Add(thanhTien.ToString());

            updateTongTien(thanhTien);

            lvDanhSach.Items.Add(item);

            
        }

        //ph2 b1 ce
        private void btChonBo_Click(object sender, EventArgs e)
        {
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show("Ban co muan xoa?", "thong bao", button);
            if (res == DialogResult.No)
                return;

            ListViewItem selectedRow = lvDanhSach.Items[lvDanhSach.SelectedIndices[0]];
            lvDanhSach.Items.Remove(selectedRow);

            string strThanhTien = selectedRow.SubItems[3].Text;
            strThanhTien.Remove(strThanhTien.Length - 1, 1);
            double thanhTien;
            double.TryParse(strThanhTien, out thanhTien);

            updateTongTien(-thanhTien);


        }

        


    }
}
