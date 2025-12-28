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
            List<QLDM_DTO> list = bus.GetAllCategories();
            dgvDanhMuc.DataSource = list;
            SetHeader("CategoryId", "Mã DM");
            SetHeader("Name", "Tên Danh Mục");
            SetHeader("Description", "Mô Tả / Ghi Chú");

            dgvDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void SetHeader(string propertyName, string headerText)
        {
            if (dgvDanhMuc.Columns[propertyName] != null)
                dgvDanhMuc.Columns[propertyName].HeaderText = headerText;
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
                QLDM_DTO cat = dgvDanhMuc.Rows[e.RowIndex].DataBoundItem as QLDM_DTO;

                if (cat != null)
                {
                    txtTenDanhMuc.Text = cat.Name;
                    txtGhiChu.Text = cat.Description;
                    txtMaDanhMuc.Text = cat.CategoryId.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QLDM_DTO cat = new QLDM_DTO();
            cat.Name = txtTenDanhMuc.Text.Trim();
            cat.Description = txtGhiChu.Text.Trim();

            string ketQua = bus.AddCategory(cat);
            if (ketQua == "Success")
            {
                MessageBox.Show("Thêm thành công!");
                LoadGrid();
                ResetForm();
            }
            else
            {
                MessageBox.Show(ketQua);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvDanhMuc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa!");
                return;
            }

            QLDM_DTO currentCat = dgvDanhMuc.CurrentRow.DataBoundItem as QLDM_DTO;

            QLDM_DTO catMoi = new QLDM_DTO();
            catMoi.CategoryId = currentCat.CategoryId;
            catMoi.Name = txtTenDanhMuc.Text.Trim(); 
            catMoi.Description = txtGhiChu.Text.Trim();

            if (bus.UpdateCategory(catMoi))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadGrid();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvDanhMuc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!");
                return;
            }

            QLDM_DTO currentCat = dgvDanhMuc.CurrentRow.DataBoundItem as QLDM_DTO;

            if (MessageBox.Show($"Bạn có chắc muốn xóa danh mục '{currentCat.Name}'?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string ketQua = bus.DeleteCategory(currentCat.CategoryId);
                if (ketQua == "Success")
                {
                    MessageBox.Show("Đã xóa!");
                    LoadGrid();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show(ketQua);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            List<QLDM_DTO> results = bus.SearchCategory(keyword);
            dgvDanhMuc.DataSource = results;

            if (results.Count > 0)
            {
                SetHeader("CategoryId", "Mã DM");
                SetHeader("Name", "Tên Danh Mục");
                SetHeader("Description", "Mô Tả / Ghi Chú");
            }
            else
            {
                MessageBox.Show("Không tìm thấy!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
