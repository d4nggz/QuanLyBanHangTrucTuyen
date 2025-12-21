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
    public partial class FormThongKeDoanhThu : Form
    {
        public FormThongKeDoanhThu()
        {
            InitializeComponent();
        }

        private void FormThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            VeBieuDoTangTruong();
        }
        private void VeBieuDoTangTruong()
        {
            // 1. Dọn dẹp dữ liệu cũ
            chartDoanhThu.Series.Clear();
            chartDoanhThu.Titles.Clear();

            // 2. Đặt tiêu đề
            Title title = chartDoanhThu.Titles.Add("BIỂU ĐỒ TĂNG TRƯỞNG 2025");
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.Red;

            // 3. Tạo Series (Dây dữ liệu)
            Series series = new Series("Doanh Thu");

            // --- QUAN TRỌNG: CHỌN LOẠI BIỂU ĐỒ ---
            // Spline: Đường cong mềm mại (Phù hợp nhất cho tăng trưởng)
            // Line: Đường gấp khúc (Cứng hơn)
            series.ChartType = SeriesChartType.Spline;

            // --- TRANG TRÍ CHO ĐẸP ---
            series.BorderWidth = 3;             // Đường kẻ dày lên (3px)
            series.Color = Color.Red;           // Màu đường kẻ
            series.IsValueShownAsLabel = true;  // Hiện số tiền cạnh điểm

            // Tạo các dấu chấm tròn tại mỗi tháng để dễ nhìn
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 10;
            series.MarkerColor = Color.White;
            series.MarkerBorderColor = Color.Red;

            // 4. NHẬP DỮ LIỆU TƯỢNG TRƯNG (Fix cứng)
            // Mình nhập số liệu có xu hướng tăng dần để đúng chất "tăng trưởng"
            series.Points.AddXY("Tháng 1", 15000000);
            series.Points.AddXY("Tháng 2", 18000000);
            series.Points.AddXY("Tháng 3", 16000000); // Hơi giảm nhẹ
            series.Points.AddXY("Tháng 4", 25000000); // Tăng lại
            series.Points.AddXY("Tháng 5", 32000000); // Tăng mạnh
            series.Points.AddXY("Tháng 6", 45000000); // Đỉnh điểm

            // 5. Đẩy vào Chart
            chartDoanhThu.Series.Add(series);

            // 6. Kẻ thêm lưới mờ nhạt cho dễ dóng hàng (Tùy chọn)
            var chartArea = chartDoanhThu.ChartAreas[0];
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
        }
    }
}
