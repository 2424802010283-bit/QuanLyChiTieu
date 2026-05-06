using QuanLyChiTieu.DTO;
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

        // Trả về danh sách tất cả giao dịch (dùng bởi BLL)
        public DataTable LayDanhSachGiaoDich()
        {
            string query = "SELECT * FROM GiaoDich ORDER BY NgayGD DESC";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // Trả về tổng chi tiêu trong tháng hiện tại (dùng bởi BLL)
        public long LayTongChiTieuThangNay()
        {
            string query = "SELECT SUM(SoTien) FROM GiaoDich WHERE MONTH(NgayGD) = MONTH(GETDATE()) AND YEAR(NgayGD) = YEAR(GETDATE())";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value && dt.Rows[0][0].ToString() != "")
            {
                try
                {
                    return Convert.ToInt64(dt.Rows[0][0]);
                }
                catch
                {
                    // Nếu DB trả về số thập phân, chuyển về long an toàn
                    return Convert.ToInt64(Math.Round(Convert.ToDouble(dt.Rows[0][0])));
                }
            }
            return 0;
        }

        // Hỗ trợ BLL.ThemGiaoDich(soTien, ghiChu)
        // Lưu ý: vì chưa có thông tin danh mục trong Smart Input, đặt mặc định MaDanhMuc = 1
        public bool ThemGiaoDich(long soTien, string ghiChu)
        {
            GiaoDichDTO gd = new GiaoDichDTO()
            {
                SoTien = soTien,
                GhiChu = ghiChu,
                MaDanhMuc = 1 // mặc định nếu chưa chọn danh mục
            };

            return ThemGiaoDich(gd);
        }

        public bool ThemGiaoDich(GiaoDichDTO gd)
        {
            string query = "INSERT INTO GiaoDich (SoTien, GhiChu, MaDanhMuc, NgayGD) VALUES ( @SoTien , @GhiChu , @MaDanhMuc , GETDATE() )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { gd.SoTien, gd.GhiChu, gd.MaDanhMuc });
            return result > 0;
        }

        // Truy vấn tính tổng chi tiêu theo Danh Mục trong tháng hiện tại
        public decimal LayTongChiTheoDanhMucThangNay(int maDanhMuc)
        {
            string query = "SELECT SUM(SoTien) FROM GiaoDich WHERE MaDanhMuc = @MaDM AND MONTH(NgayGD) = MONTH(GETDATE()) AND YEAR(NgayGD) = YEAR(GETDATE())";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { maDanhMuc });
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
                return Convert.ToDecimal(dt.Rows[0][0]);
            return 0;
        }
    }
}