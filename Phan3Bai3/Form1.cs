using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phan3Bai3
{
    public partial class Form1 : Form
    {
        int currentRecord;
        int totalRecord;
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnLoad.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            BtnSearch.Enabled = false;

            cboSearch.Items.Add("Ho ten");
            cboSearch.Items.Add("Lop");
            cboSearch.Items.Add("Khoa");

        }

        private DataTable executeQuery(string query)
        {
            DataTable dt = new DataTable();
            string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=QLSV; Integrated Security=true";
            using(SqlConnection conn=new SqlConnection(connString))
            {
                using(SqlCommand cmd=new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        private void executeNonQuery(string command)
        {
            string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=QLSV; Integrated Security=true";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdatePosition(int index, int total)
        {
            labelPosition.Text = index.ToString() + " of " + total.ToString();
            currentRecord = index;
            totalRecord = total;
            
        }

        //cau 1
        private void btnLoad_Click(object sender, EventArgs e)
        {
            btnLoad.Enabled = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnNext.Enabled = true;
            btnPrev.Enabled = true;
            BtnSearch.Enabled = true;

            cboSearch.Items.Add("Họ tên");
            cboSearch.Items.Add("Lớp");
            cboSearch.Items.Add("Khoa");

            DataTable dt = executeQuery("select * from SinhVien");
            dgvSinhVien.DataSource = dt;

            UpdatePosition(1, dt.Rows.Count);

        }

        private void jumpToPosition(int pos)
        {
            dgvSinhVien.CurrentCell = dgvSinhVien.Rows[pos-1].Cells[0];
        }
        //cau 2
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentRecord == 1)
                return;
            currentRecord = dgvSinhVien.CurrentRow.Index +1;
            
            UpdatePosition(currentRecord - 1, totalRecord);
            jumpToPosition(currentRecord);

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentRecord == totalRecord)
                return;

            currentRecord = dgvSinhVien.CurrentRow.Index + 1;
            UpdatePosition(currentRecord + 1, totalRecord);
            jumpToPosition(currentRecord);
        }

        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Equals("Cancel"))
            {
                btnAdd.Text = "Add";
                btnDelete.Enabled = true;
                btnNext.Enabled = true;
                btnPrev.Enabled = true;
                BtnSearch.Enabled = true;

                DataTable dt = ((DataTable)dgvSinhVien.DataSource);
                DataRow dr = dt.Rows[dt.Rows.Count - 1];

                string msv = dr["MaSV"] as string;
                string khoa = dr["Khoa"] as string;
                double diem= (Double)dr["Diem"];
                string hoten = dr["HoTen"] as string;
                string lop = dr["MaLop"] as string;
                string diachi = dr["DiaChi"] as string;
                DateTime ngaysinh = (DateTime)dr["NgaySinh"];
                string gioitinh = dr["GioiTinh"] as string;
                executeNonQuery($"insert into SinhVien values ('{msv}','{hoten}','{khoa}','{lop}',{diem},'{diachi}','{gioitinh}','{ngaysinh}')");

            }
            else
            {
                DataGridViewRow row = dgvSinhVien.CurrentRow;
                string maSV = row.Cells["MaSV"].Value as string;
                string hoTen = row.Cells["HoTen"].Value as string;
                string khoa = row.Cells["Khoa"].Value as string;
                string maLop = row.Cells["MaLop"].Value as string;
                Double diem = (Double)row.Cells["Diem"].Value;
                string diaChi = row.Cells["DiaChi"].Value as string;
                string gioiTinh = row.Cells["GioiTinh"].Value as string;
                string ngaySinh = ((DateTime)row.Cells["NgaySinh"].Value).ToString("yyyy-MM-d");
                executeNonQuery($"UPDATE SinhVien SET " +
                    $"HoTen = '{hoTen}', Khoa= '{khoa}',MaLop='{maLop}'," +
                    $"Diem={diem}, DiaChi='{diaChi}', GioiTinh='{gioiTinh}'," +
                    $"NgaySinh='{ngaySinh}' where MaSV='{maSV}'");

                DataTable dt = executeQuery("select * from SinhVien");
                dgvSinhVien.DataSource = dt;
                UpdatePosition(1, dt.Rows.Count);

                dt = executeQuery("select * from SinhVien");
                dgvSinhVien.DataSource = dt;
                UpdatePosition(1, dt.Rows.Count);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = dgvSinhVien.CurrentRow;
            if (row == null)
                return;

            executeNonQuery($"delete from SinhVien where MaSV='{row.Cells["MaSV"].Value.ToString()}'");
            DataTable dt = executeQuery("select * from SinhVien");
            dgvSinhVien.DataSource = dt;
            UpdatePosition(1, dt.Rows.Count);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Equals("Cancel"))
            {
                btnAdd.Text = "Add";
                btnDelete.Enabled = true;
                btnNext.Enabled = true;
                btnPrev.Enabled = true;
                BtnSearch.Enabled = true;
                DataTable dt1 = (DataTable)dgvSinhVien.DataSource;
                dt1.Rows.RemoveAt(dt1.Rows.Count - 1);
                return;
            }

            btnAdd.Text = "Cancel";
            btnDelete.Enabled = false;
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            BtnSearch.Enabled = false;

            DataTable dt = (DataTable)dgvSinhVien.DataSource;

            string msv = txtMaSV.Text;
            string khoa = txtKhoa.Text;
            float diem;
            float.TryParse(txtDiem.Text, out diem);
            string hoten = txtHoTen.Text;
            string lop = txtLop.Text;
            string diachi = txtDiaChi.Text;
            DateTime ngaysinh = dtpNgaySinh.Value;
            string gioitinh;
            if (rdoNam.Checked)
                gioitinh = "Nam";
            else
            {
                gioitinh = "Nu";
            }

            dt.Rows.Add(msv, hoten, khoa, lop, diem, diachi, gioitinh, ngaysinh);

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTxt = txtSearch.Text;
            string searchOption = cboSearch.Text;
            DataTable dt;
            if(searchOption.Equals("Ho ten"))
            {
                dt = executeQuery($"select * from SinhVien where HoTen like '%{searchTxt}%'");
            }
            else if (searchOption.Equals("Lop"))
            {

                dt = executeQuery($"select * from SinhVien where MaLop='{searchTxt}'");
            }
            else
            {
                dt = executeQuery($"select * from SinhVien where Khoa='{searchTxt}'");
            }
            dgvSinhVien.DataSource = dt;
            UpdatePosition(1, dt.Rows.Count);
        }
    }
}
