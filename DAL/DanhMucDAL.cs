using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; // Dùng để đọc App.config
using QuanLyChiTieu.DTO;
namespace QuanLyChiTieu.DAL
{
    public class DanhMucDAL
    {
        // Lấy chuỗi kết nối cực kỳ chuyên nghiệp
        private string strConn = ConfigurationManager.ConnectionStrings["ChuoiKetNoi"].ConnectionString;

        public List<DanhMucDTO> LayDanhSachDanhMuc()
        {
            List<DanhMucDTO> list = new List<DanhMucDTO>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DanhMuc", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DanhMucDTO dm = new DanhMucDTO();
                    dm.MaDM = (int)reader["MaDM"];
                    dm.TenDM = reader["TenDM"].ToString();
                    dm.Loai = reader["Loai"].ToString();
                    list.Add(dm);
                }
            }
            return list;
        }
        public bool ThemDanhMuc(DanhMucDTO dm)
        {
            // 1. Khai báo câu lệnh SQL
            string query = "INSERT INTO DanhMuc (TenDM, Loai) VALUES (@ten, @loai)";

            // 2. Sử dụng kết nối (Sử dụng biến chuỗi kết nối của bạn, ví dụ: connString)
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    // 3. Tạo đối tượng Command và truyền tham số để chống SQL Injection
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ten", dm.TenDM);
                    cmd.Parameters.AddWithValue("@loai", dm.Loai);

                    // 4. Mở kết nối và thực thi
                    conn.Open();
                    int result = cmd.ExecuteNonQuery(); // Trả về số dòng bị tác động

                    // 5. Trả về true nếu thêm thành công (result > 0)
                    return result > 0;
                }
                catch (Exception)
                {
                    // Nếu có lỗi (trùng tên, mất kết nối...) thì trả về false
                    return false;
                }
            }
        }
        public List<string> LayTenTatCaDanhMuc()
        {
            List<string> list = new List<string>();
            string query = "SELECT TenDM FROM DanhMuc";
            DataTable dt = DAL.DataProvider.Instance.ExecuteQuery(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["TenDM"].ToString());
                }
            }
            return list;
        }

    }
}
