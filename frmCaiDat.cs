using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QuanLyChiTieu
{
    public partial class frmCaiDat : Form
    {
        private string path = "ngansach.txt"; // Tên file lưu ngân sách

        public frmCaiDat()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string nganSach = txtNganSach.Text.Trim();

            // Kiểm tra rỗng
            if (nganSach == "")
            {
                MessageBox.Show("Vui lòng nhập ngân sách tháng!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtNganSach.Focus();
                return;
            }

            // Kiểm tra có phải số không
            if (!decimal.TryParse(nganSach, out decimal soTien))
            {
                MessageBox.Show("Ngân sách phải là số!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtNganSach.Focus();
                return;
            }

            // Kiểm tra số tiền hợp lệ
            if (soTien <= 0)
            {
                MessageBox.Show("Ngân sách phải lớn hơn 0!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtNganSach.Focus();
                return;
            }

            // Lưu ngân sách
            MessageBox.Show("Lưu cài đặt thành công!\nNgân sách tháng: "
                            + soTien.ToString("N0") + " VNĐ",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // Format lại textbox
            txtNganSach.Text = soTien.ToString("N0");
        }
    }
}
