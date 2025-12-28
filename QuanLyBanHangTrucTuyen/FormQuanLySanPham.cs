using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHangTrucTuyen
{
    public partial class FormQuanLySanPham: Form
    {
        ProductBUS bus = new ProductBUS();
        string selectedImagePath = "";
        string finalFileName = "";
        public FormQuanLySanPham()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = open.FileName;
                picAnhSP.Image = Image.FromFile(selectedImagePath);
                picAnhSP.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        string SaveImage()
        {
            if (string.IsNullOrEmpty(selectedImagePath)) return "default.png"; // Nếu không chọn thì lấy ảnh mặc định

            string projectPath = Application.StartupPath + "\\Images";
            if (!Directory.Exists(projectPath)) Directory.CreateDirectory(projectPath);

            string fileName = Path.GetFileName(selectedImagePath);
            string destPath = Path.Combine(projectPath, fileName);

            // Copy file vào thư mục project
            try
            {
                File.Copy(selectedImagePath, destPath, true);
            }
            catch { } // Bỏ qua nếu file đã tồn tại

            return fileName;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                QLSP_DTO p = new QLSP_DTO();
                p.ProductName = txtTenSP.Text;
                p.Price = nudGiaBan.Value;
                p.StockQuantity = (int)nudTonKho.Value;
                p.Description = txtMoTa.Text;
                p.CategoryId = (int)cboDanhMuc.SelectedValue;
                p.ImageUrl = SaveImage();

                bus.AddProduct(p);
                LoadGridView();
                MessageBox.Show("Add Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        void LoadGridView()
        {
            dgvSanPham.DataSource = bus.GetAllProducts();
            dgvSanPham.Columns["ProductID_N01"].HeaderText = "Mã SP";
            dgvSanPham.Columns["ProductName_N01"].HeaderText = "Tên Sản Phẩm";
            dgvSanPham.Columns["Price_N01"].HeaderText = "Giá Bán";
            dgvSanPham.Columns["StockQuantity_N01"].HeaderText = "Tồn Kho";
            dgvSanPham.Columns["CategoryID_N01"].HeaderText = "Mã Danh Mục";
            dgvSanPham.Columns["ImageUrl_N01"].HeaderText = "Url Ảnh SP";
            dgvSanPham.Columns["description_N01"].HeaderText = "Mô Tả";
            dgvSanPham.Columns["Price_N01"].DefaultCellStyle.Format = "N0";
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void LoadComboBox()
        {
            cboDanhMuc.DataSource = bus.GetAllCategories();
            cboDanhMuc.DisplayMember = "CategoryName_N01";
            cboDanhMuc.ValueMember = "CategoryID_N01";
        }

        private void FormQuanLySanPham_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadGridView();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                QLSP_DTO p = new QLSP_DTO();
                // Lấy ID từ cột đầu tiên (hoặc theo tên cột)
                p.ProductId = int.Parse(dgvSanPham.CurrentRow.Cells["ProductID_N01"].Value.ToString());
                p.ProductName = txtTenSP.Text;
                p.Price = nudGiaBan.Value;
                p.StockQuantity = (int)nudTonKho.Value;
                p.Description = txtMoTa.Text;
                p.CategoryId = (int)cboDanhMuc.SelectedValue;

                // Nếu có chọn ảnh mới thì lưu ảnh mới, không thì giữ ảnh cũ
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    p.ImageUrl = SaveImage();
                }
                else
                {
                    p.ImageUrl = dgvSanPham.CurrentRow.Cells["ImageURL_N01"].Value.ToString();
                }

                bus.UpdateProduct(p);
                LoadGridView();
                MessageBox.Show("Update Success!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                int id = int.Parse(dgvSanPham.CurrentRow.Cells["ProductID_N01"].Value.ToString());
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bus.DeleteProduct(id);
                    LoadGridView();
                }
            }
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                txtTenSP.Text = row.Cells["ProductName_N01"].Value.ToString();
                nudGiaBan.Value = Convert.ToDecimal(row.Cells["Price_N01"].Value);
                nudTonKho.Value = Convert.ToDecimal(row.Cells["StockQuantity_N01"].Value);
                cboDanhMuc.SelectedValue = row.Cells["CategoryID_N01"].Value;
                txtMoTa.Text = row.Cells["description_N01"].Value.ToString();

                // Load ảnh lên PictureBox
                string imgName = row.Cells["ImageURL_N01"].Value.ToString();
                string imgPath = Application.StartupPath + "\\Images\\" + imgName;
                if (File.Exists(imgPath))
                {
                    picAnhSP.Image = Image.FromFile(imgPath);
                    selectedImagePath = ""; // Reset biến tạm
                }
                else
                {
                    picAnhSP.Image = null;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
