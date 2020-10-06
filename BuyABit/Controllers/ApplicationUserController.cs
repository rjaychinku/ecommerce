using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BuyABit.Extensions;
using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        private readonly AppSettings _appSettings;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IMapper mapper, IIdentityService identityService, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _mapper = mapper;
            _identityService = identityService;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        //POST : /ApplicationUser/Register
        public async Task<ActionResult> Register(RegisterRequestModelDTO model)
        {
            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(source: model);

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                return result.Succeeded ? Ok(result) : (ActionResult)BadRequest(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModelDTO>> Login(LoginRequestModelDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    return Unauthorized();
                }

                return Ok(new LoginResponseModelDTO
                {
                    Token = _identityService.GenerateJwtToken(user.Id, user.UserName, _appSettings)
                });
            }
            return BadRequest("No user found.");
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(GetProfile))]
        public async Task<ActionResult<LoginResponseModelDTO>> GetProfile()
        {
            string userId = GetUserProfile(User);
            ApplicationUser userProfile = await _userManager.FindByIdAsync(userId);

            if (userProfile == null)
            {
                return BadRequest("User profile is empty.");
            }

            return Ok(_mapper.Map<RegisterRequestModelDTO>(source: userProfile));
        }
        private string GetUserProfile(ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
