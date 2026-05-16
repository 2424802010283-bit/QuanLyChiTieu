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

        public frmChiTieu() { InitializeComponent(); }

        private void frmChiTieu_Load(object sender, EventArgs e)
        {
            LoadDanhMuc(); LoadTaiKhoan(); LoadDanhSach();
        }

        private void LoadDanhMuc()
        {
            // Phải là "Chi tiêu" để khớp với Database
            var list = _dmBLL.GetByLoai(Session.MaNguoiDung, "Chi tiêu");     // ✅ đã đúng


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
            _danhSach = _gdBLL.GetByLoaiVaNgay(Session.MaNguoiDung, "Chi tiêu", dateTimePicker_NgayChi.Value.Date);
            dgvChiTieu.Rows.Clear();
            decimal tong = 0;
            foreach (var gd in _danhSach)
            {
                dgvChiTieu.Rows.Add(gd.NgayGiaoDich.ToString("dd/MM/yyyy"),
                    gd.MoTa, gd.TenDanhMuc, gd.TenTaiKhoan, gd.SoTien.ToString("N0") + " đ", gd.MoTa);
                tong += gd.SoTien;
            }
            lblTongSoGiaoDich.Text = _danhSach.Count.ToString();
            lblTongChiTieu.Text = tong.ToString("N0") + " đ";
        }

        private void dateTimePicker_NgayChi_ValueChanged(object sender, EventArgs e) => LoadDanhSach();

        private void btnThemChiTieu_Click(object sender, EventArgs e)
        {
            if (cboChiTieu_DanhMuc.SelectedValue == null || cboChiTieu_TaiKhoan.SelectedValue == null)
            { MessageBox.Show("Chọn Danh mục và Tài khoản!"); return; }
            if (!decimal.TryParse(txtChiTieu_SoTien.Text.Replace(",", ""), out decimal soTien) || soTien <= 0)
            { MessageBox.Show("Số tiền không hợp lệ!"); return; }

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
            if (_gdBLL.Them(gd)) { MessageBox.Show("Thêm thành công!"); XoaForm(); LoadDanhSach(); }
        }

        private void dgvChiTieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _danhSach.Count) return;
            var gd = _danhSach[e.RowIndex];
            if (dgvChiTieu.Columns[e.ColumnIndex].Name == "colXoa")
                if (MessageBox.Show("Xác nhận xóa?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (_gdBLL.Xoa(gd)) LoadDanhSach();
            if (dgvChiTieu.Columns[e.ColumnIndex].Name == "colSua")
            {
                dateTimePicker_NgayChi.Value = gd.NgayGiaoDich;
                txtChiTieu_SoTien.Text = gd.SoTien.ToString();
                txtChiTieu_GhiChu.Text = gd.MoTa;
                if (gd.MaDanhMuc.HasValue) cboChiTieu_DanhMuc.SelectedValue = gd.MaDanhMuc.Value;
                cboChiTieu_TaiKhoan.SelectedValue = gd.MaTaiKhoan;
            }
        }

        private void XoaForm()
        {
            txtChiTieu_SoTien.Text = ""; txtChiTieu_GhiChu.Text = "";
            if (cboChiTieu_DanhMuc.Items.Count > 0) cboChiTieu_DanhMuc.SelectedIndex = 0;
            if (cboChiTieu_TaiKhoan.Items.Count > 0) cboChiTieu_TaiKhoan.SelectedIndex = 0;
        }
    }
}
