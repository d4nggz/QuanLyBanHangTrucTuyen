using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class QLDM_DAL
    {
        Connect db = new Connect();
        public DataTable GetCategories()
        {
            return db.LoadData("SELECT category_id, name, description FROM categories ORDER BY category_id DESC");
        }
        public void InsertCategory(QLDM_DTO cat)
        {
            string sql = "INSERT INTO categories(name, description) VALUES(@Name, @Desc)";
            MySqlParameter[] para = {
                new MySqlParameter("@Name", cat.Name),
                new MySqlParameter("@Desc", cat.Description)
            };
            db.Execute(sql, para);
        }
        public void UpdateCategory(QLDM_DTO cat)
        {
            string sql = "UPDATE categories SET name = @Name, description = @Desc WHERE category_id = @Id";
            MySqlParameter[] para = {
                new MySqlParameter("@Id", cat.CategoryId),
                new MySqlParameter("@Name", cat.Name),
                new MySqlParameter("@Desc", cat.Description)
            };
            db.Execute(sql, para);
        }
        public void DeleteCategory(int id)
        {
            string sql = "DELETE FROM categories WHERE category_id = @Id";
            MySqlParameter[] para = { new MySqlParameter("@Id", id) };
            db.Execute(sql, para);
        }
        public DataTable SearchCategory(string keyword)
        {
            string sql = "SELECT * FROM categories WHERE name LIKE @Kw OR description LIKE @Kw";
            MySqlParameter[] para = { new MySqlParameter("@Kw", "%" + keyword + "%") };
            return db.LoadData(sql, para);
        }
        public bool CheckNameExists(string name)
        {
            string sql = "SELECT COUNT(*) FROM categories WHERE name = @Name";
            MySqlParameter[] para = { new MySqlParameter("@Name", name) };
            DataTable dt = db.LoadData(sql, para);
            return (Convert.ToInt32(dt.Rows[0][0]) > 0);
        }
    }
}
