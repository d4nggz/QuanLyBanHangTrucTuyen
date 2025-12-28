using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace BUS
{
    public class QLND_BUS
    {
        QLND_DAL dal = new QLND_DAL();

        public List<QLND_DTO> GetListUsers()
        {
            DataTable dt = dal.GetAllUsers();
            List<QLND_DTO> list = new List<QLND_DTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new QLND_DTO()
                {
                    UserId = Convert.ToInt32(row["user_id"]),
                    Username = row["username"].ToString(),
                    Email = row["email"].ToString(),
                    Phone = row["phone"].ToString(),
                    Address = row["address"].ToString(),
                    RoleId = Convert.ToInt32(row["role_id"]),
                    RoleName = row["role_name"].ToString(),
                    Status = row["status"].ToString()
                });
            }
            return list;
        }

        public DataTable GetRoles()
        {
            return dal.GetRoles();
        }

        public string AddUser(QLND_DTO u)
        {
            if (dal.CheckUsernameExists(u.Username))
            {
                return "Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.";
            }

            if (string.IsNullOrEmpty(u.Password))
            {
                return "Mật khẩu không được để trống!";
            }

            u.Password = MD5Hash(u.Password);

            try
            {
                dal.InsertUser(u);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        public bool UpdateUser(QLND_DTO u)
        {
            try
            {
                dal.UpdateUser(u);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ResetPassword(int userId, string newRawPassword)
        {
            try
            {
                string hashPass = MD5Hash(newRawPassword);
                dal.UpdatePassword(userId, hashPass);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                dal.DeleteUser(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<QLND_DTO> SearchUsers(string keyword, int roleId)
        {
            DataTable dt = dal.SearchUsers(keyword, roleId);
            List<QLND_DTO> list = new List<QLND_DTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new QLND_DTO()
                {
                    UserId = Convert.ToInt32(row["user_id"]),
                    Username = row["username"].ToString(),
                    Email = row["email"].ToString(),
                    Phone = row["phone"].ToString(),
                    Address = row["address"].ToString(),
                    RoleId = Convert.ToInt32(row["role_id"]),
                    RoleName = row["role_name"].ToString(),
                    Status = row["status"].ToString()
                });
            }
            return list;
        }

        private string MD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); // Chuyển sang chuỗi Hex in hoa
                }
                return sb.ToString();
            }
        }
    }
}