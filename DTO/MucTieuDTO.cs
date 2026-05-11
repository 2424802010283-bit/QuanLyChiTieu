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
        public string TenMucTieu { get; set; }
        public decimal SoTienCanDat { get; set; }
        public decimal SoTienHienCo { get; set; }
        public DateTime HanChot { get; set; }
        public int MaNguoiDung { get; set; }

        // Cột tự tính % tiến độ
        public string TienDo
        {
            get
            {
                if (SoTienCanDat <= 0) return "0%";
                double phanTram = (double)(SoTienHienCo / SoTienCanDat) * 100;
                return $"{Math.Min(phanTram, 100):N1}%";
            }
        }

        // Cột tự xác định trạng thái
        public string TrangThai
        {
            get
            {
                if (SoTienHienCo >= SoTienCanDat) return "Hoàn thành";
                if (DateTime.Now > HanChot) return "Quá hạn";
                return "Đang thực hiện";
            }
        }
    }

        }
