using Limupa.DtoLayer.ProductDtos;
using Limupa.UI.Extension;
using Limupa.UI.Services.CatalogServices.CategoryServices;
using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;


namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public ProductController(IHttpClientFactory httpClientFactory,ICategoryService categoryService,IProductService productService)
        {
            this.httpClientFactory = httpClientFactory;
            this.categoryService = categoryService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await productService.GetProductWithCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var values = await categoryService.GetAllCategoryAsync();

            List<SelectListItem> selectListItems = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.CategoryName,
                                                        Value = x.CategoryID
                                                    }).ToList();
            ViewBag.CategoryValues = selectListItems;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            string token = "";

            using (var httpClient = new HttpClient())
            {

                var request = new HttpRequestMessage
                {

                    RequestUri = new Uri("http://localhost:5001/connect/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"client_id","LimupaMemberId"},
                        {"client_secret","deneme"},
                        {"grant_type","client_credentials"},
                    })
                };

                using (var response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var tokenResponse = JObject.Parse(content);
                        token = tokenResponse["access_token"]!.ToString();
                    }

                }

            }

            if (createProductDto.Photo != null && createProductDto.Photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createProductDto.Photo.CopyToAsync(memoryStream);
                }
            }

            // Burada product nesnesini ve dosyayı API'ye gönder
            using (var client = httpClientFactory.CreateClient())
            {
                // API endpoint'
                string apiEndpoint = "http://localhost:5294/api/Products";

                // Ürün bilgilerini ve dosyayı API'ye göndermek için bir form oluştur
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(createProductDto.ProductName), "ProductName");
                    content.Add(new StringContent(createProductDto.ProductDescription), "ProductDescription");
                    content.Add(new StringContent(createProductDto.ProductPrice.ToString()), "ProductPrice");
                    content.Add(new StringContent(createProductDto.ProductImageUrl), "ProductImageUrl");
                    content.Add(new StringContent(createProductDto.CategoryID), "CategoryID");
                    content.Add(new StringContent(createProductDto.SavedFileName!), "SavedFileName");
                    content.Add(new StringContent(createProductDto.SavedUrl!), "SavedUrl");
                    content.Add(new StringContent(Url.FriendlyUrl(createProductDto.ProductName)), "ProductUrlSeo");

                    // Dosyayı da ekleyelim
                    if (createProductDto.Photo != null && createProductDto.Photo.Length > 0)
                    {
                        content.Add(new StreamContent(createProductDto.Photo.OpenReadStream()), "Photo", createProductDto.Photo.FileName);
                    }

                    // POST isteği gönder
                    var response = await client.PostAsync(apiEndpoint, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Başarısızsa, hata mesajını göster
                        TempData["Error"] = "Error creating product: " + response.ReasonPhrase;
                    }
                }
            }

            return RedirectToAction("Index", "Home");           
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var values = await categoryService.GetAllCategoryAsync();

            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID

                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;

            var value = await productService.GetByIdProductAsync(id);

            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.Photo != null && updateProductDto.Photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updateProductDto.Photo.CopyToAsync(memoryStream);
                }
            }

            // Burada product nesnesini ve dosyayı API'ye gönder
            using (var client = httpClientFactory.CreateClient())
            {
                // API endpoint'i
                string apiEndpoint ="https://localhost:5294/api/Products";

                // Ürün bilgilerini ve dosyayı API'ye göndermek için bir form oluştur
                using (var content = new MultipartFormDataContent())
                {

                    content.Add(new StringContent(updateProductDto.ProductID), "ProductID");
                    content.Add(new StringContent(updateProductDto.ProductName), "ProductName");
                    content.Add(new StringContent(updateProductDto.ProductPrice.ToString()), "ProductPrice");
                    content.Add(new StringContent(updateProductDto.ProductDescription), "ProductDescription");
                    content.Add(new StringContent(updateProductDto.ProductImageUrl), "ProductImageUrl");
                    content.Add(new StringContent(updateProductDto.CategoryID), "CategoryID");
                    content.Add(new StringContent(updateProductDto.SavedFileName!), "SavedFileName");
                    content.Add(new StringContent(updateProductDto.SavedUrl!), "SavedUrl");
                    content.Add(new StringContent(Url.FriendlyUrl(updateProductDto.ProductName)), "ProductUrlSeo");

                    // Dosyayı da ekleyelim
                    if (updateProductDto.Photo != null && updateProductDto.Photo.Length > 0)
                    {
                        content.Add(new StreamContent(updateProductDto.Photo.OpenReadStream()), "Photo", updateProductDto.Photo.FileName);
                    }

                    // PUT isteği gönder
                    var response = await client.PutAsync(apiEndpoint, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Başarısızsa, hata mesajını göster
                        TempData["Error"] = "Error updating product: " + response.ReasonPhrase;
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
