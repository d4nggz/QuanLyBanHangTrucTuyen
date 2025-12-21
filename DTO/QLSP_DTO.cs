using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class QLSP_DTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
