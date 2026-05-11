using System;
using System.Data;

namespace QuanLyChiTieu.DAL
{
    public class GiaoDichDAL
    {
        private static GiaoDichDAL instance;
        public static GiaoDichDAL Instance
        {
            get { if (instance == null) instance = new GiaoDichDAL(); return instance; }
        }

        // Lấy toàn bộ danh sách giao dịch
        public DataTable LayDanhSachGiaoDich()
        {
            string query = "SELECT * FROM GiaoDich ORDER BY NgayGD DESC";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // Lấy tổng chi tiêu tháng hiện tại
        public long LayTongChiTieuThangNay()
        {
            string query = "SELECT SUM(SoTien) FROM GiaoDich WHERE MONTH(NgayGD) = MONTH(GETDATE()) AND YEAR(NgayGD) = YEAR(GETDATE())";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            // ✅ FIX: Kiểm tra DBNull đúng cách
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                try
                {
                    return Convert.ToInt64(Math.Round(Convert.ToDouble(dt.Rows[0][0])));
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        // Thêm giao dịch nhanh (Smart Input - chỉ cần số tiền và ghi chú)
        public bool ThemGiaoDich(long soTien, string ghiChu)
        {
            GiaoDichDTO gd = new GiaoDichDTO()
            {
                SoTien = soTien,
                GhiChu = ghiChu,
                MaDanhMuc = 1 // Mặc định nếu chưa chọn danh mục
            };
            return ThemGiaoDich(gd);
        }

        // Thêm giao dịch đầy đủ thông tin
        public bool ThemGiaoDich(GiaoDichDTO gd)
        {
            string query = "INSERT INTO GiaoDich (SoTien, GhiChu, MaDanhMuc, NgayGD) VALUES (@SoTien, @GhiChu, @MaDanhMuc, GETDATE())";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { gd.SoTien, gd.GhiChu, gd.MaDanhMuc });
            return result > 0;
        }

        // Tổng chi theo danh mục trong tháng
        public decimal LayTongChiTheoDanhMucThangNay(int maDanhMuc)
        {
            string query = "SELECT SUM(SoTien) FROM GiaoDich WHERE MaDanhMuc = @MaDM AND MONTH(NgayGD) = MONTH(GETDATE()) AND YEAR(NgayGD) = YEAR(GETDATE())";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { maDanhMuc });

            // ✅ FIX: Kiểm tra DBNull đúng cách
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                return Convert.ToDecimal(dt.Rows[0][0]);
            return 0;
        }

        // ✅ FIX: Sửa bug tham số thừa khi loai = "Tất cả"
        public DataTable TimKiemGiaoDich(string keywords, string loai)
        {
            bool coLoc = !string.IsNullOrEmpty(loai) && loai != "Tất cả";

            string query = "SELECT * FROM GiaoDich WHERE (GhiChu LIKE @key OR CAST(SoTien AS NVARCHAR) LIKE @key)";
            if (coLoc)
                query += " AND LoaiGiaoDich = @loai";

            // ✅ Chỉ truyền @loai vào parameter khi câu SQL thực sự dùng @loai
            object[] parameter = coLoc
                ? new object[] { "%" + keywords + "%", loai }
                : new object[] { "%" + keywords + "%" };

            return DataProvider.Instance.ExecuteQuery(query, parameter);
        }
    }
}