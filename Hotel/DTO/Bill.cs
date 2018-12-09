using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class Bill
    {
        private int id;
        private DateTime? dateCheckin;
        private DateTime? dateCheckout;
        private int status;
        private int roomID;
        private int discount;

        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckin { get => dateCheckin; set => dateCheckin = value; }
        public DateTime? DateCheckout { get => dateCheckout; set => dateCheckout = value; }
        public int Status { get => status; set => status = value; }
        public int RoomID { get => roomID; set => roomID = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id, DateTime? dateCheckin, DateTime? dateCheckout, int status, int roomID, int discount = 0)
        {
            this.id = id;
            this.dateCheckin = dateCheckin;
            this.dateCheckout = dateCheckout;
            this.status = status;
            this.roomID = roomID;
            this.discount = discount;
        }

        public Bill(DataRow row)
        {
            this.id = (int)row["id"];
            this.dateCheckin = (DateTime?)row["date_check_in"];

            var dateCheckoutTemp = row["date_check_out"];
            if (dateCheckoutTemp.ToString() != "")
            {
                this.dateCheckout = (DateTime?)dateCheckoutTemp;
            }

            this.status = (int)row["status"];
            this.roomID = (int)row["room_id"];
            this.discount = (int)row["discount"];
        }
    }
}
