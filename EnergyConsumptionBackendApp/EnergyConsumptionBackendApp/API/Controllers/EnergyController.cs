using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnergyConsumptionBackendApp.API.Controllers
{
    public class EnergyController : ControllerBase
    {
        private readonly IEnergyService m_energyService;

        public EnergyController(IEnergyService energyService)
        {
            m_energyService = energyService;
        }

        [HttpPost]
        [Route("CreateEnergy")]
        public async Task<ActionResult> CreateEnergy([FromBody] EnergyCreateViewModel energyCreateViewModel)
        {
            try
            {
                await m_energyService.CreateEnergyAsync(energyCreateViewModel);
                return Ok();
            }
            catch (Exception exeption)
            {
                return BadRequest(exeption.Message);
            }
        }

        [HttpPost]
        [Route("GetEnergies")]
        public async Task<ActionResult<List<int>>> GetEnergies([FromBody] EnergyGetViewModel energyGetViewModel)
        {
            if (!ModelState.IsValid) {
                Console.WriteLine("invalid request");
            }
            try
            {
                return Ok(await m_energyService.GetEnergiesAsync(energyGetViewModel));
            }
            catch (Exception exeption)
            {
                return BadRequest(exeption.Message);
            }
        }
    }
}
