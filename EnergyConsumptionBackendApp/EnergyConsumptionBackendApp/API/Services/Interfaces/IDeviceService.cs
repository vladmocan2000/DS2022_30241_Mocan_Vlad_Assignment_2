using EnergyConsumptionBackendApp.API.ViewModels;

namespace EnergyConsumptionBackendApp.API.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<List<DeviceDetailsViewModel>> GetAllDevicesDetailsAsync();
        Task<List<DeviceDetailsViewModel>> GetUserDevicesDetailsAsync(DeviceGetByUserViewModel deviceGetByUserViewModel);
        Task<DeviceDetailsViewModel> GetDeviceDetailsByIdAsync(DeviceGetByIdViewModel deviceGetByIdViewModel);
        Task UpdateDeviceAsync(DeviceUpdateViewModel deviceUpdateViewModel);
        Task DeleteDeviceAsync(DeviceDeleteViewModel deviceDeleteViewModel);
        Task CreateDeviceAsync(DeviceCreateViewModel deviceCreateViewModel);
    }
}
