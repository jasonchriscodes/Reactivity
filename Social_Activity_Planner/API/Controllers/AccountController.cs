using API.DTOs;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
 [ApiController]
 [Route("api/[controller]")]

 public class AccountController : ControllerBase
 {
  private readonly SignInManager<AppUser> signInManager;
  private readonly UserManager<AppUser> userManager;
  public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
  {
   this.userManager = userManager;
   this.signInManager = signInManager;
  }

  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
   var user = await this.userManager.FindByEmailAsync(loginDto.Email);

   if (user == null) return Unauthorized();

   var result = await this.signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

   if (result.Succeeded)
   {
    return new UserDto
    {
     DisplayName = user.DisplayName,
     Username = user.UserName,
     Token = "This will be a token",
     Image = null
    };
   }

   return Unauthorized();
  }
 }
}