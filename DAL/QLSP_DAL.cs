using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DAL
{
    public class QLSP_DAL
    {
        Connect db = new Connect();
        private readonly string BASE_URL = "http://localhost:8080";

        // 1. Hàm Upload ảnh: Trả về URL ảnh trên server
        public async Task<string> UploadImageAsync(string localFilePath)
        {
            if (string.IsNullOrEmpty(localFilePath) || !File.Exists(localFilePath))
                return "";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Chuẩn bị form-data
                    using (var form = new MultipartFormDataContent())
                    {
                        // Đọc file từ máy tính
                        using (var fileStream = File.OpenRead(localFilePath))
                        using (var content = new StreamContent(fileStream))
                        {
                            // Set header cho file (quan trọng)
                            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

                            // "file" phải trùng tên với @RequestParam("file") bên Java
                            form.Add(content, "file", Path.GetFileName(localFilePath));

                            // Gọi API Upload
                            var response = await client.PostAsync($"{BASE_URL}/public/files/upload", form);

                            if (response.IsSuccessStatusCode)
                            {
                                // Trả về đường dẫn ảnh (VD: /images/abc.jpg)
                                return await response.Content.ReadAsStringAsync();
                            }
                        }
                    }
                }
                catch { return ""; }
            }
            return "";
        }

        // 2. Hàm Thêm sản phẩm (Sửa lại để nhận link ảnh server)
        public async Task<bool> InsertProductAsync(QLSP_DTO p, string localImagePath)
        {
            // ... (Phần upload ảnh giữ nguyên) ...
            string serverImageUrl = "";
            if (!string.IsNullOrEmpty(localImagePath) && File.Exists(localImagePath))
            {
                serverImageUrl = await UploadImageAsync(localImagePath);
            }
            p.ImageUrl = !string.IsNullOrEmpty(serverImageUrl) ? serverImageUrl : "";

            using (HttpClient client = new HttpClient())
            {
                var productData = new
                {
                    name = p.ProductName,
                    price = p.Price,
                    stock_quantity = p.StockQuantity,

                    // --- SỬA CHỖ NÀY ---
                    // Phải đổi tên biến từ "id" thành "categoryID" cho khớp với Java CategoryRequest
                    categoryRequest = new
                    {
                        categoryID = p.CategoryID // Lưu ý: Property này lấy từ DTO của bạn (thường là CategoryId)
                    },
                    // -------------------

                    description = p.Description,
                    image_url = p.ImageUrl
                };

                string json = JsonConvert.SerializeObject(productData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Gọi đúng đường dẫn public
                var response = await client.PostAsync($"{BASE_URL}/public/products", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errorBody = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Lỗi Server ({response.StatusCode}): {errorBody}");
                }
            }
        }
        public async Task<List<QLSP_DTO>> GetProductsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Đường dẫn mới theo bạn cung cấp: /public/products
                    HttpResponseMessage response = await client.GetAsync("http://localhost:8080/public/products");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        // BƯỚC QUAN TRỌNG: Deserialize vào ApiResponse trước
                        var rootData = JsonConvert.DeserializeObject<ApiResponse>(json);

                        // Sau đó mới lấy .data (là List sản phẩm) để trả về
                        return rootData != null ? rootData.data : null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi DAL: " + ex.Message);
                }
                return null;
            }
        }
        public DataTable GetCategories()
        {
            // Nhớ dùng đúng tên bảng và tên cột trong SQL của bạn
            return db.LoadData("SELECT category_id, name FROM categories");
        }
    }
}
