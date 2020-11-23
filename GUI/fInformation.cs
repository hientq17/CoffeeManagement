using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement {
    public partial class fInformation : Form {

        Account account;

        private string username;

        public fInformation(string Username) {
            InitializeComponent();
            this.username = Username;
            if (username.Equals("admin")) {
                account = AccountDAO.Instance.GetAdmin();
            } else {
                account =AccountDAO.Instance.GetAccount(username);
            }
            loadData();
        }

        private void loadData() {
            txtEmployeeUser.Text = account.Username;
            txtEmployeeName.Text = account.EmpName;
        }

        private void btnEmployeeOK_Click(object sender, EventArgs e) {
            bool stt = false;
            string empName = txtEmployeeName.Text;
            if (empName.Length == 0) {
                MessageBox.Show("Vui lòng nhập tên!","Chưa nhập tên",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtEmployeeName;
                return;
            }
            if (txtNewPassword.Enabled == true) {
                string newPass = txtNewPassword.Text;
                if (newPass.Length < 8) {
                    MessageBox.Show("Mật khẩu phải bao gồm 8 kí tự trở lên!", "Mật khẩu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ActiveControl = txtNewPassword;
                    return;
                }
                if (!txtRepeatPassword.Text.Equals(newPass)) {
                    MessageBox.Show("Vui lòng nhập lại mật khẩu trùng với mật khẩu!", "Mật khẩu không trùng nhau",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ActiveControl = txtRepeatPassword;
                    return;
                }
                if (username.Equals("admin")){
                    stt = AccountDAO.Instance.UpdateAdmin(empName, newPass);
                } else {
                    stt = AccountDAO.Instance.UpdateAccount(username,newPass,empName);
                }

            } else {
                if (username.Equals("admin")) {
                    stt = AccountDAO.Instance.UpdateAdmin(empName);
                } else {
                    stt = AccountDAO.Instance.UpdateAccount(username, account.Password, empName);
                }
            }
            if (stt) {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOldPassword.Text = "";
                txtNewPassword.Text = "";
                txtRepeatPassword.Text = "";
                txtRepeatPassword.Enabled = false;
                txtNewPassword.Enabled = false;
            } else {
                MessageBox.Show("Có lỗi xảy xa!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEmployeeCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnCheck_Click(object sender, EventArgs e) {
            if (!txtOldPassword.Text.Equals(account.Password)) {
                MessageBox.Show("Vui lòng nhập lại mật khẩu!", "Mật khẩu không đúng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtRepeatPassword.Enabled = true;
            txtNewPassword.Enabled = true;
        }
    }
}
