using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BuyABit.Extensions;
using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace BuyABit.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IDatabaseService _databaseService;
        private readonly AppSettings _appSettings;

        public ApplicationUserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper, IIdentityService identityService,
            IOptions<AppSettings> appSettings, IDatabaseService databaseService)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _mapper = mapper;
            _identityService = identityService;
            _appSettings = appSettings.Value;
            _databaseService = databaseService;
        }

        [HttpPost]
        [Route(nameof(Register))]
        //POST : /ApplicationUser/Register
        public async Task<ActionResult> Register(RegisterRequestModelDTO model)
        {
            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(source: model);

            try
            {
                IdentityResult result = await _userManager.CreateAsync(applicationUser, model.Password);
                return result.Succeeded ? Ok(result) : (ActionResult)BadRequest(result);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route(nameof(Login))]
        //POST : /ApplicationUser/Login
        public async Task<ActionResult<LoginResponseModelDTO>> Login(LoginRequestModelDTO model)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (!await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        return Unauthorized();
                    }

                    string refreshToken = _identityService.GenerateRefreshToken();
                    string token = _identityService.GenerateJwtToken(user.Id, user.UserName, _appSettings);
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                    await _databaseService.UpdateUser(user);

                    return Ok(new LoginResponseModelDTO
                    {
                        Token = token,
                        RefreshToken = refreshToken
                    });
                }
                else
                    return BadRequest("No user found.");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route(nameof(Refresh))]
        public async Task<IActionResult> Refresh(TokenApiModelDTO tokenApiModel)
        {
            if (tokenApiModel is null || tokenApiModel.RefreshToken == null
                || tokenApiModel.AccessToken == null)
            {
                return BadRequest("Invalid client request");
            }
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            ClaimsPrincipal principal = _identityService.GetPrincipalFromExpiredToken(accessToken, _appSettings);
            string username = principal.Identity.Name; //this is mapped to the Name claim by default
            ApplicationUser user = await _userManager.FindByEmailAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }

            string newRefreshToken = _identityService.GenerateRefreshToken();
            string newAccessToken = _identityService.GenerateJwtToken(user.Id, user.UserName, _appSettings);
            user.RefreshToken = newRefreshToken;
            await _databaseService.UpdateUser(user);

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route(nameof(Revoke))]
        public async Task<IActionResult> Revoke(string nothing)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (user == null) return BadRequest();
            user.RefreshToken = null;
            _databaseService.UpdateUser(user);

            return NoContent();
        }

        [HttpGet, Authorize]
        [Route(nameof(GetProfile))]
        //GET : /ApplicationUser/GetProfile
        public async Task<ActionResult<LoginResponseModelDTO>> GetProfile()
        {
            ApplicationUser userProfile;
            try
            {
                string userId = this.GetUserProfile(User);
                userProfile = await _userManager.FindByIdAsync(userId);

                if (userProfile == null)
                {
                    Log.Fatal("User profile is empty.");
                    return BadRequest("User profile is empty.");
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                return BadRequest(ex);
            }

            return Ok(_mapper.Map<RegisterRequestModelDTO>(source: userProfile));
        }
        private string GetUserProfile(ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
