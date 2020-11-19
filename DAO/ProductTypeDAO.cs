using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    class ProductTypeDAO
    {
        private static ProductTypeDAO instance;

        public static ProductTypeDAO Instance
        {
            get { if (instance == null) instance = new ProductTypeDAO(); return instance; }
            private set { { instance = value; } }
        }

        private ProductTypeDAO() { }

        public List<ProductType> GetAllProductType()
        {
            List<ProductType> listProduct = new List<ProductType>();
            DataTable data = ConnectDB.Instance.ExecuteQuery("Select typeName from ProductType");
            foreach (DataRow item in data.Rows)
            {
                ProductType product = new ProductType(item);
                listProduct.Add(product);
            }
            return listProduct;
        }

        public string GetProductTypeByTypeId(int id)
        {
            List<ProductType> listProduct = new List<ProductType>();
            DataTable data = ConnectDB.Instance.ExecuteQuery("Select typeName from ProductType where typeId="+id);
            if(data.Rows.Count>0)
            {
                ProductType productType = new ProductType(data.Rows[0]);
                return productType.TypeName;
            }
            return null;
        }
    }
}
