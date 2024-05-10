using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDBIdentity.Models;
using MongoDBIdentity.Models.ViewModels;

namespace MongoDBIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<ApplicationUsers> userManager;
        private SignInManager<ApplicationUsers> signInManager;

        private RoleManager<ApplicationRoles> roleManager;

        public UsersController(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUsers appUser = new ApplicationUsers
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                var userResponse = userManager.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (result.Succeeded)
                    return Ok(userResponse);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUsers appUser = await userManager.FindByEmailAsync(user.Email);

                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, user.Password, false, false);
                if (result.Succeeded)
                    return Ok(appUser);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { Message = "Logout successful" });
        }
    }
}