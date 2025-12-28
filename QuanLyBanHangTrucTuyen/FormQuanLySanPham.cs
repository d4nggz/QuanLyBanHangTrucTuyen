using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks; // Bắt buộc có để dùng async/await
using System.Windows.Forms;

namespace QuanLyBanHangTrucTuyen
{
    public partial class FormQuanLySanPham : Form
    {
   
        ProductBUS bus = new ProductBUS();

     
        string selectedImagePath = "";

       
        const string BASE_IMAGE_URL = "http://localhost:8080";

        public FormQuanLySanPham()
        {
            InitializeComponent();
        }

        void LoadComboBox()
        {
            try
            {
                // Gọi BUS lấy dữ liệu
                DataTable dt = bus.GetAllCategories();

                // 1. Kiểm tra xem có lấy được bảng không
                if (dt == null)
                {
                    MessageBox.Show("Lỗi: DataTable bị Null (Có thể do lỗi SQL trong DAL)!");
                    return;
                }

                // 2. Kiểm tra xem có dòng dữ liệu nào không
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Thành công: Kết nối được nhưng bảng 'categories' đang TRỐNG (0 dòng)!");
                    return;
                }

                // 3. DEBUG: In ra tên các cột để bạn xem có khớp không
                // (Sau khi chạy ngon thì xóa đoạn comment này đi)
                /*
                string columns = "";
                foreach(DataColumn col in dt.Columns) columns += col.ColumnName + " | ";
                MessageBox.Show($"Tìm thấy {dt.Rows.Count} danh mục.\nTên các cột là: {columns}", "Debug Cột");
                */

                // 4. Gán dữ liệu
                cboDanhMuc.DataSource = dt;

                // --- SỬA TÊN CỘT Ở ĐÂY CHO KHỚP VỚI CÁI MESSAGEBOX Ở TRÊN ---
                // Giả sử DB của bạn cột tên là "name" và "category_id"
                // Nếu DB của bạn là "CategoryName" thì phải sửa dòng dưới thành "CategoryName"
                cboDanhMuc.DisplayMember = "name";
                cboDanhMuc.ValueMember = "category_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tại LoadComboBox: " + ex.Message);
            }
        }

        private async void FormQuanLySanPham_Load(object sender, EventArgs e)
        {
            LoadComboBox();
     
            await LoadGridView();
        }

        async Task LoadGridView()
        {
            
            dgvSanPham.AutoGenerateColumns = true;

       
            List<QLSP_DTO> listSP = await bus.GetAllProducts();

            if (listSP != null)
            {
                
                dgvSanPham.DataSource = listSP;


                if (dgvSanPham.Columns["ProductId"] != null)
                    dgvSanPham.Columns["ProductId"].HeaderText = "Mã SP";

                if (dgvSanPham.Columns["ProductName"] != null)
                    dgvSanPham.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";

                if (dgvSanPham.Columns["Price"] != null)
                {
                    dgvSanPham.Columns["Price"].HeaderText = "Giá Bán";
                    dgvSanPham.Columns["Price"].DefaultCellStyle.Format = "N0"; 
                }

                if (dgvSanPham.Columns["StockQuantity"] != null)
                    dgvSanPham.Columns["StockQuantity"].HeaderText = "Tồn Kho";

                if (dgvSanPham.Columns["Description"] != null)
                    dgvSanPham.Columns["Description"].HeaderText = "Mô Tả";

             
                if (dgvSanPham.Columns["CategoryId"] != null)
                    dgvSanPham.Columns["CategoryId"].Visible = false;

                if (dgvSanPham.Columns["ImageUrl"] != null)
                    dgvSanPham.Columns["ImageUrl"].Visible = false; 

                dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

     
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = open.FileName; // Lưu đường dẫn gốc C:\...
                picAnhSP.Image = Image.FromFile(selectedImagePath);
                picAnhSP.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

       
        private async void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTenSP.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm!"); return;
                }

                QLSP_DTO p = new QLSP_DTO();
                p.ProductName = txtTenSP.Text;
                p.Price = nudGiaBan.Value;
                p.StockQuantity = (int)nudTonKho.Value;
                p.Description = txtMoTa.Text;

                if (cboDanhMuc.SelectedValue != null)
                    p.CategoryID = (int)cboDanhMuc.SelectedValue;

         
                string localPath = "";
                if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath))
                {
                    localPath = selectedImagePath;
                }

               
                btnThem.Enabled = false;

          
                bool result = await bus.AddProduct(p, localPath);

                btnThem.Enabled = true;

                if (result)
                {
                    MessageBox.Show("Thêm thành công!");
                    ResetForm();
                    await LoadGridView(); 
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! Kiểm tra kết nối Server.");
                }
            }
            catch (Exception ex)
            {
                btnThem.Enabled = true;
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

     
        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                try
                {
                    QLSP_DTO p = new QLSP_DTO();
              
                    p.ProductId = int.Parse(dgvSanPham.CurrentRow.Cells["ProductId"].Value.ToString());
                    p.ProductName = txtTenSP.Text;
                    p.Price = nudGiaBan.Value;
                    p.StockQuantity = (int)nudTonKho.Value;
                    p.Description = txtMoTa.Text;
                    p.CategoryID = (int)cboDanhMuc.SelectedValue;

                
                    string localPath = (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath)) ? selectedImagePath : "";

        
                    bool result = await bus.UpdateProduct(p, localPath);

                    if (result)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                        ResetForm();
                        await LoadGridView();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại (Chưa viết API hoặc lỗi Server)!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                try
                {
                    int id = int.Parse(dgvSanPham.CurrentRow.Cells["ProductId"].Value.ToString());

                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                    
                        bool result = await bus.DeleteProduct(id);

                        if (result)
                        {
                            MessageBox.Show("Xóa thành công!");
                            ResetForm();
                            await LoadGridView();
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
        }

 
        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                txtTenSP.Text = row.Cells["ProductName"].Value.ToString();
                nudGiaBan.Value = Convert.ToDecimal(row.Cells["Price"].Value);
                nudTonKho.Value = Convert.ToDecimal(row.Cells["StockQuantity"].Value);

                if (row.Cells["CategoryId"].Value != null)
                    cboDanhMuc.SelectedValue = row.Cells["CategoryId"].Value;

                if (row.Cells["Description"].Value != null)
                    txtMoTa.Text = row.Cells["Description"].Value.ToString();

          
                string imgUrl = row.Cells["ImageUrl"].Value != null ? row.Cells["ImageUrl"].Value.ToString() : "";

                try
                {
                    if (!string.IsNullOrEmpty(imgUrl))
                    {
                        // Ghép link: "http://localhost:8080" + "/images/abc.jpg"
                        string fullUrl = imgUrl.StartsWith("http") ? imgUrl : BASE_IMAGE_URL + imgUrl;

                        picAnhSP.Load(fullUrl); 
                        picAnhSP.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        picAnhSP.Image = null;
                    }
                }
                catch
                {
                    picAnhSP.Image = null; 
                }

                selectedImagePath = ""; 
            }
        }

      
        void ResetForm()
        {
            txtTenSP.Text = "";
            txtMoTa.Text = "";
            nudGiaBan.Value = 0;
            nudTonKho.Value = 0;
            picAnhSP.Image = null;
            selectedImagePath = "";
        }

        private void button5_Click(object sender, EventArgs e) => this.Close(); 

       
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}