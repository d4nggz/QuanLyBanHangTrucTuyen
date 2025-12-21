using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BUS
{
    public class ProductBUS
    {
        QLSP_DAL dal = new QLSP_DAL();

        public DataTable GetAllProducts() => dal.GetAllProducts();
        public DataTable GetAllCategories() => dal.GetCategories();
        public void AddProduct(QLSP_DTO p) => dal.Insert(p);
        public void UpdateProduct(QLSP_DTO p) => dal.Update(p);
        public void DeleteProduct(int id) => dal.Delete(id);
    }
}
