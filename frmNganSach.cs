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
    public partial class frmNganSach : Form
    {
        public frmNganSach()
        {
            InitializeComponent();
            this.Load += frmNganSach_Load;
            btnLuu.Click += btnLuu_Click;
        }

        // Load Form
        private void frmNganSach_Load(object sender, EventArgs e)
        {
            // Tạo cột DataGridView
            dgvNganSach.ColumnCount = 5;

            dgvNganSach.Columns[0].Name = "Tháng";
            dgvNganSach.Columns[1].Name = "Loại";
            dgvNganSach.Columns[2].Name = "Ngân sách";
            dgvNganSach.Columns[3].Name = "Đã dùng";
            dgvNganSach.Columns[4].Name = "Còn lại";

            dgvNganSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvNganSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvNganSach.ReadOnly = true;

            // Mặc định progress
            progressBudget.Value = 0;

            lblTienDaDung.Text = "Chưa có dữ liệu ngân sách";
        }

        // Nút lưu ngân sách
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string thang =
                dtpThang.Value.Month.ToString("00") + "/" +
                dtpThang.Value.Year.ToString();

            string loai = cboLoai.Text.Trim();

            string nganSachText = txtNganSach.Text.Trim();

            // Kiểm tra rỗng
            if (loai == "" || nganSachText == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Kiểm tra số
            if (!decimal.TryParse(nganSachText, out decimal nganSach))
            {
                MessageBox.Show(
                    "Ngân sách phải là số!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtNganSach.Focus();
                return;
            }

            // Kiểm tra > 0
            if (nganSach <= 0)
            {
                MessageBox.Show(
                    "Ngân sách phải lớn hơn 0!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtNganSach.Focus();
                return;
            }

            // Demo dữ liệu đã dùng
            decimal daDung = nganSach * 60 / 100;

            decimal conLai = nganSach - daDung;

            // Add vào DataGridView
            dgvNganSach.Rows.Add(
                thang,
                loai,
                nganSach.ToString("N0") + " VNĐ",
                daDung.ToString("N0") + " VNĐ",
                conLai.ToString("N0") + " VNĐ"
            );

            // ProgressBar
            int phanTram = (int)((daDung / nganSach) * 100);

            progressBudget.Value = phanTram;

            lblTienDaDung.Text =
                "Đã dùng " +
                daDung.ToString("N0") +
                " / " +
                nganSach.ToString("N0") +
                " VNĐ";

            // Đổi màu progress
            if (phanTram < 50)
            {
                progressBudget.ProgressColor = System.Drawing.Color.DodgerBlue;
            }
            else if (phanTram < 80)
            {
                progressBudget.ProgressColor = System.Drawing.Color.Orange;
            }
            else
            {
                progressBudget.ProgressColor = System.Drawing.Color.Crimson;
            }

            MessageBox.Show(
                "Lưu ngân sách thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Reset
            txtNganSach.Clear();

            cboLoai.SelectedIndex = -1;

            txtNganSach.Focus();
        }
    }
}