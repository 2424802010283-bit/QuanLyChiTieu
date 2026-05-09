using System.Data;
using QuanLyChiTieu.DAL;

namespace QuanLyChiTieu.BLL
{
    public class GiaoDichBLL
    {
        // ✅ FIX: Dùng Singleton thay vì new GiaoDichDAL()
        private GiaoDichDAL dal = GiaoDichDAL.Instance;

        // Lấy lịch sử giao dịch
        public DataTable LayLichSuGiaoDich()
        {
            return dal.LayDanhSachGiaoDich();
        }

        // Lấy tổng chi tiêu tháng hiện tại
        public long LayTongChiTieu()
        {
            return dal.LayTongChiTieuThangNay();
        }

        // Lưu giao dịch mới
        public bool LuuGiaoDich(long soTien, string ghiChu)
        {
            return dal.ThemGiaoDich(soTien, ghiChu);
        }

        // Lọc / tìm kiếm giao dịch
        public DataTable LocDuLieu(string search, string loai)
        {
            return dal.TimKiemGiaoDich(search, loai);
        }

        // ✅ FIX: Xử lý "tr" trước "k" để tránh phá chuỗi
        // Ví dụ: "30tr an toi" → soTien=30000000, ghiChu="an toi"
        //        "150k cafe"   → soTien=150000,   ghiChu="cafe"
        public (long soTien, string ghiChu) PhanTichChuoi(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (0, "");

            long soTien = 0;
            string ghiChu = input.Trim();

            try
            {
                string[] parts = input.Trim().Split(' ');
                foreach (string part in parts)
                {
                    // ✅ FIX: Replace "tr" trước để không bị "k" phá chuỗi "tr" → "t000"
                    string temp = part.ToLower();
                    temp = temp.Replace("tr", "000000").Replace("k", "000");

                    if (long.TryParse(temp, out long parsed))
                    {
                        soTien = parsed;
                        ghiChu = input.Replace(part, "").Trim();
                        break;
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi định dạng không mong muốn
            }

            return (soTien, ghiChu);
        }
    }
}