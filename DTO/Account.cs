using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    class Account
    {
        private string username, password, empName;
        int role;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string EmpName { get => empName; set => empName = value; }
        public int Role { get => role; set => role = value; }

        public Account() { }


        public Account(string username, string password, string empName, int role, int iD, string status)
        {
            this.Username = username;
            this.Password = password;
            this.EmpName = empName;
            this.Role = role;
        }


        public Account(DataRow row)
        {
            this.Username = (string)row["employeeUser"];
            this.Password = (string)row["password"];
            this.empName=(string)row["employeeName"];
            this.role = (int)row["roleId"];
        }

        public override string ToString() {
            return this.empName;
        }
    }
}
