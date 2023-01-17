using EnergyConsumptionBackendApp.API.Services;
using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnergyConsumptionBackendApp.API.Controllers
{
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService m_deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            m_deviceService = deviceService;
        }

        [HttpGet]
        [Route("GetAllDevicesDetails")]
        public async Task<ActionResult<List<DeviceDetailsViewModel>>> GetAllDevicesDetails()
        {
            return Ok(await m_deviceService.GetAllDevicesDetailsAsync());
        }

        [HttpGet]
        [Route("GetUserDevicesDetails")]
        public async Task<ActionResult<List<DeviceDetailsViewModel>>> GetUserDevicesDetails([FromQuery] DeviceGetByUserViewModel deviceGetByUserViewModel)
        {
            return Ok(await m_deviceService.GetUserDevicesDetailsAsync(deviceGetByUserViewModel));
        }

        [HttpGet]
        [Route("GetDeviceById")]
        public async Task<ActionResult<List<DeviceDetailsViewModel>>> GetDeviceById([FromQuery] DeviceGetByIdViewModel deviceGetByIdViewModel)
        {
            try
            {
                return Ok(await m_deviceService.GetDeviceDetailsByIdAsync(deviceGetByIdViewModel));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("CreateDevice")]
        public async Task<ActionResult> CreateDevice([FromBody] DeviceCreateViewModel deviceCreateViewModel)
        {
            try
            {
                await m_deviceService.CreateDeviceAsync(deviceCreateViewModel);
                return Ok();
            }
            catch(Exception exeption)
            {
                return BadRequest(exeption.Message);
            }
        }

        [HttpPut]
        [Route("UpdateDevice")]
        public async Task<ActionResult> UpdateDevice([FromBody] DeviceUpdateViewModel deviceUpdateViewModel)
        {
            try
            {
                await m_deviceService.UpdateDeviceAsync(deviceUpdateViewModel);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteDevice")]
        public async Task<ActionResult> DeleteUserById([FromBody] DeviceDeleteViewModel deviceDeleteViewModel)
        {
            try
            {
                await m_deviceService.DeleteDeviceAsync(deviceDeleteViewModel);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
