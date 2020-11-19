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
    
    public partial class fAccount : Form
    {
        private string username;
        public fAccount()
        {
            InitializeComponent();
        }

        public fAccount(string Username)
        {
            InitializeComponent();
            this.username = Username;
            txtLogin.Text = username;
            Account acc = new Account();
            acc = DAO.AccountDAO.Instance.GetAccount(username);
            txtName.Text = acc.EmpName;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string oldPass = txtPassword.Text;
            string username = txtLogin.Text;
            string verifyPass = DAO.AccountDAO.Instance.GetAccount(username).Password;
            string newPass = txtNewPass.Text;
            string newPassAgain = txtNewPassAgain.Text;
            string nickName = txtName.Text;
            if (oldPass.Equals(""))
                MessageBox.Show("Nhập mật khẩu");
            else if (string.Compare(oldPass, verifyPass) != 0)
                MessageBox.Show("Sai mật khẩu");
            else
            {
                if (string.Compare(newPass, newPassAgain) != 0)
                    MessageBox.Show("Mật khẩu không trùng khớp");
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn muốn cập nhật thông tin ?", "Title", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        if (newPass.Equals("") && newPassAgain.Equals(""))
                            if (DAO.AccountDAO.Instance.UpdateAccount(username, oldPass, nickName) != false)
                                MessageBox.Show("Update thành công!");
                            else
                                MessageBox.Show("Update thất bại!");
                        else
                        {
                            if (DAO.AccountDAO.Instance.UpdateAccount(username, newPass, nickName) != false)
                                MessageBox.Show("Update thành công!");
                            else
                                MessageBox.Show("Update thất bại!");
                        }
                        txtPassword.Clear(); txtNewPass.Clear(); txtNewPassAgain.Clear();
                    }

                }
            }
        }
    }
    
}
