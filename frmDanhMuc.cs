using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyChiTieu
{
    public partial class frmDanhMuc : Form
    {
        // Chỉ cần khai báo 1 đối tượng BLL dùng chung cho cả Form
        DanhMucBLL dmBLL = new DanhMucBLL();

        public frmDanhMuc()
        {
            InitializeComponent();
            // Khởi tạo một số control và nạp dữ liệu ngay khi tạo form
            // Đảm bảo ComboBox loại danh mục có giá trị để người dùng chọn
            if (cboLoaiDM != null)
            {
                cboLoaiDM.Items.Clear();
                cboLoaiDM.Items.AddRange(new string[] { "Chi", "Thu" });
                cboLoaiDM.SelectedIndex = 0;
            }

            // Nạp dữ liệu lần đầu
            LoadData();
        }

        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            LoadData(); // Gọi hàm này khi load form để nạp dữ liệu lần đầu
        }

        // Wrapper để khớp với tên button trong Designer (btnThemDanhMuc)
        private void btnThemDanhMuc_Click(object sender, EventArgs e)
        {
            btnThem_Click(sender, e);
        }

        // Hàm nạp dữ liệu dùng chung (để gọi lại sau khi Thêm/Sửa/Xóa)
        void LoadData()
        {
            try
            {
                // Gán dữ liệu vào bảng
                dgvListDanhMuc.DataSource = dmBLL.LayDanhSachDanhMuc();

                // Định dạng tiêu đề cột
                if (dgvListDanhMuc.Columns["MaDanhMuc"] != null)
                    dgvListDanhMuc.Columns["MaDanhMuc"].HeaderText = "Mã Danh Mục";

                dgvListDanhMuc.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
                dgvListDanhMuc.Columns["LoaiGiaoDich"].HeaderText = "Loại";

                dgvListDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ Liệu: " + ex.Message);
            }
        }

        private void dgvListDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click vào dòng dữ liệu hay không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvListDanhMuc.Rows[e.RowIndex];
                txtTenDM.Text = row.Cells["TenDanhMuc"].Value.ToString();
                cboLoaiDM.Text = row.Cells["LoaiGiaoDich"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDM.Text))
            {
                MessageBox.Show("vui lòng nhập tên danh mục!");
                return;
            }

            // 1. Đóng gói dữ liệu vào DTO
            DanhMucDTO dm = new DanhMucDTO();
            // Sửa 2 dòng này
            dm.TenDanhMuc = txtTenDM.Text.Trim();
            dm.LoaiGiaoDich = cboLoaiDM.Text;

            // 2. Gọi BLL xử lý
            if (dmBLL.LuuDanhMuc(dm)) // Đảm bảo hàm LuuDanhMuc đã có trong lớp BLL
            {
                MessageBox.Show("Thêm thành công!");
                LoadData(); // Nạp lại bảng để thấy dòng mới ngay lập tức
                txtTenDM.Clear();
            }
            else
            {
                MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại!");
            }
        }
    }
}

