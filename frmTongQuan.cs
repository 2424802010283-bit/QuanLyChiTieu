using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace QuanLyChiTieu
{
    public partial class frmTongQuan : Form
    {
        // 1. Nhúng API đổi màu Progress Bar (Chỉ khai báo 1 lần duy nhất ở đây)
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public frmTongQuan()
        {
            InitializeComponent();
        }

        // 2. Chạy ngay khi mở form Tổng quan
        private void frmTongQuan_Load(object sender, EventArgs e)
        {
            CapNhatDashboard();
            LoadLichSu();
        }

        // 3. Bấm nút Nhập Nhanh (Smart Input)
        private void btnNhapNhanh_Click(object sender, EventArgs e)
        {
            QuanLyChiTieu.BLL.GiaoDichBLL bll = new QuanLyChiTieu.BLL.GiaoDichBLL();
            var ketQua = bll.PhanTichChuoi(txtSmartInput.Text);

            if (ketQua.soTien > 0)
            {
                // Truyền tham số tuỳ theo BLL cũ của bạn
                if (bll.LuuGiaoDich(ketQua.soTien, ketQua.ghiChu))
                {
                    MessageBox.Show($"Đã lưu thành công: {ketQua.soTien:N0} VNĐ", "Thông báo");
                    txtSmartInput.Clear();

                    // Cập nhật lại UI sau khi thêm
                    CapNhatDashboard();
                    LoadLichSu();
                }
            }
            else
            {
                MessageBox.Show("Không hiểu định dạng. Hãy thử gõ: 150k an toi");
            }
        }

        // 4. Cập nhật thanh Ngân sách (Gộp chung logic đổi màu vào đây cho gọn)
        public void CapNhatDashboard()
        {
            QuanLyChiTieu.BLL.GiaoDichBLL bll = new QuanLyChiTieu.BLL.GiaoDichBLL();

            // Lấy ngân sách từ file txt
            long nganSach = 3000000;
            if (File.Exists("ngansach.txt"))
            {
                long.TryParse(File.ReadAllText("ngansach.txt"), out nganSach);
            }

            // Lấy số tiền đã tiêu
            long daTieu = bll.LayTongChiTieu();

            // Cập nhật Progress Bar
            prbNganSach.Maximum = (int)nganSach;
            prbNganSach.Value = (daTieu > nganSach) ? (int)nganSach : (int)daTieu;

            lblSoTien.Text = $"{daTieu:N0} / {nganSach:N0} VNĐ";

            // Đổi màu Progress Bar theo %
            double phanTram = (double)daTieu / nganSach;
            if (phanTram >= 0.8)
            {
                lblSoTien.ForeColor = Color.Red;
                SendMessage(prbNganSach.Handle, 1040, (IntPtr)2, IntPtr.Zero); // Trạng thái ĐỎ (Error)
            }
            else
            {
                lblSoTien.ForeColor = Color.Black;
                SendMessage(prbNganSach.Handle, 1040, (IntPtr)1, IntPtr.Zero); // Trạng thái XANH (Normal)
            }

            // Gọi luôn hàm Cảnh báo thông minh (Logic tính ngày của bạn rất hay)
            CapNhatCanhBao(nganSach, daTieu);
        }

        // Public wrapper so other forms (Cài đặt) can request a refresh after changing settings
        public void RefreshFromSettings()
        {
            CapNhatDashboard();
            LoadLichSu();
        }

        // 5. Cảnh báo thông minh: Tính tiền tiêu trung bình mỗi ngày (Logic của bạn làm cực kỳ xuất sắc)
        private void CapNhatCanhBao(long nganSach, long daTieu)
        {
            int soNgayTrongThang = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int ngayHienTai = DateTime.Now.Day;

            long choPhepMoiNgay = nganSach / soNgayTrongThang;
            long dangTieuMoiNgay = (ngayHienTai > 0) ? (daTieu / ngayHienTai) : 0;

            if (dangTieuMoiNgay > choPhepMoiNgay)
            {
                lblCanhBao.Text = $"⚠️ CẢNH BÁO: Bạn đang tiêu {dangTieuMoiNgay:N0}đ/ngày, vượt mức cho phép ({choPhepMoiNgay:N0}đ/ngày)!";
                lblCanhBao.ForeColor = Color.Red;
            }
            else
            {
                lblCanhBao.Text = $"✅ TUYỆT VỜI: Tốc độ chi tiêu hợp lý ({dangTieuMoiNgay:N0}đ/ngày). Cứ phát huy nhé!";
                lblCanhBao.ForeColor = Color.Green;
            }
        }

        // 6. Tải dữ liệu vào DataGridView
        private void LoadLichSu()
        {
            QuanLyChiTieu.BLL.GiaoDichBLL bll = new QuanLyChiTieu.BLL.GiaoDichBLL();
            dgvGiaoDich.DataSource = bll.LayLichSuGiaoDich();
        }

        // Wrapper methods to match designer-generated event names (keeps original handlers intact)
        private void btnNhapNhanh_Click_1(object sender, EventArgs e)
        {
            btnNhapNhanh_Click(sender, e);
        }

        private void frmTongQuan_Load_1(object sender, EventArgs e)
        {
            frmTongQuan_Load(sender, e);
        }
    }
}