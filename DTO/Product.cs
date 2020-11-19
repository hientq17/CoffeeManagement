using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO {
    class Product {
        private int productId;
        private string productName;
        private double unitPrice;
        private int typeId;
        private byte[] productImg;

        public Product() {
        }

        public Product(int productID, string productName, double unitPrice, int typeId, byte[] productImg) {
            this.ProductID = productId;
            this.ProductName = productName;
            this.UnitPrice = unitPrice;
            this.TypeId = typeId;
            this.ProductImg = productImg;
        }

        public int ProductID { get => productId; set => productId = value; }
        public string ProductName { get => productName; set => productName = value; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
        public int TypeId { get => typeId; set => typeId = value; }
        public byte[] ProductImg { get => productImg; set => productImg = value; }

        public Product(DataRow row) {
            this.ProductID = (int)row["productId"];
            this.ProductName = (string)row["productName"];
            this.UnitPrice = (double)row["unitPrice"];
            this.TypeId = (int)row["typeId"];
            this.productImg = (byte[])row["productImg"];
        }
    }
}
