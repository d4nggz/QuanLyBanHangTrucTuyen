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
    public partial class FormChuongTrinh: Form
    {
        public FormChuongTrinh()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            FormDangNhap frm = new FormDangNhap();
            frm.Show();

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void quảnLýĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void báoCáoDoanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void FormChuongTrinh_Load(object sender, EventArgs e)
        {

        }

        private void quảnLýSảnPhẩmToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Form frm = new Form();
            switch (e.ClickedItem.Name)
            {
                case "quảnLýSảnPhẩmToolStripMenuItem1":
                    FormQuanLySanPham formQuanLySanPham = new FormQuanLySanPham();
                    frm = formQuanLySanPham;
                    break;
                case "quảnLýHóaĐơnToolStripMenuItem":
                    FormQuanLyDonHang formQuanLyDonHang = new FormQuanLyDonHang();
                    frm = formQuanLyDonHang;
                    break;
                case "quảnLýNgườiDùngToolStripMenuItem1":
                    FormQuanLyNguoiDung formQuanLyNguoiDung = new FormQuanLyNguoiDung();
                    frm = formQuanLyNguoiDung;
                    break;
                case "quảnLýDanhMụcToolStripMenuItem":
                    FormQuanLyDanhMuc formQuanLyDanhMuc = new FormQuanLyDanhMuc();
                    frm = formQuanLyDanhMuc;
                    break;
                case "quảnLýPhươngThứcThanhToánToolStripMenuItem":
                    FormQuanLyPhuongThucThanhToan formQuanLyPhuongThucThanhToan = new FormQuanLyPhuongThucThanhToan();
                    frm = formQuanLyPhuongThucThanhToan;
                    break;

            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
            frm.BringToFront();
        }

        private void quảnLýĐơnHàngToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Form frm = new Form();
            switch (e.ClickedItem.Name)
            {
                case "thốngKêDoanhThuToolStripMenuItem":
                    FormThongKeDoanhThu formThongKeDoanhThu = new FormThongKeDoanhThu();
                    frm = formThongKeDoanhThu;
                    break;
                case "thốngKêSảnPhẩmToolStripMenuItem":
                    FormThongKeSanPham formThongKeSanPham = new FormThongKeSanPham();
                    frm = formThongKeSanPham;
                    break;
              

            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
            frm.BringToFront();
        }
    }
}
