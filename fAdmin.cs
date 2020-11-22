using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using MySql.Data.MySqlClient;
using System.IO;

namespace CoffeeManagement
{
    public partial class fAdmin : Form {

        List<Button> productButtons = new List<Button>();


        public fAdmin() {
            InitializeComponent();
            dataGridViewInvoice.Columns[2].DefaultCellStyle.Format = "dd-MM-yyyy";
            pbProductImage.Image = pbProductImage.InitialImage;
            loadInvoice();
            loadComboboxCashier();
            loadProducts();
            loadComboboxProductType();
            loadProductType();
            loadEmployees();
        }

        //Tab Invoice

        private void loadInvoice() {
            DataTable invoiceList = InvoiceDAO.Instance.getInvoices();
            dataGridViewInvoice.DataSource = invoiceList;
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePicker)sender).Name == "dateFrom") {
                dateFrom.CustomFormat = "dddd dd-MM-yyyy";
            } else {
                dateTo.CustomFormat = "dddd dd-MM-yyyy";
            }

        }

        private void loadComboboxCashier() {
            cbbEmployee.Items.Clear();
            cbbEmployee.Items.Add("Tất cả");
            cbbEmployee.SelectedIndex = 0;
            List<Account> employeeList = AccountDAO.Instance.getCashiers();
            foreach (Account emp in employeeList) {
                cbbEmployee.Items.Add(emp);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e) {
            string strDateFrom = "";
            string strDateTo = "";
            string employeeUser = "";
            if (dateFrom.CustomFormat != "NULL") {
                strDateFrom = dateFrom.Value.ToString("yyyy-MM-dd");
            }
            if (dateTo.CustomFormat != "NULL") {
                strDateTo = dateTo.Value.ToString("yyyy-MM-dd");
            }
            if (cbbEmployee.SelectedIndex > 0) {
                employeeUser = ((Account)cbbEmployee.SelectedItem).Username;
            }
            DataTable invoiceList = InvoiceDAO.Instance.searchInvoices(strDateFrom, strDateTo, employeeUser);
            dataGridViewInvoice.DataSource = invoiceList;
        }

        private void btnReset_Click(object sender, EventArgs e) {
            dateFrom.CustomFormat = "NULL";
            dateTo.CustomFormat = "NULL";
            cbbEmployee.SelectedIndex = 0;
            loadInvoice();
        }

        //Tab Product

        private void loadProducts() {
            panelProducts.Controls.Clear();
            List<ProductType> productTypeList = ProductDAO.Instance.getProductTypes();
            int xBox = 5;
            int yBox = 5;
            for (int i = 0; i < productTypeList.Count; i++) {
                //add groupbox for product type
                Size size = new Size(400, 170);
                GroupBox groupBox = new GroupBox();
                groupBox.Text = productTypeList[i].ProductTypeName;
                groupBox.Location = new Point(xBox, yBox);
                groupBox.Size = size;
                panelProducts.Controls.Add(groupBox);
                yBox += 180;
                int xButton = 20;
                int yButton = 20;
                List<Product> productList = ProductDAO.Instance.getProductsByType(productTypeList[i].ProductTypeId);
                for (int j = 0; j < productList.Count; j++) {
                    if (j != 0 && j % 3 == 0) {
                        xButton = 20;
                        yButton += 140;
                        size.Height += 150;
                        groupBox.Size = size;
                        yBox += 150;
                    }
                    //add button for product
                    Button button = new Button();
                    button.Tag = productList[j];
                    button.Location = new Point(xButton, yButton);
                    button.Size = new Size(100, 100);
                    byte[] img = productList[j].ProductImg;
                    MemoryStream ms = new MemoryStream(img);
                    button.BackgroundImage = Image.FromStream(ms);
                    //add event click for button
                    button.Click += btnProduct_Click;
                    //add label for product name
                    Label label = new Label();
                    label.Location = new Point(xButton - 10, yButton + 105);
                    label.Size = new Size(120, 20);
                    label.Text = productList[j].ProductName;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    groupBox.Controls.Add(button);
                    productButtons.Add(button);
                    groupBox.Controls.Add(label);
                    xButton += 120;
                }
            }
        }

        private void btnProduct_Click(object sender, EventArgs e) {
            Product product = (Product)((Button)sender).Tag;
            byte[] img = product.ProductImg;
            MemoryStream ms = new MemoryStream(img);
            pbProductImage.Image = Image.FromStream(ms);
            txtProductId.Text = product.ProductID.ToString();
            txtProductName.Text = product.ProductName;
            txtProductPrice.Text = product.UnitPrice.ToString();
            for (int i = 0; i < cbbProductType.Items.Count; i++) {
                if (product.TypeId == ((ProductType)cbbProductType.Items[i]).ProductTypeId) {
                    cbbProductType.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnImage_Click(object sender, EventArgs e) {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Filter = "Choose Image(*.jpg; *.png; *.jpeg)|*.jpg; *.png; *.jpeg";
            if (opn.ShowDialog() == DialogResult.OK) {
                pbProductImage.Image = Image.FromFile(opn.FileName);
            }
        }

        private void loadComboboxProductType() {
            List<ProductType> productTypeList = ProductDAO.Instance.getProductTypes();
            cbbProductType.Items.Clear();
            foreach (ProductType type in productTypeList) {
                cbbProductType.Items.Add(type);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtProductPrice.Text = "";
            pbProductImage.Image = pbProductImage.InitialImage;
            cbbProductType.SelectedIndex = -1;
            btnOKProduct.Tag = "Add";
            txtProductName.Enabled = true;
            txtProductPrice.Enabled = true;
            cbbProductType.Enabled = true;
            btnProductImage.Enabled = true;
            panelOK.Visible = true;
            panelButton.Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if (txtProductId.Text == "") {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm!","Chưa chọn sản phẩm",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            btnOKProduct.Tag = "Edit";
            txtProductName.Enabled = true;
            txtProductPrice.Enabled = true;
            cbbProductType.Enabled = true;
            btnProductImage.Enabled = true;
            panelOK.Visible = true;
            panelButton.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (txtProductId.Text == "") {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm!", "Chưa chọn sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?",
                "Xóa sản phẩm", buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                if (ProductDAO.Instance.deleteProduct(Int32.Parse(txtProductId.Text)) > 0) {
                    MessageBox.Show("Xóa sản phẩm thành công!","Thành công",
                        MessageBoxButtons.OK,MessageBoxIcon.Information);
                    loadProducts();
                } else {
                    MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!productValidation()) return;
            Button button = (Button)sender;
            MemoryStream ms = new MemoryStream();
            pbProductImage.Image.Save(ms, pbProductImage.Image.RawFormat);
            byte[] img = ms.ToArray();
            if (button.Tag.Equals("Add")) {
                string productName = txtProductName.Text;
                double unitPrice = Double.Parse(txtProductPrice.Text);
                int productTypeId = ((ProductType)cbbProductType.SelectedItem).ProductTypeId;
                if (ProductDAO.Instance.addProduct(productName, unitPrice, productTypeId, img) > 0) {
                    MessageBox.Show("Thêm sản phẩm mới thành công!","Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadProducts();
                    resetProductComponents();
                } else {
                    MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else if (button.Tag.Equals("Edit")) {
                string productName = txtProductName.Text;
                double unitPrice = Double.Parse(txtProductPrice.Text);
                int productTypeId = ((ProductType)cbbProductType.SelectedItem).ProductTypeId;
                int productId = Int32.Parse(txtProductId.Text);
                if (ProductDAO.Instance.editProduct(productName, unitPrice, productTypeId, img, productId) > 0) {
                    MessageBox.Show("Cập nhật sản phẩm thành công!","Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadProducts();
                    resetProductComponents();
                } else {
                    MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            resetProductComponents();
        }

        private void resetProductComponents() {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtProductPrice.Text = "";
            pbProductImage.Image = pbProductImage.InitialImage;
            cbbProductType.SelectedIndex = -1;
            txtProductName.Enabled = false;
            txtProductPrice.Enabled = false;
            cbbProductType.Enabled = false;
            btnProductImage.Enabled = false;
            panelOK.Visible = false;
            panelButton.Visible = true;
        }

        private Boolean productValidation() {
            if (txtProductName.Text.Length == 0) {
                MessageBox.Show("Vui lòng nhập vào tên sản phẩm!","Chưa nhập tên sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtProductName;
                return false;
            }
            try {
                double price = Double.Parse(txtProductPrice.Text);
                if (price <= 0) {
                    MessageBox.Show("Vui lòng nhập giá sản phẩm hợp lệ!","Giá không hợp lệ",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ActiveControl = txtProductPrice;
                    return false;
                }
            } catch (Exception ex) {
                MessageBox.Show("Vui lòng nhập giá sản phẩm hợp lệ!", "Giá không hợp lệ",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtProductPrice;
                return false;
            }
            if (cbbProductType.SelectedIndex < 0) {
                MessageBox.Show("Vui lòng chọn loại sản phẩm!","Chưa chọn loại sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (pbProductImage.Image == pbProductImage.InitialImage) {
                MessageBox.Show("Vui lòng chọn hình ảnh cho sản phẩm!","Chưa chọn hình ảnh",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        //Tab Product Type

        private void loadProductType() {
            DataTable productTypeList = ProductDAO.Instance.getDataTableProductTypes();
            dataGridViewProducType.DataSource = productTypeList;
        }

        private void btnAddType_Click(object sender, EventArgs e) {
            txtTypeId.Text = "";
            txtTypeName.Text = "";
            txtTypeName.Enabled = true;
            panelTypeButton.Visible = false;
            panelTypeOK.Visible = true;
            btnTypeOK.Tag = "Add";
        }

        private void btnEditType_Click(object sender, EventArgs e) {
            if (txtTypeId.Text == "") {
                MessageBox.Show("Vui lòng chọn loại sản phẩm!", "Chưa chọn loại sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtTypeName.Enabled = true;
            panelTypeButton.Visible = false;
            panelTypeOK.Visible = true;
            btnTypeOK.Tag = "Edit";
        }

        private void btnDeleteType_Click(object sender, EventArgs e) {
            if (txtTypeId.Text == "") {
                MessageBox.Show("Vui lòng chọn 1 loại sản phẩm!", "Chưa chọn loại sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa loại sản phẩm này? \n" +
                "Tất cả sản phẩm thuộc loại này đồng thời cũng bị xóa!",
                "Xóa loại sản phẩm", buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) {
                if (ProductDAO.Instance.deleteProductType(Int32.Parse(txtTypeId.Text)) > 0) {
                    MessageBox.Show("Xóa loại sản phẩm thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadProductType();
                } else {
                     MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTypeOK_Click(object sender, EventArgs e) {
            string typeName = txtTypeName.Text;
            if (typeName.Length == 0) {
                MessageBox.Show("Vui lòng nhập vào tên loại sản phẩm!", "Chưa nhập tên loại sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtTypeName;
                return;
            }
            Button button = (Button)sender;
            if (button.Tag.Equals("Add")) {
                if (ProductDAO.Instance.addProductType(typeName) > 0) {
                    MessageBox.Show("Thêm loại sản phẩm mới thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadProductType();
                    loadProducts();
                    loadComboboxProductType();
                    resetProductTypeComponents();
                } else {
                     MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else if (button.Tag.Equals("Edit")) {
                int typeId = Int32.Parse(txtTypeId.Text);
                if (ProductDAO.Instance.editProductType(typeName, typeId) > 0) {
                    MessageBox.Show("Cập nhật loại sản phẩm thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadProductType();
                    loadProducts();
                    loadComboboxProductType();
                    resetProductTypeComponents();
                } else {
                     MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTypeCancel_Click(object sender, EventArgs e) {
            resetProductTypeComponents();
        }

        private void resetProductTypeComponents() {
            txtTypeId.Text = "";
            txtTypeName.Text = "";
            txtTypeName.Enabled = false;
            panelTypeButton.Visible = true;
            panelTypeOK.Visible = false;
        }

        private void dataGridViewProducType_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            txtTypeId.Text = dataGridViewProducType.SelectedRows[0].Cells[0].Value.ToString();
            txtTypeName.Text = dataGridViewProducType.SelectedRows[0].Cells[1].Value.ToString();
        }

        //Auto refress invoice after 10 seconds

        public void autoRefresh() {
            Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 10000;//2 seconds
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            loadInvoice();
        }

        //Tab Employee

        private void loadEmployees() {
            dataGridViewEmployee.DataSource = AccountDAO.Instance.getDataTableEmployees();
        }

        private void btnEmployeeAdd_Click(object sender, EventArgs e) {
            txtEmployeeUser.Text = "";
            txtPassword.Text = "";
            txtEmployeeName.Text = "";
            cbbRoleId.SelectedIndex = -1;
            txtEmployeeName.Enabled = true;
            txtEmployeeUser.Enabled = true;
            txtPassword.Enabled = true;
            cbbRoleId.Enabled = true;
            panelEmployeeButton.Visible = false;
            panelEmployeeOK.Visible = true;
            btnEmployeeOK.Tag = "Add";
        }

        private void btnEmployeeEdit_Click(object sender, EventArgs e) {
            txtPassword.Enabled = true;
            txtEmployeeName.Enabled = true;
            cbbRoleId.Enabled = true;
            panelEmployeeButton.Visible = false;
            panelEmployeeOK.Visible = true;
            btnEmployeeOK.Tag = "Edit";
        }

        private void btnEmployeeDelete_Click(object sender, EventArgs e) {
            if (txtEmployeeUser.Text == "") {
                MessageBox.Show("Vui lòng chọn 1 nhân viên","Chưa chọn nhân viên",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này? \n" ,
                "Xóa nhân viên", buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                if (AccountDAO.Instance.deleteEmployee(txtEmployeeUser.Text) > 0) {
                    MessageBox.Show("Xóa nhân viên thành công!","Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadEmployees();
                    loadComboboxCashier();
                    resetEmployeeComponents();
                } else {
                     MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEmployeeOK_Click(object sender, EventArgs e) {
            Button button = (Button)sender;
            string empUser = txtEmployeeUser.Text; 
            string emplName = txtEmployeeName.Text;
            string password = txtPassword.Text;
            int roleId = 1;
            string roleName = cbbRoleId.SelectedItem.ToString();
            Console.WriteLine("Role Name: " + roleName);
            switch (roleName) {
                case "Thu ngân": roleId = 1; break;
                case "Pha chế": roleId = 2; break;
            }
            Console.WriteLine("Role ID: " + roleId);
            if (button.Tag.Equals("Add")) {
                if (!addEmployeeValidation()) return;
                if (AccountDAO.Instance.addEmployee(empUser, password, emplName, roleId) > 0) {
                    MessageBox.Show("Thêm nhân viên thành công!","Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadEmployees();
                    loadComboboxCashier();
                    resetEmployeeComponents();
                } else {
                     MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else if (button.Tag.Equals("Edit")) {
                if (!editEmployeeValidation()) return;
                if (AccountDAO.Instance.editEmployee(empUser, password, emplName, roleId) > 0) {
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadEmployees();
                    loadComboboxCashier();
                    resetEmployeeComponents();
                } else {
                     MessageBox.Show("Có lỗi xảy ra!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEmployeeCancel_Click(object sender, EventArgs e) {
            resetEmployeeComponents();
        }

        private void resetEmployeeComponents() {
            txtEmployeeUser.Text = "";
            txtPassword.Text = "";
            txtEmployeeName.Text = "";
            cbbRoleId.SelectedIndex = -1;
            txtEmployeeName.Enabled = false;
            txtPassword.Enabled = false;
            txtEmployeeUser.Enabled = false;
            cbbRoleId.Enabled = false;
            panelEmployeeButton.Visible = true;
            panelEmployeeOK.Visible = false;
        }

        private void dataGridViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            txtEmployeeUser.Text = dataGridViewEmployee.SelectedRows[0].Cells[0].Value.ToString();
            txtPassword.Text = dataGridViewEmployee.SelectedRows[0].Cells[1].Value.ToString();
            txtEmployeeName.Text = dataGridViewEmployee.SelectedRows[0].Cells[2].Value.ToString();
            string roleName= dataGridViewEmployee.SelectedRows[0].Cells[3].Value.ToString();
            switch (roleName) {
                case "Thu ngân":
                    cbbRoleId.SelectedIndex = 0;
                    break;
                case "Pha chế":
                    cbbRoleId.SelectedIndex = 1;
                    break;
            }
            
        }

        private Boolean addEmployeeValidation() {
            if (txtEmployeeUser.Text.Length < 8) {
                MessageBox.Show("Tên tài khoản phải bao gồm 8 kí tự trở lên!","Tên tài khoản không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtPassword;
                return false;
            }
            if (AccountDAO.Instance.usernameExisted(txtEmployeeUser.Text)) {
                MessageBox.Show("Vui lòng chọn tên tài khoản khác!","Tên tài khoản đã tồn tại",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtEmployeeUser;
                return false;
            }
            if (txtPassword.Text.Length < 8) {
                MessageBox.Show("Mật khẩu phải bao gồm 8 kí tự trở lên!","Mật khẩu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtPassword;
                return false;
            }
            if (txtEmployeeName.Text.Length == 0) {
                MessageBox.Show("Vui lòng nhập tên nhân viên!","Chưa nhập tên nhân viên",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbbEmployee.SelectedIndex < 0) {
                MessageBox.Show("Vui lòng chọn chức vụ!","Chưa chọn chức vụ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private Boolean editEmployeeValidation() {
            if (txtPassword.Text.Length < 8) {
                MessageBox.Show("Mật khẩu phải bao gồm 8 kí tự trở lên!", "Mật khẩu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtPassword;
                return false;
            }
            if (txtEmployeeName.Text.Length == 0) {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Chưa nhập tên nhân viên",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbbEmployee.SelectedIndex < 0) {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Chưa chọn chức vụ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnChangeInformation_Click(object sender, EventArgs e) {
            fInformation changeInformation = new fInformation("admin");
            changeInformation.Show();
        }

        private void btnChangePassword_Click(object sender, EventArgs e) {

        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }
    }


}
