﻿using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instace;

        public static AccountDAO Instace
        {
            get { if (instace == null) instace = new AccountDAO(); return AccountDAO.instace; }
            private set { AccountDAO.instace = value; }
        }

        private AccountDAO() { }

        public bool Login(string username, string password)
        {
            //string query = "select * from Accounts where username = N'" + username + "' and password = N'" + password + "'";
            string query = "exec USP_Login @username , @password";
            DataTable result = DataProvider.Instace.ExecuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }

        public Account GetAccountByUsername(string username)
        {
            string query = "exec USP_GetAccountByUsername @username";

            DataTable row = DataProvider.Instace.ExecuteQuery(query, new object[] { username });

            foreach (DataRow item in row.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public bool UpdateAccount(string username, string pass, string displayedName, string newPass)
        {
            string query = "exec USP_UpdateAccount @username , @displayedName , @password , @newPassword ";

            int result = DataProvider.Instace.ExecuteNonQuery(query, new object[] { username, displayedName, pass, newPass });

            return result > 0;
        }
    }
}
