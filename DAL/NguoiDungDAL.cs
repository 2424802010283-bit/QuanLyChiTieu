using QuanLyChiTieu.DTO;
using System.Data;

namespace QuanLyChiTieu.DAL
{
    public class NguoiDungDAL
    {
        // Kiểm tra đăng nhập và trả về thông tin người dùng
        public NguoiDungDTO LayThongTinNguoiDung(string user, string pass)
        {
            string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @u AND MatKhau = @p";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { user, pass });

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return new NguoiDungDTO
                {
                    TenDangNhap = r["TenDangNhap"].ToString(),
                    MatKhau = r["MatKhau"].ToString(),
                    HoTen = r["HoTen"].ToString(),
                    VaiTro = r["VaiTro"].ToString()
                };
            }
            return null;
        }

        // Đăng ký tài khoản mới
        public bool DangKy(NguoiDungDTO nd)
        {
            string query = "INSERT INTO NguoiDung VALUES (@u, @p, @h, @v)";
            int res = DataProvider.Instance.ExecuteNonQuery(query,
                new object[] { nd.TenDangNhap, nd.MatKhau, nd.HoTen, nd.VaiTro });
            return res > 0;
        }
    }
}