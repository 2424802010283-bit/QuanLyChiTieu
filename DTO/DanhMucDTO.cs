    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace QuanLyChiTieu.DTO
    {
        public class DanhMucDTO
        {
        public int MaDanhMuc { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenDanhMuc { get; set; }
        public string LoaiGiaoDich { get; set; }
        public string MauSac { get; set; }
        public string GhiChu { get; set; }
        // ĐÂY LÀ DÒNG BỊ THIẾU:
        public string Loai { get; set; }
        // Nếu bạn có thêm icon hoặc màu sắc thì thêm ở đây
        public string BieuTuong { get; set; }


    }
    }
