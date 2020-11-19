using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thật sự muốn thoát chương trình?","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLogin.Text;
            string password = txtPassword.Text;
            switch(Login(username, password))
            {
                case 0:
                    fAdmin fadmin = new fAdmin();
                    fadmin.Text = "Hello Boss";
                    this.Hide();
                    fadmin.ShowDialog();
                    txtPassword.Text = "";
                    txtLogin.Text = "";                    
                    this.Show();
                    break;
                case 1:
                    fOrder forder = new fOrder(username);
                    forder.Text = "Hello "+username;
                    this.Hide();
                    forder.ShowDialog();
                    txtPassword.Text = "";
                    txtLogin.Text = "";     
                    this.Show();
                    break;
                default:
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
                    break;
            }
        }

        int Login(string username, string password)
        {
            return DAO.AccountDAO.Instance.Login(username, password);
        }
    }
}
