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

        public DataTable GetAllCategories() => dal.GetAllCategories();
        public void AddCategory(QLDM_DTO cat) => dal.InsertCategory(cat);
        public void UpdateCategory(QLDM_DTO cat) => dal.UpdateCategory(cat);
        public void DeleteCategory(int id) => dal.DeleteCategory(id);
        public DataTable SearchCategory(string keyword) => dal.SearchCategory(keyword);
    }
}
