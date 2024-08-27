using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant;

namespace RestarauntGui
{
    public partial class frmPOS : Form
    {
        RestaurantContext context = new RestaurantContext();

        public frmPOS()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();
            Productpanal.Controls.Clear();
            LoadProducts();
        }
        void AddCategory()
        {
            categorypanal.Controls.Clear();

            if (context.Categories.Count()>0)
                foreach (var item in context.Categories)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(149, 45);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = item.Name;
                    b.Click += new EventHandler(_Click);
                    categorypanal.Controls.Add(b);
                }
        }

        private void _Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;

            foreach (var item in Productpanal.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        void AddItems(string id,string name,string cat, string price,Image image)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = image,
                ID = Convert.ToInt32(id),
            };
            Productpanal.Controls.Add(w);
            w.onSelect += (s, e) =>
             {
                 var wdg = (ucProduct)s;
                 foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                 {
                     if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.ID)
                     {
                         item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                         item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                            double.Parse(item.Cells["dgvprice"].Value.ToString());
                         return;
                     }


                 }
                 guna2DataGridView1.Rows.Add(new object[] { 0, wdg.ID, wdg.PName,1,wdg.PPrice, wdg.PPrice });
                 GetTotal();
             };
        }
        void LoadProducts()
        {
            var products = context.Products;
            foreach (var item in products)
            {
                Byte[]imagarray=(byte[])item.PImage;
                byte[] immagbytearray = imagarray;
                AddItems(item.PID.ToString(), item.PName.ToString(), item.Category.Name.ToString(), item.PPrice.ToString()
                    , Image.FromStream(new MemoryStream(imagarray)));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in Productpanal.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower()); 
            }
        }

        private void btnTakeAway_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach(DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotal()
        {
            double tot = 0;
            lbTotal.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }
            lbTotal.Text = tot.ToString("N2");
        }
    }
}
