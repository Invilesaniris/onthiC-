using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phan3Bai1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //cau a
        private void btnDocDuLieu_Click(object sender, EventArgs e)
        {
            //error prone: remember to clear your view
            lvHoKhau.Items.Clear();

            string sql = "select * from HoKhau";
            DataTable dt=DatabaseHelper.executeQuery(sql);
            
            foreach(DataRow dr in dt.Rows)
            {
                ListViewItem item = new ListViewItem(dr[0].ToString());
                //error prone: ListViewItem da dai dien cho column dau tien roi
                for(int i=1; i<dt.Columns.Count; ++i)
                {
                    item.SubItems.Add(dr[i].ToString());
                }
                lvHoKhau.Items.Add(item);
            }
            

        }

        //cau b
        private void lvHoKhau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHoKhau.SelectedItems.Count == 0)
                return;
            ListViewItem item = lvHoKhau.SelectedItems[0];
            string mahk = item.Text;
            DataTable hoKhau = DatabaseHelper.executeQuery($"select * from HoKhau where MaHoKhau='{mahk}'");
            //Error prone: need to cast type when retrieving value from cell of data table
            txtMaHoKhau.Text = (string) hoKhau.Rows[0]["MaHoKhau"];
            txtDiaChi.Text = (string)hoKhau.Rows[0]["DiaChi"];
            txtTenChuHo.Text = (string)hoKhau.Rows[0]["ChuHo"];


            //error prone: remember to clear your view
            lvNhanKhau.Items.Clear();
            DataTable nhanKhau = DatabaseHelper.executeQuery($"select * from NhanKhau where MaHK='{mahk}'");
            foreach(DataRow dr in nhanKhau.Rows)
            {
                var item1 = new ListViewItem(dr["MaNK"].ToString());
                item1.SubItems.Add(dr["TenNK"].ToString());
                //error prone, ToString() is safer than (string)
                item1.SubItems.Add(dr["NgaySinh"].ToString());
                lvNhanKhau.Items.Add(item1);

            }


        }

        //cau c
        private void lvNhanKhau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvNhanKhau.SelectedItems.Count == 0)
                return;
            ListViewItem item = lvNhanKhau.SelectedItems[0];
            string maNK = item.Text;
            DataTable nhanKhau = DatabaseHelper.executeQuery($"select * from NhanKhau where MaNK='{maNK}'");
            txtMaNhanKhau.Text = maNK;
            txtHoTenNK.Text = nhanKhau.Rows[0]["TenNK"].ToString();
            dtpNgaySinh.Value = (DateTime)nhanKhau.Rows[0]["NgaySinh"];
            if ((bool)nhanKhau.Rows[0]["GioiTinh"])
            {
                rdoNam.Checked = true;
                rdoNu.Checked = false;
            }
            else
            {
                rdoNam.Checked = false;
                rdoNu.Checked = true;
            }
            if((bool)nhanKhau.Rows[0]["LoaiCuTru"])
                cboCuTru.Text = "thuong tru";
            else
                cboCuTru.Text = "tam tru";
            txtCMND.Text = nhanKhau.Rows[0]["CMND"].ToString();

        }

        //cau d
        private void btnThoat_Click(object sender, EventArgs e)
        {
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show("ban co muon thoat", "thong bao", button);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
