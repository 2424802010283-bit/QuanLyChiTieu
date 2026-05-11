using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    public partial class frmGiaoDich : Form
    {
        private GiaoDichBLL bllGD = new GiaoDichBLL();
        private DanhMucBLL bllDM = new DanhMucBLL();
        private int maGiaoDichDangChon = -1;

        public frmGiaoDich()
        {
            InitializeComponent();
        }

        // CHỖ NÀY QUAN TRỌNG: Bạn không nên khai báo lại các hàm trùng tên với BLL ở đây 
        // trừ khi bạn muốn đóng gói lại. Mình đã chuyển trực tiếp vào các hàm sự kiện bên dưới.

        private void frmGiaoDich_Load(object sender, EventArgs e)
        {
            LoadComboBoxDanhMuc();
            LoadData();
        }

        private void LoadComboBoxDanhMuc()
        {
            try
            {
                cboDanhMuc.DataSource = bllDM.LayDanhSachDanhMuc();
                cboDanhMuc.DisplayMember = "TenDanhMuc";
                cboDanhMuc.ValueMember = "MaDanhMuc";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                // Gọi trực tiếp từ bllGD để tránh nhầm lẫn
                dgvGiaoDich.DataSource = bllGD.LayDanhSachGiaoDich();

                if (dgvGiaoDich.Columns.Contains("MaGiaoDich"))
                    dgvGiaoDich.Columns["MaGiaoDich"].Visible = false;
                if (dgvGiaoDich.Columns.Contains("MaNguoiDung"))
                    dgvGiaoDich.Columns["MaNguoiDung"].Visible = false;
                if (dgvGiaoDich.Columns.Contains("MaDanhMuc"))
                    dgvGiaoDich.Columns["MaDanhMuc"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu giao dịch: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSoTien.Clear();
            txtMoTa.Clear();
            dateTimePicker_GiaoDich.Value = DateTime.Now;
            if (cboDanhMuc.Items.Count > 0)
            {
                cboDanhMuc.SelectedIndex = 0;
            }
            maGiaoDichDangChon = -1;
            txtSoTien.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoTien.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSoTien.Text, out decimal soTien))
            {
                MessageBox.Show("Số tiền phải là một con số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GiaoDichDTO gd = new GiaoDichDTO
            {
                SoTien = soTien,
                NgayGiaoDich = dateTimePicker_GiaoDich.Value,
                MoTa = txtMoTa.Text, // Đã khớp với DTO mới
                MaDanhMuc = Convert.ToInt32(cboDanhMuc.SelectedValue),
                MaNguoiDung = 1
            };

            if (bllGD.Them(gd)) // Gọi trực tiếp từ bllGD
            {
                MessageBox.Show("Thêm giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Thêm giao dịch thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maGiaoDichDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch từ danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoTien.Text) || !decimal.TryParse(txtSoTien.Text, out decimal soTien))
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GiaoDichDTO gd = new GiaoDichDTO
            {
                MaGiaoDich = maGiaoDichDangChon,
                SoTien = soTien,
                NgayGiaoDich = dateTimePicker_GiaoDich.Value,
                MoTa = txtMoTa.Text,
                MaDanhMuc = Convert.ToInt32(cboDanhMuc.SelectedValue)
            };

            if (bllGD.Sua(gd)) // Gọi trực tiếp từ bllGD
            {
                MessageBox.Show("Cập nhật giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maGiaoDichDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn giao dịch cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // TẠO DTO ĐỂ TRUYỀN VÀO BLL 
                GiaoDichDTO gdCanXoa = new GiaoDichDTO();
                gdCanXoa.MaGiaoDich = maGiaoDichDangChon;

                // Lấy thêm thông tin từ DataGridView để BLL tính toán lại số dư tài khoản
                if (dgvGiaoDich.CurrentRow != null)
                {
                    var row = dgvGiaoDich.CurrentRow;
                    if (row.Cells["LoaiGiaoDich"].Value != DBNull.Value)
                        gdCanXoa.LoaiGiaoDich = row.Cells["LoaiGiaoDich"].Value.ToString();
                    if (row.Cells["SoTien"].Value != DBNull.Value)
                        gdCanXoa.SoTien = Convert.ToDecimal(row.Cells["SoTien"].Value);
                    if (row.Cells["MaTaiKhoan"].Value != DBNull.Value)
                        gdCanXoa.MaTaiKhoan = Convert.ToInt32(row.Cells["MaTaiKhoan"].Value);
                }

                if (bllGD.Xoa(gdCanXoa)) // Lúc này đã truyền đúng kiểu GiaoDichDTO
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(sender, e);
                }
            }
        }

        private void dgvGiaoDich_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGiaoDich.Rows[e.RowIndex];

                maGiaoDichDangChon = Convert.ToInt32(row.Cells["MaGiaoDich"].Value);
                txtSoTien.Text = row.Cells["SoTien"].Value.ToString();

                // Lưu ý: Nếu Database trả về null thì dùng chuỗi rỗng
                txtMoTa.Text = row.Cells["MoTa"].Value != DBNull.Value ? row.Cells["MoTa"].Value.ToString() : "";

                if (row.Cells["NgayGiaoDich"].Value != DBNull.Value)
                {
                    dateTimePicker_GiaoDich.Value = Convert.ToDateTime(row.Cells["NgayGiaoDich"].Value);
                }

                if (row.Cells["MaDanhMuc"].Value != DBNull.Value)
                {
                    cboDanhMuc.SelectedValue = row.Cells["MaDanhMuc"].Value;
                }
            }
        }
    }
}