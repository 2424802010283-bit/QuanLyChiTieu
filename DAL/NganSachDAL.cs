using QuanLyChiTieu.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyChiTieu.DAL
{
    public class NganSachDAL
    {
        // Các hàm cũ của bạn (giữ nguyên để tránh lỗi form khác)
        public DataTable GetTheoThang(int maNguoiDung, int thang, int nam)
            => DataProvider.ExecuteSP("SP_GetNganSachTheoThang", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });

        public (decimal, decimal) GetTong(int maNguoiDung, int thang, int nam)
        {
            DataTable dt = DataProvider.ExecuteSP("SP_GetTongNganSachThang", new SqlParameter[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
            if (dt.Rows.Count > 0)
                return ((decimal)dt.Rows[0]["TongNganSach"], (decimal)dt.Rows[0]["TongDaChi"]);
            return (0, 0);
        }

        public int Xoa(int maNganSach)
        {
            return DataProvider.ExecuteNonQuery("DELETE FROM NganSach WHERE MaNganSach=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maNganSach) });
        }

        // ==========================================
        // CÁC HÀM MỚI BỔ SUNG ĐỂ CHẠY FORM NGÂN SÁCH
        // ==========================================
        public List<NganSachDTO> GetAll(int maNguoiDung)
        {
            var list = new List<NganSachDTO>();
            DataTable dt = DataProvider.ExecuteQuery(
                "SELECT * FROM NganSach WHERE MaNguoiDung=@Ma",
                new SqlParameter[] { new SqlParameter("@Ma", maNguoiDung) });

            foreach (DataRow r in dt.Rows)
            {
                list.Add(new NganSachDTO
                {
                    MaNganSach = (int)r["MaNganSach"],
                    MaNguoiDung = (int)r["MaNguoiDung"],
                    MaDanhMuc = (int)r["MaDanhMuc"],
                    SoTienNganSach = (decimal)r["SoTienNganSach"],
                    Thang = (int)r["Thang"],
                    Nam = (int)r["Nam"]
                });
            }
            return list;
        }

        public int Them(NganSachDTO ns)
        {
            string sql = @"INSERT INTO NganSach(MaNguoiDung, MaDanhMuc, SoTienNganSach, Thang, Nam)
                           VALUES(@MaND, @MaDM, @SoTien, @Thang, @Nam)";
            return DataProvider.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@MaND", ns.MaNguoiDung),
                new SqlParameter("@MaDM", ns.MaDanhMuc),
                new SqlParameter("@SoTien", ns.SoTienNganSach),
                new SqlParameter("@Thang", ns.Thang),
                new SqlParameter("@Nam", ns.Nam)
            });
        }

        public int Sua(NganSachDTO ns)
        {
            string sql = @"UPDATE NganSach 
                           SET MaDanhMuc=@MaDM, SoTienNganSach=@SoTien, Thang=@Thang, Nam=@Nam 
                           WHERE MaNganSach=@MaNS";
            return DataProvider.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@MaDM", ns.MaDanhMuc),
                new SqlParameter("@SoTien", ns.SoTienNganSach),
                new SqlParameter("@Thang", ns.Thang),
                new SqlParameter("@Nam", ns.Nam),
                new SqlParameter("@MaNS", ns.MaNganSach)
            });
        }
    }
}