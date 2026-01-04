using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using DTO;

namespace DAL
{
    public class ThanhToan_DAL
    {
        Connect db = new Connect();

        public DataTable GetMethods()
        {
            return db.LoadData("SELECT method_id, method_name FROM methods");
        }
        public DataTable GetStatuses()
        {
            return db.LoadData("SELECT statusID, statusName FROM payment_status");
        }

        public List<ThanhToan_DTO> SearchPayments(string orderIdStr, int methodId, int statusId)
        {
            List<ThanhToan_DTO> list = new List<ThanhToan_DTO>();
            string sql = @"
                SELECT p.order_id, p.amount, p.payment_date,
                       p.method_id, m.method_name,
                       p.status_id, ps.statusName
                FROM payments p
                LEFT JOIN methods m ON p.method_id = m.method_id
                LEFT JOIN payment_status ps ON p.status_id = ps.statusID
                WHERE (@oid = '' OR CAST(p.order_id AS CHAR) LIKE @oid)
                  AND (@mid = -1 OR p.method_id = @mid)
                  AND (@sid = -1 OR p.status_id = @sid)";

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@oid", string.IsNullOrEmpty(orderIdStr) ? "" : "%" + orderIdStr + "%");
                    cmd.Parameters.AddWithValue("@mid", methodId);
                    cmd.Parameters.AddWithValue("@sid", statusId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThanhToan_DTO()
                            {
                                OrderId = reader.GetInt32("order_id"),
                                Amount = reader.GetDecimal("amount"),
                                PaymentDate = reader.GetDateTime("payment_date"),

                                MethodId = reader.GetInt32("method_id"),
                                MethodName = reader["method_name"].ToString(),

                                StatusId = reader.GetInt32("status_id"),
                                StatusName = reader["statusName"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool UpdatePayment(int orderId, int methodId, int statusId)
        {
            string sql = "UPDATE payments SET method_id = @mid, status_id = @sid WHERE order_id = @oid";
            MySqlParameter[] p = {
                new MySqlParameter("@mid", methodId),
                new MySqlParameter("@sid", statusId),
                new MySqlParameter("@oid", orderId)
            };
            return db.Execute(sql, p) > 0;
        }
    }
}