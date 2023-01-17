using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnergyConsumptionBackendApp.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService m_userService;

        public UserController(IUserService userService)
        {
            m_userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsersDetails")]
        public async Task<ActionResult<List<UserDetailsViewModel>>> GetAllUsersDetails()
        {
            return Ok(await m_userService.GetAllUsersDetailsAsync());
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<ActionResult<List<UserDetailsViewModel>>> GetUserById([FromQuery] UserGetByIdViewModel userGetByIdViewModel)
        {
            try
            {
                return Ok(await m_userService.GetUserDetailsByIdAsync(userGetByIdViewModel));
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            } 
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserUpdateViewModel userUpdateViewModel)
        {
            try
            {
                await m_userService.UpdateUserAsync(userUpdateViewModel);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult> DeleteUserById([FromBody] UserDeleteViewModel userDeleteViewModel)
        {
            try
            {
                await m_userService.DeleteUserById(userDeleteViewModel);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
