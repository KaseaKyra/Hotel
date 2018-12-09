using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAO
{
    public class RoomTypeDAO
    {
        private static RoomTypeDAO instace;

        public static RoomTypeDAO Instace
        {
            get { if (instace == null) instace = new RoomTypeDAO(); return RoomTypeDAO.instace; }
            private set { RoomTypeDAO.instace = value; }
        }

        private RoomTypeDAO() { }

        public List<RoomType> GetListRoomType(int id)
        {
            List<RoomType> listRoomType = new List<RoomType>();

            string query = "exec USP_GetRoomTypeByID @id";
            DataTable data = DataProvider.Instace.ExecuteQuery(query, new object[] { id });
            //string query = "select * from RoomTypes where id = " + id;
            //DataTable data = DataProvider.Instace.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RoomType type = new RoomType(item);
                listRoomType.Add(type);
            }
            return listRoomType;
        }

        public List<RoomType> GetListRoomType()
        {
            List<RoomType> listRoomType = new List<RoomType>();
            string query = "exec USP_GetRoomTypes";
            DataTable data = DataProvider.Instace.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RoomType type = new RoomType(item);
                listRoomType.Add(type);
            }
            return listRoomType;
        }

        public RoomType GetRoomTypeByID(int id)
        {
            RoomType rt = null;
            string query = "exec USP_GetRoomTypeByID @id";
            DataTable data = DataProvider.Instace.ExecuteQuery(query, new object[] { id });

            foreach (DataRow item in data.Rows)
            {
                rt = new RoomType(item);

            }
            return rt;
        }

        public bool UpdateRoomTypeByID(int id, string name, double price)
        {
            string query = "exec USP_UpdateRoomTypeByID @id , @name , @price ";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id, name, price });
            return result > 0;
        }

        public bool DeleteRoomType(int id)
        {

            RoomDAO.Instace.DeleteRoomByRoomTypeID(id);

            string query = "exec USP_DeleteRoomTypeByID @id";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }

        public bool InsertRoomType(string name, double price)
        {
            string query = "exec USP_InsertRoomType @name , @price ";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { name, price });
            return result > 0;
        }
    }
}
