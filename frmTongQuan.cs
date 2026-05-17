using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System.Windows.Forms.DataVisualization.Charting; // Thư viện biểu đồ mặc định của .NET

namespace QuanLyChiTieu
{
    public partial class frmTongQuan : Form
    {
        // Khởi tạo các tầng xử lý nghiệp vụ
        private readonly GiaoDichBLL _gdBLL = new GiaoDichBLL();
        private readonly TaiKhoanBLL _tkBLL = new TaiKhoanBLL();
        private readonly NganSachBLL _nsBLL = new NganSachBLL();
        private readonly MucTieuBLL _mtBLL = new MucTieuBLL();

        // Bảng màu cho thanh ngân sách
        private readonly Color[] _mauNganSach = {
            Color.FromArgb(231, 76,  60), // Đỏ
            Color.FromArgb( 52, 73, 180), // Xanh dương
            Color.FromArgb( 26,188, 156), // Xanh ngọc
            Color.FromArgb(231,100,  80), // Cam đỏ
            Color.FromArgb(230,126,  34), // Cam
            Color.FromArgb(189,195, 199), // Xám (Cho danh mục "Khác")
        };

        public frmTongQuan()
        {
            InitializeComponent();
            // Lắng nghe sự kiện người dùng chuyển qua lại giữa các Form/Tab để Auto-Refresh
            this.VisibleChanged += FrmTongQuan_VisibleChanged;
        }

        private void frmTongQuan_Load(object sender, EventArgs e)
        {
            if (Session.MaNguoiDung > 0) LoadTongQuan();
        }

        private void FrmTongQuan_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && Session.MaNguoiDung > 0) LoadTongQuan();
        }

        // ─── HÀM GỌI TỔNG HỢP ───
        public void LoadTongQuan()
        {
            int thang = DateTime.Now.Month;
            int nam = DateTime.Now.Year;

            Load4Card(thang, nam);
            LoadTongChiTieuThang(thang, nam);
            LoadNganSachDanhMuc(thang, nam);
            LoadGiaoDichGanDay();
            LoadBieuDo(nam);
            LoadMucTieu();
        }

        // ─── 1. BỐN THẺ CARD TRÊN CÙNG ───
        private void Load4Card(int thang, int nam)
        {
            // Lấy tổng số dư từ tất cả tài khoản
            decimal soDu = _tkBLL.GetTongSoDu(Session.MaNguoiDung);

            // Theo SQL mới nhất, Giao dịch đang được lưu là 'Thu nhập' và 'Chi tiêu'
            decimal tongThu = _gdBLL.GetTong(Session.MaNguoiDung, "Thu nhập", thang, nam);
            decimal tongChi = _gdBLL.GetTong(Session.MaNguoiDung, "Chi tiêu", thang, nam);
            decimal tietKiem = tongThu - tongChi;

            lblTongSoDu.Text = soDu.ToString("N0") + " đ";
            lblTongThuNhap.Text = tongThu.ToString("N0") + " đ";
            lblTongChiTieu.Text = tongChi.ToString("N0") + " đ";

            lblTietKiem.Text = tietKiem.ToString("N0") + " đ";
            // Nếu tiết kiệm dương thì màu xanh, âm thì màu đỏ
            lblTietKiem.ForeColor = tietKiem >= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
        }

        // ─── 2. THANH TỔNG CHI TIÊU THÁNG ───
        private void LoadTongChiTieuThang(int thang, int nam)
        {
            var (tongNS, tongDaChi) = _nsBLL.GetTong(Session.MaNguoiDung, thang, nam);

            lblDaChi.Text = "Đã chi: " + tongDaChi.ToString("N0") + " đ";
            lblDaTieu.Text = "Đã tiêu: " + tongDaChi.ToString("N0") + " / " + tongNS.ToString("N0") + " đ";

            if (tongNS > 0)
            {
                int pct = (int)Math.Min(100, (tongDaChi * 100) / tongNS);
                pbTongChiTieuThang.Value = pct;

                // Đổi màu cảnh báo
                pbTongChiTieuThang.ProgressColor = pct < 70 ? Color.FromArgb(46, 204, 113)
                                                 : pct < 90 ? Color.FromArgb(241, 196, 15)
                                                            : Color.FromArgb(231, 76, 60);
            }
            else pbTongChiTieuThang.Value = 0;
        }

        // ─── 3. NGÂN SÁCH THEO TỪNG DANH MỤC ───
        private void LoadNganSachDanhMuc(int thang, int nam)
        {
            DataTable dt = _nsBLL.GetTheoThang(Session.MaNguoiDung, thang, nam);

            // Gom nhóm Progress Bar trên UI
            var map = new Dictionary<string, Guna.UI2.WinForms.Guna2ProgressBar>
            {
                { "Ăn uống",  pbAnUong  },
                { "Đi lại",   pbDiLai   },
                { "Mua sắm",  pbMuaSam  },
                { "Giải trí", pbGiaiTri },
                { "Hóa đơn",  pbHoaDon  }
            };

            foreach (var pb in map.Values) pb.Value = 0;
            pbKhac.Value = 0;

            int colorIdx = 0;
            foreach (DataRow row in dt.Rows)
            {
                string ten = row["TenDanhMuc"].ToString();
                int pct = row["PhanTram"] == DBNull.Value ? 0 : Convert.ToInt32(row["PhanTram"]);
                pct = Math.Min(100, Math.Max(0, pct));

                if (map.ContainsKey(ten))
                {
                    map[ten].Value = pct;
                    map[ten].ProgressColor = _mauNganSach[colorIdx % (_mauNganSach.Length - 1)];
                    colorIdx++;
                }
                else
                {
                    // Các danh mục khác gom vào pbKhac
                    pbKhac.Value = Math.Min(100, pbKhac.Value + pct);
                    pbKhac.ProgressColor = _mauNganSach[5];
                }
            }
        }

        // ─── 4. GIAO DỊCH GẦN ĐÂY ───
        private void LoadGiaoDichGanDay()
        {
            var list = _gdBLL.GetGanDay(Session.MaNguoiDung);
            dgvGanDay.Rows.Clear();
            foreach (var gd in list)
            {
                int idx = dgvGanDay.Rows.Add(
                    gd.NgayGiaoDich.ToString("dd/MM"),
                    gd.LoaiGiaoDich,
                    gd.TenDanhMuc,
                    gd.TenTaiKhoan,
                    gd.SoTien.ToString("N0") + " đ",
                    gd.MoTa);

                Color c = gd.LoaiGiaoDich == "Thu nhập" ? Color.FromArgb(46, 204, 113)
                        : gd.LoaiGiaoDich == "Chi tiêu" ? Color.FromArgb(231, 76, 60)
                                                        : Color.FromArgb(52, 152, 219);
                dgvGanDay.Rows[idx].Cells[4].Style.ForeColor = c; // Đổi màu số tiền
            }
        }

        // ─── 5. BIỂU ĐỒ THỐNG KÊ 12 THÁNG ───
        private void LoadBieuDo(int nam)
        {
            DataTable dt = _gdBLL.GetThongKeTheoThang(Session.MaNguoiDung, nam);

            chartThuChi6Thang.Series.Clear();

            Series seriesThu = new Series("Thu nhập")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.FromArgb(46, 204, 113),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

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
                double tongThu = 0;
                double tongChi = 0;

                DataRow[] rows = dt.Select("Thang = " + i);
                if (rows.Length > 0)
                {
                    tongThu = Convert.ToDouble(rows[0]["TongThu"]);
                    tongChi = Convert.ToDouble(rows[0]["TongChi"]);
                }

                seriesThu.Points.AddXY("T" + i, tongThu);
                seriesChi.Points.AddXY("T" + i, tongChi);
            }

            chartThuChi6Thang.Series.Add(seriesThu);
            chartThuChi6Thang.Series.Add(seriesChi);

            chartThuChi6Thang.ChartAreas[0].AxisX.Interval = 1;
            chartThuChi6Thang.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0 đ";
            chartThuChi6Thang.Legends[0].Docking = Docking.Top;
        }

        // ─── 6. BẢNG MỤC TIÊU ───
        // ─── 6. BẢNG MỤC TIÊU (TỐI GIẢN) ───
        // ─── 6. BẢNG MỤC TIÊU (TỐI GIẢN) ───
        private void LoadMucTieu()
        {
            List<MucTieuDTO> dsMucTieu = _mtBLL.LayDanhSach(Session.MaNguoiDung);

            // Xóa dữ liệu cũ
            dgvMucTieu.Rows.Clear();

            // Setup lại cột bằng code (đề phòng bạn chưa chỉnh ở giao diện)
            if (dgvMucTieu.ColumnCount != 2)
            {
                dgvMucTieu.Columns.Clear();
                dgvMucTieu.Columns.Add("TenMucTieu", "Tên mục tiêu");
                dgvMucTieu.Columns.Add("TienDo", "Tiến độ");

                // Căn lề phải cho cột Phần trăm nhìn cho ngay ngắn, chuyên nghiệp
                dgvMucTieu.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            foreach (var mt in dsMucTieu)
            {
                // Trả lại đúng tên biến nguyên thủy của bạn: SoTienHienCo và SoTienCanDat
                decimal daTichLuy = mt.SoTienHienCo;
                decimal mucTieu = mt.SoTienCanDat;

                // Tính phần trăm
                decimal tienDo = mucTieu > 0 ? (daTichLuy / mucTieu) * 100 : 0;
                tienDo = Math.Min(100, Math.Max(0, tienDo));

                // Mẹo nhỏ: Nếu đạt 100% thì hiển thị chữ "Hoàn thành" kèm icon cho sinh động
                string hienThiTienDo = Math.Round(tienDo, 1) + " %";
                if (tienDo >= 100)
                {
                    hienThiTienDo = "Hoàn thành 🎯";
                }

                // Thêm đúng 2 thông tin vào bảng
                dgvMucTieu.Rows.Add(mt.TenMucTieu, hienThiTienDo);
            }

            // Xóa dòng chọn mặc định để bảng nhìn sạch sẽ như một list danh sách
            dgvMucTieu.ClearSelection();
        }
    }
}