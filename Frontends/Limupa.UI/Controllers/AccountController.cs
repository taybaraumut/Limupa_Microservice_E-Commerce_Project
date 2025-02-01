using Limupa.DtoLayer.AccountDtos;
using Limupa.DtoLayer.DataDtos;
using Limupa.UI.Services.Concrete;
using Limupa.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace Limupa.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IAccountLoginService accountLoginService;
        private readonly IIdentityService ıdentityService;
        private readonly IUserService userService;

        public AccountController(IHttpClientFactory httpClientFactory, IAccountLoginService accountLoginService, IIdentityService ıdentityService, IUserService userService)
        {
            this.httpClientFactory = httpClientFactory;
            this.accountLoginService = accountLoginService;
            this.ıdentityService = ıdentityService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterDto accountRegisterDto)
        {
            if (accountRegisterDto.Password == accountRegisterDto.ConfirmPassword)
            {
                var client = httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(accountRegisterDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("http://localhost:5001/api/Registers", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginDto accountLoginDto)
        {
            //accountLoginDto.Username = "umuttaybara62";
            //accountLoginDto.Password = "Nf[eXI35$rE.C4VGS8Qb1=A$";
            var status = await ıdentityService.SignIn(accountLoginDto);
            if (status == false)
            {
                ViewBag.account_error = "Email Veya Şifreniz Hatalı";
                return View();
            }
            return RedirectToAction("User", "Account");
        }
        [HttpGet]
        public async Task<IActionResult> User()
        {
            var values = await userService.GetUserInfo();
            return View(values);
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(AccountLoginDto accountLoginDto)
        //{
        //    var client = httpClientFactory.CreateClient();
        //    var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(accountLoginDto),Encoding.UTF8,"application/json");
        //    var response = await client.PostAsync("http://localhost:5001/api/Logins",content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var body = await response.Content.ReadAsStringAsync();

        //        var tokenModel = System.Text.Json.JsonSerializer.Deserialize<JwtResponseModel>(body,new JsonSerializerOptions{
        //            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //        });

        //        if (tokenModel != null)
        //        {
        //            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //            var token = jwtSecurityTokenHandler.ReadJwtToken(tokenModel.Token);
        //            var claims = token.Claims.ToList();

        //            if (tokenModel != null)
        //            {
        //                claims.Add(new Claim("limupashop", tokenModel.Token));

        //                var claimsIdentity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);

        //                var authProp = new AuthenticationProperties
        //                {
        //                    ExpiresUtc = tokenModel.ExpireDate,
        //                    IsPersistent = true
        //                };

        //                await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProp);
        //                var id = accountLoginService.GetUserID;
        //                return RedirectToAction("Index", "Default");
        //            }
        //        }
        //    }
        //    return View();
        //}

    }
}





















