using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChiTieu.DTO
{
    public class MucTieuDTO
    {
        public int MaMucTieu { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenMucTieu { get; set; }
        public decimal SoTienMucTieu { get; set; }
        public decimal SoTienDaTichLuy { get; set; }
        public DateTime? HanHoanThanh { get; set; }
        public string TrangThai { get; set; }
        public int PhanTram { get; set; }
    }

}
