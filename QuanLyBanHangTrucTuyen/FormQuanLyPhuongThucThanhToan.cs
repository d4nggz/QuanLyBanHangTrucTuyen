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
            dgvQLPT.AutoGenerateColumns = true;
            LoadGrid();
        }
        void LoadGrid()
        {
            List<QLPTTT_DTO> list = bus.GetAll();
            dgvQLPT.DataSource = list;

            if (dgvQLPT.Columns["PaymentMethodId"] != null)
                dgvQLPT.Columns["PaymentMethodId"].HeaderText = "Mã Phương Thức";

            if (dgvQLPT.Columns["MethodName"] != null)
                dgvQLPT.Columns["MethodName"].HeaderText = "Tên Phương Thức Thanh Toán";

            dgvQLPT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvQLPT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var item = dgvQLPT.Rows[e.RowIndex].DataBoundItem as QLPTTT_DTO;
                if (item != null){
                    txtTenPT.Text = item.MethodName;
                }
            }
        }
        void ResetForm()
        {
            txtTenPT.Clear();
            txtTenPT.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            QLPTTT_DTO p = new QLPTTT_DTO();
            p.MethodName = txtTenPT.Text.Trim();

            string result = bus.Add(p);
            if (result == "Success")
            {
                MessageBox.Show("Thêm phương thức thanh toán thành công!", "Thông báo");
                LoadGrid();
                ResetForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvQLPT.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phương thức cần sửa!", "Thông báo");
                return;
            }
            var currentItem = dgvQLPT.CurrentRow.DataBoundItem as QLPTTT_DTO;
            QLPTTT_DTO p = new QLPTTT_DTO();
            p.PaymentMethodId = currentItem.PaymentMethodId;
            p.MethodName = txtTenPT.Text.Trim();

            if (bus.Update(p))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadGrid();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra dữ liệu!", "Lỗi");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvQLPT.CurrentRow == null) return;

            var currentItem = dgvQLPT.CurrentRow.DataBoundItem as QLPTTT_DTO;

            if (MessageBox.Show($"Bạn có chắc muốn xóa phương thức '{currentItem.MethodName}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string result = bus.Delete(currentItem.PaymentMethodId);
                if (result == "Success")
                {
                    MessageBox.Show("Đã xóa thành công!", "Thông báo");
                    LoadGrid();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show(result, "Lỗi ràng buộc dữ liệu");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
