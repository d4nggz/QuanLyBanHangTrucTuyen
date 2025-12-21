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
        public int currentUserId = -1;
        UserBUS bus = new UserBUS();
        public FormCTNguoiDung()
        {
            InitializeComponent();
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

            LoadComboBoxRole();
            if (currentUserId != -1)
            {
                this.Text = "Cập Nhật Người Dùng";
                LoadDataToEdit();
            }
            else
            {
                this.Text = "Thêm Mới Người Dùng";
            }
        }
        void LoadComboBoxRole()
        {
            cboVaiTro.DataSource = bus.GetAllRoles();
            cboVaiTro.DisplayMember = "RoleName_N01";
            cboVaiTro.ValueMember = "RoleID_N01";
        }

        void LoadDataToEdit()
        {
            DataTable dt = bus.GetUserByID(currentUserId);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                txtTenDangNhap.Text = r["Username_N01"].ToString();
                txtMatKhau.Text = r["Password_N01"].ToString();
                txtEmail.Text = r["Email_N01"].ToString();
                txtSDT.Text = r["Phone_N01"].ToString();
                txtDiaChi.Text = r["Address_N01"].ToString();
                cboVaiTro.SelectedValue = r["RoleID_N01"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                QLND_DTO u = new QLND_DTO();
                u.Username = txtTenDangNhap.Text;
                u.Password = txtMatKhau.Text;
                u.Email = txtEmail.Text;
                u.Phone = txtSDT.Text;
                u.Address = txtDiaChi.Text;
                u.RoleId = Convert.ToInt32(cboVaiTro.SelectedValue);

                if (currentUserId == -1)
                {
                    bus.AddUser(u);
                    MessageBox.Show("Thêm mới thành công!");
                }
                else
                {
                    u.UserId = currentUserId;
                    bus.UpdateUser(u);
                    MessageBox.Show("Cập nhật thành công!");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
    }
}
