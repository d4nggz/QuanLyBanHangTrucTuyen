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
    public partial class FormQuanLyDanhMuc : Form
    {
        CategoryBUS bus = new CategoryBUS();
        public FormQuanLyDanhMuc()
        {
            InitializeComponent();
        }

        private void FormQuanLyDanhMuc_Load(object sender, EventArgs e)
        {
           LoadGrid();
        }
        
        void LoadGrid()
        {
            dgvDanhMuc.DataSource = bus.GetAllCategories();
            dgvDanhMuc.Columns["CategoryID_N01"].HeaderText = "Mã DM";
            dgvDanhMuc.Columns["CategoryName_N01"].HeaderText = "Tên Danh Mục";
            if (dgvDanhMuc.Columns.Contains("Description_N01"))
                dgvDanhMuc.Columns["Description_N01"].HeaderText = "Ghi Chú";
            dgvDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void ResetForm()
        {
            txtTenDanhMuc.Clear();
            txtGhiChu.Clear();
            txtTenDanhMuc.Focus();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhMuc.Rows[e.RowIndex];
                txtTenDanhMuc.Text = row.Cells["CategoryName_N01"].Value.ToString();
                if (dgvDanhMuc.Columns.Contains("Description_N01") && row.Cells["Description_N01"].Value != null)
                {
                    txtGhiChu.Text = row.Cells["Description_N01"].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!");
                return;
            }

            QLDM_DTO cat = new QLDM_DTO();
            cat.CategoryName = txtTenDanhMuc.Text;
            cat.Description = txtGhiChu.Text;

            try
            {
                bus.AddCategory(cat);
                MessageBox.Show("Thêm danh mục thành công!");
                LoadGrid();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvDanhMuc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa!");
                return;
            }

            try
            {
                QLDM_DTO cat = new QLDM_DTO();
                cat.CategoryId = Convert.ToInt32(dgvDanhMuc.CurrentRow.Cells["CategoryID_N01"].Value);
                cat.CategoryName = txtTenDanhMuc.Text;
                cat.Description = txtGhiChu.Text;

                bus.UpdateCategory(cat);
                MessageBox.Show("Cập nhật thành công!");
                LoadGrid();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvDanhMuc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!");
                return;
            }

            int id = Convert.ToInt32(dgvDanhMuc.CurrentRow.Cells["CategoryID_N01"].Value);

            if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    bus.DeleteCategory(id);
                    MessageBox.Show("Đã xóa!");
                    LoadGrid();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa danh mục này vì đang có sản phẩm thuộc về nó.\nHãy xóa sản phẩm trước!", "Lỗi ràng buộc dữ liệu");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            dgvDanhMuc.DataSource = bus.SearchCategory(keyword);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
