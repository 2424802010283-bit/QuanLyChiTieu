using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu
{
    public partial class frmNganSach : Form
    {
        // Khai báo các tầng xử lý
        private readonly NganSachBLL _nsBLL = new NganSachBLL();
        private readonly DanhMucBLL _dmBLL = new DanhMucBLL();
        private readonly GiaoDichBLL _gdBLL = new GiaoDichBLL();

        private List<NganSachDTO> _danhSachNS = new List<NganSachDTO>();
        private int maNganSachDangSua = -1; // -1 là Thêm mới, khác -1 là Sửa

        public frmNganSach()
        {
            InitializeComponent();

            // Dùng VisibleChanged thay cho Load để lúc nào chuyển tab cũng có dữ liệu mới nhất
            this.VisibleChanged += new EventHandler(frmNganSach_VisibleChanged);

            // Nếu bạn có một cái DateTimePicker tên là dtpThang để chọn tháng/năm
            dtpThang.ValueChanged += (s, e) => { if (this.Visible) LoadDanhSach(); };
        }

        private void frmNganSach_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && Session.MaNguoiDung > 0)
            {
                SetupGrid();
                LoadDanhMuc();
                LoadDanhSach();
            }
        }

        private void SetupGrid()
        {
            dgvNganSach.DefaultCellStyle.ForeColor = Color.Black;
            dgvNganSach.AllowUserToAddRows = false;
            dgvNganSach.ReadOnly = true;
            dgvNganSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Thiết lập cột nếu chưa kéo thả bên Design
            if (dgvNganSach.ColumnCount == 0)
            {
                dgvNganSach.ColumnCount = 5;
                dgvNganSach.Columns[0].Name = "Tháng";
                dgvNganSach.Columns[1].Name = "Danh mục";
                dgvNganSach.Columns[2].Name = "Ngân sách";
                dgvNganSach.Columns[3].Name = "Đã dùng";
                dgvNganSach.Columns[4].Name = "Còn lại";
                dgvNganSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LoadDanhMuc()
        {
            // Chỉ lấy các danh mục thuộc loại "Chi tiêu"
            var list = _dmBLL.GetByLoai(Session.MaNguoiDung, "Chi tiêu");
            cboLoai.DataSource = list;
            cboLoai.DisplayMember = "TenDanhMuc";
            cboLoai.ValueMember = "MaDanhMuc";
        }

        private void LoadDanhSach()
        {
            int thang = dtpThang.Value.Month;
            int nam = dtpThang.Value.Year;

            // 1. Lấy danh sách ngân sách của tháng/năm đang chọn
            _danhSachNS = _nsBLL.GetAll(Session.MaNguoiDung)
                                .Where(n => n.Thang == thang && n.Nam == nam).ToList();

            // 2. Lấy tất cả giao dịch CHI TIÊU của tháng/năm đó để tính toán tiền ĐÃ DÙNG
            var listChiTieuThangNay = _gdBLL.GetAll(Session.MaNguoiDung)
                                      .Where(g => g.LoaiGiaoDich == "Chi tiêu" &&
                                                  g.NgayGiaoDich.Month == thang &&
                                                  g.NgayGiaoDich.Year == nam).ToList();

            dgvNganSach.Rows.Clear();
            decimal tongDaDungAll = 0;
            decimal tongNganSachAll = 0;

            foreach (var ns in _danhSachNS)
            {
                // TÍNH TOÁN THỰC TẾ 100%: Tìm các giao dịch khớp với Mã Danh Mục của ngân sách này rồi cộng dồn lại
                decimal daDung = listChiTieuThangNay
                                 .Where(g => g.MaDanhMuc == ns.MaDanhMuc)
                                 .Sum(g => g.SoTien);

                decimal conLai = ns.SoTienNganSach - daDung;

                // Lấy tên danh mục để hiển thị
                string tenDanhMuc = cboLoai.Items.Cast<DanhMucDTO>()
                                    .FirstOrDefault(d => d.MaDanhMuc == ns.MaDanhMuc)?.TenDanhMuc ?? "Không rõ";

                dgvNganSach.Rows.Add(
                    $"{ns.Thang:00}/{ns.Nam}",
                    tenDanhMuc,
                    ns.SoTienNganSach.ToString("N0") + " đ",
                    daDung.ToString("N0") + " đ",
                    conLai.ToString("N0") + " đ"
                );

                tongNganSachAll += ns.SoTienNganSach;
                tongDaDungAll += daDung;
            }

            // Cập nhật Thanh Progress Bar Tổng
            if (tongNganSachAll > 0)
            {
                int phanTram = (int)((tongDaDungAll / tongNganSachAll) * 100);
                progressBudget.Value = phanTram > 100 ? 100 : phanTram; // Không vượt quá 100%

                if (phanTram < 50) progressBudget.ProgressColor = Color.DodgerBlue;
                else if (phanTram < 80) progressBudget.ProgressColor = Color.Orange;
                else progressBudget.ProgressColor = Color.Crimson;

                lblTienDaDung.Text = $"Đã dùng {tongDaDungAll:N0} đ / {tongNganSachAll:N0} đ";
            }
            else
            {
                progressBudget.Value = 0;
                lblTienDaDung.Text = "Chưa có dữ liệu ngân sách tháng này";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboLoai.SelectedValue == null)
            { MessageBox.Show("Vui lòng chọn loại danh mục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!decimal.TryParse(txtNganSach.Text.Replace(",", "").Replace(".", ""), out decimal tienNS) || tienNS <= 0)
            { MessageBox.Show("Số tiền ngân sách không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var ns = new NganSachDTO
            {
                MaNguoiDung = Session.MaNguoiDung,
                MaDanhMuc = (int)cboLoai.SelectedValue,
                SoTienNganSach = tienNS,
                Thang = dtpThang.Value.Month,
                Nam = dtpThang.Value.Year
            };

            if (maNganSachDangSua == -1)
            {
                // THÊM MỚI (Lưu ý: BLL của bạn nên kiểm tra nếu danh mục này tháng này có rồi thì cấm thêm trùng)
                if (_nsBLL.Them(ns))
                {
                    MessageBox.Show("Tạo ngân sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XoaForm();
                    LoadDanhSach();
                }
            }
            else
            {
                // CẬP NHẬT
                ns.MaNganSach = maNganSachDangSua;
                if (_nsBLL.Sua(ns))
                {
                    MessageBox.Show("Cập nhật ngân sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XoaForm();
                    LoadDanhSach();
                }
            }
        }


        // Click vào bảng để SỬA
        private void dgvNganSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _danhSachNS.Count) return;

            var ns = _danhSachNS[e.RowIndex];

            // Đưa dữ liệu lên text box
            cboLoai.SelectedValue = ns.MaDanhMuc;
            txtNganSach.Text = ns.SoTienNganSach.ToString("G29");

            // Lưu trạng thái đang sửa
            maNganSachDangSua = ns.MaNganSach;
            btnLuu.Text = "Cập nhật ngân sách";
        }

        private void XoaForm()
        {
            txtNganSach.Clear();
            if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;
            maNganSachDangSua = -1;
            btnLuu.Text = "Lưu ngân sách"; // Trả lại tên nút cũ
        }
      
    }
}