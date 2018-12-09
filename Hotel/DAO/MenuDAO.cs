using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Hotel.DTO.Menu;

namespace Hotel.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instace;

        public static MenuDAO Instace
        {
            get { if (instace == null) instace = new MenuDAO(); return MenuDAO.instace; }
            private set { MenuDAO.instace = value; }
        }

        private MenuDAO() { }

        public List<Menu> GetListMenuByRoom(int id)
        {
            List<Menu> menuList = new List<Menu>();
            //string query = "select * from BillInfo where id = " + id/* + " and b.status = 0"*/;
            string query = "exec USP_GetServiceForUser @roomID";
            DataTable data = DataProvider.Instace.ExecuteQuery(query, new object[] { id });

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                menuList.Add(menu);
            }
            return menuList;
        }
    }
}
