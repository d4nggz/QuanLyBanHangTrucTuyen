using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BUS
{
    public class ThanhToan_BUS
    {
        ThanhToan_DAL dal = new ThanhToan_DAL();

        public DataTable GetMethods() => dal.GetMethods();
        public DataTable GetStatuses() => dal.GetStatuses();

        public List<ThanhToan_DTO> SearchPayment(string orderIdStr, int methodId, int statusId)
        {
            return dal.SearchPayments(orderIdStr, methodId, statusId);
        }

        public bool UpdatePayment(int orderId, int methodId, int statusId)
        {
            return dal.UpdatePayment(orderId, methodId, statusId);
        }
    }
}
