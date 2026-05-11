using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // Đừng quên dòng này nhé!

using System.Windows.Forms;
using System.IO;
using QuanLyChiTieu.DTO;

namespace QuanLyChiTieu.DAL

{
    public class DanhMucDAL
    {
        public List<DanhMucDTO> GetByUser(int maNguoiDung)
        {
            var list = new List<DanhMucDTO>();
            DataTable dt = DataProvider.ExecuteQuery(
                "SELECT * FROM DanhMuc WHERE MaNguoiDung=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maNguoiDung) });
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
        }

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

        public int Them(DanhMucDTO dm)
        {
            string sql = @"INSERT INTO DanhMuc(MaNguoiDung, TenDanhMuc, LoaiGiaoDich, MauSac, GhiChu)
                   VALUES(@Ma, @Ten, @Loai, @Mau, @GhiChu)";
            var pms = new SqlParameter[] {
        new SqlParameter("@Ma",    dm.MaNguoiDung),
        new SqlParameter("@Ten",   dm.TenDanhMuc),
        new SqlParameter("@Loai",  dm.LoaiGiaoDich), // Dùng LoaiGiaoDich ở đây
        new SqlParameter("@Mau",   (object)dm.MauSac  ?? DBNull.Value),
        new SqlParameter("@GhiChu",(object)dm.GhiChu  ?? DBNull.Value)
    };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }

        public int Sua(DanhMucDTO dm)
        {
            string sql = @"UPDATE DanhMuc SET TenDanhMuc=@Ten, LoaiGiaoDich=@Loai,
                           MauSac=@Mau, GhiChu=@GhiChu WHERE MaDanhMuc=@MaDM";
            var pms = new SqlParameter[] {
                new SqlParameter("@MaDM",  dm.MaDanhMuc),
                new SqlParameter("@Ten",   dm.TenDanhMuc),
                new SqlParameter("@Loai",  dm.LoaiGiaoDich),
                new SqlParameter("@Mau",   (object)dm.MauSac  ?? DBNull.Value),
                new SqlParameter("@GhiChu",(object)dm.GhiChu  ?? DBNull.Value)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }

        public int Xoa(int maDanhMuc)
        {
            return DataProvider.ExecuteNonQuery(
                "DELETE FROM DanhMuc WHERE MaDanhMuc=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maDanhMuc) });
        }

        private DanhMucDTO Map(DataRow r) => new DanhMucDTO
        {
            MaDanhMuc = (int)r["MaDanhMuc"],
            MaNguoiDung = (int)r["MaNguoiDung"],
            TenDanhMuc = r["TenDanhMuc"].ToString(),
            LoaiGiaoDich = r["LoaiGiaoDich"].ToString(),
            MauSac = r["MauSac"] == DBNull.Value ? "" : r["MauSac"].ToString(),
            GhiChu = r["GhiChu"] == DBNull.Value ? "" : r["GhiChu"].ToString()
        };
    }
}