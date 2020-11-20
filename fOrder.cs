using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class fOrder : Form
    {
        private List<CoffeeTable> tableList;
        private List<Product> listProduct;
        private List<ProductType> listProductType;
        private string username;
        public fOrder(string username)
        {
            InitializeComponent();
            LoadAllTableProduct();
            LoadTable();
            this.username = username;
            autoRefresh();
        }

        #region method
        private void LoadAllTableProduct()
        {
            tableList = CoffeeTableDAO.Instance.LoadTableList();
            listProductType = ProductDAO.Instance.getProductTypes();
            listProduct = ProductDAO.Instance.GetAllProduct();
        }
        private void LoadTable()
        {
            btnCancel.Visible = false;
            btnConfirm.Visible = false;
            lbTable.Visible = false;
            lbTotal.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            btnAdd.Visible = false;
            nmFoodCount.Visible = false;
            cbxCategory.Visible = false;
            flpTable.Controls.Clear();
            foreach (CoffeeTable item in tableList)
            {
                Button btn = new Button() { Width = CoffeeTableDAO.TableWidth, Height = CoffeeTableDAO.TableHeight };
                btn.Text = item.ID + Environment.NewLine + item.Status;
                btn.Click += btnTable_Click;
                btn.Tag = item;
                if (item.Status.Equals("Trống"))
                {
                    btn.BackColor = Color.Aqua;
                }
                else
                {
                    btn.BackColor = Color.LightPink;
                }
                flpTable.Controls.Add(btn);
            }

        }

        private void LoadTableRefresh()
        {
            btnCancel.Visible = false;
            btnConfirm.Visible = false;
            btnAdd.Visible = false;
            nmFoodCount.Visible = false;
            cbxCategory.Visible = false;
            flpTable.Controls.Clear();
            foreach (CoffeeTable item in tableList)
            {
                Button btn = new Button() { Width = CoffeeTableDAO.TableWidth, Height = CoffeeTableDAO.TableHeight };
                btn.Text = item.ID + Environment.NewLine + item.Status;
                btn.Click += btnTable_Click;
                btn.Tag = item;
                if (item.Status.Equals("Trống"))
                {
                    btn.BackColor = Color.Aqua;
                    listViewInvoice.Items.Clear();
                }
                else
                {
                    btn.BackColor = Color.LightPink;
                }
                flpTable.Controls.Add(btn);
            }

        }

        //private void LoadTableByCategory(string category)
        //{
        //    flpTable.Controls.Clear();
        //    foreach (CoffeeTable item in tableList)
        //    {
        //        if (item.Status.Equals(category))
        //        {
        //            Button btn = new Button() { Width = CoffeeTableDAO.TableWidth, Height = CoffeeTableDAO.TableHeight };
        //            btn.Text = item.ID + Environment.NewLine + item.Status;
        //            btn.Click += btnTable_Click;
        //            btn.Tag = item;
        //            btn.BackColor = (item.Status.Equals("Trống")) ? Color.Aqua : Color.LightPink;
        //            flpTable.Controls.Add(btn);
        //        }
        //    }
        //}

        private void ShowBill(int id)
        {
            lbTable.Text = "Bàn " + id;
            int invoiceID = InvoiceDAO.Instance.GetUncheckoutInvoiceIDByTableID(id);
            listViewInvoice.Items.Clear();
            List<InvoiceDetail> listInvoiceDetail = InvoiceDetailDAO.Instance.GetListInvoiceDetailByTable(invoiceID);
            foreach (InvoiceDetail item in listInvoiceDetail)
            {
                ListViewItem lsvItem = new ListViewItem(item.ProductName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                lsvItem.SubItems.Add(item.ProductId.ToString());
                listViewInvoice.Items.Add(lsvItem);
            }
            lbTotal.Text = InvoiceDAO.Instance.GetUncheckoutInvoiceTotalPaymentByTableID(id).ToString();
        }

        private void LoadMenu()
        {
            flpTable.Controls.Clear();
            cbxCategory.Visible = true;
            cbxCategory.Items.Clear();
            foreach (ProductType item in listProductType)
            {
                cbxCategory.Items.Add(item.ProductTypeName);
            }
            foreach (Product item in listProduct)
            {
                FlowLayoutPanel panel = new FlowLayoutPanel() { Width = ProductDAO.ProductWidth, Height = ProductDAO.ProductHeight+40 };
                Button btn = new Button() { Width = ProductDAO.ProductWidth, Height = ProductDAO.ProductHeight };
                Label label = new Label() { Width = ProductDAO.ProductWidth, Height = 40 };
                label.Text = item.ProductName + Environment.NewLine + item.UnitPrice + " VND";
                byte[] img = item.ProductImg;
                MemoryStream ms = new MemoryStream(img);
                btn.Image = Image.FromStream(ms);
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.Click += btnProduct_Click;
                btn.Tag = item;
                panel.Controls.Add(btn);
                panel.Controls.Add(label);
                flpTable.Controls.Add(panel);
            }
        }

        private void LoadMenuByCategory(string category)
        {
            flpTable.Controls.Clear();
            foreach (Product item in listProduct)
            {
                string typeName = ProductDAO.Instance.getTypeNameByTypeId(item.TypeId);
                if (typeName.Equals(category))
                {
                    FlowLayoutPanel panel = new FlowLayoutPanel() { Width = ProductDAO.ProductWidth, Height = ProductDAO.ProductHeight + 40 };
                    Button btn = new Button() { Width = ProductDAO.ProductWidth, Height = ProductDAO.ProductHeight };
                    Label label = new Label() { Width = ProductDAO.ProductWidth, Height = 40 };
                    label.Text = item.ProductName + Environment.NewLine + item.UnitPrice + " VND";
                    byte[] img = item.ProductImg;
                    MemoryStream ms = new MemoryStream(img);
                    btn.Image = Image.FromStream(ms);
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.TextAlign = ContentAlignment.BottomCenter;
                    btn.Click += btnProduct_Click;
                    btn.Tag = item;
                    panel.Controls.Add(btn);
                    panel.Controls.Add(label);
                    flpTable.Controls.Add(panel);
                }
            }
        }

        private void insertInvoiceDetail(int invoiceId)
        {
            int productId, productAmount;
            foreach (ListViewItem item in listViewInvoice.Items)
            {
                productAmount = int.Parse(item.SubItems[1].Text);
                productId = int.Parse(item.SubItems[4].Text);
                MySqlCommand cmd = new MySqlCommand("insert_InvoiceDetail", new ConnectDB().OpenConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@invoiceId", invoiceId));
                cmd.Parameters.Add(new MySqlParameter("@productId", productId));
                cmd.Parameters.Add(new MySqlParameter("@productAmount", productAmount));
                cmd.ExecuteNonQuery();
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
            if (!cbxCategory.Visible)
            {
                tableList = CoffeeTableDAO.Instance.LoadTableList();
                LoadTableRefresh();
            } 
        }
        #endregion
        #region event
        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccount f = new fAccount(username);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listViewInvoice.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn món!!!");
                return;
            }
            double count = (double)nmFoodCount.Value;
            if (count == 0)
            {
                listViewInvoice.Items.Remove(listViewInvoice.SelectedItems[0]);
                return;
            }
            double unitPrice = double.Parse(listViewInvoice.SelectedItems[0].SubItems[2].Text);
            double totalPayment = 0;
            listViewInvoice.SelectedItems[0].SubItems[1].Text = count.ToString();
            listViewInvoice.SelectedItems[0].SubItems[3].Text = (count * unitPrice).ToString();
            foreach (ListViewItem item in listViewInvoice.Items)
            {
                totalPayment += double.Parse(item.SubItems[3].Text);
            }
            lbTotal.Text = totalPayment.ToString();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {

            int tableID = ((sender as Button).Tag as CoffeeTable).ID;
            string status = ((sender as Button).Tag as CoffeeTable).Status;
            lbTable.Visible = true;
            lbTotal.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            ShowBill(tableID);
            if (status.Equals("Trống"))
            {
                LoadMenu();
                btnConfirm.Visible = true;
                btnCancel.Visible = true;
                btnAdd.Visible = true;
                nmFoodCount.Visible = true;
                nmFoodCount.Value = 0;
                cbxCategory.Visible = true;
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {

            string productName = ((sender as Button).Tag as Product).ProductName;
            double unitPrice = ((sender as Button).Tag as Product).UnitPrice;
            int ID = ((sender as Button).Tag as Product).ProductID;
            double total = unitPrice * 1;
            double totalPayment = 0;
            ListViewItem lsvItem = new ListViewItem(productName);
            if (listViewInvoice.Items.ContainsKey(productName))
            {
                int count = int.Parse(listViewInvoice.Items[productName].SubItems[1].Text) + 1;
                double price = double.Parse(listViewInvoice.Items[productName].SubItems[2].Text);
                listViewInvoice.Items[productName].SubItems[1].Text = count.ToString();
                listViewInvoice.Items[productName].SubItems[3].Text = (count * price).ToString();
            }
            else
            {
                lsvItem.Name = productName;
                lsvItem.SubItems.Add(1.ToString());
                lsvItem.SubItems.Add(unitPrice.ToString());
                lsvItem.SubItems.Add(total.ToString());
                lsvItem.SubItems.Add(ID.ToString());
                listViewInvoice.Items.Add(lsvItem);
            }

            foreach (ListViewItem item in listViewInvoice.Items)
            {
                totalPayment += double.Parse(item.SubItems[3].Text);
            }
            lbTotal.Text = totalPayment.ToString();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {

            int tableId = int.Parse(lbTable.Text.Substring(3));
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            ConnectDB.Instance.ExecuteNonQuery("update CoffeeTable set tableStatus = 1 where tableId = '" + tableId + "'");
            ConnectDB.Instance.ExecuteNonQuery("Insert into Invoice (tableId,dateSale,employeeUser, invoiceStatus) values (" + tableId + ", '" + date + "','" + username + "'," + 1 + ")");
            int invoiceId = InvoiceDAO.Instance.GetUncheckoutInvoiceIDByTableID(tableId);
            insertInvoiceDetail(invoiceId);
            tableList = CoffeeTableDAO.Instance.LoadTableList();
            LoadTable();
            listViewInvoice.Items.Clear();
            lbTable.Text = "";
            lbTotal.Text = "0";
        }

        private void listViewInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewInvoice.SelectedItems.Count > 0)
            {
                nmFoodCount.Value = decimal.Parse(listViewInvoice.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (listViewInvoice.Items.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát? Mọi thay đổi sẽ bị hủy!", "Xác nhận hủy order", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }
            LoadTable();
            listViewInvoice.Items.Clear();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cbxCategory.SelectedItem.ToString();
            LoadMenuByCategory(category);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (listViewInvoice.Items.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất? Mọi thay đổi sẽ bị hủy!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn muốn đăng xuất ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.No)
                    e.Cancel = true;
            }

        }
        #endregion
    }
}
