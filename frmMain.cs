using System;
using System.Windows.Forms;
using QuanLyChiTieu.DTO;
using OfficeOpenXml; // Thêm thư viện này để cấu hình bản quyền

namespace QuanLyChiTieu
{
    public partial class frmMain : Form
    {
        private NguoiDungDTO currentUser;

        public frmMain()
        {
            InitializeComponent();        }

        // Constructor overload để nhận thông tin người dùng từ form đăng nhập
        public frmMain(NguoiDungDTO user) : this()
        {
            currentUser = user;
            // TODO: Sử dụng currentUser.VaiTro hoặc currentUser.HoTen để phân quyền/hiển thị nếu cần
        }

        private Form currentFormChild;

        // Hàm mở Form con vào panel (Giữ nguyên của bạn)
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

            pnBody.Controls.Add(childForm);
            pnBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // Lúc vừa chạy phần mềm, tự động mở thẳng vào trang Tổng Quan
        private void frmMain_Load(object sender, EventArgs e)
        {
            OpenChildForm(new frmTongQuan());
        }

        // Nút mở trang Danh mục
        private void btnDanhMuc_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new frmDanhMuc());
        }

        // TÍNH NĂNG MỚI: BẠN NHỚ KÉO THÊM 1 NÚT BÊN TRÁI ĐẶT TÊN LÀ btnTongQuan NHÉ
        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTongQuan());
        }

        private void btnTongQuan_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new frmTongQuan());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmCaiDat());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe());
        }


        // Nút Giao dịch bây giờ sẽ dùng chung hàm OpenChildForm để đồng bộ
        private void btnGiaoDich_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmGiaoDich());
        }
    }
    }
