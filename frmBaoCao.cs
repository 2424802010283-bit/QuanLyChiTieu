using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyChiTieu.BLL;

namespace QuanLyChiTieu
{
    public partial class frmBaoCao : Form
    {
        private readonly GiaoDichBLL _gdBLL = new GiaoDichBLL();

        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            // Thiết lập mặc định cho các ComboBox
            cmbloaibc.SelectedIndex = 0; // Chọn "Tất cả"
            cmbngaythang.SelectedIndex = DateTime.Now.Month - 1; // Chọn tháng hiện tại

            SetupChart();
        }

        private void SetupChart()
        {
            chartBaoCao.Series.Clear();
            chartBaoCao.Titles.Clear();
            chartBaoCao.Titles.Add("Phân bổ tài chính");

            Series s = new Series("Số tiền");
            s.ChartType = SeriesChartType.Column; // Dạng cột như hình bạn thiết kế
            s["PointWidth"] = "0.6";
            chartBaoCao.Series.Add(s);
        }

        // SỰ KIỆN NÚT XUẤT BÁO CÁO
        private void btnXuatbaocao_Click(object sender, EventArgs e)
        {
            LoadDuLieuBaoCao();
        }

        private void LoadDuLieuBaoCao()
        {
            try
            {
                // 1. Lấy giá trị từ các ComboBox
                int thang = cmbngaythang.SelectedIndex + 1; // Index 0 là Tháng 1
                int nam = DateTime.Now.Year;
                string loaiLoc = cmbloaibc.Text; // "Tất cả", "Chi tiêu", hoặc "Thu nhập"
                int maNguoiDung = 1;

                // 2. Gọi BLL lấy dữ liệu (Sử dụng hàm thống kê đã sửa lỗi ở bước trước)
                DataTable dt = _gdBLL.LayThongKeTheoDanhMuc(maNguoiDung, thang, nam);

                // 3. Lọc lại DataTable dựa trên loại báo cáo đã chọn
                DataView dv = new DataView(dt);
                if (loaiLoc != "Tất cả")
                {
                    dv.RowFilter = $"Loai = '{loaiLoc}'";
                }

                // 4. Hiển thị lên DataGridView (Cột phải)
                dgvBaoCao.DataSource = dv.ToTable();
                DinhDangGrid();

                // 5. Hiển thị lên Chart (Cột trái)
                HienThiBieuDo(dv.ToTable());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message);
            }
        }

        private void HienThiBieuDo(DataTable dt)
        {
            chartBaoCao.Series["Số tiền"].Points.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string tenDM = row["TenDanhMuc"].ToString();
                double soTien = Convert.ToDouble(row["TongTien"]);
                string loai = row["Loai"].ToString();

                int idx = chartBaoCao.Series["Số tiền"].Points.AddXY(tenDM, soTien);

                // Đổi màu cột: Thu nhập màu xanh, Chi tiêu màu đỏ
                if (loai == "Thu nhập")
                    chartBaoCao.Series["Số tiền"].Points[idx].Color = Color.FromArgb(46, 204, 113);
                else
                    chartBaoCao.Series["Số tiền"].Points[idx].Color = Color.FromArgb(231, 76, 60);
            }
        }

        private void DinhDangGrid()
        {
            if (dgvBaoCao.Columns.Contains("TongTien"))
            {
                dgvBaoCao.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvBaoCao.Columns["TongTien"].HeaderText = "Tổng số tiền";
            }
            if (dgvBaoCao.Columns.Contains("TenDanhMuc"))
                dgvBaoCao.Columns["TenDanhMuc"].HeaderText = "Tên danh mục";
        }
    }
}