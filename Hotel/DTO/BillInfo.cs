using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class BillInfo
    {
        private int id;
        private int billID;
        private int serviceID;
        private int count;
        private int quantityOfDay;

        public int Id { get => id; set => id = value; }
        public int BillID { get => billID; set => billID = value; }
        public int ServiceID { get => serviceID; set => serviceID = value; }
        public int Count { get => count; set => count = value; }
        public int QuantityOfDay { get => quantityOfDay; set => quantityOfDay = value; }

        public BillInfo(int id, int billID, int serviceID, int count, int quantityOfDay)
        {
            this.id = id;
            this.billID = billID;
            this.serviceID = serviceID;
            this.count = count;
            this.quantityOfDay = quantityOfDay;
        }

        public BillInfo(DataRow row)
        {
            this.id = (int)row["id"];
            this.billID = (int)row["bill_id"];
            this.serviceID = (int)row["service_id"];
            this.count = (int)row["count"];
            this.count = (int)row["quantity_of_day"];
        }
    }
}
