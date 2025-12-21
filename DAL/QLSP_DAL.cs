using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace DAL
{
    public class QLSP_DAL
    {
        Connect db = new Connect();
        public DataTable GetAllProducts()
        {
            return db.LoadData("SELECT * FROM Products_N01");
        }
        public DataTable GetCategories()
        {
            return db.LoadData("SELECT CategoryID_N01, CategoryName_N01 FROM Categories_N01");
        }
        public void Insert(QLSP_DTO p)
        {
            string sql = string.Format("INSERT INTO Products_N01 (ProductName_N01, Price_N01, StockQuantity_N01, CategoryID_N01, ImageURL_N01, description_N01) " +
                                       "VALUES (N'{0}', {1}, {2}, {3}, '{4}', N'{5}')",
                                       p.ProductName, p.Price, p.StockQuantity, p.CategoryId, p.ImageUrl, p.Description);
            db.Execute(sql);
        }

        public void Update(QLSP_DTO p)
        {
            string sql = string.Format("UPDATE Products_N01 SET ProductName_N01 = N'{0}', Price_N01 = {1}, StockQuantity_N01 = {2}, " +
                                       "CategoryID_N01 = {3}, ImageURL_N01 = '{4}', description_N01 = N'{5}' WHERE ProductID_N01 = {6}",
                                       p.ProductName, p.Price, p.StockQuantity, p.CategoryId, p.ImageUrl, p.Description, p.ProductId);
            db.Execute(sql);
        }
        public void Delete(int id)
        {
            string sql = string.Format("DELETE FROM Products_N01 WHERE ProductID_N01 = {0}", id);
            db.Execute(sql);
        }

    }
}
