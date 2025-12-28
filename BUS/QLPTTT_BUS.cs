using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static DAL.QLPTTT_DAL;

namespace BUS
{
    public class QLPTTT_BUS
    {
        QLPTTT_DAL dal = new QLPTTT_DAL();

        public List<QLPTTT_DTO> GetAll()
        {
            DataTable dt = dal.GetAllMethods();
            List<QLPTTT_DTO> list = new List<QLPTTT_DTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new QLPTTT_DTO
                {
                    PaymentMethodId = Convert.ToInt32(row["methodID"]),
                    MethodName = row["methodName"].ToString()
                });
            }
            return list;
        }
        public string Add(QLPTTT_DTO pttt)
        {
            if (string.IsNullOrWhiteSpace(pttt.MethodName)) return "Tên phương thức không được để trống!";
            if (dal.CheckNameExist(pttt.MethodName)) return "Phương thức này đã tồn tại!";

            try
            {
                dal.InsertMethod(pttt);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public bool Update(QLPTTT_DTO pttt)
        {
            if (string.IsNullOrWhiteSpace(pttt.MethodName)) return false;
            try
            {
                dal.UpdateMethod(pttt);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string Delete(int id)
        {
            try
            {
                dal.DeleteMethod(id);
                return "Success";
            }
            catch
            {
                return "Không thể xóa vì phương thức này đã được sử dụng trong các giao dịch!";
            }
        }
    }
}
