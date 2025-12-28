using DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class QLND_DAL
    {
        Connect db = new Connect();
        public DataTable GetAllUsers()
        {
            string sql = @"
                SELECT u.user_id, u.username, u.email, u.phone, u.address, u.status,
                       u.role_id, r.role_name
                FROM users u
                LEFT JOIN roles r ON u.role_id = r.role_id
                ORDER BY u.user_id DESC";

            return db.LoadData(sql);
        }
        public DataTable GetRoles()
        {
            return db.LoadData("SELECT role_id, role_name FROM roles");
        }
        public void InsertUser(QLND_DTO u)
        {
            string sql = @"INSERT INTO users(username, password_hash, email, phone, address, role_id, status) 
                           VALUES(@User, @Pass, @Email, @Phone, @Addr, @Role, @Status)";

            MySqlParameter[] para = {
                new MySqlParameter("@User", u.Username),
                new MySqlParameter("@Pass", u.Password),
                new MySqlParameter("@Email", u.Email),
                new MySqlParameter("@Phone", u.Phone),
                new MySqlParameter("@Addr", u.Address),
                new MySqlParameter("@Role", u.RoleId),
                new MySqlParameter("@Status", string.IsNullOrEmpty(u.Status) ? "Active" : u.Status)
            };

            db.Execute(sql, para);
        }
        public void UpdateUser(QLND_DTO u)
        {
            string sql = @"UPDATE users 
                           SET email = @Email, 
                               phone = @Phone, 
                               address = @Addr, 
                               role_id = @Role, 
                               status = @Status
                           WHERE user_id = @Id";

            MySqlParameter[] para = {
                new MySqlParameter("@Id", u.UserId),
                new MySqlParameter("@Email", u.Email),
                new MySqlParameter("@Phone", u.Phone),
                new MySqlParameter("@Addr", u.Address),
                new MySqlParameter("@Role", u.RoleId),
                new MySqlParameter("@Status", u.Status)
            };

            db.Execute(sql, para);
        }
        public void UpdatePassword(int userId, string newPassHash)
        {
            string sql = "UPDATE users SET password_hash = @Pass WHERE user_id = @Id";
            MySqlParameter[] para = {
                new MySqlParameter("@Id", userId),
                new MySqlParameter("@Pass", newPassHash)
            };
            db.Execute(sql, para);
        }
        public void DeleteUser(int id)
        {
            string sql = "DELETE FROM users WHERE user_id = @Id";
            MySqlParameter[] para = {
                new MySqlParameter("@Id", id)
            };

            db.Execute(sql, para);
        }

        public DataTable GetUserByID(int id)
        {
            string sql = "SELECT * FROM users WHERE user_id = @Id";
            MySqlParameter[] para = {
                new MySqlParameter("@Id", id)
            };
            return db.LoadData(sql, para);
        }

        public DataTable SearchUsers(string keyword, int roleId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                SELECT u.user_id, u.username, u.email, u.phone, u.address, u.status,
                       u.role_id, r.role_name
                FROM users u
                LEFT JOIN roles r ON u.role_id = r.role_id
                WHERE (u.username LIKE @Keyword OR u.email LIKE @Keyword OR u.phone LIKE @Keyword)");

            List<MySqlParameter> pList = new List<MySqlParameter>();
            pList.Add(new MySqlParameter("@Keyword", "%" + keyword + "%"));

            // Nếu roleId != -1 (nghĩa là có chọn lọc theo quyền)
            if (roleId != -1)
            {
                sql.Append(" AND u.role_id = @RoleID");
                pList.Add(new MySqlParameter("@RoleID", roleId));
            }

            return db.LoadData(sql.ToString(), pList.ToArray());
        }
        public bool CheckUsernameExists(string username)
        {
            string sql = "SELECT COUNT(*) FROM users WHERE username = @User";
            MySqlParameter[] para = { new MySqlParameter("@User", username) };

            DataTable dt = db.LoadData(sql, para);
            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                return true;
            return false;
        }
    }

}
