
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CoffeeManagement.DTO;

namespace CoffeeManagement.DAO {
    class ProductDAO {

        public static int ProductWidth = 100;
        public static int ProductHeight = 90;

        private static ProductDAO instance;


        public static ProductDAO Instance {
            get { if (instance == null) instance = new ProductDAO(); return ProductDAO.instance; }
            private set { ProductDAO.instance = value; }
        }

        private ProductDAO() { }

        public List<Product> GetAllProduct()
        {
            List<Product> listProduct = new List<Product>();
            DataTable data = ConnectDB.Instance.ExecuteQuery("Select * from Product");
            foreach(DataRow item in data.Rows)
            {
                Product product = new Product(item);
                listProduct.Add(product);
            }
            return listProduct;
        }

        public List<Product> getProducts() {
            List<Product> productList = new List<Product>();
            string query = "Select * from Product where productStatus = 1";
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            for (int i = 0; i < data.Rows.Count; i++) {
                productList.Add(new Product(data.Rows[i]));
            }
            return productList;
        }

        public DataTable getDataTableProductTypes() {
            string query = "Select * from ProductType where typeStatus = 1";
            DataTable productTypeList = ConnectDB.Instance.ExecuteQuery(query);
            return productTypeList;
        }

        public List<Product> getProductsByType(int typeId) {
            List<Product> productList = new List<Product>();
            string query = "Select * from Product where productStatus = 1 and typeId = " + typeId;
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            for (int i = 0; i < data.Rows.Count; i++) {
                productList.Add(new Product(data.Rows[i]));
            }
            return productList;
        }

        public List<ProductType> getProductTypes() {
            List<ProductType> productTypeList = new List<ProductType>();
            string query = "Select * from ProductType where typeStatus = 1";
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            for (int i = 0; i < data.Rows.Count; i++) {
                productTypeList.Add(new ProductType(data.Rows[i]));
            }
            return productTypeList;
        }

        public string getTypeNameByTypeId(int typeId) {
            string query = "Select * from ProductType where typeStatus = 1 and typeId = "+typeId;
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                ProductType productType = new ProductType(data.Rows[0]);
                return productType.ProductTypeName;
            }
            return "";
        }

        public int addProduct(String name, double price, int productId, byte[] img) {
            String query = "Insert into Product(productName, unitPrice, typeId, productImg, productStatus) " +
                    "values( @productName , @unitPrice , @typeId , @productImg , 1 )";
            MySqlParameter[] parameters = new MySqlParameter[4];
            parameters[0] = new MySqlParameter("@productName", MySqlDbType.VarChar);
            parameters[0].Value = name;
            parameters[1] = new MySqlParameter("@unitPrice", MySqlDbType.Double);
            parameters[1].Value = price;
            parameters[2] = new MySqlParameter("@typeId", MySqlDbType.Int32);
            parameters[2].Value = productId;
            parameters[3] = new MySqlParameter("@productImg", MySqlDbType.MediumBlob);
            parameters[3].Value = img;
            return ConnectDB.Instance.ExecuteNonQuery(query, parameters);
        }

        public int editProduct(String name, double price, int productTypeId, byte[] img, int productId) {
            String query = "Update Product set productName =  @productName , unitPrice = @unitPrice , " +
                "typeId = @typeId , productImg = @productImg where productId = @productId ";
            MySqlParameter[] parameters = new MySqlParameter[5];
            parameters[0] = new MySqlParameter("@productName", MySqlDbType.VarChar);
            parameters[0].Value = name;
            parameters[1] = new MySqlParameter("@unitPrice", MySqlDbType.Double);
            parameters[1].Value = price;
            parameters[2] = new MySqlParameter("@typeId", MySqlDbType.Int32);
            parameters[2].Value = productTypeId;
            parameters[3] = new MySqlParameter("@productImg", MySqlDbType.MediumBlob);
            parameters[3].Value = img;
            parameters[4] = new MySqlParameter("@productId", MySqlDbType.Int32);
            parameters[4].Value = productId;
            return ConnectDB.Instance.ExecuteNonQuery(query, parameters);
        }

        public int deleteProduct(int productId) {
            String query = "Update Product set productStatus = 0 where productId = " + productId;
            return ConnectDB.Instance.ExecuteNonQuery(query);
        }

        public int addProductType(String typeName) {
            String query = "Insert into ProductType(typeName,typeStatus) " +
                    "values( @typeName , 1 )";
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("@typeName", MySqlDbType.VarChar);
            parameters[0].Value = typeName;
            return ConnectDB.Instance.ExecuteNonQuery(query, parameters);
        }

        public int editProductType(String typeName, int typeId) {
            String query = "Update ProductType set typeName = @typeName where typeId = @typeId ";
            MySqlParameter[] parameters = new MySqlParameter[2];
            parameters[0] = new MySqlParameter("@typeName", MySqlDbType.VarChar);
            parameters[0].Value = typeName;
            parameters[1] = new MySqlParameter("@typeId", MySqlDbType.Int32);
            parameters[1].Value = typeId;
            return ConnectDB.Instance.ExecuteNonQuery(query, parameters);
        }

        public int deleteProductType(int productTypeId) {
            string query = "Update Product set productStatus = 0 where typeId = " + productTypeId;
            ConnectDB.Instance.ExecuteNonQuery(query);
            query = "Update ProductType set typeStatus = 0 where typeId = " + productTypeId;
            return ConnectDB.Instance.ExecuteNonQuery(query);
        }

    }
}

    