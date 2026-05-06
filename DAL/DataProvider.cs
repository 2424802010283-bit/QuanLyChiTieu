using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuanLyChiTieu.DAL
{
    public class DataProvider
    {
        // Khởi tạo Singleton: Đảm bảo chỉ có 1 luồng kết nối CSDL duy nhất chạy trong RAM
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }

        private DataProvider() { }

        // Đọc chuỗi kết nối từ App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["ChuoiKetNoi"].ConnectionString;

        // Xử lý thuật toán truy vấn trả về dạng Bảng (SELECT)
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }
            return data;
        }

        // Xử lý thuật toán Thêm, Sửa, Xóa (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
            }
            return data;
        }

        // Xử lý đếm hoặc tính tổng (SUM, COUNT)
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                // ... (Logic gán tham số @ tương tự như trên) ...
                data = command.ExecuteScalar();
            }
            return data;
        }
    }
}