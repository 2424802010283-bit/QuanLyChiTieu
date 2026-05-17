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
    public partial class frmTaiKhoan : Form
    {
        private readonly TaiKhoanBLL _tkBLL = new TaiKhoanBLL();
        private List<TaiKhoanDTO> _danhSach = new List<TaiKhoanDTO>();
        private TaiKhoanDTO _dangSua = null;
        private readonly string[] _loaiTK = { "Tiền mặt", "Tài khoản ngân hàng", "Ví điện tử", "Thẻ tín dụng", "Khác" };

        public frmTaiKhoan() { InitializeComponent(); }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            cboDanhMucTK.Items.AddRange(_loaiTK);
            cboDanhMucTK.SelectedIndex = 0;
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            _danhSach = _tkBLL.GetByUser(Session.MaNguoiDung);
            dgvTaiKhoan.Rows.Clear();
            foreach (var tk in _danhSach)
                dgvTaiKhoan.Rows.Add(tk.TenTaiKhoan, tk.LoaiTaiKhoan, tk.SoDu.ToString("N0") + " đ", tk.GhiChu);
            cboTaiKhoanTK.DataSource = new List<TaiKhoanDTO>(_danhSach);
            cboTaiKhoanTK.DisplayMember = "TenTaiKhoan";
            cboTaiKhoanTK.ValueMember = "MaTaiKhoan";
        }

        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            string ten = cboTaiKhoanTK.Text.Trim();
            if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Nhập tên tài khoản!"); return; }
            decimal.TryParse(txtSoTienTK.Text.Replace(",", ""), out decimal soDu);

            if (_dangSua == null)
            {
                var tk = new TaiKhoanDTO
                {
                    MaNguoiDung = Session.MaNguoiDung,
                    TenTaiKhoan = ten,
                    LoaiTaiKhoan = cboDanhMucTK.SelectedItem?.ToString(),
                    SoDu = soDu,
                    GhiChu = txtGhiChuTK.Text.Trim()
                };
                if (_tkBLL.Them(tk)) { MessageBox.Show("Thêm thành công!"); XoaForm(); LoadDanhSach(); }
            }
            else
            {
                _dangSua.TenTaiKhoan = ten;
                _dangSua.LoaiTaiKhoan = cboDanhMucTK.SelectedItem?.ToString();
                _dangSua.SoDu = soDu;
                _dangSua.GhiChu = txtGhiChuTK.Text.Trim();
                if (_tkBLL.Sua(_dangSua)) { MessageBox.Show("Cập nhật thành công!"); XoaForm(); LoadDanhSach(); }
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _danhSach.Count) return;
            var tk = _danhSach[e.RowIndex];
            if (dgvTaiKhoan.Columns[e.ColumnIndex].Name == "colXoa")
                if (MessageBox.Show("Xóa tài khoản '" + tk.TenTaiKhoan + "'?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (_tkBLL.Xoa(tk.MaTaiKhoan)) LoadDanhSach();
            if (dgvTaiKhoan.Columns[e.ColumnIndex].Name == "colSua")
            {
                _dangSua = tk;
                cboTaiKhoanTK.Text = tk.TenTaiKhoan;
                txtSoTienTK.Text = tk.SoDu.ToString();
                txtGhiChuTK.Text = tk.GhiChu;
                int idx = Array.IndexOf(_loaiTK, tk.LoaiTaiKhoan);
                cboDanhMucTK.SelectedIndex = idx >= 0 ? idx : 0;
                btnThemTaiKhoan.Text = "Cập nhật";
            }
        }

        private void XoaForm()
        {
            cboTaiKhoanTK.Text = ""; txtSoTienTK.Text = ""; txtGhiChuTK.Text = "";
            cboDanhMucTK.SelectedIndex = 0; btnThemTaiKhoan.Text = "+ Thêm tài khoản"; _dangSua = null;
        }

     
    }
}