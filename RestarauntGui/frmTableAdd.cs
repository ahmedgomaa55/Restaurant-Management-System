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
    public partial class frmTableAdd : Form
    {
        public frmTableAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                // Retrieve the table name from a TextBox (e.g., txtCategoryName)
                string tableName = txtName.Text.Trim();

                // Ensure the table name is not empty
                if (string.IsNullOrEmpty(tableName))
                {
                    MessageBox.Show("Table name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a new table object
                Table newTable = new Table
                {
                    Name = tableName
                    // Add other properties if needed
                };

                // Add the new table to the context
                bool tableExists = context.Tables.Any(c => c.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));

                if (!tableExists)
                    context.Tables.Add(newTable);

                // Save changes to the database
                context.SaveChanges();
                // update the DataGridView or other controls to reflect the new table
                MessageBox.Show("Saved successfully.");

                // clear the input field after saving
                txtName.Text = string.Empty;
                txtName.Focus();

                // refresh the DataGridView to show the new data
                frmCategoryView frm = new frmCategoryView();
                frm.GetData();  // Call the GetData() method to reload the data
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
