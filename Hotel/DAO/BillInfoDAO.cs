using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instace;

        public static BillInfoDAO Instace
        {
            get { if (instace == null) instace = new BillInfoDAO(); return BillInfoDAO.instace; }
            private set { BillInfoDAO.instace = value; }
        }

        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            string query = "exec USP_GetListBillInfoByID @id";
            DataTable data = DataProvider.Instace.ExecuteQuery(query, new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                BillInfo billInfo = new BillInfo(item);
                listBillInfo.Add(billInfo);
            }
            return listBillInfo;
        }

        public void insertBillInfo(int bill_id, int service_id, int count)
        {
            string query = "exec USP_InsertBillInfo @bill_id , @service_id , @count";
            DataProvider.Instace.ExecuteNonQuery(query, new object[] { bill_id, service_id, count });
        }

        public void DeleteBillInfoByBillID(int id)
        {
            string query = "exec USP_DeleteBillInfoByBillID @id";
            DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
        }

        //public bool DeleteBillInfoByServiceID(int id)
        //{
        //    string query = "exec USP_DeleteBillInfoByServiceID @id";
        //    int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
        //    return result > 1;
        //}

        public void DeleteBillInfoByServiceID(int id)
        {
            string query = "exec USP_DeleteBillInfoByServiceID @id";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
        }
    }
}
