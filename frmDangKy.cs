using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QuanLyChiTieu
{
    public partial class frmDangKy : Form
    {
        private readonly NguoiDungBLL _bll = new NguoiDungBLL();

        public frmDangKy()
        {
            InitializeComponent();
            btnDangKy.Click += btnDangKy_Click;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        }

        // Hàm đăng ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtTen.Text.Trim();
            string emailSdt = txtEmailorsdt.Text.Trim();
            string matKhau = txtPassword.Text;
            string nhapLaiMK = txtPasswordnhaplai.Text;

            // Kiểm tra rỗng
            if (hoTen == "" || emailSdt == "" || matKhau == "" || nhapLaiMK == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra email
            if (!KiemTraEmail(emailSdt))
            {
                MessageBox.Show("Email không hợp lệ!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtEmailorsdt.Focus();
                return;
            }

            // Kiểm tra mật khẩu
            if (matKhau.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            // Kiểm tra nhập lại mật khẩu
            if (matKhau != nhapLaiMK)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtPasswordnhaplai.Focus();
                return;
            }

            // Kiểm tra đồng ý điều khoản
            if (!ckdongy.Checked)
            {
                MessageBox.Show("Bạn phải đồng ý điều khoản sử dụng!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Thành công
            NguoiDungDTO ndNew = new NguoiDungDTO
            {
                HoTen = hoTen,
                EmailOrSDT = emailSdt,
                MatKhau = matKhau
            };

            // Gọi BLL xử lý đăng ký
            string ketQua = _bll.DangKy(ndNew, nhapLaiMK, ckdongy.Checked);

            if (ketQua == "OK")
            {
                MessageBox.Show("Đăng ký thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                frmlogin frm = new frmlogin();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(ketQua,
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // Hàm kiểm tra email
        private bool KiemTraEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        // Link chuyển sang form đăng nhập
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmlogin frm = new frmlogin();
            frm.Show();
            this.Hide();
        }
    }
}