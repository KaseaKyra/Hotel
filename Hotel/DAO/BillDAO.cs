using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAO
{
    public class BillDAO
    {
        private static BillDAO instace;

        public static BillDAO Instace
        {
            get { if (instace == null) instace = new BillDAO(); return BillDAO.instace; }
            private set { BillDAO.instace = value; }
        }

        private BillDAO() { }

        /**
         * 
         * thanh cong tra bill id 
         * that bai tra -1
         * 
         */
        public int getUncheckBillIDByRoomID(int id)
        {
            //return (int)DataProvider.Instace.ExecuteScalar("");
            string query = "exec USP_GetUncheckBillIDByRoomID @id";
            DataTable data = DataProvider.Instace.ExecuteQuery(query, new object[] { id });
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }

        public void insertBill(int id)
        {
            DataProvider.Instace.ExecuteNonQuery("exec USP_InsertBill @id", new object[] { id });
        }

        public int getMaxBillID()
        {
            try
            {
                string query = "exec USP_getMaxBillID";
                return (int)DataProvider.Instace.ExecuteScalar(query);
            }
            catch
            {
                return 1;
            }
        }

        public void Checkout(int id, int discount, double totalPrice)
        {
            string query = "USP_UpdateBill @id , @discount , @totalPrice";
            DataProvider.Instace.ExecuteNonQuery(query, new object[] { id , discount , totalPrice });

            //string query = "update Bills set status = 1, date_check_out = getdate(), discount = " + discount + ", total_price = " + totalPrice + " where id =  @id";
            //DataProvider.Instace.ExecuteNonQuery(query);
        }

        public DataTable GetListBill()
        {
            string query = "exec USP_BillFormat";
            return DataProvider.Instace.ExecuteQuery(query);

            //string query = "update Bills set status = 1, date_check_out = getdate(), discount = " + discount + ", total_price = " + totalPrice + " where id =  @id";
            //DataProvider.Instace.ExecuteNonQuery(query);
        }

        public void DeleteBillByRoomID(int id)
        {
            BillInfoDAO.Instace.DeleteBillInfoByBillID(id);

            string query = "exec USP_DeleteBillByRoomID @id";
            DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
        }
    }
}
