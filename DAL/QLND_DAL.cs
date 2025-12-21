using DTO;
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
                SELECT u.*, r.RoleName_N01 
                FROM User_N01 u 
                LEFT JOIN Roles_N01 r ON u.RoleID_N01 = r.RoleID_N01";
            return db.LoadData(sql);
        }
        public DataTable GetRoles()
        {
            return db.LoadData("SELECT RoleID_N01, RoleName_N01 FROM Roles_N01");
        }
        public void InsertUser(QLND_DTO u)
        {
            string sql = string.Format("INSERT INTO User_N01 (Username_N01, Password_N01, Email_N01, Phone_N01, Address_N01, RoleID_N01) " +
                                       "VALUES (N'{0}', '{1}', '{2}', '{3}', N'{4}', {5})",
                                       u.Username, u.Password, u.Email, u.Phone, u.Address, u.RoleId);
            db.Execute(sql);
        }
        public void UpdateUser(QLND_DTO u)
        {
            string sql = string.Format("UPDATE User_N01 SET Username_N01 = N'{0}', Password_N01 = '{1}', Email_N01 = '{2}', " +
                                       "Phone_N01 = '{3}', Address_N01 = N'{4}', RoleID_N01 = {5} WHERE UserID_N01 = {6}",
                                       u.Username, u.Password, u.Email, u.Phone, u.Address, u.RoleId, u.UserId);
            db.Execute(sql);
        }
        public void DeleteUser(int id)
        {
            db.Execute($"DELETE FROM User_N01 WHERE UserID_N01 = {id}");
        }

        public DataTable GetUserByID(int id)
        {
            return db.LoadData($"SELECT * FROM User_N01 WHERE UserID_N01 = {id}");
        }

        public DataTable SearchUsers(string keyword, int roleId)
        {
            string sql = $@"
                SELECT u.*, r.RoleName_N01 
                FROM User_N01 u 
                LEFT JOIN Roles_N01 r ON u.RoleID_N01 = r.RoleID_N01
                WHERE u.Username_N01 LIKE N'%{keyword}%'";

            if (roleId != -1) // Nếu có chọn vai trò cụ thể
                sql += $" AND u.RoleID_N01 = {roleId}";

            return db.LoadData(sql);
        }
    }

}
