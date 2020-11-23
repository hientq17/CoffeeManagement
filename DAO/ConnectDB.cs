using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement
{
    class ConnectDB
    {
        private static ConnectDB instance;

        public static ConnectDB Instance
        {
            get { 
                if (instance == null) instance = new ConnectDB();
                return ConnectDB.instance; 
            }
            private set { ConnectDB.instance = value; }
        }

        public ConnectDB() { }

        // MySQL Cpanel 
        private MySqlConnection conn = null;
        private string server = "103.97.125.254";
        private string database = "mrendzpc_CoffeeManagement";
        private string uid = "mrendzpc_llhh";
        private string password = "Longhuynh";

        public MySqlConnection OpenConnection()
        {
            string connectionStr = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";charset=utf8;";
            if (conn == null) {
                conn = new MySqlConnection(connectionStr);
                conn.Open();
                Console.WriteLine("Open connection successfully");
            }
            return conn;
        }

        public void CloseConnection()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                Console.WriteLine("Close connection successfully");
            }
        }

        public DataTable ExecuteQuery(string query,object[] parameter = null)
        {
            DataTable data = new DataTable();
            OpenConnection();
            MySqlCommand command = new MySqlCommand(query, conn);

            if (parameter != null)
            {
                string[] listPara = query.Split(' ');
                int i = 0;
                foreach(string item in listPara)
                {
                    if (item.Contains('@'))
                    {
                        command.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(data);
            CloseConnection();
            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            conn = new ConnectDB().OpenConnection();
            MySqlCommand command = new MySqlCommand(query, conn);

            if (parameter != null)
            {
                string[] listPara = query.Split(' ');
                int i = 0;
                foreach (string item in listPara)
                {
                    if (item.Contains('@'))
                    {
                        command.Parameters.AddWithValue(item, ((MySqlParameter)parameter[i]).Value);
                        i++;
                    }
                    
                }
            }
            data = command.ExecuteNonQuery();
            CloseConnection();
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            OpenConnection();
            MySqlCommand command = new MySqlCommand(query, conn);

            if (parameter != null)
            {
                string[] listPara = query.Split(' ');
                int i = 0;
                foreach (string item in listPara)
                {
                    if (item.Contains('@'))
                    {
                        command.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }

            data = command.ExecuteScalar();
            CloseConnection();
            return data;
        }

    }


}



