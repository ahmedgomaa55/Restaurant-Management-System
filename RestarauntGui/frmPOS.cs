using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant;

namespace RestarauntGui
{
    public partial class frmPOS : Form
    {
        RestaurantContext context = new RestaurantContext();

        public frmPOS()
        {
            InitializeComponent();
        }

        public int mainID = 0;
        public string OrderType;

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();
            Productpanal.Controls.Clear();
            LoadProducts();
        }
        void AddCategory()
        {
            categorypanal.Controls.Clear();

            if (context.Categories.Count() > 0)
                foreach (var item in context.Categories)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(149, 45);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = item.Name;
                    b.Click += new EventHandler(_Click);
                    categorypanal.Controls.Add(b);
                }
        }

        private void _Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;

            if (b.Text == "All Categories")
            {
                txtSearch.Text = "1";
                txtSearch.Text = "";
                return;
            }

            foreach (var item in Productpanal.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        void AddItems(string id, String proID, string name, string cat, string price, Image image)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = image,
                ID = Convert.ToInt32(proID),
            };
            Productpanal.Controls.Add(w);
            w.onSelect += (s, e) =>
             {
                 var wdg = (ucProduct)s;
                 foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                 {
                     if (Convert.ToInt32(item.Cells["ProID"].Value) == wdg.ID)
                     {
                         item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                         item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                            double.Parse(item.Cells["dgvprice"].Value.ToString());
                         return;
                     }


                 }
                 guna2DataGridView1.Rows.Add(new object[] { 0, 0, wdg.ID, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                 GetTotal();
             };
        }
        void LoadProducts()
        {
            var products = context.Products;
            foreach (var item in products)
            {
                Byte[] imagarray = (byte[])item.PImage;
                byte[] immagbytearray = imagarray;
                AddItems("0", item.PID.ToString(), item.PName.ToString(), item.Category.Name.ToString(), item.PPrice.ToString()
                    , Image.FromStream(new MemoryStream(imagarray)));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in Productpanal.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }



        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotal()
        {
            double tot = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }
            lblTotal.Text = tot.ToString("N2");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            mainID = 0;
            lblTotal.Text = "00";

        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Delivery";
        }

        private void btnTakeAway_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Take Away";
        }

        private void btnDinIn_Click(object sender, EventArgs e)
        {
            OrderType = "Din IN";
            // create a form for table selection and waiter selection

            frmTableSelect frm = new frmTableSelect();
            frm.ShowDialog();
            if (frm.TableName != "")
            {
                lblTable.Text = frm.TableName;
                lblTable.Visible = true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;

            }

            frmWaiterSelect frm2 = new frmWaiterSelect();
            frm2.ShowDialog();
            if (frm2.WaiterName != "")
            {
                lblWaiter.Text = frm2.WaiterName;
                lblWaiter.Visible = true;
            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;

            }
        }

        private void btnKot_Click(object sender, EventArgs e)
        {
            // Save the data in database 

            int detailID = 0;

            using (RestaurantContext context = new RestaurantContext())
            {


                if (mainID == 0)  // Insert into tblMain
                {
                    tblMain tableMain = new tblMain()
                    {
                        //MainID = mainID,
                        aDate = Convert.ToDateTime(DateTime.Now.Date),
                        aTime = DateTime.Now.ToShortTimeString(),
                        TableName = lblTable.Text,
                        WaiterName = lblWaiter.Text,
                        status = "Pending",
                        orderType = OrderType,
                        Total = Convert.ToDouble(0),
                        received = Convert.ToDouble(0),
                        change = Convert.ToDouble(0)
                    };

                    context.TableMain.Add(tableMain);


                }

                else    // Update
                {
                    var tableMainUpdated = context.TableMain.Where(t => t.MainID == mainID).Select(t => t).FirstOrDefault();
                    tableMainUpdated.status = "Pending";
                    tableMainUpdated.Total = Convert.ToDouble(0);
                    tableMainUpdated.received = Convert.ToDouble(0);
                    tableMainUpdated.change = Convert.ToDouble(0);
                }

                context.SaveChanges();
                mainID = context.TableMain.OrderByDescending(m => m.MainID).Select(c => c.MainID).FirstOrDefault();



                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    detailID = Convert.ToInt32(row.Cells["dgvid"].Value);

                    if (detailID == 0)  // insert into tbleDetails
                    {
                        tblDetails tbl1 = new tblDetails()
                        {
                            DetailID = detailID,
                            MainID = mainID,
                            proID = Convert.ToInt32(row.Cells["ProID"].Value),
                            qty = Convert.ToInt32(row.Cells["dgvQty"].Value),
                            price = Convert.ToDouble(row.Cells["dgvprice"].Value),
                            amount = Convert.ToDouble(row.Cells["dgvAmount"].Value)

                        };
                        context.TableDetails.Add(tbl1);
                    }
                    else  // update 
                    {
                        var tbl2 = context.TableDetails.Where(t => t.DetailID == detailID).Select(t => t).FirstOrDefault();
                        tbl2.proID = Convert.ToInt32(row.Cells["ProID"].Value);
                        tbl2.qty = Convert.ToInt32(row.Cells["dgvQty"].Value);
                        tbl2.price = Convert.ToDouble(row.Cells["dgvprice"].Value);
                        tbl2.amount = Convert.ToDouble(row.Cells["dgvAmount"].Value);
                    }
                }

                context.SaveChanges();
                guna2MessageDialog1.Show("Saved Sucessfully");

                mainID = 0;
                detailID = 0;
                guna2DataGridView1.Rows.Clear();
                lblTable.Text = "";
                lblWaiter.Text = "";
                lblTable.Visible = false;
                lblWaiter.Visible = false;
                lblTotal.Text = "00";

            }


        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public int id = 0;
        private void btnBillList_Click(object sender, EventArgs e)
        {
            frmBillList frm = new frmBillList();
            frm.ShowDialog();

            if (frm.MainID > 0)
            {
                id = frm.MainID;
                LoadEntries();
            }
        }


        private void LoadEntries()
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                var query = (from m in context.TableMain
                            join d in context.TableDetails on m.MainID equals d.MainID
                            join p in context.Products on d.proID equals p.PID
                            where m.MainID == id
                            select new
                            {
                                m,
                               p,
                                d
                            }).ToList();
                if (query[0].m.orderType.ToString() == "Delivery")
                {
                    btnDelivery.Checked = true;
                    lblWaiter.Visible = false;
                    lblTable.Visible = false;

                }
                else if(query[0].m.orderType.ToString() == "Take away")
                {
                    btnTakeAway.Checked = true;
                    lblWaiter.Visible = false;
                    lblTable.Visible = false;

                }
                else
                {
                    btnDinIn.Checked = true;
                    lblWaiter.Visible = true;
                    lblTable.Visible = true;

                }

                guna2DataGridView1.Rows.Clear();

                foreach (var item in query)
                {
                    //object[] obj = { item.DetailID, item.proID, item.qty, item.price, item.Amount };
                    //guna2DataGridView1.Rows.Add(obj);
                    lblTable.Text = item.m.TableName.ToString();
                    lblWaiter.Text = item.m.WaiterName.ToString();


                    string detailid = item.d.DetailID.ToString();
                    string proName = item.p.PName.ToString();

                    string priod = item.p.PID.ToString();
                    string qty = item.d.qty.ToString();
                    string price = item.d.price.ToString();
                    string amount = item.d.amount.ToString();
                    object[] obj = { 0, detailid, priod, proName, qty, price, amount   };
                    guna2DataGridView1.Rows.Add(obj);

                }
                GetTotal();



            }
            
        }

        private void btncheckout_Click(object sender, EventArgs e)
        {
            frmCheckout frm = new frmCheckout();
            frm.MainID = id;
            frm.amt = Convert.ToDouble(lblTotal.Text);
            frm.Show();
            //MainClass
            mainID = 0;
            guna2DataGridView1.Rows.Clear();
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "00";
        }
    }
}
