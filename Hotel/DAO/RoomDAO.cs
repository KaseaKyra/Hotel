using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAO
{
    public class RoomDAO
    {
        public static int roomHeight = 80;
        public static int roomWidth = 80;

        private static RoomDAO instace;

        public static RoomDAO Instace
        {
            get { if (instace == null) instace = new RoomDAO(); return RoomDAO.instace; }
            private set { RoomDAO.instace = value; }
        }

        private RoomDAO() { }

        public List<Room> LoadRoomList()
        {
            List<Room> roomList = new List<Room>();
            string query = "exec USP_GetRoomList";
            DataTable data = DataProvider.Instace.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Room room = new Room(item);
                roomList.Add(room);
            }
            return roomList;
        }

        public bool InsertRoom(string name, int type, string des = "Không có", int status = 0)
        {
            string query = "exec USP_InsertRoom @name , @type , @des , @status ";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { name, type, des, status });
            return result > 0;
        }

        public bool UpdateRoom(int id, string name, int type, string des, int status)
        {
            string query = "exec USP_UpdateRoom @id , @name , @type , @des , @status ";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id, name, type, des, status });
            return result > 0;
        }

        public bool DeleteRoom(int id)
        {
            BillDAO.Instace.DeleteBillByRoomID(id);

            string query = "exec USP_DeleteRoomByID @id";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }
    }
}
