using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class QLDM_DAL
    {
        Connect db = new Connect();
        public DataTable GetAllCategories()
        {
            return db.LoadData("SELECT * FROM Categories_N01");
        }
        public void InsertCategory(QLDM_DTO cat)
        {
            string sql = $"INSERT INTO Categories_N01 (CategoryName_N01, Description_N01) VALUES (N'{cat.CategoryName}', N'{cat.Description}')";
            db.Execute(sql);
        }
        public void UpdateCategory(QLDM_DTO cat)
        {
            string sql = $"UPDATE Categories_N01 SET CategoryName_N01 = N'{cat.CategoryName}', Description_N01 = N'{cat.Description}' WHERE CategoryID_N01 = {cat.CategoryId}";
            db.Execute(sql);
        }
        public void DeleteCategory(int id)
        {
            string sql = $"DELETE FROM Categories_N01 WHERE CategoryID_N01 = {id}";
            db.Execute(sql);
        }
        public DataTable SearchCategory(string keyword)
        {
            string sql = $"SELECT * FROM Categories_N01 WHERE CategoryName_N01 LIKE N'%{keyword}%'";
            return db.LoadData(sql);
        }
    }
}
