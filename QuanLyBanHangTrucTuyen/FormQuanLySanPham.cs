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
    public partial class FormQuanLySanPham: Form
    {
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
            // 1. Cấu hình bộ lọc file (chỉ cho chọn ảnh)
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            dlgOpen.Title = "Chọn ảnh sản phẩm";

            // 2. Hiện hộp thoại chọn file
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                // 3. Hiển thị ảnh lên PictureBox
                picAnhSP.Image = new Bitmap(dlgOpen.FileName);
                picAnhSP.SizeMode = PictureBoxSizeMode.Zoom; // Co giãn ảnh cho đẹp

                // 4. Lưu đường dẫn file lại (để lát nữa gửi lên API)
                string duongDanAnh = dlgOpen.FileName;
                // txtHinhAnh.Text = duongDanAnh; // Nếu có ô text hiển thị đường dẫn
            }
        }
    }
}
