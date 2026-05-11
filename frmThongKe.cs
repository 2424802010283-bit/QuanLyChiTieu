using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // CHỈ DÙNG CHART MẶC ĐỊNH
using QuanLyChiTieu.BLL;

namespace QuanLyChiTieu
{
    public partial class frmThongKe : Form
    {
        private readonly GiaoDichBLL _gdBLL = new GiaoDichBLL();
        private readonly TaiKhoanBLL _tkBLL = new TaiKhoanBLL();

        private readonly Color[] _mauTop5 = {
            Color.FromArgb(230,126, 34),
            Color.FromArgb(231, 76, 60),
            Color.FromArgb( 52, 73,180),
            Color.FromArgb( 26,188,156),
            Color.FromArgb(231,100, 80),
        };

        public frmThongKe()
        {
            InitializeComponent();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            KhoiTaoCombo();
            LoadThongKe();
        }

        private void KhoiTaoCombo()
        {
            cboThongKe_Thang.Items.Clear();
            for (int i = 1; i <= 12; i++) cboThongKe_Thang.Items.Add("Tháng " + i);
            cboThongKe_Thang.SelectedIndex = DateTime.Now.Month - 1;

            cboThongKe_Nam.Items.Clear();
            for (int y = DateTime.Now.Year; y >= DateTime.Now.Year - 5; y--)
                cboThongKe_Nam.Items.Add(y.ToString());
            cboThongKe_Nam.SelectedIndex = 0;
        }

        private void LoadThongKe()
        {
            int thang = cboThongKe_Thang.SelectedIndex + 1;
            int nam = int.Parse(cboThongKe_Nam.SelectedItem?.ToString() ?? DateTime.Now.Year.ToString());
            LoadTongHop(thang, nam);
            LoadBieuDoThuChi(nam);
            LoadTop5(thang, nam);
            LoadBieuDoBienDong(nam);
        }

        // ─── 1. Tổng hợp ───
        private void LoadTongHop(int thang, int nam)
        {
            int maNguoiDung = 1; // Mình tạm để là 1 vì project bạn chưa có biến Session
            decimal thu = _gdBLL.GetTong(maNguoiDung, "Thu", thang, nam);
            decimal chi = _gdBLL.GetTong(maNguoiDung, "Chi", thang, nam);
            decimal rong = thu - chi;

            lblTongThuNhap.Text = thu.ToString("N0") + " đ";
            lblTongChiTieu.Text = chi.ToString("N0") + " đ";
            lblSoDuRong.Text = rong.ToString("N0") + " đ";
            lblSoDuRong.ForeColor = rong >= 0 ? Color.FromArgb(52, 152, 219) : Color.FromArgb(231, 76, 60);
        }

        // ─── 2. Biểu đồ Thu chi (myChartThuChi) ───
        private void LoadBieuDoThuChi(int nam)
        {
            int maNguoiDung = 1;
            DataTable dt = _gdBLL.GetThongKeTheoThang(maNguoiDung, nam);

            myChartThuChi.Series.Clear(); // Xóa dữ liệu cũ

            // Tạo đường vẽ Thu nhập
            Series seriesThu = new Series("Thu nhập")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.FromArgb(46, 204, 113),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            // Tạo đường vẽ Chi tiêu
            Series seriesChi = new Series("Chi tiêu")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.FromArgb(231, 76, 60),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            for (int i = 1; i <= 12; i++)
            {
                double thu = 0;
                double chi = 0;
                DataRow[] rows = dt.Select("Thang = " + i);
                if (rows.Length > 0)
                {
                    thu = Convert.ToDouble(rows[0]["TongThu"]);
                    chi = Convert.ToDouble(rows[0]["TongChi"]);
                }

                seriesThu.Points.AddXY("T" + i, thu);
                seriesChi.Points.AddXY("T" + i, chi);
            }

            myChartThuChi.Series.Add(seriesThu);
            myChartThuChi.Series.Add(seriesChi);

            myChartThuChi.ChartAreas[0].AxisX.Interval = 1;
            myChartThuChi.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0 đ";
            myChartThuChi.Legends[0].Docking = Docking.Top;
        }

        // ─── 3. Top 5 danh mục (ProgressBar) ───
        private void LoadTop5(int thang, int nam)
        {
            int maNguoiDung = 1;
            DataTable dt = _gdBLL.GetTop5DanhMucChi(maNguoiDung, thang, nam);
            var rows = new (Guna.UI2.WinForms.Guna2HtmlLabel lbl, Guna.UI2.WinForms.Guna2ProgressBar pb)[]
            {
                (lblTop1, pbHoaDon),
                (lblTop2, pbAnUong),
                (lblTop3, pbDiLai),
                (lblTop4, pbMuaSam),
                (lblTop5, pbGiaiTri),
            };
            for (int i = 0; i < rows.Length; i++) { rows[i].lbl.Text = (i + 1) + ". —"; rows[i].pb.Value = 0; }
            if (dt.Rows.Count == 0) return;

            double maxVal = Convert.ToDouble(dt.Rows[0]["TongChi"]);
            if (maxVal == 0) return;

            for (int i = 0; i < dt.Rows.Count && i < rows.Length; i++)
            {
                string ten = dt.Rows[i]["TenDanhMuc"].ToString();
                double tong = Convert.ToDouble(dt.Rows[i]["TongChi"]);
                int pct = (int)Math.Round(tong / maxVal * 100);

                rows[i].lbl.Text = (i + 1) + ". " + ten;
                rows[i].pb.Value = Math.Min(100, pct);
                rows[i].pb.ProgressColor = _mauTop5[i];
            }
        }

        // ─── 4. Biểu đồ Biến động số dư (myChartBienDong) ───
        private void LoadBieuDoBienDong(int nam)
        {
            int maNguoiDung = 1;
            DataTable dt = _gdBLL.GetThongKeTheoThang(maNguoiDung, nam);
            var tcMap = new Dictionary<int, (double thu, double chi)>();

            foreach (DataRow r in dt.Rows)
            {
                int thang = Convert.ToInt32(r["Thang"]);
                tcMap[thang] = (Convert.ToDouble(r["TongThu"]), Convert.ToDouble(r["TongChi"]));
            }

            double soDu = (double)_tkBLL.GetTongSoDu(maNguoiDung);
            for (int i = 1; i <= 12; i++)
                if (tcMap.ContainsKey(i)) soDu -= (tcMap[i].thu - tcMap[i].chi);

            myChartBienDong.Series.Clear();

            Series seriesSoDu = new Series("Số dư")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.FromArgb(155, 89, 182),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            for (int i = 1; i <= 12; i++)
            {
                if (tcMap.ContainsKey(i)) soDu += tcMap[i].thu - tcMap[i].chi;
                seriesSoDu.Points.AddXY("T" + i, soDu);
            }

            myChartBienDong.Series.Add(seriesSoDu);

            myChartBienDong.ChartAreas[0].AxisX.Interval = 1;
            myChartBienDong.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0 đ";

            // Ẩn chú thích nếu không cần thiết
            if (myChartBienDong.Legends.Count > 0)
            {
                myChartBienDong.Legends[0].Enabled = false;
            }
        }

        private void cboThongKe_Thang_SelectedIndexChanged(object sender, EventArgs e)
        { if (cboThongKe_Nam.SelectedItem != null) LoadThongKe(); }

        private void cboThongKe_Nam_SelectedIndexChanged(object sender, EventArgs e)
        { if (cboThongKe_Thang.SelectedItem != null) LoadThongKe(); }
    }
}