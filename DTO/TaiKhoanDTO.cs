using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChiTieu.DTO
{
    public class TaiKhoanDTO
    {
        public int MaTaiKhoan { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenTaiKhoan { get; set; }
        public string LoaiTaiKhoan { get; set; }
        public decimal SoDu { get; set; }
        public string GhiChu { get; set; }
    }
}

