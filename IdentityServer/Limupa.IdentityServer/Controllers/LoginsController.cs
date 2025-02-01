using Limupa.IdentityServer.Dtos;
using Limupa.IdentityServer.Models;
using Limupa.IdentityServer.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Limupa.IdentityServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, true, true);
                var user = await userManager.FindByNameAsync(userLoginDto.Username);
                if (result.Succeeded)
                {
                    GetCheckAppUserViewModel getCheckAppUserViewModel = new GetCheckAppUserViewModel();
                    getCheckAppUserViewModel.Username = userLoginDto.Username;
                    getCheckAppUserViewModel.ID = user.Id;
                    getCheckAppUserViewModel.Role = "Admin";
                    var token = JwtTokenGenerator.GeneratorToken(getCheckAppUserViewModel);
                    return Ok(userLoginDto);
                }
                else
                {
                    return Ok("There is an error in the password or password");
                }
            }
            return BadRequest();
        }
    }
}
