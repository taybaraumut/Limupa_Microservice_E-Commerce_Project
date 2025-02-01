using Limupa.DtoLayer.ProductDetailDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductDetailController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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


            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.GetAsync("http://localhost:5294/api/ProductDetails/ProductDetailWithProductList");

            if (responseMessage.IsSuccessStatusCode)
            {
                var body = await responseMessage.Content.ReadAsStringAsync();
                var jsondata = JsonConvert.DeserializeObject<List<GetProductDetailWithProductDto>>(body);
                return View(jsondata);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProductDetail(string id)
        {
            var client_one = httpClientFactory.CreateClient();
            var responseMessage_one = await client_one.GetAsync($"http://localhost:5294/api/ProductDetails/ProductDetailByProductId/{id}");

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
                        var jsondata = JsonConvert.DeserializeObject<GetByIdProductDetailDto>(body);

                        return View(jsondata);
                    }
                }

                var response = TempData["response_alert_productdetail"] = "Tıkladığınız Ürün Bilgileri Daha Önceden Yüklemesi Yapılmış Eğer Ürün Bilgilerini Değiştirecekseniz Lütfen Aşağıdan Ürün Bilgilerini Güncelleyiniz";

                return RedirectToAction("Index", "ProductDetail", new { area = "Admin" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            var client = httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(createProductDetailDto);
            StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5294/api/ProductDetails", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        } 

        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id)
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


            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.GetAsync($"http://localhost:5294/api/ProductDetails/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var body = await responseMessage.Content.ReadAsStringAsync();
                var jsondata = JsonConvert.DeserializeObject<GetByIdProductDetailDto>(body);

                return View(jsondata);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            var client = httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(updateProductDetailDto);
            StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5294/api/ProductDetails", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
