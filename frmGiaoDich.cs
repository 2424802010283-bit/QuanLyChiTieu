using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
namespace QuanLyChiTieu
{
    public partial class frmGiaoDich : Form
    {
        // 1. KHAI BÁO BLL DUY NHẤT VÀ GỌN GÀNG
        private GiaoDichBLL bllGD = new GiaoDichBLL();
        private DanhMucBLL bllDM = new DanhMucBLL();
        private TaiKhoanBLL bllTK = new TaiKhoanBLL();

        private int maGiaoDichDangChon = -1;
        private int maNguoiDung = 1; // Fix cứng tạm thời để test

        public frmGiaoDich()
        {
            InitializeComponent();
        }

        private void frmGiaoDich_Load(object sender, EventArgs e)
        {
            try
            {
                // ĐÃ SỬA: Dùng GetByUser theo đúng tên hàm trong TaiKhoanBLL của bạn
                cboTaiKhoan.DataSource = bllTK.GetByUser(maNguoiDung);
                cboTaiKhoan.DisplayMember = "TenTaiKhoan";
                cboTaiKhoan.ValueMember = "MaTaiKhoan";

                // Load dữ liệu cho ComboBox Danh mục
                cboDanhMuc.DataSource = bllDM.LayDanhSachDanhMuc();
                cboDanhMuc.DisplayMember = "TenDanhMuc";
                cboDanhMuc.ValueMember = "MaDanhMuc";

                // Chọn mặc định Loại giao dịch
                if (cboLoaiGiaoDich.Items.Count > 0)
                    cboLoaiGiaoDich.SelectedIndex = 0;

                // Load danh sách giao dịch lên DataGridView
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                dgvGiaoDich.DataSource = bllGD.LayDanhSachGiaoDich();

                // Ẩn các cột ID không cần thiết
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

            if (cboDanhMuc.Items.Count > 0) cboDanhMuc.SelectedIndex = 0;
            if (cboTaiKhoan.Items.Count > 0) cboTaiKhoan.SelectedIndex = 0;
            if (cboLoaiGiaoDich.Items.Count > 0) cboLoaiGiaoDich.SelectedIndex = 0;

            maGiaoDichDangChon = -1;
            txtSoTien.Focus();
        }

        // ================== NÚT THÊM ==================
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra Validate
                if (string.IsNullOrEmpty(cboLoaiGiaoDich.Text))
                {
                    MessageBox.Show("Vui lòng chọn Loại giao dịch!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSoTien.Text) || !decimal.TryParse(txtSoTien.Text, out decimal soTien))
                {
                    MessageBox.Show("Số tiền không hợp lệ! Vui lòng nhập số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTien.Focus();
                    return;
                }

                // 2. Gom dữ liệu vào DTO
                GiaoDichDTO gd = new GiaoDichDTO
                {
                    MaNguoiDung = maNguoiDung,
                    LoaiGiaoDich = cboLoaiGiaoDich.Text.Trim(),
                    MaTaiKhoan = Convert.ToInt32(cboTaiKhoan.SelectedValue),
                    SoTien = soTien,
                    NgayGiaoDich = dateTimePicker_GiaoDich.Value,
                    MoTa = txtMoTa.Text.Trim()
                };

                // Nếu không phải chuyển tiền thì lấy Mã Danh Mục
                if (gd.LoaiGiaoDich != "ChuyenTien")
                {
                    gd.MaDanhMuc = Convert.ToInt32(cboDanhMuc.SelectedValue);
                }

                // 3. Thực thi
                if (bllGD.Them(gd))
                {
                    MessageBox.Show("Thêm giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(sender, e); // Xóa trắng form
                }
                else
                {
                    MessageBox.Show("Thêm giao dịch thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== NÚT SỬA ==================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maGiaoDichDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch từ danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Kiểm tra Validate
                if (string.IsNullOrEmpty(cboLoaiGiaoDich.Text))
                {
                    MessageBox.Show("Vui lòng chọn Loại giao dịch!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSoTien.Text) || !decimal.TryParse(txtSoTien.Text, out decimal soTien))
                {
                    MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Gom dữ liệu vào DTO
                GiaoDichDTO gd = new GiaoDichDTO
                {
                    MaGiaoDich = maGiaoDichDangChon,
                    MaNguoiDung = maNguoiDung,
                    LoaiGiaoDich = cboLoaiGiaoDich.Text.Trim(),
                    MaTaiKhoan = Convert.ToInt32(cboTaiKhoan.SelectedValue),
                    SoTien = soTien,
                    NgayGiaoDich = dateTimePicker_GiaoDich.Value,
                    MoTa = txtMoTa.Text.Trim()
                };

                if (gd.LoaiGiaoDich != "ChuyenTien")
                {
                    gd.MaDanhMuc = Convert.ToInt32(cboDanhMuc.SelectedValue);
                }

                // 3. Thực thi
                if (bllGD.Sua(gd))
                {
                    MessageBox.Show("Cập nhật giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sửa giao dịch thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== NÚT XÓA ==================
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
                try
                {
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

                    if (bllGD.Xoa(gdCanXoa))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        btnLamMoi_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================== SỰ KIỆN CLICK VÀO LƯỚI ==================
        private void dgvGiaoDich_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGiaoDich.Rows[e.RowIndex];

                maGiaoDichDangChon = Convert.ToInt32(row.Cells["MaGiaoDich"].Value);
                txtSoTien.Text = row.Cells["SoTien"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value != DBNull.Value ? row.Cells["MoTa"].Value.ToString() : "";

                if (row.Cells["NgayGiaoDich"].Value != DBNull.Value)
                    dateTimePicker_GiaoDich.Value = Convert.ToDateTime(row.Cells["NgayGiaoDich"].Value);

                if (row.Cells["MaDanhMuc"].Value != DBNull.Value)
                    cboDanhMuc.SelectedValue = row.Cells["MaDanhMuc"].Value;

                if (row.Cells["MaTaiKhoan"].Value != DBNull.Value)
                    cboTaiKhoan.SelectedValue = row.Cells["MaTaiKhoan"].Value;

                // Bind thêm Loại giao dịch nếu bạn có Cột này trên Grid
                if (row.Cells["LoaiGiaoDich"].Value != DBNull.Value)
                    cboLoaiGiaoDich.Text = row.Cells["LoaiGiaoDich"].Value.ToString();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.ToLower().Trim();
                var danhSachTatCa = bllGD.LayDanhSachGiaoDich(); // Lấy tất cả

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    LoadData(); // Nếu ô tìm kiếm trống, load lại toàn bộ
                }
                else
                {
                    // Lọc ra các giao dịch có chứa từ khóa trong cột MoTa (hoặc tùy bạn thêm LoaiGiaoDich)
                    // Cú pháp này dùng LINQ rất tiện lợi trong C#
                    var danhSachLoc = danhSachTatCa.Where(gd =>
                        gd.MoTa != null && gd.MoTa.ToLower().Contains(tuKhoa)).ToList();

                    dgvGiaoDich.DataSource = danhSachLoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                // Giả sử bạn đặt tên 2 thanh chọn ngày lọc là dtpTuNgay và dtpDenNgay
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); // Lấy đến hết ngày hôm đó

                var danhSachTatCa = bllGD.LayDanhSachGiaoDich();

                // Lọc những giao dịch nằm trong khoảng thời gian
                var danhSachLoc = danhSachTatCa.Where(gd =>
                    gd.NgayGiaoDich >= tuNgay && gd.NgayGiaoDich <= denNgay).ToList();

                dgvGiaoDich.DataSource = danhSachLoc;

                if (danhSachLoc.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy giao dịch nào trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trên lưới để xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Workbook (*.csv)|*.csv";
                sfd.FileName = "DanhSachGiaoDich.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Sử dụng Encoding.UTF8 để Excel hiển thị đúng tiếng Việt có dấu
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        // 1. Xuất tiêu đề cột (Chỉ xuất các cột đang hiển thị công khai)
                        List<string> headers = new List<string>();
                        foreach (DataGridViewColumn col in dgvGiaoDich.Columns)
                        {
                            if (col.Visible) headers.Add(col.HeaderText);
                        }
                        sw.WriteLine(string.Join(",", headers));

                        // 2. Xuất dữ liệu từng dòng
                        foreach (DataGridViewRow row in dgvGiaoDich.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                List<string> cells = new List<string>();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.OwningColumn.Visible)
                                    {
                                        string value = cell.Value != null ? cell.Value.ToString() : "";
                                        // Xử lý nếu trong ô mô tả có dấu phẩy tránh bị lệch cột
                                        if (value.Contains(",")) value = $"\"{value}\"";
                                        cells.Add(value);
                                    }
                                }
                                sw.WriteLine(string.Join(",", cells));
                            }
                        }
                    }
                    MessageBox.Show("Xuất dữ liệu ra Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtSoTien.Clear();
            txtMoTa.Clear();
            dateTimePicker_GiaoDich.Value = DateTime.Now;

            if (cboDanhMuc.Items.Count > 0) cboDanhMuc.SelectedIndex = 0;
            if (cboTaiKhoan.Items.Count > 0) cboTaiKhoan.SelectedIndex = 0;
            if (cboLoaiGiaoDich.Items.Count > 0) cboLoaiGiaoDich.SelectedIndex = 0;

            maGiaoDichDangChon = -1;
            txtSoTien.Focus();

            // THÊM DÒNG NÀY: Để nạp lại toàn bộ giao dịch hiển thị lên lưới dgvGiaoDich
            LoadData();
        }
    }
}