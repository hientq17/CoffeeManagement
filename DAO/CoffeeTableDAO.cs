using CoffeeManagement.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    class CoffeeTableDAO
    {

        public static int TableWidth = 80;
        public static int TableHeight = 80;
        private static CoffeeTableDAO instance;

        public static CoffeeTableDAO Instance
        {
            get { if (instance == null) instance = new CoffeeTableDAO(); return CoffeeTableDAO.instance; }
            private set { CoffeeTableDAO.instance = value; }
        }

        private CoffeeTableDAO() { }

        public List<CoffeeTable> LoadTableList()
        {
            List<CoffeeTable> tableList = new List<CoffeeTable>();
            string query = "select * from CoffeeTable";
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                CoffeeTable table = new CoffeeTable(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public bool UpdateTable(int tableID) {
            bool result = false;
            List<CoffeeTable> tableList = new List<CoffeeTable>();
            string query = "Update CoffeeTable set tableStatus= 0 where tableId= @tableId";
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("@tableId", MySqlDbType.Int32);
            parameters[0].Value = tableID;
            int data = ConnectDB.Instance.ExecuteNonQuery(query, parameters);
            if (data != 0)
                result = true;
            return result;
        }
    }
}
