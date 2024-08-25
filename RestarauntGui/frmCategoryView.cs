using Restaurant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestarauntGui
{
    public partial class frmCategoryView : Form
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                var data = context.Categories.Where(c => string.IsNullOrEmpty(txtSearch.Text) || c.Name.Contains(txtSearch.Text)).ToList();

                // Set the DataSource of the DataGridView to the retrieved data
                guna2DataGridView1.DataSource = data;

                // If you need to manually set the DataPropertyName for specific columns:
                guna2DataGridView1.Columns["dgvid"].DataPropertyName = "Id";  //  'Id' is a property in the Categories class
                guna2DataGridView1.Columns["dgvName"].DataPropertyName = "Name";  // 'Name' is a property in the Categories class
            }
        }
        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCategoryAdd frm = new frmCategoryAdd();
            frm.ShowDialog();
            GetData();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

                if (guna2DataGridView1.CurrentCell.OwningColumn.Name=="dgvdel")
            {
                using (RestaurantContext context = new RestaurantContext())
                {

                    // Get the ID of the selected category
                    int categoryId = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                    // Find the category to remove
                    var removedCat = context.Categories.FirstOrDefault(c => c.ID == categoryId);

                    // Check if the category was found
                    if (removedCat != null)
                    {
                        // Remove the category from the context
                        context.Categories.Remove(removedCat);

                        // Save changes to the database
                        context.SaveChanges();

                        MessageBox.Show("Deleted Successfully");

                        // Refresh the DataGridView 
                        GetData();
                    }
                }
                
            }
        }
    }
}
