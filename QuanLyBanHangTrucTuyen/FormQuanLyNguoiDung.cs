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
        QLND_BUS bus = new QLND_BUS();
        public FormQuanLyNguoiDung()
        {
            InitializeComponent();
        }
        void SetHeader(string propertyName, string headerText)
        {
            if (dgvNguoiDung.Columns[propertyName] != null)
            {
                dgvNguoiDung.Columns[propertyName].HeaderText = headerText;
            }
        }
        void LoadGrid()
        {
            List<QLND_DTO> listUser = bus.GetListUsers();
            dgvNguoiDung.DataSource = listUser;
            SetHeader("UserId", "Mã ID");
            SetHeader("Username", "Tên Đăng Nhập");
            SetHeader("Email", "Email");
            SetHeader("Phone", "Số Điện Thoại");
            SetHeader("Address", "Địa Chỉ");
            SetHeader("RoleName", "Vai Trò");
            SetHeader("Status", "Trạng Thái");

            if (dgvNguoiDung.Columns["Password"] != null)
                dgvNguoiDung.Columns["Password"].Visible = false;

            if (dgvNguoiDung.Columns["RoleId"] != null)
                dgvNguoiDung.Columns["RoleId"].Visible = false;

        }
        void LoadComboBoxTimKiem()
        {
            DataTable dt = bus.GetRoles();
            DataRow dr = dt.NewRow();
            dr["role_id"] = -1;
            dr["role_name"] = "--- Tất cả ---";
            dt.Rows.InsertAt(dr, 0);

            cboRole.DataSource = dt;
            cboRole.DisplayMember = "role_name";
            cboRole.ValueMember = "role_id";
            cboRole.SelectedIndex = 0;
        }

        private void FormQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            LoadGrid();
            LoadComboBoxTimKiem();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormCTNguoiDung frm = new FormCTNguoiDung();
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK) LoadGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.CurrentRow != null)
            {
                QLND_DTO selectedUser = dgvNguoiDung.CurrentRow.DataBoundItem as QLND_DTO;
                if (selectedUser != null)
                {
                    FormCTNguoiDung frm = new FormCTNguoiDung();
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.SetUserDetail(selectedUser);
                    if (frm.ShowDialog() == DialogResult.OK) LoadGrid();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.CurrentRow != null)
            {
                QLND_DTO selectedUser = dgvNguoiDung.CurrentRow.DataBoundItem as QLND_DTO;
                if (MessageBox.Show($"Xóa user '{selectedUser.Username}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (bus.DeleteUser(selectedUser.UserId)) LoadGrid();
                    else MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTuKhoa.Text.Trim();
            int roleId = Convert.ToInt32(cboRole.SelectedValue);

            // Tìm kiếm trả về List, Grid tự sinh cột lại
            List<QLND_DTO> results = bus.SearchUsers(tuKhoa, roleId);
            dgvNguoiDung.DataSource = results;

            // Nếu tìm thấy thì phải set lại header vì DataSource bị reset
            if (results.Count > 0)
            {
                // Gọi lại logic đổi tên cột (có thể tách hàm SetHeader ra để tái sử dụng)
                SetHeader("UserId", "Mã ID");
                SetHeader("Username", "Tên Đăng Nhập");
                // ... (như hàm LoadGrid)
                if (dgvNguoiDung.Columns["Password"] != null) dgvNguoiDung.Columns["Password"].Visible = false;
                if (dgvNguoiDung.Columns["RoleId"] != null) dgvNguoiDung.Columns["RoleId"].Visible = false;
            }
            else
            {
                MessageBox.Show("Không tìm thấy!");
            }
        }
    }
}
