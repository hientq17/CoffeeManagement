using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement
{
    class Invoice
    {
        private int invoiceID,tableID;
        private string employeeUser, status;
        private DateTime dateSale;
        private double totalPayment;

        public Invoice()
        {
        }

        public Invoice(int invoiceID, int tableID, DateTime dateSale, string employeeUser, string status, double totalPayment)
        {
            this.InvoiceID = invoiceID;
            this.TableID = tableID;
            this.DateSale = dateSale;
            this.EmployeeUser = employeeUser;
            this.TotalPayment = totalPayment;
            this.Status = status;
        }

        public int InvoiceID { get => invoiceID; set => invoiceID = value; }
        public int TableID { get => tableID; set => tableID = value; }
        public DateTime DateSale { get => dateSale; set => dateSale = value; }
        public string EmployeeUser { get => employeeUser; set => employeeUser = value; }
        public double TotalPayment { get => totalPayment; set => totalPayment = value; }
        public string Status { get => status; set => status = value; }

        public Invoice(DataRow row)
        {
            this.invoiceID = (int)row["invoiceId"];
            this.tableID = (int)row["tableId"];
            this.dateSale = (DateTime)row["dateSale"];
            this.totalPayment = (double)row["totalPayment"];
            this.employeeUser = (string)row["employeeUser"];
            ulong u = (ulong)row["invoiceStatus"];
            this.Status = (u == 0) ? "CheckedOut" : "Waiting";
        }
    }
}
