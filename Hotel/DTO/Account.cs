using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class Account
    {
        int id;
        private string username;
        private string displayedName;
        private string password;
        private int type;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string DisplayedName { get => displayedName; set => displayedName = value; }
        public string Password { get => password; set => password = value; }
        public int Type { get => type; set => type = value; }

        public Account(int id, string username, string displayedName, int type, string password = null)
        {
            this.id = id;
            this.username = username;
            this.displayedName = displayedName;
            this.type = type;
            this.password = password;
        }

        public Account(DataRow row)
        {
            this.id = (int)row["id"];
            this.username = row["username"].ToString();
            this.displayedName = row["displayed"].ToString();
            this.type = (int)row["type"];
            this.password = row["password"].ToString();
        }
    }
}
