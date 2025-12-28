using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BUS
{
    public class BUS_Product
    {
        DAL_Product dal = new DAL_Product();

        public DataTable GetAllCategories()
        {
            return dal.GetCategoryList();
        }

        public DataTable GetReportData(int categoryID)
        {
            return dal.GetProductsByCatID(categoryID);
        }
    }
}
