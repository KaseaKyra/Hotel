using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class Menu
    {
        private string serviceName;
        private int count;
        private float price;
        private double totalPrice;

        public string ServiceName { get => serviceName; set => serviceName = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }

        public Menu(string serviceName, int count, float price, double totalPrice = 0)
        {
            this.serviceName = serviceName;
            this.count = count;
            this.price = price;
            this.TotalPrice = totalPrice;
        }

        public Menu(DataRow row)
        {
            this.serviceName = row["name"].ToString();
            this.price = (float)Convert.ToDouble(row["price"]);
            this.count = (int)row["count"];
            this.TotalPrice = (double)row["totalPrice"];
        }
    }
}
