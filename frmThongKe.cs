using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyChiTieu
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
            VeBieuDo();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            VeBieuDo();
        }

        private void VeBieuDo()
        {
            QuanLyChiTieu.BLL.GiaoDichBLL bll = new QuanLyChiTieu.BLL.GiaoDichBLL();
            DataTable dt = bll.LayLichSuGiaoDich();

            if (dt == null || dt.Rows.Count == 0)
            {
                chartChiTieu.Series.Clear();
                chartChiTieu.Titles.Clear();
                chartChiTieu.Titles.Add("Chưa có dữ liệu giao dịch!");

                chartTop10.Series.Clear();
                chartTop10.Titles.Clear();
                chartTop10.Titles.Add("Chưa có dữ liệu giao dịch!");
                return;
            }

            VeBieuDoTongQuan(dt);
            VeBieuDoTop10(dt);
        }

        // Biểu đồ Doughnut - phân tích chi tiêu theo ghi chú
        private void VeBieuDoTongQuan(DataTable dt)
        {
            chartChiTieu.Series.Clear();
            chartChiTieu.Titles.Clear();

            // Bật hiệu ứng 3D cho ảo diệu
            chartChiTieu.ChartAreas[0].Area3DStyle.Enable3D = true;
            chartChiTieu.ChartAreas[0].Area3DStyle.Inclination = 15;

            Title t = chartChiTieu.Titles.Add("CƠ CẤU CHI TIÊU");
            t.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            t.ForeColor = Color.FromArgb(44, 62, 80);

            Series s = new Series("ChiTieu");
            s.ChartType = SeriesChartType.Doughnut;
            s["DoughnutRadius"] = "35"; // Làm vòng khuyên mỏng đi cho sang
            s["PieLabelStyle"] = "Outside"; // Đẩy chữ ra ngoài để không bị đè lên màu
            s.BorderColor = Color.White;
            s.BorderWidth = 2;

            var data = from row in dt.AsEnumerable()
                       group row by row.Field<string>("GhiChu") into g
                       select new
                       {
                           Ten = string.IsNullOrEmpty(g.Key) ? "Khác" : g.Key,
                           Tien = g.Sum(x => Convert.ToDouble(x["SoTien"]))
                       };

            foreach (var item in data)
            {
                int i = s.Points.AddXY(item.Ten, item.Tien);
                s.Points[i].Label = "#PERCENT{P1}"; // Chỉ hiện % trên biểu đồ
                s.Points[i].LegendText = item.Ten;   // Tên hiện ở bảng chú thích
            }
            chartChiTieu.Series.Add(s);

            // Chỉnh bảng chú thích (Legend)
            chartChiTieu.Legends[0].Docking = Docking.Bottom; // Đưa xuống dưới
            chartChiTieu.Legends[0].Alignment = StringAlignment.Center;
        }

        // Biểu đồ Bar - Top 10 ngày chi tiêu nhiều nhất
        private void VeBieuDoTop10(DataTable dt)
        {
            chartTop10.Series.Clear();
            chartTop10.Titles.Clear();

            // 1. Tinh chỉnh Tiêu đề
            Title t = chartTop10.Titles.Add("TOP 10 NGÀY CHI TIÊU NHIỀU NHẤT");
            t.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            t.ForeColor = Color.FromArgb(44, 62, 80); // Màu xanh đen sang trọng

            // 2. Cấu hình ChartArea (Làm sạch lưới)
            var area = chartTop10.ChartAreas[0];
            area.AxisX.MajorGrid.Enabled = false; // Tắt lưới dọc cho thoáng
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            area.AxisY.LabelStyle.Format = "N0"; // Định dạng số tiền có dấu phẩy

            // 3. Cấu hình Series (Cột ngang)
            Series s = new Series("Top10Ngay");
            s.ChartType = SeriesChartType.Bar;
            s.IsValueShownAsLabel = true;
            s.LabelFormat = "{0:N0} đ"; // Hiện số tiền kèm chữ 'đ'
            s.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Hiệu ứng màu Gradient (Xanh dương sang Xanh nhạt)
            s.Color = Color.DodgerBlue;
            s.BackSecondaryColor = Color.LightSkyBlue;
            s.BackGradientStyle = GradientStyle.LeftRight;
            s.BorderColor = Color.White;
            s.BorderWidth = 1;

            try
            {
                // 4. Xử lý dữ liệu
                var top10Data = (from row in dt.AsEnumerable()
                                 let ngay = Convert.ToDateTime(row["NgayGD"]).ToString("dd/MM/yyyy")
                                 group row by ngay into g
                                 select new
                                 {
                                     Ngay = g.Key,
                                     TongTien = g.Sum(x => Convert.ToDouble(x["SoTien"]))
                                 })
                                 .OrderByDescending(x => x.TongTien)
                                 .Take(10)
                                 .ToList();

                // Đảo ngược để ngày chi nhiều nhất nằm trên cùng
                foreach (var item in top10Data.Reverse<dynamic>())
                {
                    int i = s.Points.AddXY(item.Ngay, item.TongTien);
                    // Điểm nhấn: Cột cao nhất cho màu cam đỏ
                    if (item.TongTien == top10Data.Max(x => (double)x.TongTien))
                        s.Points[i].Color = Color.OrangeRed;
                }

                chartTop10.Series.Add(s);
            }
            catch
            {
                chartTop10.Titles.Add("Lỗi: Kiểm tra lại cột NgayGD!");
            }
        }
    }
}