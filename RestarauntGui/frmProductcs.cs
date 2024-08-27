using Guna.UI2.WinForms;
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
    public partial class frmProductcs : Form
    {
        public frmProductcs()
        {
            InitializeComponent();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductAdd frm = new frmProductAdd();
            frm.ShowDialog();
            GetData();
        }

        public void GetData()
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                // Retrieve the data using a LINQ query
                var data = context.Products
    .Where(p => string.IsNullOrEmpty(txtSearch.Text) || p.PName.Contains(txtSearch.Text))
    .Select(p => new
    {
        p.PID,
        p.PName,
        p.PPrice,
        CategoryID = p.Category.ID,
        CategoryName = p.Category.Name
    })
    .ToList();


                productgrid.DataSource = data;

                // Set the DataPropertyName for specific columns
                productgrid.Columns["dgvid"].DataPropertyName = "PID";  // 'Id' is a property in the Categories class
                productgrid.Columns["dgvName"].DataPropertyName = "PName";  // 'Name' is a property in the Categories class
                productgrid.Columns["dgvPrice"].DataPropertyName = "PPrice";  // 'Phone' is a property in the Categories class
                productgrid.Columns["dgvcatID"].DataPropertyName = "CategoryID";  // '
                productgrid.Columns["dgvCat"].DataPropertyName = "CategoryName";  // '

            }
        }

        private void frmProductcs_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void productgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
        private void productgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productgrid.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
                {


                    using (RestaurantContext context = new RestaurantContext())
                    {

                        // Get the ID of the selected category
                        int prodID = Convert.ToInt32(productgrid.CurrentRow.Cells["dgvid"].Value);

                        // Find the category to remove
                        var removedprod = context.Products.FirstOrDefault(c => c.PID == prodID);

                        // Check if the category was found
                        if (removedprod != null)
                        {
                            // Remove the category from the context
                            context.Products.Remove(removedprod);

                            // Save changes to the database
                            context.SaveChanges();

                            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                            guna2MessageDialog1.Show("Deleted Successfully");

                            // Refresh the DataGridView 
                            GetData();
                        }
                    }
                }
            }
        }

        private void productgrid_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;
            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
