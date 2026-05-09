using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyChiTieu.BLL;
using OfficeOpenXml;

namespace QuanLyChiTieu
{
    public partial class frmGiaoDich : Form
    {
        GiaoDichBLL bll = new GiaoDichBLL();

        public frmGiaoDich()
        {
            InitializeComponent();
        }

        private void frmGiaoDich_Load(object sender, EventArgs e)
        {
          

            LoadData();
        }

        // Load dữ liệu lên DataGridView
        private void LoadData()
        {
            try
            {
                guna2DataGridView1.DataSource = bll.LayLichSuGiaoDich();
                long tongChi = bll.LayTongChiTieu();
                // Nếu có label hiện tổng: lblTongChi.Text = tongChi.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- NÚT THÊM (Smart Input) ---
        // ✅ FIX: Dùng guna2TextBox1 CHỈ để nhập Smart Input
        //         guna2TextBox2 (ô tìm kiếm riêng) dùng để lọc
        private void btnthem_Click(object sender, EventArgs e)
        {
            string input = guna2TextBox1.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Vui lòng nhập giao dịch, ví dụ: 'Ăn sáng 30k'");
                return;
            }

            var result = bll.PhanTichChuoi(input);

            if (result.soTien > 0)
            {
                if (bll.LuuGiaoDich(result.soTien, result.ghiChu))
                {
                    MessageBox.Show($"Đã thêm: {result.ghiChu} - {result.soTien:N0} đ", "Thành công");
                    guna2TextBox1.Clear();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại, kiểm tra kết nối database!");
                }
            }
            else
            {
                MessageBox.Show("Không nhận ra số tiền.\nVui lòng nhập đúng định dạng: 'Ăn sáng 30k' hoặc '150k cafe'");
            }
        }

        // --- XUẤT EXCEL ---
        private void btnxuat_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = "BaoCaoChiTieu_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage p = new ExcelPackage())
                        {
                            var ws = p.Workbook.Worksheets.Add("ChiTieu");

                            // Header
                            for (int i = 0; i < guna2DataGridView1.Columns.Count; i++)
                            {
                                ws.Cells[1, i + 1].Value = guna2DataGridView1.Columns[i].HeaderText;
                                ws.Cells[1, i + 1].Style.Font.Bold = true;
                                ws.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                ws.Cells[1, i + 1].Style.Fill.SetBackground(Color.LightBlue);
                            }

                            // Dữ liệu
                            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                                for (int j = 0; j < guna2DataGridView1.Columns.Count; j++)
                                    ws.Cells[i + 2, j + 1].Value = guna2DataGridView1.Rows[i].Cells[j].Value?.ToString();

                            ws.Cells.AutoFitColumns();
                            File.WriteAllBytes(sfd.FileName, p.GetAsByteArray());
                            MessageBox.Show("Xuất file Excel thành công!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất file: " + ex.Message);
                    }
                }
            }
        }

        // --- SỬA ---
        private void btnsua_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang cập nhật...");
        }

        // --- XÓA ---
        private void btnxoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang cập nhật...");
        }

        // ✅ FIX: Dùng lại guna2TextBox1 (không cần TextBox2 riêng)
        private void ThucHienLoc()
        {
            string tuKhoa = guna2TextBox1.Text;
        }

        // Sự kiện gõ vào ô tìm kiếm
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ThucHienLoc();
        }

        // Sự kiện chọn loại trong ComboBox
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThucHienLoc();
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}