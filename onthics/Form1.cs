using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace onthics
{

    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            List<Product> products = new List<Product>
            {
                new Product { Id = "P001", Name = "Laptop", Price = 800 },
                new Product { Id = "P001", Name = "Laptop", Price = 800 },
                new Product { Id = "P001", Name = "Laptop", Price = 800 },
                new Product { Id = "P002", Name = "Phone", Price = 600 },
                new Product { Id = "P003", Name = "Tablet", Price = 300 }
            };

            DataGridViewRow row=new DataGridViewRow();
           



        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewCell selectedCell=dataGridView1.SelectedCells[0];
            var value = selectedCell.Value;
            MessageBox.Show(value.ToString());

        }

        private void XoaButton_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
    class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Product(string Id, string Name, int Price) {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
        }
        public Product()
        { 
        }
            
    }
}

