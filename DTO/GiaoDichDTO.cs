using System;
using System.Data;

namespace QuanLyChiTieu.DTO
{
    public class GiaoDichDTO
    {
        public int MaGiaoDich { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaTaiKhoan { get; set; }
        public int? MaTaiKhoanNhan { get; set; }
        public int? MaDanhMuc { get; set; }
        public string LoaiGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public string MoTa { get; set; }
        public string TenDanhMuc { get; set; }
        public string TenTaiKhoan { get; set; }
        public string TenTaiKhoanNhan { get; set; }

    }
}