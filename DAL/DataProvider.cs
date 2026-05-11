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

using QuanLyChiTieu.DTO;
using QuanLyChiTieu.BLL;
namespace QuanLyChiTieu.DAL
{
    public class DataProvider
    {
        // ⚠️ Đổi tên server cho đúng máy bạn
        private static readonly string connectionString =
    @"Data Source=LAPTOP-C861HF1M\SQLEXPRESS01;Initial Catalog=QuanLyChiTieu;Integrated Security=True";

        public static SqlConnection GetConnection() => new SqlConnection(connectionString);

        public static DataTable ExecuteQuery(string sql, SqlParameter[] pms = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (pms != null) cmd.Parameters.AddRange(pms);
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            return dt;
        }

        public static int ExecuteNonQuery(string sql, SqlParameter[] pms = null)
        {
            int r = 0;
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (pms != null) cmd.Parameters.AddRange(pms);
                        r = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            return r;
        }

        public static object ExecuteScalar(string sql, SqlParameter[] pms = null)
        {
            object r = null;
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (pms != null) cmd.Parameters.AddRange(pms);
                        r = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            return r;
        }

        public static DataTable ExecuteSP(string spName, SqlParameter[] pms = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(spName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (pms != null) cmd.Parameters.AddRange(pms);
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi SP: " + ex.Message); }
            return dt;
        }
    }
}