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

            LoadListBill();

            LoadListService();
            AddServiceBinding();

            LoadListRoom();
            AddRoomBinding();
            LoadRoomTypeIntoComboBox(cbCategoryRoom);
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

        void AddServiceBinding()
        {
            txbServiceID.DataBindings.Add(new Binding("Text", dgvService.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbServiceName.DataBindings.Add(new Binding("Text", dgvService.DataSource, "name", true, DataSourceUpdateMode.Never));
            nudServicePrice.DataBindings.Add(new Binding("Value", dgvService.DataSource, "price", true, DataSourceUpdateMode.Never));
        }

        void AddRoomBinding()
        {
            txbRoomID.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "ID"));
            txbRoomName.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbRoomStatus.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "Status", true, DataSourceUpdateMode.Never));
            txbRoomDescription.DataBindings.Add(new Binding("Text", dgvRoom.DataSource, "Description", true, DataSourceUpdateMode.Never));
        }

        void LoadRoomTypeIntoComboBox(ComboBox cb)
        {
            cbCategoryRoom.DataSource = RoomTypeDAO.Instace.GetListRoomType();
            cb.DisplayMember = "Name";
            ;
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

        private void btnShowRoom_Click_1(object sender, EventArgs e)
        {
            LoadListRoom();
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
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!");
            }
        }
        #endregion
    }
}
