using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL;
using DTO;

namespace BUS
{
    public class QLDH_BUS
    {
        private QLDH_DAL dal = new QLDH_DAL();

        public List<OrderDTO> GetOrders()
        {
            DataTable dt = dal.GetOrders();
            List<OrderDTO> listOrders = new List<OrderDTO>();

            foreach (DataRow row in dt.Rows)
            {
                OrderDTO order = new OrderDTO();

                order.OrderId = Convert.ToInt32(row["order_id"]);
                order.CustomerName = row["username"].ToString();
                order.OrderDate = Convert.ToDateTime(row["order_date"]);

                order.TotalAmount = row["total_amount"] != DBNull.Value ? Convert.ToDecimal(row["total_amount"]) : 0;

                order.StatusId = Convert.ToInt32(row["status_id"]);
                order.StatusName = row["status_name"].ToString();
                order.Address = row["address"].ToString();
                order.Phone = row["phone"].ToString();

                listOrders.Add(order);
            }

            return listOrders;
        }

        public DataTable GetOrderDetails(int orderId)
        {
            return dal.GetOrderDetails(orderId);
        }

        public DataTable GetStatuses()
        {
            return dal.GetStatuses();
        }

        public bool UpdateStatus(int orderId, int statusId)
        {
            try
            {
                dal.UpdateStatus(orderId, statusId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOrder(int orderId)
        {
            try
            {
                dal.DeleteOrder(orderId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<OrderDTO> TimKiemDonHang(string keyword, DateTime dateTuNgay, DateTime dateDenNgay, int statusId)
        {
            // Logic đảo ngày
            if (dateTuNgay > dateDenNgay)
            {
                DateTime temp = dateTuNgay;
                dateTuNgay = dateDenNgay;
                dateDenNgay = temp;
            }

            string strTuNgay = dateTuNgay.ToString("yyyy-MM-dd");
            string strDenNgay = dateDenNgay.ToString("yyyy-MM-dd");

            DataTable dt = dal.TimKiemDonHang(keyword, strTuNgay, strDenNgay, statusId);

            List<OrderDTO> listResults = new List<OrderDTO>();
            foreach (DataRow row in dt.Rows)
            {
                OrderDTO order = new OrderDTO
                {
                    OrderId = Convert.ToInt32(row["order_id"]),
                    CustomerName = row["username"].ToString(),
                    OrderDate = Convert.ToDateTime(row["order_date"]),
                    TotalAmount = row["total_amount"] != DBNull.Value ? Convert.ToDecimal(row["total_amount"]) : 0,
                    StatusId = Convert.ToInt32(row["status_id"]),
                    StatusName = row["status_name"].ToString(),
                    Address = row["address"].ToString(),
                    Phone = row["phone"].ToString()
                };
                listResults.Add(order);
            }

            return listResults;
        }
    }
}