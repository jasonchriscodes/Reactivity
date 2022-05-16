using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
 [AllowAnonymous]
 [ApiController]
 [Route("api/[controller]")]

 public class AccountController : ControllerBase
 {
  private readonly SignInManager<AppUser> signInManager;
  private readonly UserManager<AppUser> userManager;
  private readonly TokenService tokenService;
  public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
  {
   this.tokenService = tokenService;
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
     Token = this.tokenService.CreateToken(user),
     Image = null
    };
   }

   return Unauthorized();
  }
 }
}