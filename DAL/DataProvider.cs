using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuanLyChiTieu.DAL
{
    public class DataProvider
    {
        // Singleton: Đảm bảo chỉ có 1 instance duy nhất
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
        }

        private DataProvider() { }

        // Đọc chuỗi kết nối từ App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["ChuoiKetNoi"].ConnectionString;

        // ✅ FIX: Dùng Regex để tìm tham số @, tránh bug khi dùng Split(' ')
        private void GanThamSo(SqlCommand command, string query, object[] parameter)
        {
            if (parameter == null) return;
            // Tìm tất cả tham số @tenBiến trong câu SQL (chỉ lấy lần xuất hiện đầu tiên mỗi tên)
            var matches = Regex.Matches(query, @"@\w+");
            int i = 0;
            foreach (Match match in matches)
            {
                if (i >= parameter.Length) break;
                string tenThamSo = match.Value;
                // Tránh gán trùng tên tham số (ví dụ @key xuất hiện 2 lần trong LIKE)
                if (!command.Parameters.Contains(tenThamSo))
                {
                    command.Parameters.AddWithValue(tenThamSo, parameter[i] ?? System.DBNull.Value);
                    i++;
                }
            }
        }

        // SELECT - trả về DataTable
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                GanThamSo(command, query, parameter);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }
            return data;
        }

        // INSERT / UPDATE / DELETE - trả về số dòng bị ảnh hưởng
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                GanThamSo(command, query, parameter);
                data = command.ExecuteNonQuery();
            }
            return data;
        }

        // SUM / COUNT - trả về giá trị đơn
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                GanThamSo(command, query, parameter);
                data = command.ExecuteScalar();
            }
            return data;
        }
    }
}