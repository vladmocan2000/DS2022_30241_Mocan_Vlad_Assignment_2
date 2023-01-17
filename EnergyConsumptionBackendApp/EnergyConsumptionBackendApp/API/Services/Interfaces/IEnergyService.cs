using EnergyConsumptionBackendApp.API.ViewModels;

namespace EnergyConsumptionBackendApp.API.Services.Interfaces
{
    public interface IEnergyService
    {
        Task CreateEnergyAsync(EnergyCreateViewModel energyCreateViewModel);
        Task<List<int>> GetEnergiesAsync(EnergyGetViewModel energyGetViewModel);
    }
}
