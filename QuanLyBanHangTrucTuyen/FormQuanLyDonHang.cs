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
            DataTable dt = bus.GetStatuses();
            cboTrangThaiXuLy.DataSource = dt;
            cboTrangThaiXuLy.DisplayMember = "status_name";
            cboTrangThaiXuLy.ValueMember = "status_id";
            cboTrangThaiXuLy.SelectedIndex = -1;
        }

        void LoadOrderGrid()
        {
            List<OrderDTO> listOrders = bus.GetOrders();
            dgvDonHang.DataSource = listOrders;
            if (dgvDonHang.Columns["OrderId"] != null)
                dgvDonHang.Columns["OrderId"].HeaderText = "Mã Đơn";

            if (dgvDonHang.Columns["CustomerName"] != null)
                dgvDonHang.Columns["CustomerName"].HeaderText = "Khách Hàng";

            if (dgvDonHang.Columns["OrderDate"] != null)
            {
                dgvDonHang.Columns["OrderDate"].HeaderText = "Ngày Đặt";
                dgvDonHang.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvDonHang.Columns["TotalAmount"] != null)
            {
                dgvDonHang.Columns["TotalAmount"].HeaderText = "Tổng Tiền";
                dgvDonHang.Columns["TotalAmount"].DefaultCellStyle.Format = "#,##0";
                dgvDonHang.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvDonHang.Columns["StatusName"] != null)
                dgvDonHang.Columns["StatusName"].HeaderText = "Trạng Thái";

            if (dgvDonHang.Columns.Contains("StatusId")) dgvDonHang.Columns["StatusId"].Visible = false;
            if (dgvDonHang.Columns.Contains("Address")) dgvDonHang.Columns["Address"].Visible = false;
            if (dgvDonHang.Columns.Contains("Phone")) dgvDonHang.Columns["Phone"].Visible = false;
        }
        private void FormQuanLyDonHang_Load(object sender, EventArgs e)
        {
            LoadStatusComboBox();
            LoadComboBoxTimKiem();
            LoadOrderGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedOrder = dgvDonHang.Rows[e.RowIndex].DataBoundItem as OrderDTO;

                if (selectedOrder != null)
                {
                    txtTenKH.Text = selectedOrder.CustomerName;
                    txtSDT.Text = selectedOrder.Phone;
                    txtDC.Text = selectedOrder.Address;
                    txtTongTien.Text = selectedOrder.TotalAmount.ToString("N0"); // Format tiền

                    cboTrangThaiXuLy.SelectedValue = selectedOrder.StatusId;

                    LoadChiTietDonHang(selectedOrder.OrderId);
                }
            }
        }
            void LoadChiTietDonHang(int orderId)
            {
                DataTable dt = bus.GetOrderDetails(orderId);
                dgvChiTiet.DataSource = dt;
                dgvChiTiet.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";
                dgvChiTiet.Columns["quantity"].HeaderText = "Số Lượng";
                dgvChiTiet.Columns["unit_price"].HeaderText = "Đơn Giá";
                dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            }

        private void btnCN_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow != null)
            {
                var currentOrder = dgvDonHang.CurrentRow.DataBoundItem as OrderDTO;
                if (currentOrder == null) return;

                int maDon = currentOrder.OrderId;

                if (cboTrangThaiXuLy.SelectedValue == null) return;
                int statusId = Convert.ToInt32(cboTrangThaiXuLy.SelectedValue);

                if (bus.UpdateStatus(maDon, statusId))
                {
                    MessageBox.Show("Cập nhật trạng thái thành công!");
                    LoadOrderGrid();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng muốn xóa!", "Thông báo");
                return;
            }

            try
            {
                var currentOrder = dgvDonHang.CurrentRow.DataBoundItem as OrderDTO;
                int maDon = currentOrder.OrderId;

                DialogResult dr = MessageBox.Show(
                    $"CẢNH BÁO: Bạn có chắc chắn muốn XÓA VĨNH VIỄN đơn hàng #{maDon}?\n" +
                    "Dữ liệu chi tiết cũng sẽ bị xóa.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (dr == DialogResult.Yes)
                {
                    if (bus.DeleteOrder(maDon))
                    {
                        MessageBox.Show("Đã xóa đơn hàng thành công!");
                        LoadOrderGrid();
                        dgvChiTiet.DataSource = null;

                        txtTenKH.Clear(); txtSDT.Clear(); txtDC.Clear(); txtTongTien.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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
            DataTable dt = bus.GetStatuses();
            DataRow dr = dt.NewRow();
            dr["status_id"] = -1;
            dr["status_name"] = "--- Tất cả ---";
            dt.Rows.InsertAt(dr, 0);

            cboTTDON.DataSource = dt;
            cboTTDON.DisplayMember = "status_name";
            cboTTDON.ValueMember = "status_id";
            cboTTDON.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                DateTime tuNgay = dtpTu.Value;
                DateTime denNgay = dtpDen.Value;
                int trangThaiID = Convert.ToInt32(cboTTDON.SelectedValue);
                List<OrderDTO> listKetQua = bus.TimKiemDonHang(tuKhoa, tuNgay, denNgay, trangThaiID);

                if (listKetQua != null && listKetQua.Count > 0)
                {
                    dgvDonHang.DataSource = listKetQua;
                }
                else
                {
                    dgvDonHang.DataSource = new List<OrderDTO>();
                    MessageBox.Show("Không tìm thấy đơn hàng nào phù hợp!", "Kết quả");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}
