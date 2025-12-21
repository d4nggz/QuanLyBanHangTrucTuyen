using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DAL;

namespace BUS
{
    public class QLDH_BUS
    {
        QLDH_DAL dal = new QLDH_DAL();
        public DataTable GetAllOrders() => dal.GetOrders();
        public DataTable GetOrderDetails(int orderId) => dal.GetOrderDetails(orderId);
        public DataTable GetAllStatuses() => dal.GetStatuses();
        public void UpdateOrderStatus(int orderId, int statusId) => dal.UpdateStatus(orderId, statusId);
        public void XoaDonHang(int orderId)
        {
            dal.DeleteOrder(orderId);
        }
        public DataTable TimKiem(string keyword, DateTime tuNgay, DateTime denNgay, int statusId)
        {
            return dal.TimKiemDonHang(keyword, tuNgay, denNgay, statusId);
        }
    }
}
