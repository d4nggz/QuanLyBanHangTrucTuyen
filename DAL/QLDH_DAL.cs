using DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class QLDH_DAL
    {
        Connect db = new Connect();
        public DataTable GetOrders()
        {
            string sql = @"
                SELECT 
                    o.order_id, 
                    u.username, 
                    o.order_date, 
                    o.total_amount, 
                    s.status_name,
                    o.address,
                    o.phone,
                    o.status_id
                FROM orders o
                JOIN users u ON o.user_id = u.user_id
                JOIN order_status s ON o.status_id = s.status_id
                ORDER BY o.order_date DESC";

            return db.LoadData(sql);
        }
        public DataTable GetOrderDetails(int orderId)
        {

            string sql = @"
                SELECT 
                    p.name AS ProductName, 
                    d.quantity, 
                    d.unit_price,
                    (d.quantity * d.unit_price) AS ThanhTien
                FROM order_items d
                JOIN products p ON d.product_id = p.product_id
                WHERE d.order_id = @OrderID";

            MySqlParameter[] para = {
                new MySqlParameter("@OrderID", orderId)
            };

            return db.LoadData(sql, para);
        }
        public DataTable GetStatuses()
        {
            return db.LoadData("SELECT status_id, status_name FROM order_status");
        }
        public void UpdateStatus(int orderId, int statusId)
        {
            string sql = "UPDATE orders SET status_id = @StatusID WHERE order_id = @OrderID";

            MySqlParameter[] para = {
                new MySqlParameter("@StatusID", statusId),
                new MySqlParameter("@OrderID", orderId)
            };

            db.Execute(sql, para);
        }
        public void DeleteOrder(int orderId)
        {
            List<MySqlCommand> cmdList = new List<MySqlCommand>();


            MySqlCommand cmdDetails = new MySqlCommand("DELETE FROM order_items WHERE order_id = @OrderID");
            cmdDetails.Parameters.AddWithValue("@OrderID", orderId);
            cmdList.Add(cmdDetails);
            MySqlCommand cmdOrder = new MySqlCommand("DELETE FROM orders WHERE order_id = @OrderID");
            cmdOrder.Parameters.AddWithValue("@OrderID", orderId);
            cmdList.Add(cmdOrder);
            db.ExecuteTransaction(cmdList);
        }

        public DataTable TimKiemDonHang(string keyword, string tuNgay, string denNgay, int statusId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                SELECT 
                    o.order_id, u.username, o.order_date, o.total_amount, 
                    s.status_name, o.address, o.phone, o.status_id
                FROM orders o
                JOIN users u ON o.user_id = u.user_id
                JOIN order_status s ON o.status_id = s.status_id
                WHERE o.order_date >= @TuNgay AND o.order_date <= @DenNgay");

            List<MySqlParameter> pList = new List<MySqlParameter>();

            pList.Add(new MySqlParameter("@TuNgay", tuNgay + " 00:00:00"));
            pList.Add(new MySqlParameter("@DenNgay", denNgay + " 23:59:59"));

            if (!string.IsNullOrEmpty(keyword))
            {
                sql.Append(" AND (u.username LIKE @Keyword OR o.order_id LIKE @Keyword)");
                pList.Add(new MySqlParameter("@Keyword", "%" + keyword + "%"));
            }

            if (statusId != -1)
            {
                sql.Append(" AND o.status_id = @StatusID");
                pList.Add(new MySqlParameter("@StatusID", statusId));
            }

            return db.LoadData(sql.ToString(), pList.ToArray());
        }
    }
}
