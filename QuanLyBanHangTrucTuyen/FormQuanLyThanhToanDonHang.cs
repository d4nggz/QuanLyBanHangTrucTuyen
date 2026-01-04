using BUS;
using DTO;
using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class FormQuanLyThanhToanDonHang : Form
    {
        ThanhToan_BUS bus = new ThanhToan_BUS();
        public FormQuanLyThanhToanDonHang()
        {
            InitializeComponent();
        }

        private void FormQuanLyThanhToanDonHang_Load(object sender, EventArgs e)
        {
            
            
            LoadPaymentMethods();
            btnTimKiem_Click(null, null);
            EnableUpdate(false);
            dgvThanhToan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        void LoadPaymentMethods()
        {
            cboPTTT.DataSource = bus.GetMethods();
            cboPTTT.DisplayMember = "method_name";
            cboPTTT.ValueMember = "method_id";
            cboPTTT.SelectedIndex = -1;

            cboTrangThaiTT.DataSource = bus.GetStatuses();
            cboTrangThaiTT.DisplayMember = "statusName";
            cboTrangThaiTT.ValueMember = "statusID";
            cboTrangThaiTT.SelectedIndex = -1;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int methodVal = (cboPTTT.SelectedValue == null) ? -1 : Convert.ToInt32(cboPTTT.SelectedValue);
            int statusVal = (cboTrangThaiTT.SelectedValue == null) ? -1 : Convert.ToInt32(cboTrangThaiTT.SelectedValue);

            List<ThanhToan_DTO> list = bus.SearchPayment(txtMaDH.Text.Trim(), methodVal, statusVal);

            dgvThanhToan.DataSource = list;
            if (dgvThanhToan.Columns["OrderId"] != null)
                dgvThanhToan.Columns["OrderId"].HeaderText = "Mã Đơn Hàng";

            if (dgvThanhToan.Columns["Amount"] != null)
                dgvThanhToan.Columns["Amount"].HeaderText = "Số Tiền";

            if (dgvThanhToan.Columns["PaymentDate"] != null)
            {
                dgvThanhToan.Columns["PaymentDate"].HeaderText = "Ngày Thanh Toán";
                dgvThanhToan.Columns["PaymentDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvThanhToan.Columns["MethodName"] != null)
                dgvThanhToan.Columns["MethodName"].HeaderText = "Phương Thức";
            if (dgvThanhToan.Columns["StatusName"] != null)
                dgvThanhToan.Columns["StatusName"].HeaderText = "Trạng Thái";

            if (dgvThanhToan.Columns["MethodId"] != null) dgvThanhToan.Columns["MethodId"].Visible = false;
            if (dgvThanhToan.Columns["StatusId"] != null) dgvThanhToan.Columns["StatusId"].Visible = false;
            if (list.Count == 0) MessageBox.Show("Không tìm thấy dữ liệu!");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaDH.Text = "";
            cboPTTT.SelectedIndex = -1;
            cboTrangThaiTT.SelectedIndex = -1;
            EnableUpdate(false);
            btnTimKiem_Click(null, null);
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDH.Text)) return;

            if (MessageBox.Show("Bạn chắc chắn muốn cập nhật?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool ketQua = bus.UpdatePayment(
                    Convert.ToInt32(txtMaDH.Text.Trim()),
                    Convert.ToInt32(cboPTTT.SelectedValue),
                    Convert.ToInt32(cboTrangThaiTT.SelectedValue)
                );

                if (ketQua)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    btnTimKiem_Click(null, null);
                    EnableUpdate(false);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
        }

        private void dgvThanhToan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var item = dgvThanhToan.Rows[e.RowIndex].DataBoundItem as ThanhToan_DTO;
                if (item != null)
                {
                    txtMaDH.Text = item.OrderId.ToString();
                    cboPTTT.SelectedValue = item.MethodId;
                    cboTrangThaiTT.SelectedValue = item.StatusId;
                    EnableUpdate(true);
                }
            }
        }
        void EnableUpdate(bool isUpdating)
        {
            btnCapNhat.Enabled = isUpdating;
            txtMaDH.ReadOnly = isUpdating;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
