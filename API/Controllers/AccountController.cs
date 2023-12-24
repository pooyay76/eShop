using API.Dtos;
using API.Dtos.Identity;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await GetUserByClaimsPrincipal();
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateAuthToken(user),
                Username = user.UserName
            };
        }


        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "Secret Text";
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
                return Ok(new UserDto()
                {
                    Email = user.Email,
                    Token = _tokenService.CreateAuthToken(user),
                    Username = user.UserName,
                    DisplayName = user.DisplayName
                });
            else
                return Unauthorized(new ApiResponse(401));
        }

        [HttpGet("emailExists")]
        public async Task<IActionResult> EmailExists([FromQuery] string email)
        {
            return Ok(await _userManager.FindByEmailAsync(email) != null);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<IActionResult> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipalWithAddressAsync(User);
            return Ok(_mapper.Map<AddressDto>(user.Address));
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDto address)
        {
            Address newAddress = new()
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                City = address.City,
                PostalCode = address.PostalCode,
                Province = address.Province,
                Street = address.Street
            };
            var user = await _userManager.FindUserByClaimsPrincipalWithAddressAsync(User);
            user.Address = newAddress;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(_mapper.Map<AddressDto>(user.Address));
            else
                return BadRequest("Problem updating the user address");

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = new()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.Username
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded == false)
                return BadRequest(new ApiResponse(401));
            else
                return Ok(new UserDto()
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    Token = _tokenService.CreateAuthToken(user),
                    Username = registerDto.Username
                });
        }
        private async Task<AppUser> GetUserByClaimsPrincipal()
        {
            return await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
        }
    }

}
