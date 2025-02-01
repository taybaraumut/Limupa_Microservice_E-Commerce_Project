using Limupa.DtoLayer.ProductDtos;
using Limupa.DtoLayer.ProductImageDtos;
using Limupa.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:5294/api/ProductImages/ProductImageWithProductList");

            if (responseMessage.IsSuccessStatusCode)
            {
                var body = await responseMessage.Content.ReadAsStringAsync();
                var jsondata = JsonConvert.DeserializeObject<List<ResultProductImageWithProductDto>>(body);
                return View(jsondata);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProductImage(string id)
        {
            var client_one = httpClientFactory.CreateClient();
            var responseMessage_one = await client_one.GetAsync($"http://localhost:5294/api/ProductImages/ProductImageByProductIdCheck/{id}");

            if (responseMessage_one.IsSuccessStatusCode)
            {

                if (responseMessage_one.StatusCode == HttpStatusCode.NoContent)
                {
                    var client_two = httpClientFactory.CreateClient();
                    var responseMessage_two = await client_two.GetAsync($"http://localhost:5294/api/Products/{id}");

                    if (responseMessage_two.IsSuccessStatusCode)
                    {
                        if (responseMessage_two.Content == null)
                        {
                            return View();
                        }

                        var body = await responseMessage_two.Content.ReadAsStringAsync();
                        var jsondata = JsonConvert.DeserializeObject<GetByIdProductDto>(body);

                        return View(jsondata);
                    }
                }

                TempData["response_alert_productımage"] = "Tıkladığınız Ürüne Daha Önceden Görsel Yüklemesi Yapılmış Eğer Ürün Görsellerini Değiştirecekseniz Lütfen Aşağıdan Ürün Görsellerini Güncelleyiniz";

                return RedirectToAction("Index", "ProductImage", new { area = "Admin" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            using (var client = httpClientFactory.CreateClient())
            {
                // API endpoint'i
                string apiEndpoint = "http://localhost:5294/api/ProductImages";

                // Ürün bilgilerini ve dosyayı API'ye göndermek için bir form oluştur
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(createProductImageDto.ProductUrlSeo), "ProductUrlSeo");
                    content.Add(new StringContent(createProductImageDto.ProductID), "ProductID");

                    // List<StringContent> olarak eklenecek string listeleri için bir yardımcı metod
                    void AddStringContentList(List<string> items, string name)
                    {
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                content.Add(new StringContent(item), name);
                            }
                        }
                    }

                    // String listelerini ekle
                    AddStringContentList(createProductImageDto.ProductBigImageUrl, "ProductBigImageUrl");
                    AddStringContentList(createProductImageDto.ProductSmallImageUrl, "ProductSmallImageUrl");
                    AddStringContentList(createProductImageDto.BigSavedUrl!, "BigSavedUrl");
                    AddStringContentList(createProductImageDto.BigSavedFileName!, "BigSavedFileName");
                    AddStringContentList(createProductImageDto.SmallSavedUrl!, "SmallSavedUrl");
                    AddStringContentList(createProductImageDto.SmallSavedFileName!, "SmallSavedFileName");

                    // List<IFormFile> olarak eklenecek dosyalar için bir yardımcı metod
                    void AddFileContentList(List<IFormFile>? files, string name)
                    {
                        if (files != null)
                        {
                            foreach (var file in files)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), name, file.FileName);
                            }
                        }
                    }

                    // Dosya listelerini ekle
                    AddFileContentList(createProductImageDto.BigPhoto, "BigPhoto");
                    AddFileContentList(createProductImageDto.SmallPhoto, "SmallPhoto");

                    // PUT isteği gönder
                    var response = await client.PostAsync(apiEndpoint, content);
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
        public async Task<IActionResult> UpdateProductImage(string id)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5294/api/ProductImages/ProductImageByProductId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var body = await responseMessage.Content.ReadAsStringAsync();
                var jsondata = JsonConvert.DeserializeObject<GetByIdProductImageDto>(body);
                return View(jsondata);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            using (var client = httpClientFactory.CreateClient())
            {
                // API endpoint'i
                string apiEndpoint = "http://localhost:5294/api/ProductImages";

                // Ürün bilgilerini ve dosyayı API'ye göndermek için bir form oluştur
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(updateProductImageDto.ProductImageID), "ProductImageID");
                    content.Add(new StringContent(updateProductImageDto.ProductID), "ProductID");

                    // List<StringContent> olarak eklenecek string listeleri için bir yardımcı metod
                    void AddStringContentList(List<string> items, string name)
                    {
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                content.Add(new StringContent(item), name);
                            }
                        }
                    }

                    // String listelerini ekle
                    AddStringContentList(updateProductImageDto.ProductBigImageUrl, "ProductBigImageUrl");
                    AddStringContentList(updateProductImageDto.ProductSmallImageUrl, "ProductSmallImageUrl");
                    AddStringContentList(updateProductImageDto.BigSavedUrl!, "BigSavedUrl");
                    AddStringContentList(updateProductImageDto.BigSavedFileName!, "BigSavedFileName");
                    AddStringContentList(updateProductImageDto.SmallSavedUrl!, "SmallSavedUrl");
                    AddStringContentList(updateProductImageDto.SmallSavedFileName!, "SmallSavedFileName");

                    // List<IFormFile> olarak eklenecek dosyalar için bir yardımcı metod
                    void AddFileContentList(List<IFormFile>? files, string name)
                    {
                        if (files != null)
                        {
                            foreach (var file in files)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), name, file.FileName);
                            }
                        }
                    }

                    // Dosya listelerini ekle
                    AddFileContentList(updateProductImageDto.BigPhoto, "BigPhoto");
                    AddFileContentList(updateProductImageDto.SmallPhoto, "SmallPhoto");

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
    }
}