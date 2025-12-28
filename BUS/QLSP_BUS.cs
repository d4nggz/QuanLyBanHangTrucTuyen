using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks; // Bắt buộc phải có thư viện này để dùng Task

namespace BUS
{
    public class ProductBUS
    {
        
        private QLSP_DAL dal = new QLSP_DAL();

    
        public async Task<List<QLSP_DTO>> GetAllProducts()
        {
            return await dal.GetProductsAsync();
        }

        public async Task<bool> AddProduct(QLSP_DTO p, string localImagePath)
        {
   
            if (p.Price < 0) return false; 
            if (p.StockQuantity < 0) return false;

       
            return await dal.InsertProductAsync(p, localImagePath);
        }

        public async Task<bool> UpdateProduct(QLSP_DTO p, string localImagePath)
        {
            
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            if (id <= 0) return false;
            return await dal.DeleteProductAsync(id);
        }
        public DataTable GetAllCategories()
        {
            return dal.GetCategories();
        }
    }
}