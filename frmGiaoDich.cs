using System.Windows.Forms;
using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu
{
    public partial class frmGiaoDich : Form
    {
        GiaoDichBLL bll = new GiaoDichBLL();

        public frmGiaoDich()
        {
            InitializeComponent();
        }

        private void frmGiaoDich_Load(object sender, EventArgs e)
        {
          

            LoadData();
        }

        // Load dữ liệu lên DataGridView
        private void LoadData()
        {
            try
            {
                dgvGiaoDich.DataSource = bllGD.LayDanhSachGiaoDich();

                // Tùy chỉnh hiển thị DataGridView (Ẩn các cột mã ID không cần thiết)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu giao dịch: " + ex.Message);
            }
        }

        // --- NÚT THÊM (Smart Input) ---
        // ✅ FIX: Dùng guna2TextBox1 CHỈ để nhập Smart Input
        //         guna2TextBox2 (ô tìm kiếm riêng) dùng để lọc
        {
            // Xóa trắng các ô nhập liệu
            txtSoTien.Clear();
            {
                cboDanhMuc.SelectedIndex = 0;
                return;
            }

            if (!decimal.TryParse(txtSoTien.Text, out decimal soTien))

            GiaoDichDTO gd = new GiaoDichDTO
            {
                SoTien = soTien,
                {
                MessageBox.Show("Thêm giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                MessageBox.Show("Thêm giao dịch thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        // --- XUẤT EXCEL ---
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
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

            if (bllGD.Sua(gd))
            {
                MessageBox.Show("Cập nhật giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    {
                MessageBox.Show("Vui lòng chọn giao dịch cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        // --- SỬA ---
        private void btnsua_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang cập nhật...");
        }

        // --- XÓA ---
        private void btnxoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang cập nhật...");
        }

        // ✅ FIX: Dùng lại guna2TextBox1 (không cần TextBox2 riêng)
        private void ThucHienLoc()
        {
            string tuKhoa = guna2TextBox1.Text;
        }

        // Sự kiện gõ vào ô tìm kiếm
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ThucHienLoc();
        }

        // Sự kiện chọn loại trong ComboBox
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThucHienLoc();
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}