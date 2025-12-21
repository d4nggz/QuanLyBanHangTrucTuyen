using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHangTrucTuyen
{
    public partial class FormQuanLyNguoiDung: Form
    {
        UserBUS bus = new UserBUS();
        public FormQuanLyNguoiDung()
        {
            InitializeComponent();
        }
        void LoadGrid()
        {
            dgvNguoiDung.DataSource = bus.GetAllUsers();
            dgvNguoiDung.Columns["UserID_N01"].HeaderText = "Mã Người Dùng";
            dgvNguoiDung.Columns["Username_N01"].HeaderText = "Tên Đăng Nhập";
            dgvNguoiDung.Columns["Email_N01"].HeaderText = "Email";
            dgvNguoiDung.Columns["Phone_N01"].HeaderText = "Số Điện Thoại";
            dgvNguoiDung.Columns["Address_N01"].HeaderText = "Địa Chỉ";
            dgvNguoiDung.Columns["RoleName_N01"].HeaderText = "Vai Trò";
            dgvNguoiDung.Columns["Status_N01"].HeaderText = "Trạng Thái Người Dùng";
            if (dgvNguoiDung.Columns.Contains("Password_N01"))
            {
                dgvNguoiDung.Columns["Password_N01"].Visible = false;
            }
            if (dgvNguoiDung.Columns.Contains("RoleID_N01"))
            {
                dgvNguoiDung.Columns["RoleID_N01"].Visible = false;
            }

        }
        void LoadRoleComboBox()
        {
            cboRole.DataSource = bus.GetAllRoles();
            cboRole.DisplayMember = "RoleName_N01";
            cboRole.ValueMember = "RoleID_N01";
            cboRole.SelectedIndex = -1;
        }
        
        private void FormQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            LoadGrid();
            LoadRoleComboBox();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormCTNguoiDung frm = new FormCTNguoiDung();
            frm.currentUserId = -1;
            frm.ShowDialog();
            LoadGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvNguoiDung.CurrentRow.Cells["UserID_N01"].Value);

                FormCTNguoiDung frm = new FormCTNguoiDung();
                frm.currentUserId = id;
                frm.ShowDialog();

                LoadGrid();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvNguoiDung.CurrentRow.Cells["UserID_N01"].Value);
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bus.DeleteUser(id);
                    LoadGrid();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
