using Guna.UI2.WinForms;
using Restaurant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace RestarauntGui
{
    public partial class frmProductAdd : Form
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public int cID = 0;
        private void frmProductAdd_Load(object sender, EventArgs e)
        {
   
                Program.CBFill(cbCat);
            if(cID>0)
            {
                cbCat.SelectedValue=cID;
            }
            
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }
        string filePath;

        byte[] imageByteArray;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png) |* .png; *jpg";
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                // Prepare the image data
              
                if (txtImage.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap temp = new Bitmap(txtImage.Image);
                        temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        imageByteArray = ms.ToArray();
                    }
                }

                Product product;

                if (id == 0) // Insert
                {
                    // Create a new Product object
                    product = new Product
                    {
                        PName = txtName.Text.Trim(),
                        PPrice = float.Parse(tctPrice.Text.Trim()),
                        CategoryID = Convert.ToInt32(cbCat.SelectedValue),
                        PImage = imageByteArray
                    };

                    // Add the new product to the context
                    context.Products.Add(product);
                }
                else // Update
                {
                    // Retrieve the existing product from the database
                    product = context.Products.FirstOrDefault(p => p.PID == id);

                    if (product != null)
                    {
                        // Update the product details
                        product.PName = txtName.Text.Trim();
                        product.PPrice = float.Parse(tctPrice.Text.Trim());
                        product.CategoryID = Convert.ToInt32(cbCat.SelectedValue);
                        product.PImage = imageByteArray;
                    }
                    else
                    {
                        guna2MessageDialog1.Show("Product not found.");
                        return;
                    }
                }

                // Save changes to the database
                if (context.SaveChanges() > 0)
                {
                    guna2MessageDialog1.Show("Saved successfully.");

                    // Reset the form fields
                    id = 0;
                    txtName.Text = "";
                    tctPrice.Text = "";
                    cbCat.SelectedIndex = -1;
                    txtImage.Image = RestarauntGui.Properties.Resources.Restaurant;
                    txtName.Focus();
                }
                else
                {
                    guna2MessageDialog1.Show("An error occurred while saving.");
                }
            }
        }
    }
}
