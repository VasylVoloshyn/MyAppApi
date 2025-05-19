using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.DTO.Authentication;
using MyApp.Infrastructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationController(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var roleExists = await _roleManager.RoleExistsAsync(request.Role);
        if (!roleExists)
            return BadRequest($"Role '{request.Role}' does not exist.");

        await _userManager.AddToRoleAsync(user, request.Role);

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        TokenRequestDto tokenRequestDto = new TokenRequestDto()
        {
            Email = request.Email,
            UserId = user.Id,
            Roles = roles
        };

        var token = _jwtTokenService.GenerateToken(tokenRequestDto);
        return Ok(new { token });
    }
}
