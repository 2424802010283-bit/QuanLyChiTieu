using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    public partial class frmlogin : Form
    {
        private readonly NguoiDungBLL _bll = new NguoiDungBLL();

        public frmlogin()
        {
            InitializeComponent();

            // Password dạng ****
            txtPassword.UseSystemPasswordChar = true;

            // Gọi hiệu ứng
            ThietKeButton();
            HieuUngHover();
        }

        // ================= THIẾT KẾ NÚT =================
        private void ThietKeButton()
        {
            GraphicsPath path = new GraphicsPath();

            int radius = 25;

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btnDangNhap.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btnDangNhap.Width - radius, btnDangNhap.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btnDangNhap.Height - radius, radius, radius, 90, 90);

            path.CloseFigure();

            btnDangNhap.Region = new Region(path);


            btnDangNhap.BackColor = Color.FromArgb(45, 25, 160);
            btnDangNhap.ForeColor = Color.White;

            btnDangNhap.Cursor = Cursors.Hand;
        }

        // ================= HIỆU ỨNG HOVER =================
        private void HieuUngHover()
        {
            // Hover nút đăng nhập
            btnDangNhap.MouseEnter += (s, e) =>
            {
                btnDangNhap.BackColor = Color.FromArgb(110, 90, 255);

                btnDangNhap.Location = new Point(
                    btnDangNhap.Location.X,
                    btnDangNhap.Location.Y - 2
                );
            };

            btnDangNhap.MouseLeave += (s, e) =>
            {
                btnDangNhap.BackColor = Color.FromArgb(45, 25, 160);

                btnDangNhap.Location = new Point(
                    btnDangNhap.Location.X,
                    btnDangNhap.Location.Y + 2
                );
            };

            // Hover textbox email
            txtEmail.Enter += (s, e) =>
            {
                txtEmail.BackColor = Color.White;
            };

            txtEmail.Leave += (s, e) =>
            {
                txtEmail.BackColor = Color.FromArgb(245, 245, 245);
            };

            // Hover textbox password
            txtPassword.Enter += (s, e) =>
            {
                txtPassword.BackColor = Color.White;
            };

            txtPassword.Leave += (s, e) =>
            {
                txtPassword.BackColor = Color.FromArgb(245, 245, 245);
            };

            // Hover link đăng ký
            lnkDangKy.MouseEnter += (s, e) =>
            {
                lnkDangKy.LinkColor = Color.DeepPink;

                lnkDangKy.Font = new Font(
                    lnkDangKy.Font,
                    FontStyle.Bold | FontStyle.Underline
                );
            };

            lnkDangKy.MouseLeave += (s, e) =>
            {
                lnkDangKy.LinkColor = Color.RoyalBlue;

                lnkDangKy.Font = new Font(
                    lnkDangKy.Font,
                    FontStyle.Regular
                );
            };

            // Hover quên mật khẩu
            lnkQuenMK.MouseEnter += (s, e) =>
            {
                lnkQuenMK.LinkColor = Color.MediumPurple;
            };

            lnkQuenMK.MouseLeave += (s, e) =>
            {
                lnkQuenMK.LinkColor = Color.Gray;
            };
        }

        // ================= ĐĂNG NHẬP =================
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string matKhau = txtPassword.Text.Trim();

            // Kiểm tra rỗng
            if (email == "" || matKhau == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Regex email
            string patternEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, patternEmail))
            {
                MessageBox.Show(
                    "Vui lòng nhập Email hợp lệ!",
                    "Sai định dạng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtEmail.Focus();

                return;
            }

            // Kiểm tra đăng nhập
            NguoiDungDTO user = _bll.DangNhap(email, matKhau);

            if (user != null)
            {
                Session.MaNguoiDung = user.MaNguoiDung;
                Session.HoTen = user.HoTen;

                MessageBox.Show(
                    $"Chào mừng {user.HoTen} đã trở lại ✨",
                    "Đăng nhập thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                frmMain fMain = new frmMain(user);

                this.Hide();

                fMain.Show();
            }
            else
            {
                MessageBox.Show(
                    "Email hoặc mật khẩu không chính xác!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= HIỆN / ẨN PASSWORD =================
        

        // ================= LINK ĐĂNG KÝ =================
        private void lnkDangKy_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e
        )
        {
            frmDangKy f = new frmDangKy();

            this.Hide();

            f.ShowDialog();

            this.Show();
        }

        // ================= QUÊN MẬT KHẨU =================
        private void lnkQuenMK_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e
        )
        {
            MessageBox.Show(
                "Chức năng đang phát triển 💜",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar =
                !txtPassword.UseSystemPasswordChar;
        }

        private void pnlLoginContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}