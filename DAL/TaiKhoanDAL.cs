using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChiTieu.DAL
{
    public class TaiKhoanDAL
    {
        public List<TaiKhoanDTO> GetByUser(int maNguoiDung)
        {
            var list = new List<TaiKhoanDTO>();
            DataTable dt = DataProvider.ExecuteQuery(
                "SELECT * FROM TaiKhoan WHERE MaNguoiDung=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maNguoiDung) });
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
        }

        public int Them(TaiKhoanDTO tk)
        {
            string sql = @"INSERT INTO TaiKhoan(MaNguoiDung,TenTaiKhoan,LoaiTaiKhoan,SoDu,GhiChu)
                           VALUES(@Ma,@Ten,@Loai,@SoDu,@GhiChu)";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",    tk.MaNguoiDung),
                new SqlParameter("@Ten",   tk.TenTaiKhoan),
                new SqlParameter("@Loai",  tk.LoaiTaiKhoan),
                new SqlParameter("@SoDu",  tk.SoDu),
                new SqlParameter("@GhiChu",(object)tk.GhiChu ?? DBNull.Value)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }

        public int Sua(TaiKhoanDTO tk)
        {
            string sql = @"UPDATE TaiKhoan SET TenTaiKhoan=@Ten, LoaiTaiKhoan=@Loai,
                           SoDu=@SoDu, GhiChu=@GhiChu WHERE MaTaiKhoan=@MaTK";
            var pms = new SqlParameter[] {
                new SqlParameter("@MaTK",  tk.MaTaiKhoan),
                new SqlParameter("@Ten",   tk.TenTaiKhoan),
                new SqlParameter("@Loai",  tk.LoaiTaiKhoan),
                new SqlParameter("@SoDu",  tk.SoDu),
                new SqlParameter("@GhiChu",(object)tk.GhiChu ?? DBNull.Value)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }

        public int Xoa(int maTaiKhoan)
        {
            return DataProvider.ExecuteNonQuery(
                "DELETE FROM TaiKhoan WHERE MaTaiKhoan=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maTaiKhoan) });
        }

        public int CapNhatSoDu(int maTaiKhoan, decimal soTien)
        {
            return DataProvider.ExecuteNonQuery(
                "UPDATE TaiKhoan SET SoDu=SoDu+@SoTien WHERE MaTaiKhoan=@Ma",
                new SqlParameter[] {
                    new SqlParameter("@SoTien", soTien),
                    new SqlParameter("@Ma",     maTaiKhoan)
                });
        }

        public decimal GetTongSoDu(int maNguoiDung)
        {
            object r = DataProvider.ExecuteScalar(
                "SELECT ISNULL(SUM(SoDu),0) FROM TaiKhoan WHERE MaNguoiDung=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maNguoiDung) });
            return r == null ? 0 : (decimal)r;
        }

        private TaiKhoanDTO Map(DataRow r) => new TaiKhoanDTO
        {
            MaTaiKhoan = (int)r["MaTaiKhoan"],
            MaNguoiDung = (int)r["MaNguoiDung"],
            TenTaiKhoan = r["TenTaiKhoan"].ToString(),
            LoaiTaiKhoan = r["LoaiTaiKhoan"].ToString(),
            SoDu = (decimal)r["SoDu"],
            GhiChu = r["GhiChu"] == DBNull.Value ? "" : r["GhiChu"].ToString()
        };
    }
}