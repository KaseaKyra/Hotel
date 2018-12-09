using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class Room
    {
        private int id;
        private string name;
        private int typeID;
        private string description;
        private int status;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int TypeID { get => typeID; set => typeID = value; }
        public string Description { get => description; set => description = value; }
        public int Status { get => status; set => status = value; }

        public Room(int id, string name, int typeID, int status, string description = null)
        {
            this.id = id;
            this.name = name;
            this.typeID = typeID;
            this.Status = status;
            this.description = description;
        }

        public Room(DataRow row)
        {
            this.id = (int)row["ID"];
            this.name = row["Name"].ToString();
            this.TypeID = (int)row["TypeID"];
            this.description = row["Description"].ToString();
            this.Status = (int)row["Status"];
        }
    }
}
