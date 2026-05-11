using QuanLyChiTieu.BLL;
using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace QuanLyChiTieu
{
    public partial class frmDanhMuc : Form
    {
        private readonly DanhMucBLL bllDM = new DanhMucBLL();

        public frmDanhMuc()
        {
            InitializeComponent();
        }

        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            cboLoaiDanhMuc.Items.Add("Thu nhập");
            cboLoaiDanhMuc.Items.Add("Chi tiêu");

            cboLoaiDanhMuc.SelectedIndex = 0;

            dgvDanhMuc.Rows.Add("Ăn uống", "Chi tiêu");
            dgvDanhMuc.Rows.Add("Lương", "Thu nhập");
        }

        private void btnThemDanhMuc_Click(object sender, EventArgs e)
        {
            string tenDanhMuc = txtTenDanhMuc.Text.Trim();
            string loaiDanhMuc = cboLoaiDanhMuc.Text;

            if (string.IsNullOrEmpty(tenDanhMuc))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên danh mục!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtTenDanhMuc.Focus();
                return;
            }

            dgvDanhMuc.Rows.Add(tenDanhMuc, loaiDanhMuc);

            MessageBox.Show(
                "Thêm danh mục thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            txtTenDanhMuc.Clear();
            cboLoaiDanhMuc.SelectedIndex = 0;
            txtTenDanhMuc.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Đã lưu danh mục!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtTenDanhMuc.Clear();
            cboLoaiDanhMuc.SelectedIndex = 0;
        }
    }
}