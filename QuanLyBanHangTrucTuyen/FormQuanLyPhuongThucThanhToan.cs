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
    public partial class FormQuanLyPhuongThucThanhToan : Form
    {
        QLPTTT_BUS bus = new QLPTTT_BUS();
        public FormQuanLyPhuongThucThanhToan()
        {
            InitializeComponent();
        }

        private void FormQuanLyPhuongThucThanhToan_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        void LoadGrid()
        {
            dgvQLPT.DataSource = bus.GetAll();
            dgvQLPT.Columns["MethodID_N01"].HeaderText = "Mã PTTT";
            dgvQLPT.Columns["MethodName_N01"].HeaderText = "Tên Phương Thức Thanh Toán";
            dgvQLPT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvQLPT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQLPT.Rows[e.RowIndex];
                txtTenPT.Text = row.Cells["MethodName_N01"].Value.ToString();
            }
        }
        void ResetForm()
        {
            txtTenPT.Clear();
            txtTenPT.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPT.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phương thức!");
                return;
            }

            try
            {
                QLPTTT_DTO p = new QLPTTT_DTO();
                p.MethodName = txtTenPT.Text;

                bus.Add(p);
                LoadGrid();
                ResetForm();
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvQLPT.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!");
                return;
            }

            try
            {
                QLPTTT_DTO p = new QLPTTT_DTO();
                p.PaymentMethodId = Convert.ToInt32(dgvQLPT.CurrentRow.Cells["MethodID_N01"].Value);
                p.MethodName = txtTenPT.Text;

                bus.Edit(p);
                LoadGrid();
                ResetForm();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvQLPT.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvQLPT.CurrentRow.Cells["MethodID_N01"].Value);

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    bus.Remove(id);
                    LoadGrid();
                    ResetForm();
                    MessageBox.Show("Đã xóa!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa phương thức này vì đang được sử dụng trong đơn hàng!");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
