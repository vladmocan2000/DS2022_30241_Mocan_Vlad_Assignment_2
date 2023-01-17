using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using EnergyConsumptionBackendApp.Core.Models;
using EnergyConsumptionBackendApp.Data.Repositories.Interfaces;
using EnergyConsumptionBackendApp.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionBackendApp.API.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepository<Device> m_deviceRepository;
        private readonly IRepository<User> m_userRepository;

        public DeviceService(IRepository<Device> deviceRepository, IRepository<User> userRepository)
        {
            m_deviceRepository = deviceRepository;
            m_userRepository = userRepository;
        }
        public async Task<List<DeviceDetailsViewModel>> GetAllDevicesDetailsAsync()
        {
            var devices = await (m_deviceRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.User)).ToListAsync();

            var devicesDetailsViewModel = new List<DeviceDetailsViewModel>();

            foreach (var device in devices)
            {
                devicesDetailsViewModel.Add(new DeviceDetailsViewModel
                {
                    Id = device.Id,
                    Name = device.Name,
                    Description = device.Description,
                    Address = device.Address,
                    MaximumHourlyEnergyConsumption = device.MaximumHourlyEnergyConsumption,
                    Username = device.User.Username
                });
            }

            return devicesDetailsViewModel;
        }

        public async Task<List<DeviceDetailsViewModel>> GetUserDevicesDetailsAsync(DeviceGetByUserViewModel deviceGetByUserViewModel)
        {
            var devices = (m_deviceRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.User)).GetWithFilter(x => x.User.Username == deviceGetByUserViewModel.Username).ToList();

            var devicesDetailsViewModel = new List<DeviceDetailsViewModel>();

            foreach (var device in devices)
            {
                devicesDetailsViewModel.Add(new DeviceDetailsViewModel
                {
                    Id = device.Id,
                    Name = device.Name,
                    Description = device.Description,
                    Address = device.Address,
                    MaximumHourlyEnergyConsumption = device.MaximumHourlyEnergyConsumption,
                    Username = device.User.Username
                });
            }

            return devicesDetailsViewModel;
        }

        public async Task<DeviceDetailsViewModel> GetDeviceDetailsByIdAsync(DeviceGetByIdViewModel deviceGetByIdViewModel)
        {
            var device = (m_deviceRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.User)).GetWithFilter(x => x.Id == deviceGetByIdViewModel.Id).FirstOrDefault();
            if (device == null)
            {
                throw new Exception("Device not found!");
            }

            return new DeviceDetailsViewModel
            {
                Id = device.Id,
                Name = device.Name,
                Description = device.Description,
                Address = device.Address,
                MaximumHourlyEnergyConsumption = device.MaximumHourlyEnergyConsumption,
                Username = device.User.Username
            };
        }

        public async Task CreateDeviceAsync(DeviceCreateViewModel deviceCreateViewModel)
        {
            var device = (m_deviceRepository.GetAllEntities()).GetWithFilter(x => x.Name == deviceCreateViewModel.Name).FirstOrDefault();
            if (device != null)
            {
                throw new Exception("Device already exists!");
            }

            var user = (m_userRepository.GetAllEntities()).GetWithFilter(x => x.Username == deviceCreateViewModel.Username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User does not exist!");
            }

            var newDevice = new Device
            {
                Name = deviceCreateViewModel.Name,
                Description= deviceCreateViewModel.Description,
                Address = deviceCreateViewModel.Address,
                MaximumHourlyEnergyConsumption = deviceCreateViewModel.MaximumHourlyEnergyConsumption,
                User = user
            };

            await m_deviceRepository.CreateAsync(newDevice);
        }

        public async Task UpdateDeviceAsync(DeviceUpdateViewModel deviceUpdateViewModel)
        {
            var device = (m_deviceRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.User)).GetWithFilter(x => x.Id == deviceUpdateViewModel.Id).FirstOrDefault();
            if (device == null)
            {
                throw new Exception("Device not found!");
            }

            var user = (m_userRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.Devices)).GetWithFilter(x => x.Username == deviceUpdateViewModel.Username).FirstOrDefault();
            if(user == null)
            {
                throw new Exception("Username not found!");
            }

            device.Id = deviceUpdateViewModel.Id;
            device.Name = deviceUpdateViewModel.Name;
            device.Description = deviceUpdateViewModel.Description;
            device.Address = deviceUpdateViewModel.Address;
            device.MaximumHourlyEnergyConsumption = deviceUpdateViewModel.MaximumHourlyEnergyConsumption;
            device.User = user;
            await m_deviceRepository.UpdateAsync(device);
        }

        public async Task DeleteDeviceAsync(DeviceDeleteViewModel deviceDeleteViewModel)
        {
            var device = (m_deviceRepository.GetAllEntities()).GetWithFilter(x => x.Id == deviceDeleteViewModel.Id).FirstOrDefault();
            if (device == null)
            {
                throw new Exception("User not found!");
            }

            await m_deviceRepository.DeleteAsync(device);
        }
    }
}
