using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class CoffeeTable
    {
        private int iD;
        string status;

        public CoffeeTable(int id, string status)
        {
            this.ID = id;
            this.Status = status;
        }

        public int ID { get => iD; set => iD = value; }
        public string Status { get => status; set => status = value; }

        public CoffeeTable(DataRow row)
        {
            this.ID = (int)row["tableId"];
            ulong u = (ulong)row["tableStatus"];
            this.Status = (u == 1) ? "Đang đợi": "Trống";
        }
    }
}
