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
    public partial class frmAddCustomer : Form
    {
        public frmAddCustomer()
        {
            InitializeComponent();
        }

        public string OrderType = "";
        public int driverID = 0;
        public string cusName = "";
        public int mainID = 0;
        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            if(OrderType=="Take Away")
            {
                lblDriver.Visible = false;
                cbDriver.Visible = false;
            }

            using (RestaurantContext context = new RestaurantContext())
            {
                var drivers = context.Staff.Where(s => s.SRole == "Driver").Select(d =>d).ToList();
                
                foreach (var item in drivers)
                {
                    cbDriver.Items.Add(item.SName);
                   }

                if (mainID > 0)
                {
                    cbDriver.SelectedValue = driverID;
                }
            }

        }

        private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            driverID = Convert.ToInt32(cbDriver.SelectedValue);
        }
    }
}
