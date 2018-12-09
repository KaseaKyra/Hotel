using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Hotel.DAO;
using Hotel.DTO;

namespace Hotel
{
    public partial class fAdmin : Form
    {
        BindingSource roomList = new BindingSource();
        BindingSource serviceList = new BindingSource();
        BindingSource billList = new BindingSource();
        BindingSource roomTypeList = new BindingSource();
        BindingSource accountList = new BindingSource();

        public fAdmin()
        {
            InitializeComponent();

            loadFormAdmin();

        }

        void loadFormAdmin()
        {
            dgvRoom.DataSource = roomList;
            dgvService.DataSource = serviceList;
            dgvBill.DataSource = billList;
            dgvAccount.DataSource = accountList;
            dgvCategory.DataSource = roomTypeList;

            LoadListBill();

            LoadListService();
            AddServiceBinding();

            LoadListRoom();
            AddRoomBinding();
            LoadRoomTypeIntoComboBox(cbCategoryRoom);

            LoadListRoomType();
            AddRoomTypeBinding();

            LoadListAccount();
            AddAccountBinding();
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        #region methods
        void LoadListBill()
        {
            billList.DataSource = BillDAO.Instace.GetListBill();
        }

        void LoadListService()
        {
            serviceList.DataSource = ServiceDAO.Instace.GetListService();
        }

        void LoadListRoom()
        {
            roomList.DataSource = RoomDAO.Instace.LoadRoomList();
        }

        void LoadListRoomType()
        {
            roomTypeList.DataSource = RoomTypeDAO.Instace.GetListRoomType();
        }

        private void LoadListAccount()
        {
            accountList.DataSource = AccountDAO.Instace.GetListAccount();
        }

        void AddServiceBinding()
        {
            txbServiceID.DataBindings.Add(new Binding("Text", dgvService.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbServiceName.DataBindings.Add(new Binding("Text", dgvService.DataSource, "name", true, DataSourceUpdateMode.Never));
            nudServicePrice.DataBindings.Add(new Binding("Value", dgvService.DataSource, "price", true, DataSourceUpdateMode.Never));
        }

        void AddRoomBinding()
        {
            txbRoomID.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbRoomName.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbRoomStatus.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "Status", true, DataSourceUpdateMode.Never));
            txbRoomDescription.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "Description", true, DataSourceUpdateMode.Never));
        }

        private void AddRoomTypeBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            nudCategoryPrice.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        private void AddAccountBinding()
        {
            txbAccountID.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbUsername.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "Username", true, DataSourceUpdateMode.Never));
            txbDisplayedName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "DisplayedName", true, DataSourceUpdateMode.Never));
        }

        void LoadRoomTypeIntoComboBox(ComboBox cb)
        {
            cbCategoryRoom.DataSource = RoomTypeDAO.Instace.GetListRoomType();
            cb.DisplayMember = "Name";
        }
        #endregion

        #region event
        private void dgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShowService_Click(object sender, EventArgs e)
        {
            LoadListService();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            string name = txbServiceName.Text;
            double price = Convert.ToInt32(nudServicePrice.Value);

            if (ServiceDAO.Instace.InsertService(name, price))
            {
                MessageBox.Show("Thêm dịch vụ thành công!");
                LoadListService();

                if (insertService != null)
                {
                    insertService(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm dịch vụ!");
            }
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbServiceID.Text);

            if (ServiceDAO.Instace.DeleteService(id))
            {
                MessageBox.Show("Xóa dịch vụ thành công!");
                LoadListService();

                if (deleteService != null)
                {
                    deleteService(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa dịch vụ!");
            }
        }

        private void btnEditService_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbServiceID.Text);
            string name = txbServiceName.Text;
            double price = Convert.ToDouble(nudServicePrice.Value);

            if (ServiceDAO.Instace.UpdateService(id, name, price))
            {
                MessageBox.Show("Cập nhật thông tin dịch vụ thành công!");
                LoadListService();

                if (updateService != null)
                {
                    updateService(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật thông tin dịch vụ!");
            }
        }

        private event EventHandler insertService;
        public event EventHandler InsertService
        {
            add { insertService += value; }
            remove { insertService -= value; }
        }

        private event EventHandler deleteService;
        public event EventHandler DeleteService
        {
            add { deleteService += value; }
            remove { deleteService -= value; }
        }

        private event EventHandler updateService;
        public event EventHandler UpdateService
        {
            add { updateService += value; }
            remove { updateService -= value; }
        }

        private void txbRoomID_TextChanged(object sender, EventArgs e)
        {

            if (dgvRoom.SelectedCells.Count > 0)
            {

                int id = (int)dgvRoom.SelectedCells[0].OwningRow.Cells["TypeID"].Value;
                RoomType rt = RoomTypeDAO.Instace.GetRoomTypeByID(id);
                int index = -1;
                int i = 0;

                foreach (RoomType item in cbCategoryRoom.Items)
                {
                    if (item.Id == rt.Id)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }

                cbCategoryRoom.SelectedIndex = index;
            }


        }

        private void btnShowRoom_Click_1(object sender, EventArgs e)
        {
            LoadListRoom();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            string name = txbRoomName.Text;
            int type = (cbCategoryRoom.SelectedItem as RoomType).Id;
            string des = txbRoomDescription.Text;
            int status = 0;

            if (RoomDAO.Instace.InsertRoom(name, type, des, status))
            {
                MessageBox.Show("Thêm phòng thành công!");
                LoadListRoom();

                if (insertRoom != null)
                {
                    insertRoom(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm phòng!");
            }
        }

        private void BtnEditRoom_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbRoomID.Text);
            string name = txbRoomName.Text;
            int type = (cbCategoryRoom.SelectedItem as RoomType).Id;
            string des = txbRoomDescription.Text;
            int status = Convert.ToInt32(txbRoomStatus.Text);

            if (RoomDAO.Instace.UpdateRoom(id, name, type, des, status))
            {
                MessageBox.Show("Cập nhật thông tin thành công!");
                LoadListRoom();

                if (updateRoom != null)
                {
                    updateRoom(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật thông tin!");
            }
        }

        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbRoomID.Text);

            if (RoomDAO.Instace.DeleteRoom(id))
            {
                MessageBox.Show("Xóa thành công!");
                LoadListRoom();

                if (deleteRoom != null)
                {
                    deleteRoom(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!");
            }
        }

        private event EventHandler insertRoom;
        public event EventHandler InsertRoom
        {
            add { insertRoom += value; }
            remove { insertRoom -= value; }
        }

        private event EventHandler deleteRoom;
        public event EventHandler DeleteRoom
        {
            add { deleteRoom += value; }
            remove { deleteRoom -= value; }
        }

        private event EventHandler updateRoom;
        public event EventHandler UpdateRoom
        {
            add { updateRoom += value; }
            remove { updateRoom -= value; }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListRoomType();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            string name = txbCategoryName.Text;
            double price = Convert.ToDouble(nudCategoryPrice.Value);

            if (RoomTypeDAO.Instace.UpdateRoomTypeByID(id, name, price))
            {
                MessageBox.Show("Cập nhật thông tin danh mục phòng thành công!");
                LoadListRoomType();

                if (updateRoomType != null)
                {
                    updateRoomType(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật thông tin danh mục phòng!");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (RoomTypeDAO.Instace.DeleteRoomType(id))
            {
                MessageBox.Show("Xóa danh mục phòng thành công!");
                LoadListRoomType();

                if (deleteRoomType != null)
                {
                    deleteRoomType(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục phòng!");
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            double price = Convert.ToInt32(nudCategoryPrice.Value);

            if (RoomTypeDAO.Instace.InsertRoomType(name, price))
            {
                MessageBox.Show("Thêm danh mục phòng thành công!");
                LoadListRoomType();

                if (insertRoomType != null)
                {
                    insertRoomType(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục phòng!");
            }
        }

        private event EventHandler insertRoomType;
        public event EventHandler InsertRoomType
        {
            add { insertRoomType += value; }
            remove { insertRoomType -= value; }
        }

        private event EventHandler deleteRoomType;
        public event EventHandler DeleteRoomType
        {
            add { deleteRoomType += value; }
            remove { deleteRoomType -= value; }
        }

        private event EventHandler updateRoomType;
        public event EventHandler UpdateRoomType
        {
            add { updateRoomType += value; }
            remove { updateRoomType -= value; }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string pass = "123456";
            string displayedName = txbDisplayedName.Text;
            int type = (cbCategoryRoom.SelectedItem as RoomType).Id; ;

            if (AccountDAO.Instace.InsertAccount(username, pass, displayedName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm tài khoản!");
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbAccountID.Text);

            if (AccountDAO.Instace.DeleteAccount(id))
            {
                MessageBox.Show("Xóa tài khoản thành công!");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa tài khoản!");
            }

        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }
        #endregion

    }
}
