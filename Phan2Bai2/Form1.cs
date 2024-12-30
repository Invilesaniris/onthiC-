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

namespace Phan2Bai2
{
    public partial class Form1 : Form
    {
        private DataTable dtDSDatVe;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //cau a
            List<string> phims=new List<string>() { "Để mai tính", "Bão táp xa mạc", 
                "Cô gái đi tìm tình yêu", "Cuốn sách của Eli"};
            cboTenPhim.DataSource = phims;

            List<string> gioChieus = new List<string>() { "14h", "16h", "18h", "20h", "22h" };
            cboGioChieu.DataSource = gioChieus;

            dtDSDatVe = new DataTable();
            dtDSDatVe.Columns.Add("Tên KH", typeof(string));
            dtDSDatVe.Columns.Add("Địa chỉ", typeof(string));
            dtDSDatVe.Columns.Add("Số ĐT", typeof (string));    
            dtDSDatVe.Columns.Add("Tên phim", typeof(string));
            dtDSDatVe.Columns.Add("Giờ", typeof(string));
            dtDSDatVe.Columns.Add("Ngày", typeof(string));
            dtDSDatVe.Columns.Add("Số vé", typeof(int));
            dgvDSDatVe.DataSource = dtDSDatVe;

            


        }

        //cau b
        private void txtTenKH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenKH.Text))
            {
                e.Cancel = true;
                MessageBox.Show("Tên khách hàng không được để trống");
            }
        }

        private void txtDiaChi_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDiaChi.Text))
            {
                e.Cancel = true;
                MessageBox.Show("Địa chỉ không được để trống");
            }
        }

        private void txtDienThoai_Validating(object sender, CancelEventArgs e)
        {
            string pattern = @"\A[0-9]{10,10}\z";
            Regex regex=new Regex(pattern);
            Match res=regex.Match(txtDienThoai.Text);
            if (!res.Success)
            {
                e.Cancel = true;
                MessageBox.Show("Số điện thoại không hợp lệ");
            }
        }

        private void btnDatVe_Click(object sender, EventArgs e)
        {
            string tenkh=txtTenKH.Text;
            string diachi = txtDiaChi.Text;
            string sdt=txtDienThoai.Text;
            string tenphim = cboTenPhim.SelectedValue as string;
            string gio=cboGioChieu.SelectedValue as string;
            string ngay = dtpNgayChieu.Value.ToString("dd/MM/yyyy");
            decimal soVe=numSoVe.Value;

            dtDSDatVe.Rows.Add(tenkh, diachi, sdt, tenphim, gio, ngay, (int)soVe);

        }
    }
}
