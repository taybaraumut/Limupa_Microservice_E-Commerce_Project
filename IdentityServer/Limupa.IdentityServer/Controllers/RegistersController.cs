using Limupa.IdentityServer.Dtos;
using Limupa.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Limupa.IdentityServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public RegistersController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> VisitorRegister(UserRegisterDto userRegisterDto)
        {
            var values = new ApplicationUser()
            {
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,               
            };
            var result = await userManager.CreateAsync(values, userRegisterDto.Password);
            if (result.Succeeded)
            {

                var role = new IdentityRole("Admin");

                await roleManager.CreateAsync(role);

                var user_info = await userManager.FindByEmailAsync(values.Email);

                await userManager.AddToRoleAsync(user_info,"Admin");

                return Ok("Successful");
            }
            else
            {
                return Ok("Danger");
            }
        }       
    }
}
