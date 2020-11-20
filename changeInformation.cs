using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagement.DAO;
using CoffeeManagement.DTO;

namespace CoffeeManagement {
    public partial class changeInformation : Form {

        Account account = AccountDAO.Instance.GetAdmin();

        public changeInformation() {
            InitializeComponent();
            loadData();
        }

        private void loadData() {
            txtEmployeeUser.Text = account.Username;
            txtEmployeeName.Text = account.EmpName;
        }

        private void btnEmployeeOK_Click(object sender, EventArgs e) {
            string empName = txtEmployeeName.Text;
            if (empName.Length == 0) {
                MessageBox.Show("Vui lòng nhập tên");
                this.ActiveControl = txtEmployeeName;
                return;
            }
            if (txtNewPassword.Enabled == true) {
                string newPass = txtNewPassword.Text;
                if (newPass.Length < 8) {
                    MessageBox.Show("Mật khẩu phải có độ dài từ 8 kí tự trở lên");
                    this.ActiveControl = txtNewPassword;
                    return;
                }
                if (!txtRepeatPassword.Text.Equals(newPass)) {
                    MessageBox.Show("Nhập lại mật khẩu không trùng");
                    this.ActiveControl = txtRepeatPassword;
                    return;
                }
                if (AccountDAO.Instance.UpdateAdmin(empName, newPass)) {
                    MessageBox.Show("Cập nhật thông tin thành công!");
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtRepeatPassword.Text = "";
                    txtRepeatPassword.Enabled = false;
                    txtNewPassword.Enabled = false;
                }
            } else {
                if (AccountDAO.Instance.UpdateAdmin(empName)) {
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
            }
        }

        private void btnEmployeeCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnCheck_Click(object sender, EventArgs e) {
            if (!txtOldPassword.Text.Equals(account.Password)) {
                MessageBox.Show("Mật khẩu không đúng!");
                return;
            }
            txtRepeatPassword.Enabled = true;
            txtNewPassword.Enabled = true;
        }
    }
}
