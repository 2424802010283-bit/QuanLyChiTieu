using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using QuanLyChiTieu.DTO;
namespace QuanLyChiTieu.DAL
{
    public class MucTieuDAL
    {
        // Sử dụng hàm GetMucTieu này thay vì GetAll
        public DataTable GetMucTieu(int maNguoiDung)
        {
            string sql = "SELECT * FROM MucTieu WHERE MaNguoiDung = @ma";
            // Sửa db thành DataProvider
            return DataProvider.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@ma", maNguoiDung) });
        }

        // Sửa hàm Them để nhận DTO giúp BLL gọi dễ hơn
        public int Them(MucTieuDTO mt)
        {
            string sql = @"INSERT INTO MucTieu(MaNguoiDung, TenMucTieu, SoTienMucTieu, SoTienDaTichLuy, HanHoanThanh)
                       VALUES(@Ma, @Ten, @MucTieu, 0, @Han)";
            var pms = new SqlParameter[] {
            new SqlParameter("@Ma",      mt.MaNguoiDung),
            new SqlParameter("@Ten",     mt.TenMucTieu),
            new SqlParameter("@MucTieu", mt.SoTienCanDat),
            new SqlParameter("@Han",     (object)mt.HanChot ?? DBNull.Value)
        };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }

        public int CapNhatTichLuy(int maMucTieu, decimal soTienThem)
        {
            return DataProvider.ExecuteNonQuery(
                "UPDATE MucTieu SET SoTienDaTichLuy=SoTienDaTichLuy+@SoTien WHERE MaMucTieu=@Ma",
                new SqlParameter[] {
                    new SqlParameter("@SoTien", soTienThem),
                    new SqlParameter("@Ma",     maMucTieu)
                });
        }

        public int CapNhatTrangThai(int maMucTieu, string trangThai)
        {
            return DataProvider.ExecuteNonQuery(
                "UPDATE MucTieu SET TrangThai=@TrangThai WHERE MaMucTieu=@Ma",
                new SqlParameter[] {
                    new SqlParameter("@TrangThai", trangThai),
                    new SqlParameter("@Ma",        maMucTieu)
                });
        }

        public int Xoa(int maMucTieu)
        {
            return DataProvider.ExecuteNonQuery(
                "DELETE FROM MucTieu WHERE MaMucTieu=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maMucTieu) });
        }
       
        }
    }
