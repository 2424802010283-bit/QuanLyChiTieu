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
using System.Text.RegularExpressions; // Nhớ thêm dòng này ở trên cùng
namespace QuanLyChiTieu
{
    public partial class frmlogin : Form
    {
        private readonly NguoiDungBLL _bll = new NguoiDungBLL();
        public frmlogin()
        {
            InitializeComponent();
    
        }
        // Nút Đăng Nhập
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtEmailorsdt.Text.Trim(); // Bạn có thể đổi tên control này thành txtEmail cho đúng nghĩa
            string matKhau = txtPassword.Text.Trim();

            // Regex chỉ dành cho Email
            string patternEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, patternEmail))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ Email hợp lệ!", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailorsdt.Focus();
                return;
            }

            NguoiDungDTO user = _bll.DangNhap(email, matKhau);

            if (user != null)
            {
                Session.MaNguoiDung = user.MaNguoiDung;
                Session.HoTen = user.HoTen;

                MessageBox.Show($"Chào mừng {user.HoTen} đã trở lại!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMain fMain = new frmMain(user);
                this.Hide();
                fMain.Show();
            }
            else
            {
                MessageBox.Show("Email hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }


