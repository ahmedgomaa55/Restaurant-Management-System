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
    public partial class frmCheckout : Form
    {
        public frmCheckout()
        {
            InitializeComponent();
        }
        RestaurantContext context = new RestaurantContext();
        public double amt;
        public int MainID=0;


        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtReceived_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double change = 0;
            double receipt = 0;
            double.TryParse(txtBillAmount.Text, out amt);
            double.TryParse(txtReceived.Text, out receipt);
            change = amt - receipt;
            txtChanged.Text = change.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             var bill=context.TableMain.FirstOrDefault(c => c.MainID == MainID);
            bill.Total = Convert.ToDouble(txtBillAmount.Text);
            bill.received = Convert.ToDouble(txtReceived.Text);
            bill.change = Convert.ToDouble(txtChanged.Text);
            bill.status = "Paid";

            var x = context.SaveChanges();
            if (x > 0)
            {

                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("saved successfully");
                this.Close();
            }


        }

        private void frmCheckout_Load(object sender, EventArgs e)
        {
            txtBillAmount.Text = amt.ToString();
        }
    }
}
