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
    public partial class frmKitchenView : Form
    {
        RestaurantContext context = new RestaurantContext();
        public frmKitchenView()
        {
            InitializeComponent();
        }

        private void frmKitchenView_Load(object sender, EventArgs e)
        {
            GetOrder();

        }
        private void GetOrder()
        {
            flowLayoutPanel1.Controls.Clear();
            //query from tblmain to call table name and time type and loop on
            var tabls = context.TableMain.Where(t=>t.status=="Pending");
            FlowLayoutPanel p1;
            foreach (var item in tabls)
            {
            //    for (int y = 0; y < 3; y++)
            //{

            
            
                p1 = new FlowLayoutPanel();
                p1.AutoSize = true;
                p1.Width = 230;
                p1.Height = 350;
                p1.FlowDirection = FlowDirection.TopDown;
                p1.BorderStyle = BorderStyle.FixedSingle;
                p1.Margin = new Padding(10, 10, 10, 10);


                FlowLayoutPanel p2 = new FlowLayoutPanel();
                p2 = new FlowLayoutPanel();
                p2.BackColor = Color.FromArgb(50, 55, 89);
                p2.AutoSize = true;
                p2.Width = 230;
                p2.Height = 125;
                p2.FlowDirection = FlowDirection.TopDown;
                p2.Margin = new Padding(0, 0, 0, 0);

                Label lb1 = new Label();
                lb1.ForeColor = Color.White;
                lb1.Margin = new Padding(10, 10, 3, 0);
                lb1.AutoSize = true;

                Label lb2 = new Label();
                lb2.ForeColor = Color.White;
                lb2.Margin = new Padding(10, 10, 3, 0);
                lb2.AutoSize = true;
                
                Label lb3 = new Label();
                lb3.ForeColor = Color.White;
                lb3.Margin = new Padding(10, 10, 3, 0);
                lb3.AutoSize = true;

                Label lb4 = new Label();
                lb4.ForeColor = Color.White;
                lb4.Margin = new Padding(10, 10, 3, 10);
                lb4.AutoSize = true;

                lb1.Text = "Table : "       + item.TableName;// table name from database 
                lb2.Text = "Waiter Name : " + item.WaiterName;// table name from database 
                lb3.Text = "Order Time  : " +item.aTime ;// table name from database 
                lb4.Text = "Order Type : "  + item.orderType;// table name from database 
                
                p2.Controls.Add(lb1);
                p2.Controls.Add(lb2);
                p2.Controls.Add(lb3);
                p2.Controls.Add(lb4);

                p1.Controls.Add(p2);
                flowLayoutPanel1.Controls.Add(p1);


                int mid = 0;
                mid = Convert.ToInt32(item.MainID.ToString());
                //var ProductTable=context.TableMain.Where(t=>t.MainID==(context.TableDetails.)
                //m.orderType,m.TableName,m.WaiterName,m.Total,m.aTime,d.proID
                var joi = (
                    from m in context.TableMain
                    join d in context.TableDetails on m.MainID equals d.MainID
                    join p in context.Products on d.proID equals p.PID
                    where m.MainID== mid
                    select new {d.qty, p.PName }
                    ).ToList();
                //var ProductTable
                for (int i = 0; i < joi.Count(); i++)
                {

                
                    Label lb5 = new Label();
                    lb5.BackColor = Color.White;
                    lb5.Margin = new Padding(10, 10, 3, 0);
                    lb5.AutoSize = true;
                    int no = i + 1;
                    lb5.Text = "" + no + " " + joi[i].PName.ToString() + " " + joi[i].qty.ToString();
                    p1.Controls.Add(lb5);
                }


                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.AutoRoundedCorners = true;
                b.Size = new Size(100, 35);
                b.FillColor = Color.FromArgb(241, 85, 126);
                b.Margin = new Padding(30, 5, 3, 10);
                b.Text = "Complete";
                //b.Tag =(1).ToString();
                b.Tag =item.MainID.ToString();


                b.Click += new EventHandler(B_Click);
                p1.Controls.Add(b);
                flowLayoutPanel1.Controls.Add(p1);




            }


        }

        private void B_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as Guna.UI2.WinForms.Guna2Button).Tag.ToString());

            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
            {
                var complteId= context.TableMain.Find(id);
                complteId.status = "Complete";
                var x=context.SaveChanges();
                if (x > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("saved successfully");

                }
                GetOrder();
            }
        }
    }
}
