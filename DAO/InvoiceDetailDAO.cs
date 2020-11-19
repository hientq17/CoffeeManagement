using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    class InvoiceDetailDAO
    {
        private static InvoiceDetailDAO instance;

        internal static InvoiceDetailDAO Instance
        { 
            get { if (instance == null) instance = new InvoiceDetailDAO(); return InvoiceDetailDAO.instance; }
            private set { InvoiceDetailDAO.instance = value; }
        }

        private InvoiceDetailDAO() { }

        public List<InvoiceDetail> GetListInvoiceDetailByTable(int id)
        {
            List<InvoiceDetail> listInvoiceDetail = new List<InvoiceDetail>();
            string query = "select Product.productId, Product.productName, InvoiceDetail.productAmount, Product.unitPrice, InvoiceDetail.totalPrice" +
                " from InvoiceDetail inner join Product on InvoiceDetail.productId=Product.productId where InvoiceDetail.invoiceId = "+id;
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                InvoiceDetail InvoiceDetail = new InvoiceDetail(item);
                listInvoiceDetail.Add(InvoiceDetail);
            }
            return listInvoiceDetail;
        }

        public void InsertInvoiceDetail()
        {
            
            
        }
    }
}
