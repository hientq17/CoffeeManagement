using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace CoffeeManagement{

    public partial class fReport : Form {

        int invoiceId;

        DataTable invoiceDetail;

        public fReport(int invoiceId) {
            this.invoiceId = invoiceId;
            invoiceDetail = InvoiceDetailDAO.Instance.GetListInvoiceDetailReport(invoiceId);
            InitializeComponent();
        }

        private void rpViewer_Load(object sender, EventArgs e) {
            //// TODO: This line of code loads data into the 'mrendzpc_CoffeeManagementDataSet.Report' table. You can move, or remove it, as needed.
            //this.ReportTableAdapter.Fill(this.mrendzpc_CoffeeManagementDataSet.Report);
            Invoice invoice = InvoiceDAO.Instance.GetInvoiceReport(invoiceId);
            Console.WriteLine("total: " + invoice.TotalPayment);
            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("tableId", invoice.TableID.ToString());
            parameters[1] = new ReportParameter("invoiceTime", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            parameters[2] = new ReportParameter("totalPayment", invoice.TotalPayment.ToString());
            parameters[3] = new ReportParameter("invoiceId", invoice.InvoiceID.ToString());
            parameters[4] = new ReportParameter("employeeName", invoice.EmployeeUser);
            rpViewer.LocalReport.SetParameters(parameters);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet";
            rds.Value = invoiceDetail;
            rpViewer.LocalReport.DataSources.Clear();
            rpViewer.LocalReport.DataSources.Add(rds);
            this.rpViewer.RefreshReport();
        }

        private void ReportViewer_Load(object sender, EventArgs e) {

        }
    }
}
