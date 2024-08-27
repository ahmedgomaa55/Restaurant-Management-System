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
    public partial class frmStaffView : Form
    {
        public frmStaffView()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public void GetData()
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                var data = context.Staff
                    .Where(c => string.IsNullOrEmpty(txtSearch.Text) || c.SName.Contains(txtSearch.Text)).ToList();

                // Set the DataSource of the DataGridView to the retrieved data
                staffgridview.DataSource = data;

                // Set the DataPropertyName for specific columns
                staffgridview.Columns["dgvid"].DataPropertyName = "StaffID";  // 'Id' is a property in the Categories class
                staffgridview.Columns["dgvName"].DataPropertyName = "SName";  // 'Name' is a property in the Categories class
                staffgridview.Columns["dgvPhone"].DataPropertyName = "SPhone";  // 'Phone' is a property in the Categories class
                staffgridview.Columns["dgvRole"].DataPropertyName = "Category";  // 'Role' is a property in the Categories class
            }
        }
        private void frmStaffView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmStaffAdd frm = new frmStaffAdd();
            frm.ShowDialog();
            GetData();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();

        }

 

        private void staffgridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (staffgridview.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
                {


                    using (RestaurantContext context = new RestaurantContext())
                    {

                        // Get the ID of the selected category
                        int staffId = Convert.ToInt32(staffgridview.CurrentRow.Cells["dgvid"].Value);

                        // Find the category to remove
                        var removedCat = context.Staff.FirstOrDefault(c => c.StaffID == staffId);

                        // Check if the category was found
                        if (removedCat != null)
                        {
                            // Remove the category from the context
                            context.Staff.Remove(removedCat);

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

        private void staffgridview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;
            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
    }
}
