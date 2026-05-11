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
            var dsFull = bllDM.LayDanhSachDanhMuc();

            // Phải khớp từng chữ, từng dấu với Items trong ComboBox và Database
            dgvdmchi.DataSource = dsFull.Where(x => x.LoaiGiaoDich == "Chi tiêu").ToList();
            dgvdmthu.DataSource = dsFull.Where(x => x.LoaiGiaoDich == "Thu nhập").ToList();
        }

        // --- NÚT THÊM DANH MỤC (Nút to ở ngoài) ---
        

        // --- NÚT LƯU (Trong panel) ---
        private void btnLuu_Click(object sender, EventArgs e)
        {// 1. Kiểm tra Tên danh mục
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!");
                return;
            }

            DanhMucDTO dm = new DanhMucDTO
            {
                TenDanhMuc = txtTenDanhMuc.Text.Trim(),
                LoaiGiaoDich = cmbLoai.Text, // PHẢI gán vào LoaiGiaoDich để trùng với cột DB
                Loai = cmbLoai.Text,         // Gán thêm Loai để phục vụ việc lọc dsFull.Where
                MaNguoiDung = Session.MaNguoiDung
            };

            if (bllDM.LuuDanhMuc(dm))
            {
                MessageBox.Show("Lưu thành công!");
                pnlInput.Visible = false;
                LoadData(); // Nạp lại bảng
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
                pnlInput.Parent = activeTab;
                pnlInput.Visible = true;

                // QUAN TRỌNG: Đẩy panel lên lớp trên cùng để nhận được chuột
                pnlInput.BringToFront();

                // Đảm bảo panel không bị ẩn bởi các control khác
                pnlInput.Focus();

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