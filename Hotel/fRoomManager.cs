
using Hotel.DAO;
using Hotel.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class fRoomManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public fRoomManager(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadRoomManager();
        }

        void LoadRoomManager()
        {
            loadRoom();
            loadRoomTypeName();
            loadServices();
        }

        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + loginAccount.DisplayedName + ")";
        }

        void loadRoom()
        {
            flpRoom.Controls.Clear();

            List<Room> roomList = RoomDAO.Instace.LoadRoomList();
            foreach (Room item in roomList)
            {
                Button btn = new Button() { Width = RoomDAO.roomWidth, Height = RoomDAO.roomHeight };
                int status = item.Status;
                string displayedStatus = "";
                if (status == 1)
                {
                    displayedStatus = "Có người ở";
                }
                else if (status == 0)
                {
                    displayedStatus = "Trống";
                }
                else
                {
                    displayedStatus = "Đã đặt";
                }

                switch (status)
                {
                    case 0:
                        btn.BackColor = Color.AliceBlue;
                        break;
                    case 1:
                        btn.BackColor = Color.YellowGreen;
                        break;
                    case 2:
                        btn.BackColor = Color.LightPink;
                        break;
                    default:
                        btn.BackColor = Color.AliceBlue;
                        break;
                }

                btn.Text = item.Id + ": " + item.Name + Environment.NewLine + displayedStatus;

                btn.Click += btn_Click;
                btn.Tag = item;

                flpRoom.Controls.Add(btn);
            }
        }

        void loadRoomTypeName()
        {
            List<RoomType> listRoomType = RoomTypeDAO.Instace.GetListRoomType();
            comboBox1.DataSource = listRoomType;
            comboBox1.DisplayMember = "Name";
        }

        void loadRoomTypePriceByID(int id)
        {
            List<RoomType> listRoomType = RoomTypeDAO.Instace.GetListRoomType(id);
            comboBox2.DataSource = listRoomType;
            comboBox2.DisplayMember = "Price";
        }

        void loadServices()
        {
            List<Service> listService = ServiceDAO.Instace.GetListService();
            cbService.DataSource = listService;
            cbService.DisplayMember = "name";
        }

        void loadServicePriceByID(int id)
        {
            List<Service> listService = ServiceDAO.Instace.GetListService(id);
            cbServicePrice.DataSource = listService;
            cbServicePrice.DisplayMember = "price";
        }
        #endregion

        void showBill(int id)
        {
            lvBill.Items.Clear();
            List<DTO.Menu> listBillInfo = MenuDAO.Instace.GetListMenuByRoom(id);
            double totalPrice = 0;

            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lvItem = new ListViewItem(item.ServiceName);
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.Count.ToString());
                lvItem.SubItems.Add(item.TotalPrice.ToString());

                totalPrice += item.TotalPrice;//tính tổng tiền dịch vụ

                lvBill.Items.Add(lvItem);
            }

            CultureInfo culture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentCulture = culture;
            txbTotalPrice.Text = totalPrice.ToString("c", culture);
        }

        #region Event
        void btn_Click(object sender, EventArgs e)
        {
            int roomID = ((sender as Button).Tag as Room).Id;
            lvBill.Tag = (sender as Button).Tag;
            showBill(roomID);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayedName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();

            f.InsertRoom += f_InsertRoom;
            f.UpdateRoom += f_UpdateRoom;
            f.DeleteRoom += f_DeleteRoom;

            f.InsertService += f_InsertService;
            f.UpdateService += f_UpdateService;
            f.DeleteService += f_DeleteService;

            f.InsertRoomType += f_InsertRoomType;
            f.UpdateRoomType += f_UpdateRoomType;
            f.DeleteRoomType += f_DeleteRoomType;

            f.ShowDialog();
        }

        void f_DeleteRoomType(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
            loadRoom();
        }

        void f_UpdateRoomType(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
        }

        void f_InsertRoomType(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
        }

        void f_DeleteService(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
            loadRoom();
        }

        void f_UpdateService(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
        }

        void f_InsertService(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
        }

        void f_DeleteRoom(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
            loadRoom();
        }

        void f_UpdateRoom(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
        }

        void f_InsertRoom(object sender, EventArgs e)
        {
            loadRoomTypeName();
            loadServices();
            if (lvBill.Tag != null)
            {
                showBill((lvBill.Tag as Room).Id);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            RoomType selected = cb.SelectedItem as RoomType;
            id = selected.Id;
            //loadRoomTypeName();
            loadRoomTypePriceByID(id);
        }

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            Service selected = cb.SelectedItem as Service;
            id = selected.Id;
            //loadRoomTypeName();
            loadServicePriceByID(id);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadRoomTypePriceByID();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            Room room = lvBill.Tag as Room;
            int idBill = BillDAO.Instace.getUncheckBillIDByRoomID(room.Id);
            int serviceID = (cbService.SelectedItem as Service).Id;
            int count = (int)nudAddService.Value;

            // phòng chưa có bill: tạo bill, thêm billinfo
            if (idBill == -1)
            {
                BillDAO.Instace.insertBill(room.Id);
                BillInfoDAO.Instace.insertBillInfo(BillDAO.Instace.getMaxBillID(), serviceID, count);
            }
            else // đã có bill thì chèn thêm dịch vụ
            {
                BillInfoDAO.Instace.insertBillInfo(idBill, serviceID, count);
            }

            showBill(room.Id);
            loadRoom();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Room room = lvBill.Tag as Room;
            int billID = BillDAO.Instace.getUncheckBillIDByRoomID(room.Id);
            int discount = (int)nudDiscount.Value;

            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finalPrice = totalPrice - (totalPrice / 100) * discount;

            if (billID != -1)
            {
                if (MessageBox.Show("Bạn có chắc thanh toán hóa đơn cho phòng " + room.Name, "Thông báo",
                                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instace.Checkout(billID, discount, finalPrice);
                    showBill(room.Id);
                }
            }
            loadRoom();
        }

        private void nudAddService_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}

