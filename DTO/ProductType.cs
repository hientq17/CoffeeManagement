using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement {
    class ProductType {
        private int productTypeId;
        private string productTypeName;


        public ProductType() {
        }

        public ProductType(int productTypeId, string productTypeName, double unitPrice, int typeId) {
            this.productTypeId = productTypeId;
            this.productTypeName = productTypeName;
        }

        public int ProductTypeId { get => productTypeId; set => productTypeId = value; }
        public string ProductTypeName { get => productTypeName; set => productTypeName = value; }
        public ProductType(DataRow row) {
            this.productTypeId = (int)row["typeId"];
            this.productTypeName = (string)row["typeName"];

        }

        public override string ToString() {
            return productTypeName;
        }
    }

}

