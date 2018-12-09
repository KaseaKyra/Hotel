using Hotel.DAO;
using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }

        private void ChangeAccount(Account acc)
        {
            txbUsername.Text = acc.Username;
            txbDisplayedName.Text = acc.DisplayedName;
            txbPassword.Text = "";
        }

        public fAccountProfile()
        {
            InitializeComponent();
        }

        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            ChangeAccount(acc);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        void UpdateAccountInfo()
        {
            string displayedName = txbDisplayedName.Text;
            string password = txbPassword.Text;
            string newPassword = txtNewPassword.Text;
            string reenterPass = txbReenterPass.Text;
            string username = txbUsername.Text;

            if (!newPassword.Equals(reenterPass))
            {
                MessageBox.Show("Mật khẩu không trùng khớp. Vui lòng nhập lại mật khẩu mới");
            }
            else
            {
                if (AccountDAO.Instace.UpdateAccount(username, password, displayedName, newPassword))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    if (updateAccount != null)
                    {
                        updateAccount(this, new AccountEvent(AccountDAO.Instace.GetAccountByUsername(username)));
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu trước khi thay đôi thông tin!");
                }
            }
        }

        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

    }
}
