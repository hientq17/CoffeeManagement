
﻿using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CoffeeManagement.DAO
{
    class AccountDAO {
        private static AccountDAO instance;

        public static AccountDAO Instance {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { { instance = value; } }
        }

        private AccountDAO() { }

        public int Login(string username, string password) {
            string query = "Select * from Employee where employeeUser=N'" + username + "'and password=N'" + password + "' and employeeStatus = 1";
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            List<Account> tableList = new List<Account>();
            if (data.Rows.Count == 1) {
                Account account = new Account(data.Rows[0]);
                return account.Role;
            }
            return -1;
        }

        public Account GetAccount(string username) {
            string query = "Select * from Employee where employeeUser=N'" + username + "'";
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            if (data.Rows.Count != 0) {
                Account account = new Account(data.Rows[0]);
                return account;
            }
            return null;
        }

        public bool UpdateAccount(string username, string newPass, string empName) {
            bool result = false;
            string query = "update Employee set password= @password , employeeName= @employeeName where employeeUser=N'" + username + "'";
            MySqlParameter[] parameters = new MySqlParameter[4];
            parameters[0] = new MySqlParameter("@password", MySqlDbType.VarChar);
            parameters[0].Value = newPass;
            parameters[1] = new MySqlParameter("@employeeName", MySqlDbType.VarChar);
            parameters[1].Value = empName;
            int data = ConnectDB.Instance.ExecuteNonQuery(query, parameters);
            if (data != 0)
                result = true;
            return result;
        }

        public List<Account> getEmployees() {
            List<Account> accountList = new List<Account>();
            string query = "Select * from Employee where employeeStatus = 1";
            DataTable data = ConnectDB.Instance.ExecuteQuery(query);
            for (int i = 0; i < data.Rows.Count; i++) {
                accountList.Add(new Account(data.Rows[i]));
            }
            return accountList;
        }

        public DataTable getDataTableEmployees() {
            string query = "Select * from Employee where roleId = 1 or roleId = 2 and employeeStatus = 1";
            DataTable employeeList = ConnectDB.Instance.ExecuteQuery(query);
            employeeList.Columns.Add("roleName", typeof(string));
            foreach (DataRow row in employeeList.Rows) {
                if (row["roleId"].ToString() == "0") row["roleName"] = "Quản lý";
                else if (row["roleId"].ToString() == "1") row["roleName"] = "Thu ngân";
                else row["roleName"] = "Pha chế";
            }
            return employeeList;
        }

        public int addEmployee(string username, string password, string name, int roleId) {
            String query = "Insert into Employee values( @employeeUser , @password , @employeeName , @roleId , 1 )";
            MySqlParameter[] parameters = new MySqlParameter[4];
            parameters[0] = new MySqlParameter("@employeeUser", MySqlDbType.VarChar);
            parameters[0].Value = username;
            parameters[1] = new MySqlParameter("@password", MySqlDbType.VarChar);
            parameters[1].Value = password;
            parameters[2] = new MySqlParameter("@employeeName", MySqlDbType.VarChar);
            parameters[2].Value = name;
            parameters[3] = new MySqlParameter("@roleId", MySqlDbType.Int32);
            parameters[3].Value = roleId;
            return ConnectDB.Instance.ExecuteNonQuery(query, parameters);
        }

        public int editEmployee(string username, string password, string name, int roleId) {
            String query = "Update Employee set password = '" + password + "', " +
                "employeeName = '" + name + "' , roleId = " + roleId + " where employeeUser = '" + username + "'";
            return ConnectDB.Instance.ExecuteNonQuery(query);
        }

        public int deleteEmployee(string username) {
            String query = "Update Employee set employeeStatus = 0 where employeeUser = '" + username + "'";
            return ConnectDB.Instance.ExecuteNonQuery(query);
        }

        public bool usernameExisted(string username) {
            ConnectDB db = new ConnectDB();
            String query = "Select * from Employee where employeeUser = '" + username + "' and employeeStatus = 1";
            MySqlCommand command = new MySqlCommand(query, db.OpenConnection());
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                db.CloseConnection();
                return true;
            }
            db.CloseConnection();
            return false;
        }
    }
        
}
