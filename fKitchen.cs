using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using MySql.Data.MySqlClient;
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
    public partial class fKitchen : Form {
        private string username;

        private void flpTable_Paint(object sender, PaintEventArgs e) {

        }
        public fKitchen(string username) {
            InitializeComponent();
            LoadTableByCategory("Đang đợi");
            this.username = username;
        }

        #region method


        private void LoadTableByCategory(string category) {

            flpTable.Controls.Clear();
            List<CoffeeTable> tableList = CoffeeTableDAO.Instance.LoadTableList();
            foreach (CoffeeTable item in tableList) {
                if (item.Status.Equals(category)) {
                    Button btn = new Button() { Width = CoffeeTableDAO.TableWidth, Height = CoffeeTableDAO.TableHeight };
                    btn.Text = item.ID + Environment.NewLine + item.Status;
                    btn.Click += btnTable_Click;
                    btn.Tag = item;
                    btn.BackColor = (item.Status.Equals("Trống")) ? Color.Aqua : Color.LightPink;
                    flpTable.Controls.Add(btn);
                }
            }

        }

        private void ShowBill(int id) {
            lbTable.Text = "Bàn " + id;
            int invoiceID = InvoiceDAO.Instance.GetUncheckoutInvoiceIDByTableID(id);
            listViewInvoice.Items.Clear();
            List<InvoiceDetail> listInvoiceDetail = InvoiceDetailDAO.Instance.GetListInvoiceDetailByTable(invoiceID);
            foreach (InvoiceDetail item in listInvoiceDetail) {
                ListViewItem lsvItem = new ListViewItem(item.ProductName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                lsvItem.SubItems.Add(item.ProductId.ToString());
                listViewInvoice.Items.Add(lsvItem);
            }

        }


        private void btnTable_Click(object sender, EventArgs e) {

            int tableID = ((sender as Button).Tag as CoffeeTable).ID;
            string status = ((sender as Button).Tag as CoffeeTable).Status;
            ShowBill(tableID);

        }



        private void btnCheckOut_Click(object sender, EventArgs e) {

            int tableId = int.Parse(lbTable.Text.Substring(3));
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            ConnectDB.Instance.ExecuteNonQuery("update CoffeeTable set tableStatus = 1 where tableId = '" + tableId + "'");
            ConnectDB.Instance.ExecuteNonQuery("Insert into Invoice (tableId,dateSale,employeeUser, invoiceStatus) values (" + tableId + ", '" + date + "','" + this.Text.Substring(6) + "'," + 1 + ")");
            int invoiceId = InvoiceDAO.Instance.GetUncheckoutInvoiceIDByTableID(tableId);
            /* insertInvoiceDetail(invoiceId);*/
            /* LoadTable();*/
            listViewInvoice.Items.Clear();
            lbTable.Text = "";

        }

        private void listViewInvoice_SelectedIndexChanged(object sender, EventArgs e) {

        }
        #endregion



        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e) {
            DialogResult dr = MessageBox.Show("Bạn muốn đăng xuất ?", "Title", MessageBoxButtons.YesNo,
       MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
                Environment.Exit(0);
        }

        private void btnHoanThanh_Click(object sender, EventArgs e) {
            int tableID = Convert.ToInt32(lbTable.Text.Substring(4));

            int invoiceId = InvoiceDAO.Instance.GetUncheckoutInvoiceIDByTableID(tableID);

            if (CoffeeTableDAO.Instance.UpdateTable(tableID) != false &&
            InvoiceDAO.Instance.UpdateInvoice(invoiceId) != false) {
                MessageBox.Show("Hoan thanh bill");
                LoadTableByCategory("Đang đợi");
            }
        }
    }
}
