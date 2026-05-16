using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu
{
    public partial class frmMucTieu : Form
    {
        private readonly MucTieuBLL _bll = new MucTieuBLL();
        private List<MucTieuDTO> _danhSachMT = new List<MucTieuDTO>();
        private int maMucTieuDangSua = -1; // -1: Chế độ Thêm mới | Khác -1: Chế độ Sửa (Lưu mã mục tiêu cần sửa)

        public frmMucTieu()
        {
            InitializeComponent();

            // Tự động gọi dữ liệu từ SQL mỗi lần người dùng bấm chuyển qua lại giữa các Tab/Form
            this.VisibleChanged += new EventHandler(frmMucTieu_VisibleChanged);
        }

        private void frmMucTieu_Load(object sender, EventArgs e)
        {
            SetupGrid();
        }

        private void frmMucTieu_VisibleChanged(object sender, EventArgs e)
        {
            // Nếu form hiển thị lên và đã có người dùng đăng nhập thì tải danh sách
            if (this.Visible && Session.MaNguoiDung > 0)
            {
                LoadDanhSach();
            }
        }

        /// <summary>
        /// Cấu hình các thuộc tính hiển thị cho DataGridView mẫu mực
        /// </summary>
        private void SetupGrid()
        {
            dgvMucTieu.DefaultCellStyle.ForeColor = Color.Black;
            dgvMucTieu.AllowUserToAddRows = false;
            dgvMucTieu.ReadOnly = true;
            dgvMucTieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dgvMucTieu.ColumnCount == 0)
            {
                dgvMucTieu.ColumnCount = 3;
                dgvMucTieu.Columns[0].Name = "Tên mục tiêu";
                dgvMucTieu.Columns[1].Name = "Số tiền cần đạt";
                dgvMucTieu.Columns[2].Name = "Hạn chót";
                dgvMucTieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        /// <summary>
        /// Đổ dữ liệu từ Database SQL lên bảng hiển thị
        /// </summary>
        private void LoadDanhSach()
        {
            // Gọi hàm LayDanhSach chuẩn trong file Word của bạn
            _danhSachMT = _bll.LayDanhSach(Session.MaNguoiDung);

            dgvMucTieu.Rows.Clear();
            foreach (var mt in _danhSachMT)
            {
                dgvMucTieu.Rows.Add(
                    mt.TenMucTieu,
                    mt.SoTienCanDat.ToString("N0") + " VNĐ",
                    mt.HanChot.ToShortDateString()
                );
            }
        }

        /// <summary>
        /// Sự kiện click nút Lưu mục tiêu / Cập nhật mục tiêu
        /// </summary>
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tenMucTieu = txtTenMucTieu.Text.Trim();
            // Loại bỏ dấu chấm hoặc phẩy phân tách nghìn nếu người dùng vô tình nhập vào
            string soTien = txtSoTienMucTieu.Text.Trim().Replace(",", "").Replace(".", "");

            // 1. Kiểm tra dữ liệu rỗng
            if (tenMucTieu == "" || soTien == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra số tiền hợp lệ
            if (!decimal.TryParse(soTien, out decimal tien) || tien <= 0)
            {
                MessageBox.Show("Số tiền nhập vào không hợp lệ!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTienMucTieu.Focus();
                return;
            }

            // 3. Đóng gói dữ liệu vào DTO
            var mt = new MucTieuDTO
            {
                MaNguoiDung = Session.MaNguoiDung,
                TenMucTieu = tenMucTieu,
                SoTienCanDat = tien,
                HanChot = dtpHanChot.Value
            };

            // 4. Kiểm tra xem đang ở chế độ THÊM MỚI hay CẬP NHẬT
            if (maMucTieuDangSua == -1)
            {
                // Hành động: THÊM MỚI
                if (_bll.Them(mt))
                {
                    MessageBox.Show("Thêm mục tiêu mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XoaForm();
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại!", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Hành động: CẬP NHẬT CHUẨN 3 LỚP (Gán mã mục tiêu cũ cần sửa vào)
                mt.MaMucTieu = maMucTieuDangSua;

                if (_bll.Sua(mt))
                {
                    MessageBox.Show("Cập nhật mục tiêu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XoaForm();
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Sự kiện Click vào một dòng trên Grid để lấy dữ liệu lên sửa
        /// </summary>
        private void dgvMucTieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra click hợp lệ (không click vào header dòng trống)
            if (e.RowIndex < 0 || e.RowIndex >= _danhSachMT.Count) return;

            // Lấy ra mục tiêu tương ứng với dòng vừa click trong List dữ liệu
            var mt = _danhSachMT[e.RowIndex];

            // Điền ngược dữ liệu lên các ô nhập liệu phía trên
            txtTenMucTieu.Text = mt.TenMucTieu;
            txtSoTienMucTieu.Text = mt.SoTienCanDat.ToString("G29"); // Hiển thị số thuần không định dạng rườm rà để dễ sửa
            dtpHanChot.Value = mt.HanChot;

            // Chuyển Form sang trạng thái "Sửa"
            maMucTieuDangSua = mt.MaMucTieu;
            guna2Button1.Text = "Cập nhật"; // Thay đổi chữ hiển thị trên nút bấm thành Cập nhật
        }

        /// <summary>
        /// Xóa trắng form để chuẩn bị nhập lượt mới hoặc hủy chế độ sửa
        /// </summary>
        private void XoaForm()
        {
            txtTenMucTieu.Clear();
            txtSoTienMucTieu.Clear();
            dtpHanChot.Value = DateTime.Now;

            // Trả Form về trạng thái Thêm mới mặc định
            maMucTieuDangSua = -1;
            guna2Button1.Text = "Lưu mục tiêu";
            txtTenMucTieu.Focus();
        }
    }
}