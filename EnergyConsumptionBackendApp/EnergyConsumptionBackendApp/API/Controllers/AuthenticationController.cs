using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnergyConsumptionBackendApp.API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService m_authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            m_authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegisterViewModel userRegisterViewModel)
        {
            try
            {
                await m_authenticationService.RegisterUserAsync(userRegisterViewModel);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserDetailsViewModel>> LoginUser([FromBody] UserLoginViewModel userLoginViewModel)
        {
            try
            {
                return Ok(await m_authenticationService.LoginUserAsync(userLoginViewModel));
            }
            catch(Exception exeption)
            {
                return BadRequest(exeption.Message);
            }
        }
    }
}
