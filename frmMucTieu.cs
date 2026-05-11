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
            LoadData();
        }

        private void LoadData()
        {
            // Đổ dữ liệu vào DataGridView (dgvMucTieu)
            dgvMucTieu.DataSource = _bll.LayDanhSach(maNguoiDungHienTai);

            // Định dạng lại các cột tiền cho đẹp
            if (dgvMucTieu.Columns.Contains("SoTienCanDat"))
                dgvMucTieu.Columns["SoTienCanDat"].DefaultCellStyle.Format = "N0";
            if (dgvMucTieu.Columns.Contains("SoTienHienCo"))
                dgvMucTieu.Columns["SoTienHienCo"].DefaultCellStyle.Format = "N0";
        }

        // Nút "Thêm mục tiêu" (Nút xanh đậm trong ảnh của bạn)
        private void btnThemMucTieu_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Thu thập dữ liệu từ 3 ô nhập
                MucTieuDTO mt = new MucTieuDTO
                {
                    TenMucTieu = txtTenMucTieu.Text.Trim(),
                    SoTienCanDat = decimal.Parse(txtSoTienMucTieu.Text),
                    HanChot = dtpHanChot.Value,
                    SoTienHienCo = 0, // Mới tạo thì hiện có bằng 0
                    MaNguoiDung = maNguoiDungHienTai
                };

                // 2. Lưu vào DB thông qua BLL
                if (_bll.Them(mt))
                {
                    MessageBox.Show("Đã thêm mục tiêu mới! Chúc bạn sớm hoàn thành.");
                    LoadData(); // Tải lại bảng
                    ClearInput(); // Xóa sạch ô nhập để nhập cái mới
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng kiểm tra lại số tiền nhập vào!");
            }
        }

        private void ClearInput()
        {
            txtTenMucTieu.Clear();
            txtSoTienMucTieu.Clear();
            dtpHanChot.Value = DateTime.Now;
        }
    }
}