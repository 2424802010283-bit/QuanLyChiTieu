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
    public partial class frmChiTieu : Form
    {
        private readonly GiaoDichBLL _gdBLL = new GiaoDichBLL();
        private readonly DanhMucBLL _dmBLL = new DanhMucBLL();
        private readonly TaiKhoanBLL _tkBLL = new TaiKhoanBLL();
        private List<GiaoDichDTO> _danhSach = new List<GiaoDichDTO>();

        // Thêm biến này để hỗ trợ tính năng Cập nhật (Sửa)
        private int maGiaoDichDangSua = -1;

        public frmChiTieu()
        {
            InitializeComponent();

            // Xóa sự kiện Load cũ, dùng VisibleChanged để lấy dữ liệu real-time
            this.VisibleChanged += new EventHandler(frmChiTieu_VisibleChanged);
        }

        private void frmChiTieu_VisibleChanged(object sender, EventArgs e)
        {
            // Chỉ load khi form đang mở và đã có user đăng nhập
            if (this.Visible && Session.MaNguoiDung > 0)
            {
                LoadDanhMuc();
                LoadTaiKhoan();
                LoadDanhSach();
            }
        }

        private void LoadDanhMuc()
        {
            var list = _dmBLL.GetByLoai(Session.MaNguoiDung, "Chi tiêu");
            cboChiTieu_DanhMuc.DataSource = list;
            cboChiTieu_DanhMuc.DisplayMember = "TenDanhMuc";
            cboChiTieu_DanhMuc.ValueMember = "MaDanhMuc";
        }

        private void LoadTaiKhoan()
        {
            var list = _tkBLL.GetByUser(Session.MaNguoiDung);
            cboChiTieu_TaiKhoan.DataSource = list;
            cboChiTieu_TaiKhoan.DisplayMember = "TenTaiKhoan";
            cboChiTieu_TaiKhoan.ValueMember = "MaTaiKhoan";
        }

        private void LoadDanhSach()
        {
            // Đổi thành GetAll và lọc bằng Where để hiển thị TẤT CẢ giao dịch chi tiêu (không bị giới hạn bởi ngày trên lịch)
            _danhSach = _gdBLL.GetAll(Session.MaNguoiDung).Where(g => g.LoaiGiaoDich == "Chi tiêu").ToList();

            dgvChiTieu.Rows.Clear();
            decimal tong = 0;

            foreach (var gd in _danhSach)
            {
                dgvChiTieu.Rows.Add(
                    gd.NgayGiaoDich.ToString("dd/MM/yyyy"),
                    gd.TenDanhMuc,
                    gd.TenTaiKhoan,
                    gd.SoTien.ToString("N0") + " đ",
                    gd.MoTa
                );
                tong += gd.SoTien;
            }
            lblTongSoGiaoDich.Text = _danhSach.Count.ToString();
            lblTongChiTieu.Text = tong.ToString("N0") + " đ";
        }

        // BỎ sự kiện value changed của dateTimePicker nếu bạn muốn hiện tất cả
        // private void dateTimePicker_NgayChi_ValueChanged(object sender, EventArgs e) => LoadDanhSach();

        private void btnThemChiTieu_Click(object sender, EventArgs e)
        {
            if (cboChiTieu_DanhMuc.SelectedValue == null || cboChiTieu_TaiKhoan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Danh mục và Tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtChiTieu_SoTien.Text.Replace(",", "").Replace(".", ""), out decimal soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var gd = new GiaoDichDTO
            {
                MaNguoiDung = Session.MaNguoiDung,
                MaTaiKhoan = (int)cboChiTieu_TaiKhoan.SelectedValue,
                MaDanhMuc = (int)cboChiTieu_DanhMuc.SelectedValue,
                LoaiGiaoDich = "Chi tiêu",
                NgayGiaoDich = dateTimePicker_NgayChi.Value,
                SoTien = soTien,
                MoTa = txtChiTieu_GhiChu.Text.Trim()
            };

            if (maGiaoDichDangSua == -1)
            {
                if (_gdBLL.Them(gd))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XoaForm();
                    LoadDanhSach();
                }
            }
            else
            {
                gd.MaGiaoDich = maGiaoDichDangSua;
                if (_gdBLL.Sua(gd))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XoaForm();
                    LoadDanhSach();
                    maGiaoDichDangSua = -1;
                    btnThemChiTieu.Text = "Thêm chi tiêu";
                }
            }
        }

        private void dgvChiTieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _danhSach.Count) return;

            var gd = _danhSach[e.RowIndex];

            if (dgvChiTieu.Columns[e.ColumnIndex].Name == "colXoa")
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_gdBLL.Xoa(gd))
                    {
                        LoadDanhSach();
                        XoaForm();
                        maGiaoDichDangSua = -1;
                        btnThemChiTieu.Text = "Thêm chi tiêu";
                    }
                }
            }

            if (dgvChiTieu.Columns[e.ColumnIndex].Name == "colSua")
            {
                dateTimePicker_NgayChi.Value = gd.NgayGiaoDich;
                txtChiTieu_SoTien.Text = gd.SoTien.ToString("G29");
                txtChiTieu_GhiChu.Text = gd.MoTa;
                if (gd.MaDanhMuc.HasValue) cboChiTieu_DanhMuc.SelectedValue = gd.MaDanhMuc.Value;
                cboChiTieu_TaiKhoan.SelectedValue = gd.MaTaiKhoan;

                maGiaoDichDangSua = gd.MaGiaoDich;
                btnThemChiTieu.Text = "Cập nhật";
            }
        }

        private void XoaForm()
        {
            txtChiTieu_SoTien.Text = "";
            txtChiTieu_GhiChu.Text = "";
            dateTimePicker_NgayChi.Value = DateTime.Now;
            if (cboChiTieu_DanhMuc.Items.Count > 0) cboChiTieu_DanhMuc.SelectedIndex = 0;
            if (cboChiTieu_TaiKhoan.Items.Count > 0) cboChiTieu_TaiKhoan.SelectedIndex = 0;
        }
    }
}