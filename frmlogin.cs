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
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
    
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            NguoiDungBLL bll = new NguoiDungBLL();
            NguoiDungDTO user = bll.DangNhap(txtTenDN.Text, txtMatKhau.Text);

            if (user != null)
            {
                MessageBox.Show("Chào " + user.HoTen );
                this.Hide();
                frmMain main = new frmMain(user); // Truyền user sang Form chính để phân quyền
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

    }

}
