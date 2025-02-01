using Limupa.DtoLayer.ContactDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:5294/api/Contacts");

            if (responseMessage.IsSuccessStatusCode)
            {
                var body = await responseMessage.Content.ReadAsStringAsync();
                var jsondata = JsonConvert.DeserializeObject<List<ResultContactDto>>(body);

                return View(jsondata);
            }
            return View();
        }
       
        [HttpGet]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:5294/api/Contacts/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
