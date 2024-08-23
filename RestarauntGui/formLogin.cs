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
    public partial class formLogin : Form
    {
        private readonly UserService _userService;

        public formLogin()
        {
            InitializeComponent();
            RestaurantContext context = new RestaurantContext();
            _userService = new UserService(context);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool isValid = _userService.isValidUser(username, password);
            if (isValid)
            {
                this.Hide();
                formMain frm = new formMain();
                frm.Show();
            }
            else
            {
                guna2MessageDialog1.Show("Invalid Username or Password!");
                return;
            }
        }
    }
}
