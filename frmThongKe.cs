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

namespace QuanLyChiTieu
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
            // Đảm bảo biểu đồ được vẽ khi khởi tạo Form
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

            chartChiTieu.Series.Clear();
            chartChiTieu.Titles.Clear();
            chartChiTieu.Titles.Add("PHÂN TÍCH CHI TIÊU THEO GHI CHÚ");

            Series s = new Series("ChiTieu");
            s.ChartType = SeriesChartType.Doughnut; // Đổi thành Doughnut cho hiện đại

            if (chartChiTieu.ChartAreas.Count > 0)
                s.ChartArea = chartChiTieu.ChartAreas[0].Name;
            if (chartChiTieu.Legends.Count > 0)
                s.Legend = chartChiTieu.Legends[0].Name;

            // Gom nhóm các Ghi chú giống nhau lại để cộng tổng tiền
            {
                // SQL của bạn dùng "GhiChu" và "SoTien"
                // Ta sẽ gom nhóm các Ghi chú giống nhau lại để cộng tổng tiền
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
                s.Points[i].Label = "#PERCENT{P1}"; // Hiện phần trăm %
                s.Points[i].LegendText = item.Ten;   // Hiện tên ở bảng chú thích
                }
                chartChiTieu.Series.Add(s);
            }

            {
                chartChiTieu.Titles.Add("Chưa có dữ liệu giao dịch!");
            }
        }
    }
}
