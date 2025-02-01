using Limupa.DtoLayer.UserCommentDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserCommentController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UserCommentController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44337/api/UserComments");

            if (responseMessage.IsSuccessStatusCode)
            {
                var body = await responseMessage.Content.ReadAsStringAsync();
                var jsondata = JsonConvert.DeserializeObject<List<ResultUserCommentDto>>(body);

                return View(jsondata);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserComment(string id)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44337/api/UserComments/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
