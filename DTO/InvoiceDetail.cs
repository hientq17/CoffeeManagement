using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    class InvoiceDetail
    {
        private string productName;
        private int count,productId;
        private double price, totalPrice;

        public InvoiceDetail(string productName, int count, double price, double totalPrice)
        {
            this.productName = productName;
            this.count = count;
            this.price = price;
            this.totalPrice = totalPrice;
        }

        public InvoiceDetail(DataRow row)
        {
            this.productName = row["productName"].ToString();
            this.count = (int)row["productAmount"];
            this.price = (double)row["unitPrice"];
            this.totalPrice = (double)row["totalPrice"];
            this.productId = (int)row["productId"];
        }

        public string ProductName { get => productName; set => productName = value; }
        public int Count { get => count; set => count = value; }
        public double Price { get => price; set => price = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int ProductId { get => productId; set => productId = value; }
    }
}
