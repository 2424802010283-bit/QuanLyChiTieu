using System;
using System.Windows.Forms;
using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu
{
    public partial class frmMucTieu : Form
    {
        private readonly MucTieuBLL _bll = new MucTieuBLL();
        private int maNguoiDungHienTai = 1; // Tạm thời

        public frmMucTieu()
        {
            InitializeComponent();
        }

        private void frmMucTieu_Load(object sender, EventArgs e)
        {
            // Tạo cột cho DataGridView
            dgvMucTieu.ColumnCount = 3;

            dgvMucTieu.Columns[0].Name = "Tên mục tiêu";
            dgvMucTieu.Columns[1].Name = "Số tiền cần đạt";
            dgvMucTieu.Columns[2].Name = "Hạn chót";

            dgvMucTieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Không cho sửa trực tiếp
            dgvMucTieu.ReadOnly = true;

            // Chọn full dòng
            dgvMucTieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tenMucTieu = txtTenMucTieu.Text.Trim();
            string soTien = txtSoTienMucTieu.Text.Trim();
            string hanChot = dtpHanChot.Value.ToShortDateString();

            // Kiểm tra rỗng
            if (tenMucTieu == "" || soTien == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số tiền có phải số không
            if (!double.TryParse(soTien, out double tien))
            {
                MessageBox.Show("Số tiền không hợp lệ!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtSoTienMucTieu.Focus();
                return;
            }

            // Thêm dữ liệu vào DataGridView
            dgvMucTieu.Rows.Add(
                tenMucTieu,
                tien.ToString("N0") + " VNĐ",
                hanChot
            );

            MessageBox.Show("Thêm mục tiêu thành công!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // Reset input
            txtTenMucTieu.Clear();
            txtSoTienMucTieu.Clear();
            dtpHanChot.Value = DateTime.Now;

            txtTenMucTieu.Focus();
        }
    }
}