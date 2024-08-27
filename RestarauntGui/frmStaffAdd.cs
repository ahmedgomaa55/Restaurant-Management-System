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
    public partial class frmStaffAdd : Form
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        private void frmStaffAdd_Load(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                // Retrieve the category name from a TextBox (e.g., txtCategoryName)
                string stafName = txtName.Text.Trim();
                string stafPhone = guna2TextBox1.Text.Trim();
                string stafRole = cbRole.Text.Trim();

                // Ensure the category name is not empty
                if (string.IsNullOrEmpty(stafName)&& string.IsNullOrEmpty(stafPhone) && string.IsNullOrEmpty(stafRole))
                {
                    MessageBox.Show("Staff Data cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if(string.IsNullOrEmpty(stafName) || string.IsNullOrEmpty(stafPhone) || string.IsNullOrEmpty(stafRole))
                {
                    MessageBox.Show("Make sure to enter all your data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Create a new staff object
                Staff newstaff = new Staff
                {
                    SName =  stafName,
                    SPhone=stafPhone,
                    SRole=stafRole

                };
                // Add the new staff to the context
                bool staffExists = context.Staff.Any(s =>
                          s.SName.Equals(stafName, StringComparison.OrdinalIgnoreCase) &&
                          s.SPhone.Equals(stafPhone, StringComparison.OrdinalIgnoreCase) &&
                          s.SRole.Equals(stafRole, StringComparison.OrdinalIgnoreCase)
                      );
                if (!staffExists)
                {
                    context.Staff.Add(newstaff);
                    context.SaveChanges();
                    MessageBox.Show("Saved successfully.");

                    // Clear the input fields after saving
                    txtName.Text = string.Empty;
                    guna2TextBox1.Text = string.Empty;
                    cbRole.SelectedIndex = -1;
                    txtName.Focus();

                    // Refresh the DataGridView or other controls to show the new data
                    frmStaffView frm = new frmStaffView();
                    frm.GetData();  // Call the GetData() method to reload the data
                }
                else
                {
                    MessageBox.Show("This staff member already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }

   

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
                this.Hide();
            
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
