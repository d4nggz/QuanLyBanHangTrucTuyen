using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyBanHangTrucTuyen
{
    public partial class FormThongKeSanPham : Form
    {
        public FormThongKeSanPham()
        {
            InitializeComponent();
        }

        private void FormThongKeSanPham_Load(object sender, EventArgs e)
        {
            VeBieuDoBanChay();
            VeBieuDoTonKho();
        }
        // --- BIỂU ĐỒ 1: TOP BÁN CHẠY (CÓ TIÊU ĐỀ) ---
        private void VeBieuDoBanChay()
        {
            // 1. Dọn dẹp
            chartBanChay.Series.Clear();
            chartBanChay.Titles.Clear();

            // 2. --- ĐẶT TÊN BIỂU ĐỒ (MAIN TITLE) ---
            Title title = chartBanChay.Titles.Add("TOP 5 SẢN PHẨM BÁN CHẠY NHẤT THÁNG 11");
            title.Font = new Font("Arial", 14, FontStyle.Bold); // Chỉnh font to, đậm
            title.ForeColor = Color.DarkGreen;                  // Màu chữ xanh
            title.Alignment = ContentAlignment.TopCenter;       // Căn giữa

            // 3. Tạo Series
            Series series = new Series("Số lượng");
            series.ChartType = SeriesChartType.Column; // Cột dọc
            series.IsValueShownAsLabel = true;
            series.Color = Color.SeaGreen;

            // Dữ liệu mẫu
            series.Points.AddXY("iPhone 15", 120);
            series.Points.AddXY("Samsung S24", 95);
            series.Points.AddXY("Sony WF-1000", 80);
            series.Points.AddXY("Logitech G502", 65);
            series.Points.AddXY("Keychron K2", 50);

            chartBanChay.Series.Add(series);

            // 4. --- ĐẶT TÊN CHO CÁC TRỤC (AXIS TITLES) ---
            var chartArea = chartBanChay.ChartAreas[0];

            // Tên trục ngang (X)
            chartArea.AxisX.Title = "Tên Sản Phẩm";
            chartArea.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Regular);

            // Tên trục dọc (Y)
            chartArea.AxisY.Title = "Số Lượng Đã Bán (Cái)";
            chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Regular);

            // Tắt lưới dọc cho đẹp
            chartArea.AxisX.MajorGrid.Enabled = false;
        }

        // --- BIỂU ĐỒ 2: TỒN KHO (CÓ TIÊU ĐỀ) ---
        private void VeBieuDoTonKho()
        {
            // 1. Dọn dẹp
            chartTonKho.Series.Clear();
            chartTonKho.Titles.Clear();

            // 2. --- ĐẶT TÊN BIỂU ĐỒ ---
            Title title = chartTonKho.Titles.Add("CẢNH BÁO: TOP 5 HÀNG TỒN KHO NHIỀU");
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.ForeColor = Color.Red; // Màu đỏ cảnh báo

            // 3. Tạo Series
            Series series = new Series("Tồn kho");
            series.ChartType = SeriesChartType.Bar; // Thanh ngang
            series.IsValueShownAsLabel = true;
            series.Color = Color.Tomato;

            // Dữ liệu mẫu
            series.Points.AddXY("Ốp lưng iPhone 11", 500);
            series.Points.AddXY("Sạc dự phòng cũ", 320);
            series.Points.AddXY("Cáp Micro-USB", 210);
            series.Points.AddXY("Tai nghe dây", 180);
            series.Points.AddXY("Chuột văn phòng", 150);

            chartTonKho.Series.Add(series);

            // 4. --- ĐẶT TÊN CHO CÁC TRỤC ---
            var chartArea = chartTonKho.ChartAreas[0];

            // Với biểu đồ Bar (Ngang):
            // AxisX là trục dọc (hiển thị tên sản phẩm)
            // AxisY là trục ngang (hiển thị số lượng)

            chartArea.AxisX.Title = "Sản Phẩm";
            chartArea.AxisX.Interval = 1; // Hiện đủ tên

            chartArea.AxisY.Title = "Số Lượng Tồn (Cái)";

            // Tắt lưới
            chartArea.AxisX.MajorGrid.Enabled = false;
        }
    }
}
