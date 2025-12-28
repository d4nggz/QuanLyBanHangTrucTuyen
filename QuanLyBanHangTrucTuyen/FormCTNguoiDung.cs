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
    public partial class FormCTNguoiDung: Form
    {
        private int _userId = 0;
        QLND_BUS bus = new QLND_BUS();
        public FormCTNguoiDung()
        {
            InitializeComponent();
            txtMatKhau.UseSystemPasswordChar = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCTNguoiDung_Load(object sender, EventArgs e)
        {

            LoadRoleComboBox();
           
        }
        void LoadRoleComboBox()
        {
            cboVaiTro.DataSource = bus.GetRoles();
            cboVaiTro.DisplayMember = "role_name";
            cboVaiTro.ValueMember = "role_id";
        }

        public void SetUserDetail(QLND_DTO user)
        {
            _userId = user.UserId;

            txtTenDangNhap.Text = user.Username;
            txtTenDangNhap.Enabled = false;

            txtEmail.Text = user.Email;
            txtSDT.Text = user.Phone;
            txtDiaChi.Text = user.Address;
            cboVaiTro.SelectedValue = user.RoleId;

            txtMatKhau.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!");
                return;
            }

            QLND_DTO u = new QLND_DTO();
            u.UserId = _userId;
            u.Username = txtTenDangNhap.Text.Trim();
            u.Email = txtEmail.Text.Trim();
            u.Phone = txtSDT.Text.Trim();
            u.Address = txtDiaChi.Text.Trim();
            u.RoleId = Convert.ToInt32(cboVaiTro.SelectedValue);
            u.Status = "Active";

            if (_userId == 0)
            {
                u.Password = txtMatKhau.Text.Trim();

                string ketQua = bus.AddUser(u);

                if (ketQua == "Success")
                {
                    MessageBox.Show("Thêm người dùng thành công!");
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + ketQua);
                }
            }
            else
            {
                if (bus.UpdateUser(u))
                {

                    if (!string.IsNullOrEmpty(txtMatKhau.Text))
                    {
                        bus.ResetPassword(u.UserId, txtMatKhau.Text);
                    }

                    MessageBox.Show("Cập nhật thông tin thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
    }
}
