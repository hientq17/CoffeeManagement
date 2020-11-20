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
            LoadTable();
            this.username = username;
            autoRefresh();
        }

        #region method

        private void LoadTable() {
            flpTable.Controls.Clear();
            List<CoffeeTable> tableList = CoffeeTableDAO.Instance.LoadTableList();
            foreach (CoffeeTable item in tableList) {
                if (item.Status.Equals("Đang đợi")) {

                    Button btn = new Button() { Width = CoffeeTableDAO.TableWidth, Height = CoffeeTableDAO.TableHeight };
                    btn.Text = item.ID + Environment.NewLine + item.Status;
                    btn.Click += btnTable_Click;
                    btn.Tag = item;
                    btn.BackColor = Color.LightPink;
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

        public void autoRefresh()
        {
            Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 5000;//2 seconds
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadTable();
        }
        
        #endregion
        #region event

        private void btnTable_Click(object sender, EventArgs e) {

            int tableID = ((sender as Button).Tag as CoffeeTable).ID;
            string status = ((sender as Button).Tag as CoffeeTable).Status;
            ShowBill(tableID);

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnHoanThanh_Click(object sender, EventArgs e) {
            if (listViewInvoice.Items.Count < 1)
            {
                MessageBox.Show("Chọn bàn!!!");
                return;
            }
            int tableID = Convert.ToInt32(lbTable.Text.Substring(4));

            int invoiceId = InvoiceDAO.Instance.GetUncheckoutInvoiceIDByTableID(tableID);

            if (CoffeeTableDAO.Instance.UpdateTable(tableID) != false &&
            InvoiceDAO.Instance.UpdateInvoice(invoiceId) != false) {
                MessageBox.Show("Hoàn thành bill!!");
                LoadTable();
                listViewInvoice.Items.Clear();
            }
        }
        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccount f = new fAccount(username);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void fKitchen_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn muốn đăng xuất ?", "Title", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.No)
                e.Cancel = true;

        }
        #endregion

    }
}
