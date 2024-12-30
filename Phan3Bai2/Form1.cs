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

namespace Phan3Bai2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //cauc
        private SqlConnection getConnection()
        {
            var conn = new SqlConnection(@"data source=(local)\SQLEXPRESS; initial catalog=HocSinhDB; integrated security=true");
            return conn;
        }

        

        //cau d
        private void Form1_Load(object sender, EventArgs e)
        {
            var conn = getConnection();
            DataTable lopHoc = new DataTable();
            using (conn)
            {
                using(SqlCommand cmd=new SqlCommand("select * from LopHoc", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(lopHoc);
                }
            }
            cboLop.DataSource = lopHoc;
            cboLop.DisplayMember = "MaLop";
            cboLop.ValueMember = "MaLop";

            txtMaLop.Text = lopHoc.Rows[0]["MaLop"].ToString();
            txtTenLop.Text = lopHoc.Rows[0]["TenLop"].ToString();

            string maLop = lopHoc.Rows[0]["MaLop"].ToString();
            DataTable dsSV = new DataTable();
            using (conn=getConnection())
            {
                using (SqlCommand cmd = new SqlCommand($"select * from HocSinh where MaLop='{maLop}'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsSV);
                }
            }
            ShowDanhSachSV(dsSV);



        }
        private void ShowDanhSachSV(DataTable DSSinhVien)
        {
            lvDanhSachSV.Items.Clear();
            foreach(DataRow dr in DSSinhVien.Rows)
            {
                var item = new ListViewItem(dr["MaHS"].ToString());
                item.SubItems.Add(dr["HoLot"].ToString());
                item.SubItems.Add(dr["TenHS"].ToString());
                if((bool)dr["GioiTinh"])
                    item.SubItems.Add("Nam");
                else
                    item.SubItems.Add("Nu");
                item.SubItems.Add(dr["NamSinh"].ToString());
                item.SubItems.Add(dr["MaLop"].ToString());
                lvDanhSachSV.Items.Add(item);
            }
        }

        //cau e

        private void cboLop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var maLop = cboLop.SelectedValue.ToString();
            var conn = getConnection();
            DataTable lopHoc = new DataTable();
            using (conn)
            {
                using (SqlCommand cmd = new SqlCommand($"select * from LopHoc where MaLop='{maLop}'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(lopHoc);
                }
            }
            txtMaLop.Text = lopHoc.Rows[0]["MaLop"].ToString();
            txtTenLop.Text = lopHoc.Rows[0]["TenLop"].ToString();

            DataTable dsSV = new DataTable();
            using (conn = getConnection())
            {
                using (SqlCommand cmd = new SqlCommand($"select * from HocSinh where MaLop='{maLop}'", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsSV);
                }
            }
            ShowDanhSachSV(dsSV);

            labelSiSo.Text = dsSV.Rows.Count.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult res = MessageBox.Show("thoat", "thong bao", button);
            if (res == DialogResult.Yes)
                this.Close();
        }
    }
}
