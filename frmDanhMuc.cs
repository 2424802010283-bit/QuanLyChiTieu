using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    public partial class frmDanhMuc : Form
    {
        private readonly DanhMucBLL bllDM = new DanhMucBLL();

        public frmDanhMuc()
        {
            InitializeComponent();
        }

        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            pnlInput.Visible = false; // Ẩn panel nhập liệu khi mới vào
            LoadData();
        }

        // --- TẢI DỮ LIỆU ---
        private void LoadData()
        {
            try
            {
                var dsFull = bllDM.LayDanhSachDanhMuc();

                // Nạp cho Tab Chi Tiêu
                dgvdmchi.DataSource = dsFull.Where(x => x.Loai == "Chi tiêu").ToList();

                // Nạp cho Tab Thu Nhập
                dgvdmthu.DataSource = dsFull.Where(x => x.Loai == "Thu nhập").ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- NÚT THÊM DANH MỤC (Nút to ở ngoài) ---
        

        // --- NÚT LƯU (Trong panel) ---
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!");
                return;
            }

            // 2. Tạo đối tượng lưu trữ
            DanhMucDTO dm = new DanhMucDTO
            {
                TenDanhMuc = txtTenDanhMuc.Text.Trim(),
                Loai = cmbLoai.Text // Lấy từ ComboBox đã tự chọn theo Tab
            };

            // 3. Gọi BLL lưu vào Database
            if (bllDM.LuuDanhMuc(dm))
            {
                MessageBox.Show("Lưu thành công!");
                pnlInput.Visible = false; // Ẩn panel sau khi lưu xong
                LoadData(); // Tải lại bảng dữ liệu
            }
            else
            {
                MessageBox.Show("Lưu thất bại!");
            }
        }

        // --- NÚT HỦY (Trong panel) ---
        private void btnHuy_Click(object sender, EventArgs e)
        {
            pnlInput.Visible = false; // Đóng panel, không làm gì cả
        }

        private void btnthemdm_Click(object sender, EventArgs e)
        {
            // 1. Lấy ra lá Tab đang được người dùng mở từ cái TabControl lớn
            // Thay 'tabControl1' bằng tên cái TabControl của bạn
            // 1. Xác định tab đang mở là Chi tiêu hay Thu nhập
            // tabControlDanhMuc là tên Guna2TabControl của bạn trong ảnh
            TabPage activeTab = tabControlDanhMuc.SelectedTab;

            if (activeTab != null)
            {
                // 2. Đưa pnlInput vào trong tab đang hiện hữu
                pnlInput.Parent = activeTab;
                pnlInput.BringToFront();

                // 3. Hiển thị và xóa dữ liệu cũ
                pnlInput.Visible = true;
                txtTenDanhMuc.Clear();

                // Căn giữa panel trong tab
                pnlInput.Left = (activeTab.Width - pnlInput.Width) / 2;
                pnlInput.Top = (activeTab.Height - pnlInput.Height) / 2;

                // 4. Tự động chọn loại trong ComboBox dựa vào tab
                if (activeTab.Name == "tabChiTieu")
                    cmbLoai.Text = "Chi tiêu";
                else
                    cmbLoai.Text = "Thu nhập";

                txtTenDanhMuc.Focus();
            }
        }



    }
}