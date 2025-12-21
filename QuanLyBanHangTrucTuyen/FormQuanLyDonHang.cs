using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace QuanLyBanHangTrucTuyen
{
    public partial class FormQuanLyDonHang: Form
    {
        QLDH_BUS bus = new QLDH_BUS();
        public FormQuanLyDonHang()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void LoadStatusComboBox()
        {            
            cboTrangThaiXuLy.DataSource = bus.GetAllStatuses();
            cboTrangThaiXuLy.DisplayMember = "StatusName_N01";
            cboTrangThaiXuLy.ValueMember = "StatusID_N01";
            cboTrangThaiXuLy.SelectedIndex = -1;
        }

        void LoadOrderGrid()
        {
            dgvDonHang.DataSource = bus.GetAllOrders();
            dgvDonHang.Columns["OrderID_N01"].HeaderText = "Mã Đơn";
            dgvDonHang.Columns["Username_N01"].HeaderText = "Khách Hàng";
            dgvDonHang.Columns["OrderDate_N01"].HeaderText = "Ngày Đặt";
            dgvDonHang.Columns["TotalAmount_N01"].HeaderText = "Tổng Tiền";
            dgvDonHang.Columns["StatusName_N01"].HeaderText = "Trạng Thái";

            // Ẩn cột ID trạng thái và thông tin phụ đi cho gọn
            if (dgvDonHang.Columns.Contains("StatusID_N01")) dgvDonHang.Columns["StatusID_N01"].Visible = false;
            if (dgvDonHang.Columns.Contains("Address_N01")) dgvDonHang.Columns["Address_N01"].Visible = false;
            if (dgvDonHang.Columns.Contains("Phone_N01")) dgvDonHang.Columns["Phone_N01"].Visible = false;
        }
        private void FormQuanLyDonHang_Load(object sender, EventArgs e)
        {
            LoadComboBoxTimKiem();
            LoadStatusComboBox();
            LoadOrderGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDonHang.Rows[e.RowIndex];
                txtTenKH.Text = row.Cells["Username_N01"].Value.ToString();
                txtSDT.Text = row.Cells["Phone_N01"].Value.ToString();
                txtDC.Text = row.Cells["Address_N01"].Value.ToString();

                txtTongTien.Text = row.Cells["TotalAmount_N01"].Value.ToString();
                cboTrangThaiXuLy.SelectedValue = row.Cells["StatusID_N01"].Value;

                int orderId = int.Parse(row.Cells["OrderID_N01"].Value.ToString());
                LoadChiTietDonHang(orderId);
            }
        }
        void LoadChiTietDonHang(int orderId)
        {
            dgvChiTiet.DataSource = bus.GetOrderDetails(orderId);
            dgvChiTiet.Columns["ProductName_N01"].HeaderText = "Tên Sản Phẩm";
            dgvChiTiet.Columns["Quantity_N01"].HeaderText = "Số Lượng";
            dgvChiTiet.Columns["UnitPrice_N01"].HeaderText = "Đơn Giá";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
        }

        private void btnCN_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow != null)
            {
                int maDon = int.Parse(dgvDonHang.CurrentRow.Cells["OrderID_N01"].Value.ToString());
                int statusId = (int)cboTrangThaiXuLy.SelectedValue;

                bus.UpdateOrderStatus(maDon, statusId);
                LoadOrderGrid(); // Load lại để thấy trạng thái mới
                MessageBox.Show("Cập nhật trạng thái thành công!");
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null || dgvDonHang.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng muốn xóa!", "Thông báo");
                return;
            }

            try
            {
                // 2. Lấy Mã Đơn
                int maDon = Convert.ToInt32(dgvDonHang.CurrentRow.Cells["OrderID_N01"].Value);

                // 3. Hỏi xác nhận 
                DialogResult dr = MessageBox.Show(
                    $"CẢNH BÁO: Bạn có chắc chắn muốn XÓA VĨNH VIỄN đơn hàng #{maDon}?\n" +
                    "Dữ liệu chi tiết của đơn hàng này cũng sẽ bị xóa theo.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (dr == DialogResult.Yes)
                {
                    bus.XoaDonHang(maDon);

                    LoadOrderGrid();

                    dgvChiTiet.DataSource = null;

                    MessageBox.Show("Đã xóa đơn hàng thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không thể xóa: " + ex.Message);
            }
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";
            dtpTu.Value = DateTime.Now.AddMonths(-1);
            dtpDen.Value = DateTime.Now;
            LoadOrderGrid();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtDC.Clear();
            txtTongTien.Clear();
            dgvChiTiet.DataSource = null;
        }
        void LoadComboBoxTimKiem()
        {
            DataTable dt = bus.GetAllStatuses();
            DataRow dr = dt.NewRow();
            dr["StatusID_N01"] = -1; 
            dr["StatusName_N01"] = "--- Tất cả ---";
            dt.Rows.InsertAt(dr, 0);

            cboTTDON.DataSource = dt;
            cboTTDON.DisplayMember = "StatusName_N01";
            cboTTDON.ValueMember = "StatusID_N01";
            cboTTDON.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                DateTime tuNgay = dtpTu.Value;
                DateTime denNgay = dtpDen.Value;

                int trangThaiID = -1;
                if (cboTTDON.SelectedValue != null)
                {
                   
                    int.TryParse(cboTTDON.SelectedValue.ToString(), out trangThaiID);
                }

              
                DataTable dt = bus.TimKiem(tuKhoa, tuNgay, denNgay, trangThaiID);

                
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvDonHang.DataSource = dt;
                }
                else
                {
                    dgvDonHang.DataSource = null;
                    MessageBox.Show("Không tìm thấy đơn hàng nào phù hợp!", "Kết quả tìm kiếm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}
