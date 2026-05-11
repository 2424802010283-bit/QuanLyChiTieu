using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChiTieu.DTO
{
    public class NganSachDTO
    {
        public int MaNganSach { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public decimal SoTienNganSach { get; set; }
        public decimal DaChi { get; set; }
        public int PhanTram { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}
