using System;
using System.Data;
using QuanLyChiTieu.DAL;

namespace QuanLyChiTieu.BLL
{
    public class GiaoDichBLL
    {
        // Khởi tạo đối tượng DAL để tương tác với Database
        GiaoDichDAL dal = new GiaoDichDAL();

        // 1. Tương đương với: Dữ liệu được truy xuất an toàn thông qua lớp BLL (Slide)
        public DataTable LayLichSuGiaoDich()
        {
            // Gọi đúng tên hàm bạn đã viết bên GiaoDichDAL.cs
            return dal.LayDanhSachGiaoDich();
        }

        // 2. Lấy tổng tiền chi tiêu trong tháng
        public long LayTongChiTieu()
        {
            return dal.LayTongChiTieuThangNay();
        }

        // 3. Lưu giao dịch xuống Database
        public bool LuuGiaoDich(long soTien, string ghiChu)
        {
            return dal.ThemGiaoDich(soTien, ghiChu);
        }

        // 4. TÍNH NĂNG SMART INPUT (Thuật toán phân tích văn bản tự nhiên)
        // Trả về Tuple gồm số tiền và chuỗi ghi chú
        public (long soTien, string ghiChu) PhanTichChuoi(string input)
        {
            long soTien = 0;
            string ghiChu = input.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return (0, "");

            try
            {
                // Cắt chuỗi người dùng nhập bằng dấu cách
                string[] parts = input.Split(' ');
                foreach (string part in parts)
                {
                    // Xử lý các từ viết tắt phổ biến: k = ngàn, tr = triệu
                    string tempPart = part.ToLower().Replace("k", "000").Replace("tr", "000000");

                    // Nếu cụm từ này là số tiền (vd: 150000)
                    if (long.TryParse(tempPart, out long parsedTien))
                    {
                        soTien = parsedTien;
                        // Ghi chú sẽ là phần chữ còn lại sau khi bỏ đi số tiền
                        ghiChu = input.Replace(part, "").Trim();
                        break; // Đã tìm thấy số tiền thì dừng vòng lặp
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi nếu nhập sai định dạng phức tạp
            }

            return (soTien, ghiChu);
        }
    }
}