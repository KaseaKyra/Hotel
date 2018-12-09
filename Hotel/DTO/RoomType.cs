using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class RoomType
    {
        private int id;
        private string name;
        private double price;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }

        public RoomType(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public RoomType(DataRow row)
        {
            this.id = (int)row["ID"];
            this.name = row["Name"].ToString();
            this.price = (double)Convert.ToDouble(row["Price"]);
        }
    }
}
