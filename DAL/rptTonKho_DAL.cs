using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class DAL_Product
    {
        Connect db = new Connect();

        public DataTable GetCategoryList()
        {
            string sql = "SELECT category_id, name FROM categories";
            return db.LoadData(sql);
        }

        public DataTable GetProductsByCatID(int catID)
        {
            string sql = @"
                SELECT 
                    p.product_id, 
                    p.name AS product_name, 
                    c.name AS category_name, 
                    p.stock_quantity, 
                    p.price, 
                    (p.stock_quantity * p.price) AS total_value
                FROM products p
                JOIN categories c ON p.category_id = c.category_id
                WHERE p.category_id = @id";

            MySqlParameter[] _params = new MySqlParameter[]
            {
                new MySqlParameter("@id", catID)
            };

            return db.LoadData(sql, _params);
        }
    }
}
