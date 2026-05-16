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
    public partial class frmThuNhap : Form
    {
        private readonly GiaoDichBLL _gdBLL = new GiaoDichBLL();
        private readonly DanhMucBLL _dmBLL = new DanhMucBLL();
        private readonly TaiKhoanBLL _tkBLL = new TaiKhoanBLL();
        private List<GiaoDichDTO> _danhSach = new List<GiaoDichDTO>();

        private int maGiaoDichDangSua = -1;

        public frmThuNhap()
        {
            InitializeComponent();

            // CHIÊU CUỐI: Dùng VisibleChanged thay cho Load. 
            // Mỗi lần Form hiện lên màn hình là nó tự động lấy dữ liệu mới nhất!
            this.VisibleChanged += new EventHandler(frmThuNhap_VisibleChanged);
        }

        private void frmThuNhap_VisibleChanged(object sender, EventArgs e)
        {
            // Chỉ nạp dữ liệu khi form đang hiển thị và đã có người đăng nhập
            if (this.Visible && Session.MaNguoiDung > 0)
            {
                try
                {
                    LoadDanhMuc();
                    LoadTaiKhoan();
                    LoadDanhSach();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu từ SQL: \n" + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDanhMuc()
        {
            var list = _dmBLL.GetByLoai(Session.MaNguoiDung, "Thu nhập");
            cboThuNhap_DanhMuc.DataSource = list;
            cboThuNhap_DanhMuc.DisplayMember = "TenDanhMuc";
            cboThuNhap_DanhMuc.ValueMember = "MaDanhMuc";
        }

        private void LoadTaiKhoan()
        {
            var list = _tkBLL.GetByUser(Session.MaNguoiDung);
            cboThuNhap_TaiKhoan.DataSource = list;
            cboThuNhap_TaiKhoan.DisplayMember = "TenTaiKhoan";
            cboThuNhap_TaiKhoan.ValueMember = "MaTaiKhoan";
        }

        private void LoadDanhSach()
        {
            _danhSach = _gdBLL.GetAll(Session.MaNguoiDung).Where(g => g.LoaiGiaoDich == "Thu nhập").ToList();
            dgvThuNhap.Rows.Clear();
            decimal tong = 0;

            foreach (var gd in _danhSach)
            {
                dgvThuNhap.Rows.Add(
                    gd.NgayGiaoDich.ToString("dd/MM/yyyy"),
                    gd.TenDanhMuc,
                    gd.TenTaiKhoan,
                    gd.SoTien.ToString("N0") + " đ",
                    gd.MoTa
                );
                tong += gd.SoTien;
            }
            lblTongSoGiaoDich.Text = _danhSach.Count.ToString();
            lblTongThuNhap.Text = tong.ToString("N0") + " đ";
        }

        private void btnThemThuNhap_Click(object sender, EventArgs e)
        {
            if (cboThuNhap_DanhMuc.SelectedValue == null || cboThuNhap_TaiKhoan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Danh mục và Tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtThuNhap_SoTien.Text.Replace(",", "").Replace(".", ""), out decimal soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var gd = new GiaoDichDTO
            {
                MaNguoiDung = Session.MaNguoiDung,
                MaTaiKhoan = (int)cboThuNhap_TaiKhoan.SelectedValue,
                MaDanhMuc = (int)cboThuNhap_DanhMuc.SelectedValue,
                LoaiGiaoDich = "Thu nhập",
                NgayGiaoDich = dateTimePicker_NgayNhan.Value,
                SoTien = soTien,
                MoTa = txtThuNhap_GhiChu.Text.Trim()
            };

            if (maGiaoDichDangSua == -1)
            {
                if (_gdBLL.Them(gd))
                {
                    MessageBox.Show("Thêm thu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    btnThemThuNhap.Text = "Thêm thu nhập";
                }
            }
        }

        private void dgvThuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _danhSach.Count) return;

            var gd = _danhSach[e.RowIndex];

            // XÓA
            if (dgvThuNhap.Columns[e.ColumnIndex].Name == "colXoa")
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này không?", "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_gdBLL.Xoa(gd))
                    {
                        LoadDanhSach();
                        XoaForm();
                        maGiaoDichDangSua = -1;
                        btnThemThuNhap.Text = "Thêm thu nhập";
                    }
                }
            }

            // SỬA
            if (dgvThuNhap.Columns[e.ColumnIndex].Name == "colSua")
            {
                dateTimePicker_NgayNhan.Value = gd.NgayGiaoDich;
                txtThuNhap_SoTien.Text = gd.SoTien.ToString("G29");
                txtThuNhap_GhiChu.Text = gd.MoTa;
                if (gd.MaDanhMuc.HasValue) cboThuNhap_DanhMuc.SelectedValue = gd.MaDanhMuc.Value;
                cboThuNhap_TaiKhoan.SelectedValue = gd.MaTaiKhoan;

                maGiaoDichDangSua = gd.MaGiaoDich;
                btnThemThuNhap.Text = "Cập nhật";
            }
        }

        private void XoaForm()
        {
            txtThuNhap_SoTien.Text = "";
            txtThuNhap_GhiChu.Text = "";
            dateTimePicker_NgayNhan.Value = DateTime.Now;
            if (cboThuNhap_DanhMuc.Items.Count > 0) cboThuNhap_DanhMuc.SelectedIndex = 0;
            if (cboThuNhap_TaiKhoan.Items.Count > 0) cboThuNhap_TaiKhoan.SelectedIndex = 0;
        }
    }
}