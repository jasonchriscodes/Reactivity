using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
  [HttpPost("register")]
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
   if (await this.userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
   {
    return BadRequest("Email already exists");
   }
   if (await this.userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
   {
    return BadRequest("Username already exists");
   }

   var user = new AppUser
   {
    DisplayName = registerDto.DisplayName,
    Email = registerDto.Email,
    UserName = registerDto.Username
   };

   var result = await this.userManager.CreateAsync(user, registerDto.Password);

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

   return BadRequest("Problem registering user");
  }
 }
}