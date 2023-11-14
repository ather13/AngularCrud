using CoreCrudApi.Db.Models;
using CoreCrudApi.Helpers;
using CoreCrudApi.Models;
using CoreCrudApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CoreCrudApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtHandler _jwtHandler;
        private readonly IUserService _userService;
        private readonly ITenantService _tenantService;
        public UserController(JwtHandler jwtHandler
            , IUserService userService
            , ITenantService tenantService)
        {
            _jwtHandler = jwtHandler;
            _userService = userService;
            _tenantService = tenantService;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]AuthRequestDto authRequestDto)
        {
            var user = await _userService.Get(authRequestDto.UserName);
            if (user == null || !_userService.VerifyPassword(user, authRequestDto))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var tenant =await _tenantService.Get(user.TenantId);

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user,tenant);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new OkObjectResult(new AuthResponseDto { IsUserAuthenticated = true, Token = token, ExpiresIn=tokenOptions.ValidTo });
        }
    }
}
