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

        // Khi vừa mở tab Cài đặt: Tự động hiện con số cũ đang có
        private void frmCaiDat_Load(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                txtNganSach.Text = File.ReadAllText(path);
            }
            else
            {
                txtNganSach.Text = "3000000"; // Mặc định nếu chưa có file
            }
        }

        // Khi bấm nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (long.TryParse(txtNganSach.Text, out long soTien))
            {
                try
                {
                    File.WriteAllText(path, soTien.ToString());
                    MessageBox.Show("Đã lưu thiết lập ngân sách mới!", "Thành công");
                    // Nếu Form Tổng Quan đang mở thì cập nhật UI ngay
                    var frm = Application.OpenForms.OfType<frmTongQuan>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.RefreshFromSettings();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chỉ nhập số nguyên (Ví dụ: 5000000)");
            }
        }
    }
}
