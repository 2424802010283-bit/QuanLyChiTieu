using QuanLyChiTieu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChiTieu.DAL
{
    public class NguoiDungDAL
    {
        // Kiểm tra đăng nhập và lấy thông tin User
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
                    VaiTro = r["VaiTro"].ToString(),
                    HoTen = r["HoTen"].ToString()
                };
            }
            return null;
        }

        public bool DangKy(NguoiDungDTO nd)
        {
            string query = "INSERT INTO NguoiDung VALUES ( @u , @p , @h , @v )";
            int res = DataProvider.Instance.ExecuteNonQuery(query, new object[] { nd.TenDangNhap, nd.MatKhau, nd.HoTen, nd.VaiTro });
            return res > 0;
        }
    }

}
