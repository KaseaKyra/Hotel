using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class Service
    {
        private int id;
        private string name;
        private double price;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }

        public Service(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public Service(System.Data.DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Price = (double)Convert.ToDouble(row["price"]);
        }
    }
}
