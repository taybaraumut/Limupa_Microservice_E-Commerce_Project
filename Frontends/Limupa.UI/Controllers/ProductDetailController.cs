using Limupa.DtoLayer.UserCommentDtos;
using Limupa.UI.Services.CatalogServices.ProductDetailService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Limupa.UI.Controllers
{
    public class ProductDetailController : Controller
    {
        private IHttpClientFactory httpClientFactory;
        private readonly IProductDetailService productDetailService;

        public ProductDetailController(IHttpClientFactory httpClientFactory, IProductDetailService productDetailService)
        {
            this.httpClientFactory = httpClientFactory;
            this.productDetailService = productDetailService;
        }

        public async Task<IActionResult> Index(string id)
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

            if (id != null)
            {
                var client_one = httpClientFactory.CreateClient();
                client_one.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage_one = await client_one.GetAsync($"http://localhost:5294/api/ProductDetails/ProductDetailByProductId/{id}");

                if (responseMessage_one.IsSuccessStatusCode)
                {
                    if (responseMessage_one.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Json("Access Failed");
                    }

                    ViewBag.x = id;
                    return View();
                }

            }

            return RedirectToAction("Index", "Default");
        }

        [NonAction]
        public PartialViewResult CreateUserComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserComment(CreateUserCommentDto createUserCommentDto)
        {
            createUserCommentDto.UserCommentCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            var client = httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUserCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44337/api/UserComments", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { id = createUserCommentDto.ProductID });
            }
            return View();
        }
    }
}
