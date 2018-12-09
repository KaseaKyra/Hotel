using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAO
{
    class ServiceDAO
    {
        private static ServiceDAO instace;

        public static ServiceDAO Instace
        {
            get { if (instace == null) instace = new ServiceDAO(); return ServiceDAO.instace; }
            private set { ServiceDAO.instace = value; }
        }

        private ServiceDAO() { }

        public List<Service> GetListService()
        {
            List<Service> listService = new List<Service>();
            string query = "exec USP_GetService";

            DataTable data = DataProvider.Instace.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Service service = new Service(item);
                listService.Add(service);
            }

            return listService;
        }

        public List<Service> GetListService(int id)
        {
            List<Service> listService = new List<Service>();

            string query = "exec USP_GetServiceByID @id";
            DataTable data = DataProvider.Instace.ExecuteQuery(query, new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                Service service = new Service(item);
                listService.Add(service);
            }
            return listService;
        }

        public bool InsertService(string name, double price)
        {
            string query = "exec USP_InsertService @name , @price ";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { name, price });
            return result > 0;
        }

        public bool UpdateService(int id, string name, double price)
        {
            string query = "exec USP_UpdateServiceByID @id , @name , @price ";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id, name, price });
            return result > 0;
        }

        //public bool DeleteService(int id)
        //{
        //    string query = "exec USP_DeleteServiceByID @id";
        //    int result = 0;

        //    if ()
        //    {
        //        BillInfoDAO.Instace.DeleteBillInfoByServiceID(id);
        //        result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
        //    }
        //    else
        //    {
        //        result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
        //    }

        //    return result > 0;
        //}

        public bool DeleteService(int id)
        {
            BillInfoDAO.Instace.DeleteBillInfoByServiceID(id);

            string query = "exec USP_DeleteServiceByID @id";
            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }
    }
}
