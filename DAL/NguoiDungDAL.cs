using QuanLyChiTieu.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // Đừng quên dòng này nhé!

using System.Windows.Forms;
using System.IO;


namespace QuanLyChiTieu.DAL
{
    public class NguoiDungDAL
    {
        public NguoiDungDTO DangNhap(string email, string matKhau)
        {
            string sql = "SELECT * FROM NguoiDung WHERE Email=@Email AND MatKhau=@MatKhau";
            var pms = new SqlParameter[] {
                new SqlParameter("@Email",   email),
                new SqlParameter("@MatKhau", matKhau)
            };
            DataTable dt = DataProvider.ExecuteQuery(sql, pms);
            if (dt.Rows.Count == 0) return null;
            DataRow r = dt.Rows[0];
            return new NguoiDungDTO
            {
                MaNguoiDung = (int)r["MaNguoiDung"],
                HoTen = r["HoTen"].ToString(),
                Email = r["Email"].ToString(),
                SoDienThoai = r["SoDienThoai"] == DBNull.Value ? "" : r["SoDienThoai"].ToString(),
                MatKhau = r["MatKhau"].ToString(),
                DiaChi = r["DiaChi"] == DBNull.Value ? "" : r["DiaChi"].ToString(),
                NgayTao = (DateTime)r["NgayTao"]
            };
        }

        public int CapNhatThongTin(NguoiDungDTO nd)
        {
            string sql = @"UPDATE NguoiDung SET HoTen=@HoTen, SoDienThoai=@SDT,
                           NgaySinh=@NgaySinh, DiaChi=@DiaChi WHERE MaNguoiDung=@Ma";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",      nd.MaNguoiDung),
                new SqlParameter("@HoTen",   nd.HoTen),
                new SqlParameter("@SDT",     (object)nd.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@NgaySinh",(object)nd.NgaySinh    ?? DBNull.Value),
                new SqlParameter("@DiaChi",  (object)nd.DiaChi      ?? DBNull.Value)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }

        public int DoiMatKhau(int maNguoiDung, string matKhauMoi)
        {
            string sql = "UPDATE NguoiDung SET MatKhau=@MatKhau WHERE MaNguoiDung=@Ma";
            var pms = new SqlParameter[] {
                new SqlParameter("@Ma",      maNguoiDung),
                new SqlParameter("@MatKhau", matKhauMoi)
            };
            return DataProvider.ExecuteNonQuery(sql, pms);
        }
        public NguoiDungDTO KiemTraDangNhap(string email, string matKhau)
        {
            // Sử dụng cột Email thay vì EmailOrSDT
            string sql = "SELECT * FROM NguoiDung WHERE Email = @email AND MatKhau = @matKhau";
            SqlParameter[] pms = {
            new SqlParameter("@email", email),
            new SqlParameter("@matKhau", matKhau)
        };

            DataTable dt = DataProvider.ExecuteQuery(sql, pms);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return new NguoiDungDTO
                {
                    MaNguoiDung = (int)r["MaNguoiDung"],
                    HoTen = r["HoTen"].ToString(),
                    Email = r["Email"].ToString(),
                    MatKhau = r["MatKhau"].ToString()
                };
            }
            return null;
        }

        // Kiểm tra tồn tại theo Email
        public bool KiemTraTonTai(string email)
        {
            string sql = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @email";
            DataTable dt = DataProvider.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@email", email) });
            return (int)dt.Rows[0][0] > 0;
        }

        // Cập nhật hàm Đăng ký để lưu vào cột Email
        public bool DangKy(NguoiDungDTO nd)
        {
            string sql = "INSERT INTO NguoiDung(HoTen, Email, MatKhau) VALUES(@HoTen, @Email, @Pass)";
            SqlParameter[] pms = {
            new SqlParameter("@HoTen", nd.HoTen),
            new SqlParameter("@Email", nd.Email),
            new SqlParameter("@Pass", nd.MatKhau)
        };
            return DataProvider.ExecuteNonQuery(sql, pms) > 0;
        }
    }
}