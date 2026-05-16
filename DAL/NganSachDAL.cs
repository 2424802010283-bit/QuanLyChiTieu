using QuanLyChiTieu.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyChiTieu.DAL
{
    // DTO bổ sung cho Ngân sách (có DaChi, PhanTram)
    // (thêm vào file NganSachDTO.cs hoặc để đây cũng được)

    public class NganSachDAL
    {
        public DataTable GetTheoThang(int maNguoiDung, int thang, int nam)
            => DataProvider.ExecuteSP("SP_GetNganSachTheoThang", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam",   nam) });

        public (decimal tongNganSach, decimal tongDaChi) GetTong(int maNguoiDung, int thang, int nam)
        {
            DataTable dt = DataProvider.ExecuteSP("SP_GetTongNganSachThang", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam",   nam) });
            if (dt.Rows.Count == 0) return (0, 0);
            return (
                dt.Rows[0]["TongNganSach"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["TongNganSach"],
                dt.Rows[0]["TongDaChi"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["TongDaChi"]
            );
        }

        public int Them(int maNguoiDung, int maDanhMuc, decimal soTien, int thang, int nam)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM NganSach WHERE MaNguoiDung=@Ma AND MaDanhMuc=@MaDM AND Thang=@Thang AND Nam=@Nam)
                               UPDATE NganSach SET SoTienNganSach=@SoTien WHERE MaNguoiDung=@Ma AND MaDanhMuc=@MaDM AND Thang=@Thang AND Nam=@Nam
                           ELSE
                               INSERT INTO NganSach(MaNguoiDung,MaDanhMuc,SoTienNganSach,Thang,Nam)
                               VALUES(@Ma,@MaDM,@SoTien,@Thang,@Nam)";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",     maNguoiDung),
                new SqlParameter("@MaDM",   maDanhMuc),
                new SqlParameter("@SoTien", soTien),
                new SqlParameter("@Thang",  thang),
                new SqlParameter("@Nam",    nam)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }

        public int Xoa(int maNganSach)
        {
            return DataProvider.ExecuteNonQuery(
                "DELETE FROM NganSach WHERE MaNganSach=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maNganSach) });
        }
    }
}