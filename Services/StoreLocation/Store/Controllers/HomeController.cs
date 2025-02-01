using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Store.Models;
using System.Diagnostics;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient httpClient;
        private readonly IMemoryCache memoryCache;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            this.httpClient = httpClient;
            this.memoryCache = memoryCache;
        }

        public async ValueTask<IActionResult> Index()
        {
            //var cacheData = memoryCache.Get<IEnumerable<City>>("products");
            //if (cacheData != null)
            //{
            //    return View(cacheData);
            //}

            //var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var response = httpClient.GetFromJsonAsync<List<City>>("https://localhost:44314/storelocationlist").GetAwaiter().GetResult();
            //memoryCache.Set("products", cacheData, expirationTime);
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
