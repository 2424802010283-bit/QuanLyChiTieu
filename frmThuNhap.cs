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

        public frmThuNhap() { InitializeComponent(); }

        private void frmThuNhap_Load(object sender, EventArgs e)
        { LoadDanhMuc(); LoadTaiKhoan(); LoadDanhSach(); }

        private void LoadDanhMuc()
        {
            var list = _dmBLL.GetByLoai(Session.MaNguoiDung, "Thu");
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
            _danhSach = _gdBLL.GetByLoaiVaNgay(Session.MaNguoiDung, "Thu", dateTimePicker_NgayNhan.Value.Date);
            dgvThuNhap.Rows.Clear();
            decimal tong = 0;
            foreach (var gd in _danhSach)
            {
                dgvThuNhap.Rows.Add(gd.NgayGiaoDich.ToString("dd/MM/yyyy"),
                    gd.TenDanhMuc, gd.TenTaiKhoan, gd.SoTien.ToString("N0") + " đ", gd.MoTa);
                tong += gd.SoTien;
            }
            lblTongSoGiaoDich.Text = _danhSach.Count.ToString();
            lblTongThuNhap.Text = tong.ToString("N0") + " đ";
        }

        private void dateTimePicker_NgayNhan_ValueChanged(object sender, EventArgs e) => LoadDanhSach();

        private void btnThemThuNhap_Click(object sender, EventArgs e)
        {
            if (cboThuNhap_DanhMuc.SelectedValue == null || cboThuNhap_TaiKhoan.SelectedValue == null)
            { MessageBox.Show("Chọn Danh mục và Tài khoản!"); return; }
            if (!decimal.TryParse(txtThuNhap_SoTien.Text.Replace(",", ""), out decimal soTien) || soTien <= 0)
            { MessageBox.Show("Số tiền không hợp lệ!"); return; }

            var gd = new GiaoDichDTO
            {
                MaNguoiDung = Session.MaNguoiDung,
                MaTaiKhoan = (int)cboThuNhap_TaiKhoan.SelectedValue,
                MaDanhMuc = (int)cboThuNhap_DanhMuc.SelectedValue,
                LoaiGiaoDich = "Thu",
                NgayGiaoDich = dateTimePicker_NgayNhan.Value,
                SoTien = soTien,
                MoTa = txtThuNhap_GhiChu.Text.Trim()
            };
            if (_gdBLL.Them(gd)) { MessageBox.Show("Thêm thành công!"); XoaForm(); LoadDanhSach(); }
        }

        private void dgvThuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _danhSach.Count) return;
            var gd = _danhSach[e.RowIndex];
            if (dgvThuNhap.Columns[e.ColumnIndex].Name == "colXoa")
                if (MessageBox.Show("Xác nhận xóa?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (_gdBLL.Xoa(gd)) LoadDanhSach();
            if (dgvThuNhap.Columns[e.ColumnIndex].Name == "colSua")
            {
                dateTimePicker_NgayNhan.Value = gd.NgayGiaoDich;
                txtThuNhap_SoTien.Text = gd.SoTien.ToString();
                txtThuNhap_GhiChu.Text = gd.MoTa;
                if (gd.MaDanhMuc.HasValue) cboThuNhap_DanhMuc.SelectedValue = gd.MaDanhMuc.Value;
                cboThuNhap_TaiKhoan.SelectedValue = gd.MaTaiKhoan;
            }
        }

        private void XoaForm()
        {
            txtThuNhap_SoTien.Text = ""; txtThuNhap_GhiChu.Text = "";
            if (cboThuNhap_DanhMuc.Items.Count > 0) cboThuNhap_DanhMuc.SelectedIndex = 0;
            if (cboThuNhap_TaiKhoan.Items.Count > 0) cboThuNhap_TaiKhoan.SelectedIndex = 0;
        }
    }
}