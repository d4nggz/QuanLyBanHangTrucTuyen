using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Connect 
    {
        
        private string strCon = @"Data Source=dang;Initial Catalog=QuanLyBanHang_N01;Integrated Security=True;Encrypt=False";

        public SqlConnection getConnection()
        {
            return new SqlConnection(strCon);
        }

        // Hàm lấy dữ liệu (Read)
        public DataTable LoadData(string sql)
        {
            using (SqlConnection conn = getConnection())
            {
                SqlDataAdapter ad = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                return dt;
            } // Kết nối tự động đóng khi thoát khỏi using
        }
        public void Execute(string sql)
        {
            using (SqlConnection conn = getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}