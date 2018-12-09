using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    class AccountType
    {
        int id;
        string name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public AccountType(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public AccountType(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
        }
    }
}
