using QuanLyChiTieu.DTO;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;


using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // Đừng quên dòng này nhé!

using System.Windows.Forms;
using System.IO;
namespace QuanLyChiTieu.DAL
{
    public class GiaoDichDAL
    {
        public List<GiaoDichDTO> GetByLoaiVaNgay(int maNguoiDung, string loai, DateTime ngay)
        {
            string sql = @"
                SELECT g.*, d.TenDanhMuc, t.TenTaiKhoan
                FROM GiaoDich g
                LEFT JOIN DanhMuc  d ON g.MaDanhMuc  = d.MaDanhMuc
                LEFT JOIN TaiKhoan t ON g.MaTaiKhoan = t.MaTaiKhoan
                WHERE g.MaNguoiDung=@Ma AND g.LoaiGiaoDich=@Loai
                  AND CAST(g.NgayGiaoDich AS DATE)=@Ngay
                ORDER BY g.NgayGiaoDich DESC";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",   maNguoiDung),
                new SqlParameter("@Loai", loai),
                new SqlParameter("@Ngay", ngay.Date)
            };
            return MapList(DataProvider.ExecuteQuery(sql, pms));
        }

        public List<GiaoDichDTO> GetAll(int maNguoiDung)
        {
            string sql = @"
                SELECT g.*, d.TenDanhMuc, t.TenTaiKhoan,
                       t2.TenTaiKhoan AS TenTaiKhoanNhan
                FROM GiaoDich g
                LEFT JOIN DanhMuc  d  ON g.MaDanhMuc      = d.MaDanhMuc
                LEFT JOIN TaiKhoan t  ON g.MaTaiKhoan     = t.MaTaiKhoan
                LEFT JOIN TaiKhoan t2 ON g.MaTaiKhoanNhan = t2.MaTaiKhoan
                WHERE g.MaNguoiDung=@Ma ORDER BY g.NgayGiaoDich DESC";
            return MapList(DataProvider.ExecuteQuery(sql,
                new SqlParameter[] { new SqlParameter("@Ma", maNguoiDung) }));
        }

        public List<GiaoDichDTO> Search(int maNguoiDung, string keyword)
        {
            string sql = @"
                SELECT g.*, d.TenDanhMuc, t.TenTaiKhoan,
                       t2.TenTaiKhoan AS TenTaiKhoanNhan
                FROM GiaoDich g
                LEFT JOIN DanhMuc  d  ON g.MaDanhMuc      = d.MaDanhMuc
                LEFT JOIN TaiKhoan t  ON g.MaTaiKhoan     = t.MaTaiKhoan
                LEFT JOIN TaiKhoan t2 ON g.MaTaiKhoanNhan = t2.MaTaiKhoan
                WHERE g.MaNguoiDung=@Ma
                  AND (g.MoTa LIKE @Kw OR d.TenDanhMuc LIKE @Kw OR t.TenTaiKhoan LIKE @Kw)
                ORDER BY g.NgayGiaoDich DESC";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma", maNguoiDung),
                new SqlParameter("@Kw", "%" + keyword + "%")
            };
            return MapList(DataProvider.ExecuteQuery(sql, pms));
        }

        public int Them(GiaoDichDTO gd)
        {
            string sql = @"
                INSERT INTO GiaoDich(MaNguoiDung,MaTaiKhoan,MaTaiKhoanNhan,MaDanhMuc,
                                     LoaiGiaoDich,NgayGiaoDich,SoTien,MoTa)
                VALUES(@Ma,@MaTK,@MaTKNhan,@MaDM,@Loai,@Ngay,@SoTien,@MoTa);
                SELECT SCOPE_IDENTITY();";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",      gd.MaNguoiDung),
                new SqlParameter("@MaTK",    gd.MaTaiKhoan),
                new SqlParameter("@MaTKNhan",(object)gd.MaTaiKhoanNhan ?? DBNull.Value),
                new SqlParameter("@MaDM",    (object)gd.MaDanhMuc      ?? DBNull.Value),
                new SqlParameter("@Loai",    gd.LoaiGiaoDich),
                new SqlParameter("@Ngay",    gd.NgayGiaoDich),
                new SqlParameter("@SoTien",  gd.SoTien),
                new SqlParameter("@MoTa",    (object)gd.MoTa           ?? DBNull.Value)
            };
            object r = DataProvider.ExecuteScalar(sql, pms);
            return r != null ? Convert.ToInt32(r) : 0;
        }

        public int Xoa(int maGiaoDich)
        {
            return DataProvider.ExecuteNonQuery(
                "DELETE FROM GiaoDich WHERE MaGiaoDich=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maGiaoDich) });
        }

        public decimal GetTong(int maNguoiDung, string loai, int thang, int nam)
        {
            string sql = @"SELECT ISNULL(SUM(SoTien),0) FROM GiaoDich
                           WHERE MaNguoiDung=@Ma AND LoaiGiaoDich=@Loai
                             AND MONTH(NgayGiaoDich)=@Thang AND YEAR(NgayGiaoDich)=@Nam";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",    maNguoiDung),
                new SqlParameter("@Loai",  loai),
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam",   nam)
            };
            object r = DataProvider.ExecuteScalar(sql, pms);
            return r == null ? 0 : (decimal)r;
        }

        public DataTable GetThongKeTheoThang(int maNguoiDung, int nam)
            => DataProvider.ExecuteSP("SP_GetThongKeTheoThang", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Nam", nam) });

        public DataTable GetTop5DanhMucChi(int maNguoiDung, int thang, int nam)
            => DataProvider.ExecuteSP("SP_GetTop5DanhMucChi", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam) });

        public List<GiaoDichDTO> GetGanDay(int maNguoiDung, int soLuong = 10)
            => MapList(DataProvider.ExecuteSP("SP_GetGiaoDichGanDay", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@SoLuong", soLuong) }));

        public DataTable BaoCao(int maNguoiDung, DateTime tuNgay, DateTime denNgay, string loai = null)
            => DataProvider.ExecuteSP("SP_BaoCaoGiaoDich", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung",  maNguoiDung),
                new SqlParameter("@TuNgay",       tuNgay),
                new SqlParameter("@DenNgay",      denNgay),
                new SqlParameter("@LoaiGiaoDich", (object)loai ?? DBNull.Value) });

        // ─── Map ───
        private List<GiaoDichDTO> MapList(DataTable dt)
        {
            var list = new List<GiaoDichDTO>();
            foreach (DataRow r in dt.Rows) list.Add(MapRow(r));
            return list;
        }

        private GiaoDichDTO MapRow(DataRow r)
        {
            var gd = new GiaoDichDTO
            {
                MaGiaoDich = Col<int>(r, "MaGiaoDich"),
                MaNguoiDung = Col<int>(r, "MaNguoiDung"),
                MaTaiKhoan = Col<int>(r, "MaTaiKhoan"),
                LoaiGiaoDich = r["LoaiGiaoDich"].ToString(),
                NgayGiaoDich = r.Table.Columns.Contains("NgayGiaoDich") && r["NgayGiaoDich"] != DBNull.Value
                               ? (DateTime)r["NgayGiaoDich"] : DateTime.Now,
                SoTien = r.Table.Columns.Contains("SoTien") && r["SoTien"] != DBNull.Value
                               ? (decimal)r["SoTien"] : 0,
                MoTa = Str(r, "MoTa"),
                TenDanhMuc = Str(r, "TenDanhMuc"),
                TenTaiKhoan = Str(r, "TenTaiKhoan"),
                TenTaiKhoanNhan = Str(r, "TenTaiKhoanNhan")
            };
            if (r.Table.Columns.Contains("MaTaiKhoanNhan") && r["MaTaiKhoanNhan"] != DBNull.Value)
                gd.MaTaiKhoanNhan = (int)r["MaTaiKhoanNhan"];
            if (r.Table.Columns.Contains("MaDanhMuc") && r["MaDanhMuc"] != DBNull.Value)
                gd.MaDanhMuc = (int)r["MaDanhMuc"];
            return gd;
        }

        private T Col<T>(DataRow r, string col)
        {
            if (r.Table.Columns.Contains(col) && r[col] != DBNull.Value) return (T)r[col];
            return default;
        }
        private string Str(DataRow r, string col)
            => r.Table.Columns.Contains(col) && r[col] != DBNull.Value ? r[col].ToString() : "";

        public int Sua(GiaoDichDTO gd)
        {
            string sql = @"UPDATE GiaoDich 
                   SET MaDanhMuc=@MaDM, NgayGiaoDich=@Ngay, SoTien=@SoTien, MoTa=@MoTa 
                   WHERE MaGiaoDich=@MaGD";
            var pms = new System.Data.SqlClient.SqlParameter[] {
        new System.Data.SqlClient.SqlParameter("@MaGD", gd.MaGiaoDich),
        new System.Data.SqlClient.SqlParameter("@MaDM", (object)gd.MaDanhMuc ?? DBNull.Value),
        new System.Data.SqlClient.SqlParameter("@Ngay", gd.NgayGiaoDich),
        new System.Data.SqlClient.SqlParameter("@SoTien", gd.SoTien),
        new System.Data.SqlClient.SqlParameter("@MoTa", (object)gd.MoTa ?? DBNull.Value)
    };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }
    }

}