using Newtonsoft.Json;
using System.Collections.Generic;

namespace DTO
{
    public class QLSP_DTO
    {
        // JSON trả về "product_id": "1" -> Tự động ép sang int
        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string ProductName { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("stock_quantity")]
        public int StockQuantity { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        // JSON ghi là "categoryID" (không có dấu gạch dưới)
        [JsonProperty("categoryID")]
        public int CategoryID { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
    }

    // Class phụ để hứng cục to { code, message, data }
    public class ApiResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<QLSP_DTO> data { get; set; }
    }
}