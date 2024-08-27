using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestarauntGui
{
    public partial class ucProduct : UserControl
    {
        public ucProduct()
        {
            InitializeComponent();
        }
        public int ID { get; set; }
        public string PPrice { get; set; }
        public event EventHandler onSelect=null;
        public string PCategory { get; set; }
        public string PName
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Image PImage
        {
            get { return guna2PictureBox1.Image; }
            set { guna2PictureBox1.Image = value; }
        }








        private void ucProduct_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);
        }
    }
}
