
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CoffeeManagement
{

    

    class InvoiceDAO
    {

        private static InvoiceDAO instance;

        public static InvoiceDAO Instance
        {
            get { if (instance == null) instance = new InvoiceDAO(); return InvoiceDAO.instance; }
            private set { InvoiceDAO.instance = value; }
        }

        private InvoiceDAO() { }
        
        public int GetUncheckoutInvoiceIDByTableID(int id)
        {
            DataTable data = ConnectDB.Instance.ExecuteQuery("Select invoiceId,tableId,dateSale,totalPayment,employeeUser,invoiceStatus from Invoice where tableId= " + id + " and invoiceStatus=1");
            if (data.Rows.Count > 0)
            {
                Invoice invoice = new Invoice(data.Rows[0]);
                return invoice.InvoiceID;
            }
            return -1;
        }

        public double GetUncheckoutInvoiceTotalPaymentByTableID(int id)
        {
            DataTable data = ConnectDB.Instance.ExecuteQuery("Select invoiceId,tableId,dateSale,totalPayment,employeeUser,invoiceStatus from Invoice where tableId= " + id + " and invoiceStatus=1");
            if (data.Rows.Count > 0)
            {
                Invoice invoice = new Invoice(data.Rows[0]);
                return invoice.TotalPayment;
            }
            return 0;
        }

        public bool UpdateInvoice(int invoiceId) {
            bool result = false;
            string query = "Update Invoice set invoiceStatus= 0 where invoiceId= @invoice";
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("@invoiceId", MySqlDbType.Int32);
            parameters[0].Value = invoiceId;
            int data = ConnectDB.Instance.ExecuteNonQuery(query, parameters);
            if (data != 0)
                result = true;
            return result;
        }

        public DataTable getInvoices() {
            string query = "Select invoiceId, tableId, dateSale, totalPayment, employeeName " +
                "from Invoice inner join Employee on Invoice.employeeUser = Employee.employeeUser order by invoiceId";
            DataTable invoiceList = ConnectDB.Instance.ExecuteQuery(query);
            return invoiceList;
        }

        public DataTable searchInvoices(string dateFrom = null, string dateTo = null, string employeeUser = null) {
            string query = "Select invoiceId, tableId, dateSale, totalPayment, employeeName " +
                "from Invoice inner join Employee on Invoice.employeeUser = Employee.employeeUser";
            if (dateFrom != null && dateFrom != "") {
                query += " where dateSale >= '" + dateFrom + "'";
                if (dateTo != null && dateTo != "") {
                    query += " and dateSale <= '" + dateTo + "'";
                }
                if (employeeUser != null && employeeUser != "") query += " and Invoice.employeeUser = '" + employeeUser + "'";
            } else if (dateTo != null && dateTo != "") {
                query += " where dateSale <= '" + dateTo + "'";
                if (employeeUser != null && employeeUser != "") query += " and Invoice.employeeUser = '" + employeeUser + "'";
            } else if (employeeUser != null && employeeUser != "") {
                query += " where Invoice.employeeUser = '" + employeeUser + "'";
            }
            query += " Order by invoiceId DESC";
            Console.WriteLine(query);
            DataTable invoiceList = ConnectDB.Instance.ExecuteQuery(query);
            return invoiceList;
        }

        public Invoice GetInvoiceReport(int id) {
            DataTable data = ConnectDB.Instance.ExecuteQuery(
                "Select invoiceId,tableId,dateSale,totalPayment,employeeName as employeeUser,invoiceStatus from Invoice" +
                " inner join Employee on Invoice.employeeUser = Employee.employeeUser where invoiceId = " + id);
            Invoice invoice = new Invoice(data.Rows[0]);
            return invoice;
        }

    }
}
