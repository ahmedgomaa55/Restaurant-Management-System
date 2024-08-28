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
    public partial class frmBillList : Form
    {
        public frmBillList()
        {
            InitializeComponent();
        }
        public int MainID = 0;
        private void frmBillList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                // Retrieve the data using a LINQ query
                var data = context.TableMain
                             .Where(o => o.status != "Pending")
                             .Select(o => new
                             {
                                 o.MainID,
                                 o.TableName,
                                 o.WaiterName,
                                 o.orderType,
                                 o.status,
                                 o.Total
                             })
                             .ToList();


                BillGrid.DataSource = data;

                // Set the DataPropertyName for specific columns
                BillGrid.Columns["dgvid"].DataPropertyName = "MainID";  // 'Id' is a property in the Categories class
                BillGrid.Columns["dgvTable"].DataPropertyName = "TableName";  // 'Name' is a property in the Categories class
                BillGrid.Columns["dgvWaiter"].DataPropertyName = "WaiterName";  // 'Phone' is a property in the Categories class
                BillGrid.Columns["dgvType"].DataPropertyName = "orderType";  // '
                BillGrid.Columns["dgvStatus"].DataPropertyName = "status";  // '
                BillGrid.Columns["dgvTotal"].DataPropertyName = "Total";
            }

        }

        private void BillGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in BillGrid.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }


        private void BillGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (BillGrid.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                MainID = Convert.ToInt32(BillGrid.CurrentRow.Cells["dgvid"].Value);
                this.Close();               
                    

                }
            }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    }
