using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using QuanLyChiTieu.DTO;
namespace QuanLyChiTieu.DAL
{
    public class MucTieuDAL
    {
        public DataTable GetAll(int maNguoiDung)
            => DataProvider.ExecuteSP("SP_GetMucTieu",
                new SqlParameter[] { new SqlParameter("@MaNguoiDung", maNguoiDung) });

        public int Them(int maNguoiDung, string tenMucTieu, decimal soTienMucTieu, DateTime? han)
        {
            string sql = @"INSERT INTO MucTieu(MaNguoiDung,TenMucTieu,SoTienMucTieu,SoTienDaTichLuy,HanHoanThanh)
                           VALUES(@Ma,@Ten,@MucTieu,0,@Han)";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",      maNguoiDung),
                new SqlParameter("@Ten",     tenMucTieu),
                new SqlParameter("@MucTieu", soTienMucTieu),
                new SqlParameter("@Han",     (object)han ?? DBNull.Value)
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