using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu.DAL
{
    public class MucTieuDAL
    {
        // 1. Lấy dữ liệu từ Database lên
        public DataTable GetMucTieu(int maNguoiDung)
        {
            string sql = "SELECT * FROM MucTieu WHERE MaNguoiDung = @ma";
            return DataProvider.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@ma", maNguoiDung) });
        }

        // 2. Thêm mục tiêu mới
        public int Them(MucTieuDTO mt)
        {
            string sql = @"INSERT INTO MucTieu(MaNguoiDung, TenMucTieu, SoTienMucTieu, HanHoanThanh) 
                           VALUES(@MaND, @Ten, @SoTien, @HanChot)";
            return DataProvider.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@MaND", mt.MaNguoiDung),
                new SqlParameter("@Ten", mt.TenMucTieu),
                new SqlParameter("@SoTien", mt.SoTienCanDat),
                new SqlParameter("@HanChot", mt.HanChot)
            });
        }

        // 3. Cập nhật mục tiêu (Sửa)
        public int Sua(MucTieuDTO mt)
        {
            string sql = @"UPDATE MucTieu 
                           SET TenMucTieu = @Ten, SoTienMucTieu = @SoTien, HanHoanThanh = @HanChot 
                           WHERE MaMucTieu = @MaMT";
            return DataProvider.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@Ten", mt.TenMucTieu),
                new SqlParameter("@SoTien", mt.SoTienCanDat),
                new SqlParameter("@HanChot", mt.HanChot),
                new SqlParameter("@MaMT", mt.MaMucTieu)
            });
        }

        // 4. Xóa mục tiêu
        public int Xoa(int maMucTieu)
        {
            return DataProvider.ExecuteNonQuery(
                "DELETE FROM MucTieu WHERE MaMucTieu=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maMucTieu) });
        }

        // 5. Cập nhật số tiền đã tích lũy (Dành cho sau này làm chức năng nạp tiền vào mục tiêu)
        public int CapNhatTichLuy(int maMucTieu, decimal soTienThem)
        {
            return DataProvider.ExecuteNonQuery(
                "UPDATE MucTieu SET SoTienDaTichLuy = SoTienDaTichLuy + @SoTien WHERE MaMucTieu = @Ma",
                new SqlParameter[] {
                    new SqlParameter("@SoTien", soTienThem),
                    new SqlParameter("@Ma",     maMucTieu)
                });
        }

        // 6. Cập nhật trạng thái mục tiêu (Đang thực hiện -> Hoàn thành)
        public int CapNhatTrangThai(int maMucTieu, string trangThai)
        {
            return DataProvider.ExecuteNonQuery(
                "UPDATE MucTieu SET TrangThai = @TrangThai WHERE MaMucTieu = @Ma",
                new SqlParameter[] {
                    new SqlParameter("@TrangThai", trangThai),
                    new SqlParameter("@Ma",        maMucTieu)
                });
        }
    }
}