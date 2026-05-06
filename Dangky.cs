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
    public partial class Dangky : Form
    {
        public Dangky()
        {
            InitializeComponent();
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtPass.Text != txtConfirm.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            NguoiDungDTO nd = new NguoiDungDTO
            {
                TenDangNhap = txtUser.Text,
                MatKhau = txtPass.Text,
                HoTen = txtHoTen.Text,
                VaiTro = "User" // Đăng ký mới mặc định là User
            };

            NguoiDungBLL bll = new NguoiDungBLL();
            if (bll.DangKy(nd))
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Close();
            }
        }
    }
}
