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

        //public List<String> GetListRoomType()
        //{
        //    List<String> listRoomType = new List<String>();
        //    string query = "select Name from RoomTypes";
        //    DataTable data = DataProvider.Instace.ExecuteQuery(query);

        //    foreach (DataRow item in data.Rows)
        //    {
        //        String type = item.ToString();
        //        listRoomType.Add(type);
        //    }
        //    return listRoomType;
        //}
    }
}
