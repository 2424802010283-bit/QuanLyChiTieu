using QuanLyChiTieu.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    public partial class frmMain : Form
    {
        private NguoiDungDTO currentUser;
        private Form currentFormChild;

        public frmMain()
        {
            InitializeComponent();
        }

        // Nhận thông tin người dùng từ login
        public frmMain(NguoiDungDTO user) : this()
        {
            currentUser = user;
            if (currentUser != null)
            {
                txtHoTen.Text = currentUser.HoTen;
                txtEmail.Text = currentUser.Email;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnBody.Controls.Add(childForm); // pnBody là cái panel trắng bóc của bạn
            pnBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Hiển thị tên và email người dùng đăng nhập (nếu có)
            if (currentUser != null)
            {
                txtHoTen.Text = currentUser.HoTen;
                txtEmail.Text = currentUser.Email; // Hoặc currentUser.EmailOrSDT tùy DTO của bạn
            }

            // GỌI DÒNG NÀY ĐỂ HIỆN TRANG TỔNG QUAN NGAY LẬP TỨC
            OpenChildForm(new frmTongQuan());

            // (Tùy chọn) Đổi màu nút Dashboard để người dùng biết đang ở trang này
            btnDashboard.Checked = true;
        }

        // --- CÁC SỰ KIỆN CLICK NÚT TRÊN SIDEBAR ---

        // Mở Dashboard (Tổng quan)
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTongQuan());
        }

        // Mở Giao Dịch
        private void btnGiaoDich_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmGiaoDich());
        }

        // Mở Thu Nhập
        private void btnThuNhap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThuNhap());
        }

        // Mở Chi Tiêu
        private void btnChiTieu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmChiTieu());
        }

        // Mở Ngân Sách
        private void btnNganSach_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNganSach());
        }

        // Mở Mục Tiêu
        private void btnMucTieu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmMucTieu());
        }

        // Mở Danh Mục (Nơi có cái Panel thêm danh mục bạn cần)
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDanhMuc());
        }

        // Mở Báo Cáo
        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmBaoCao());
        }

        // Mở Thống Kê
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // 1. Xóa thông tin phiên làm việc
                Session.MaNguoiDung = 0;
                Session.HoTen = null;

                // 2. Tìm lại form Login đã bị ẩn và hiện nó lên
                Form loginForm = null;
                foreach (Form f in Application.OpenForms)
                {
                    if (f is frmlogin)
                    {
                        loginForm = f;
                        break;
                    }
                }

                if (loginForm != null)
                {
                    loginForm.Show(); // Hiện lại form đăng nhập
                    this.Dispose();   // Giải phóng bộ nhớ form Main (không dùng this.Close() vì có thể làm tắt app)
                }
                else
                {
                    // Trường hợp không tìm thấy (hiếm gặp), khởi tạo mới
                    frmlogin newLogin = new frmlogin();
                    newLogin.Show();
                    this.Dispose();
                }
            }
        }
    }
}