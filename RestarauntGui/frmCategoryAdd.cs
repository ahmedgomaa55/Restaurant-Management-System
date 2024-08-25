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
    public partial class frmCategoryAdd : Form
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                // Retrieve the category name from a TextBox (e.g., txtCategoryName)
                string categoryName = txtName.Text.Trim();

                // Ensure the category name is not empty
                if (string.IsNullOrEmpty(categoryName))
                {
                    MessageBox.Show("Category name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a new Category object
                Category newCategory = new Category
                {
                    Name = categoryName
                    // Add other properties if needed
                };

                // Add the new category to the context
                bool categoryExists = context.Categories.Any(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

                if (!categoryExists)
                context.Categories.Add(newCategory);

                // Save changes to the database
                context.SaveChanges();
                // update the DataGridView or other controls to reflect the new category
                MessageBox.Show("Saved successfully.");

                // clear the input field after saving
                txtName.Text = string.Empty;
                txtName.Focus();

                // refresh the DataGridView to show the new data
                frmCategoryView frm = new frmCategoryView();
                frm.GetData() ;  // Call the GetData() method to reload the data
                }
            

            }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    }

