using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
namespace QuanLyChiTieu
{
    public partial class frmDangKy : Form
    {
        private readonly NguoiDungBLL _bll = new NguoiDungBLL();

        public frmDangKy()
        {
            InitializeComponent();
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            // Ẩn mật khẩu mặc định
            txtPassword.UseSystemPasswordChar = true;
            txtPasswordnhaplai.UseSystemPasswordChar = true;
        }

        // Nút Đăng Ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            NguoiDungDTO ndNew = new NguoiDungDTO
            {
                HoTen = txtTen.Text.Trim(),
                EmailOrSDT = txtEmailorsdt.Text.Trim(),
                MatKhau = txtPassword.Text.Trim()
            };

            string matKhauNhapLai = txtPasswordnhaplai.Text.Trim();
            bool dongY = ckdongy.Checked;

            // Gọi BLL xử lý
            string ketQua = _bll.DangKy(ndNew, matKhauNhapLai, dongY);

            if (ketQua == "OK")
            {
                MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở lại trang Login
                frmlogin fLogin = new frmlogin();
                this.Hide();
                fLogin.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show(ketQua, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Label chuyển sang trang đăng nhập (Đã có tài khoản? Đăng nhập ngay)
        private void lblDangnhapngay_Click(object sender, EventArgs e)
        {
            frmlogin fLogin = new frmlogin();
            this.Hide();
            fLogin.ShowDialog();
            this.Close();
        }
    }
}