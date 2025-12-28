using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DAL;
using DTO;

namespace BUS
{
    public class CategoryBUS
    {
        QLDM_DAL dal = new QLDM_DAL();

        public List<QLDM_DTO> GetAllCategories()
        {
            DataTable dt = dal.GetCategories();
            List<QLDM_DTO> list = new List<QLDM_DTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new QLDM_DTO
                {
                    CategoryId = Convert.ToInt32(row["category_id"]),
                    Name = row["name"].ToString(),
                    Description = row["description"].ToString()
                });
            }
            return list;
        }
        public string AddCategory(QLDM_DTO cat)
        {
            if (string.IsNullOrWhiteSpace(cat.Name)) return "Tên danh mục không được để trống!";
            if (dal.CheckNameExists(cat.Name)) return "Tên danh mục đã tồn tại!";

            try
            {
                dal.InsertCategory(cat);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public bool UpdateCategory(QLDM_DTO cat)
        {
            if (string.IsNullOrWhiteSpace(cat.Name)) return false;
            try
            {
                dal.UpdateCategory(cat);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string DeleteCategory(int id)
        {
            try
            {
                dal.DeleteCategory(id);
                return "Success";
            }
            catch
            {
                return "Không thể xóa! Danh mục đang chứa sản phẩm.";
            }
        }
        public List<QLDM_DTO> SearchCategory(string keyword)
        {
            DataTable dt = dal.SearchCategory(keyword);
            List<QLDM_DTO> list = new List<QLDM_DTO>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new QLDM_DTO
                {
                    CategoryId = Convert.ToInt32(row["category_id"]),
                    Name = row["name"].ToString(),
                    Description = row["description"].ToString()
                });
            }
            return list;
        }
    }
}
