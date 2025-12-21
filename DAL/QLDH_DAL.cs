using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DTO;

namespace DAL
{
    public class QLDH_DAL
    {
        Connect db = new Connect();
        public DataTable GetOrders()
        {
            string sql = @"
                SELECT 
                    o.OrderID_N01, 
                    u.Username_N01, 
                    o.OrderDate_N01, 
                    o.TotalAmount_N01, 
                    s.StatusName_N01,
                    o.Address_N01,
                    o.Phone_N01,
                    o.StatusID_N01
                FROM Orders_N01 o
                JOIN User_N01 u ON o.UserID_N01 = u.UserID_N01
                JOIN OrderStatus_N01 s ON o.StatusID_N01 = s.StatusID_N01";
            return db.LoadData(sql);
        }
        public DataTable GetOrderDetails(int orderId)
        {
            string sql = $@"
                SELECT 
                    p.ProductName_N01, 
                    d.Quantity_N01, 
                    d.UnitPrice_N01,
                    (d.Quantity_N01 * d.UnitPrice_N01) AS ThanhTien
                FROM OrderItems_N01 d
                JOIN Products_N01 p ON d.ProductID_N01 = p.ProductID_N01
                WHERE d.OrderID_N01 = {orderId}";

            return db.LoadData(sql);
        }
        public DataTable GetStatuses()
        {
            return db.LoadData("SELECT StatusID_N01, StatusName_N01 FROM OrderStatus_N01");
        }
        public void UpdateStatus(int orderId, int statusId)
        {
            string sql = $"UPDATE Orders_N01 SET StatusID_N01 = {statusId} WHERE OrderID_N01 = {orderId}";
            db.Execute(sql);
        }
        public void DeleteOrder(int orderId)
        {
            string sqlDetails = $"DELETE FROM OrderItems_N01 WHERE OrderID_N01 = {orderId}";
            db.Execute(sqlDetails);
            string sqlOrder = $"DELETE FROM Orders_N01 WHERE OrderID_N01 = {orderId}";
            db.Execute(sqlOrder);
        }
        
        public DataTable TimKiemDonHang(string keyword, DateTime tuNgay, DateTime denNgay, int statusId)
        {
          
            string strTuNgay = tuNgay.ToString("yyyy-MM-dd");
            string strDenNgay = denNgay.ToString("yyyy-MM-dd");

                    string sql = $@"
                SELECT 
                    o.OrderID_N01, u.Username_N01, o.OrderDate_N01, o.TotalAmount_N01, 
                    s.StatusName_N01, o.Address_N01, o.Phone_N01, o.StatusID_N01
                FROM Orders_N01 o
                JOIN User_N01 u ON o.UserID_N01 = u.UserID_N01
                JOIN OrderStatus_N01 s ON o.StatusID_N01 = s.StatusID_N01
                WHERE (o.OrderDate_N01 >= '{strTuNgay} 00:00:00' AND o.OrderDate_N01 <= '{strDenNgay} 23:59:59')
                AND (u.Username_N01 LIKE N'%{keyword}%' OR o.OrderID_N01 LIKE '%{keyword}%')";

            if (statusId != -1)
            {
                sql += $" AND o.StatusID_N01 = {statusId}";
            }

            return db.LoadData(sql);
        }
    }
}
