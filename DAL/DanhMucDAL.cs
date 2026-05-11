using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu.DAL
{
    public class DanhMucDAL
    {
        // =========================================================
        // PHẦN 1: CÁC HÀM BỔ SUNG CHO KHỚP VỚI DanhMucBLL ĐANG GỌI

        public List<DanhMucDTO> LayDanhSachDanhMuc()
        {
            var list = new List<DanhMucDTO>();
            DataTable dt = DataProvider.ExecuteQuery("SELECT * FROM DanhMuc");
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
        }

        // Lấy danh mục theo User
        public List<DanhMucDTO> GetByUser(int maNguoiDung)
            {
            var list = new List<DanhMucDTO>();
            DataTable dt = DataProvider.ExecuteQuery(
                "SELECT * FROM DanhMuc WHERE MaNguoiDung=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maNguoiDung) });
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
                }

        // Lấy danh mục theo Loại (Thu/Chi)
        public List<DanhMucDTO> GetByLoai(int maNguoiDung, string loai)
                {
            var list = new List<DanhMucDTO>();
            DataTable dt = DataProvider.ExecuteQuery(
                "SELECT * FROM DanhMuc WHERE MaNguoiDung=@Ma AND LoaiGiaoDich=@Loai",
                new SqlParameter[] {
                    new SqlParameter("@Ma",   maNguoiDung),
                    new SqlParameter("@Loai", loai)
                });
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
        }
        public List<string> LayTenTatCaDanhMuc()
            {
            var list = new List<string>();
            DataTable dt = DataProvider.ExecuteQuery("SELECT TenDanhMuc FROM DanhMuc");
            foreach (DataRow r in dt.Rows)
                {
                list.Add(r["TenDanhMuc"].ToString());
                }
            }
            return list;
        }

        // Thêm danh mục
        public int Them(DanhMucDTO dm)
        {
            string sql = @"INSERT INTO DanhMuc(MaNguoiDung, TenDanhMuc, LoaiGiaoDich, MauSac, GhiChu)
                           VALUES(@Ma, @Ten, @Loai, @Mau, @GhiChu)";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",     dm.MaNguoiDung == 0 ? 1 : dm.MaNguoiDung), // Tạm fix User = 1 nếu quên truyền
                new SqlParameter("@Ten",    dm.TenDanhMuc),
                new SqlParameter("@Loai",   dm.LoaiGiaoDich),
                new SqlParameter("@Mau",    (object)dm.MauSac  ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)dm.GhiChu  ?? DBNull.Value)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
    }

        // Sửa danh mục
        public int Sua(DanhMucDTO dm)
        {
            string sql = @"UPDATE DanhMuc SET TenDanhMuc=@Ten, LoaiGiaoDich=@Loai,
                           MauSac=@Mau, GhiChu=@GhiChu WHERE MaDanhMuc=@MaDM";
            var pms = new SqlParameter[] {
                new SqlParameter("@MaDM",   dm.MaDanhMuc),
                new SqlParameter("@Ten",    dm.TenDanhMuc),
                new SqlParameter("@Loai",   dm.LoaiGiaoDich),
                new SqlParameter("@Mau",    (object)dm.MauSac  ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)dm.GhiChu  ?? DBNull.Value)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
}

        // Xóa danh mục
        public int Xoa(int maDanhMuc)
        {
            return DataProvider.ExecuteNonQuery(
                "DELETE FROM DanhMuc WHERE MaDanhMuc=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maDanhMuc) });
        }
    }
}