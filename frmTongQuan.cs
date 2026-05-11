using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LiveCharts;
using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyChiTieu
{
    public partial class frmTongQuan : Form
    {
        private readonly GiaoDichBLL _gdBLL = new GiaoDichBLL();
        private readonly TaiKhoanBLL _tkBLL = new TaiKhoanBLL();
        private readonly NganSachBLL _nsBLL = new NganSachBLL();
        private readonly MucTieuBLL _mtBLL = new MucTieuBLL();

        private readonly Color[] _mauNganSach = {
            Color.FromArgb(231, 76,  60),
            Color.FromArgb( 52, 73, 180),
            Color.FromArgb( 26,188, 156),
            Color.FromArgb(231,100,  80),
            Color.FromArgb(230,126,  34),
            Color.FromArgb(189,195, 199),
        };

        public frmTongQuan()
        {
            InitializeComponent();
        }

        private void frmTongQuan_Load(object sender, EventArgs e) => LoadTongQuan();

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

        // ─── 4 Card ───
        private void Load4Card(int thang, int nam)
        {
            decimal soDu = _tkBLL.GetTongSoDu(Session.MaNguoiDung);
            decimal tongThu = _gdBLL.GetTong(Session.MaNguoiDung, "Thu", thang, nam);
            decimal tongChi = _gdBLL.GetTong(Session.MaNguoiDung, "Chi", thang, nam);
            decimal tietKiem = tongThu - tongChi;

            lblTongSoDu.Text = soDu.ToString("N0") + " đ";
            lblTongThuNhap.Text = tongThu.ToString("N0") + " đ";
            lblTongChiTieu.Text = tongChi.ToString("N0") + " đ";
            lblTietKiem.Text = tietKiem.ToString("N0") + " đ";
            lblTietKiem.ForeColor = tietKiem >= 0 ? Color.FromArgb(155, 89, 182) : Color.FromArgb(231, 76, 60);
        }

        // ─── Tổng chi tiêu tháng ───
        private void LoadTongChiTieuThang(int thang, int nam)
        {
            var (tongNS, tongDaChi) = _nsBLL.GetTong(Session.MaNguoiDung, thang, nam);
            lblDaChi.Text = "Đã chi: " + tongDaChi.ToString("N0") + " đ";
            lblDaTieu.Text = "Đã tiêu: " + tongDaChi.ToString("N0") + " / " + tongNS.ToString("N0") + " đ";

            if (tongNS > 0)
            {
                int pct = (int)Math.Min(100, tongDaChi * 100 / tongNS);
                pbTongChiTieuThang.Value = pct;
                pbTongChiTieuThang.ProgressColor = pct < 70
                    ? Color.FromArgb(46, 204, 113)
                    : pct < 90 ? Color.FromArgb(241, 196, 15)
                               : Color.FromArgb(231, 76, 60);
            }
            else pbTongChiTieuThang.Value = 0;
        }

        // ─── Ngân sách từng danh mục ───
        private void LoadNganSachDanhMuc(int thang, int nam)
        {
            DataTable dt = _nsBLL.GetTheoThang(Session.MaNguoiDung, thang, nam);

            var map = new Dictionary<string, Guna.UI2.WinForms.Guna2ProgressBar>
            {
                { "Ăn uống",  pbAnUong  },
                { "Đi lại",   pbDiLai   },
                { "Mua sắm",  pbMuaSam  },
                { "Giải trí", pbGiaiTri },
                { "Hóa đơn",  pbHoaDon  },
                { "Khác",     pbKhac    },
            };
            foreach (var pb in map.Values) pb.Value = 0;

            int colorIdx = 0;
            foreach (DataRow row in dt.Rows)
            {
                string ten = row["TenDanhMuc"].ToString();
                int pct = row["PhanTram"] == DBNull.Value ? 0 : Convert.ToInt32(row["PhanTram"]);
                pct = Math.Min(100, pct);
                if (map.ContainsKey(ten))
                {
                    map[ten].Value = pct;
                    map[ten].ProgressColor = _mauNganSach[colorIdx % _mauNganSach.Length];
                }
                else
                {
                    pbKhac.Value = Math.Max(pbKhac.Value, pct);
                    pbKhac.ProgressColor = _mauNganSach[5];
                }
                colorIdx++;
            }
        }

        // ─── Giao dịch gần đây ───
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
                Color c = gd.LoaiGiaoDich == "Thu" ? Color.FromArgb(46, 204, 113)
                        : gd.LoaiGiaoDich == "Chi" ? Color.FromArgb(231, 76, 60)
                                                   : Color.FromArgb(52, 152, 219);
                dgvGanDay.Rows[idx].Cells["colSoTien"].Style.ForeColor = c;
            }
        }

        // ─── Biểu đồ 6 tháng ───
        // ─── Biểu đồ 6 tháng (Dùng Chart mặc định của C#) ───
        private void LoadBieuDo(int nam)
        {
            // Giả sử maNguoiDung = 1, nhớ thay bằng Session.MaNguoiDung nếu bạn đã có đăng nhập
            int maNguoiDung = 1;
            DataTable dt = _gdBLL.GetThongKeTheoThang(maNguoiDung, nam);

            // Xóa dữ liệu cũ của biểu đồ
            chartThuChi6Thang.Series.Clear();

            // Tạo đường vẽ Thu nhập
            Series seriesThu = new Series("Thu nhập")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.FromArgb(52, 152, 219),
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

            // Chuẩn bị dữ liệu cho 12 tháng
            for (int i = 1; i <= 12; i++)
            {
                double tongThu = 0;
                double tongChi = 0;

                // Tìm dữ liệu của tháng 'i' trong DataTable trả về
                DataRow[] rows = dt.Select("Thang = " + i);
                if (rows.Length > 0)
                {
                    tongThu = Convert.ToDouble(rows[0]["TongThu"]);
                    tongChi = Convert.ToDouble(rows[0]["TongChi"]);
                }

                // Thêm điểm vào biểu đồ (Trục X là "Tx", Trục Y là số tiền)
                seriesThu.Points.AddXY("T" + i, tongThu);
                seriesChi.Points.AddXY("T" + i, tongChi);
            }

            // Gắn 2 đường vẽ vào biểu đồ
            chartThuChi6Thang.Series.Add(seriesThu);
            chartThuChi6Thang.Series.Add(seriesChi);

            // Tùy chỉnh làm đẹp biểu đồ
            chartThuChi6Thang.ChartAreas[0].AxisX.Interval = 1; // Hiện đủ 12 tháng không bị thiếu chữ
            chartThuChi6Thang.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0 đ"; // Định dạng tiền tệ
            chartThuChi6Thang.Legends[0].Docking = Docking.Top; // Đưa chú thích lên trên cùng
        }
        // ─── Mục tiêu ───
        private void LoadMucTieu()
        {
            // Lấy danh sách từ BLL
            List<MucTieuDTO> dsMucTieu = GetDt();

            dgvMucTieu.Rows.Clear();

            foreach (var mt in dsMucTieu)
            {
                dgvMucTieu.Rows.Add(
                    mt.TenMucTieu,
                    mt.SoTienCanDat.ToString("N0") + " đ",
                    mt.SoTienHienCo.ToString("N0") + " đ",
                    mt.HanChot.ToString("dd/MM/yyyy"),
                    mt.TienDo,    // Đây là thuộc tính tự tính toán trong DTO
                    mt.TrangThai  // Đây là thuộc tính tự tính toán trong DTO
                );
            }
        }

        private List<MucTieuDTO> GetDt()
        {
            // Đổi GetAll thành LayDanhSach để khớp với BLL
            return _mtBLL.LayDanhSach(Session.MaNguoiDung);
        }
    }
}
