using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BUS
{
    public class UserBUS
    {
        QLND_DAL dal = new QLND_DAL();

        public DataTable GetAllUsers() => dal.GetAllUsers();
        public DataTable GetAllRoles() => dal.GetRoles();
        public DataTable GetUserByID(int id) => dal.GetUserByID(id);
        public void AddUser(QLND_DTO u) => dal.InsertUser(u);
        public void UpdateUser(QLND_DTO u) => dal.UpdateUser(u);
        public void DeleteUser(int id) => dal.DeleteUser(id);
        public DataTable Search(string key, int roleId) => dal.SearchUsers(key, roleId);
    }
}
