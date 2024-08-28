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
    public partial class frmWaiterSelect : Form
    {
        public frmWaiterSelect()
        {
            InitializeComponent();
        }

        public string WaiterName;
        private void frmWaiterSelect_Load(object sender, EventArgs e)
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                var waiters = context.Staff.Where(s=>s.SRole=="Waiter").Select(w=>w);

                foreach (var item in waiters)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.Text = item.SName.ToString();
                    b.Width = 150;
                    b.Height = 50;
                    b.FillColor = Color.FromArgb(241, 85, 126);
                    b.HoverState.FillColor = Color.FromArgb(50, 55, 89);
                    b.Click += new EventHandler(b_Click);
                    flowLayoutPanel1.Controls.Add(b);
                }
            }
        }


        private void b_Click(object sender, EventArgs e)
        {
            WaiterName = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }
    }
}
