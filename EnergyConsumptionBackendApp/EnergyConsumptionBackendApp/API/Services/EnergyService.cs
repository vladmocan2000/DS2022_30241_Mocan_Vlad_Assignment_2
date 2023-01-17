using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using EnergyConsumptionBackendApp.Core.Models;
using EnergyConsumptionBackendApp.Data;
using EnergyConsumptionBackendApp.Data.Repositories.Interfaces;
using EnergyConsumptionBackendApp.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionBackendApp.API.Services
{
    public class EnergyService : IEnergyService
    {
        private readonly IRepository<Energy> m_energyRepository;
        private readonly IRepository<Device> m_deviceRepository;
        private readonly IRepository<User> m_userRepository;

        public EnergyService(IRepository<Energy> repository, IRepository<Device> deviceRepository, IRepository<User> userRepository)
        {
            m_energyRepository = repository;
            m_deviceRepository = deviceRepository;
            m_userRepository = userRepository;
        }

        public async Task CreateEnergyAsync(EnergyCreateViewModel energyCreateViewModel)
        {
            var device = (m_deviceRepository.GetAllEntities()).GetWithFilter(x => x.Name == energyCreateViewModel.Device).FirstOrDefault();
            if (device == null)
            {
                throw new Exception("Device not found!");
            }

            var newEnergy = new Energy
            {
                Timestamp = energyCreateViewModel.Timestamp,
                EnergyConsumption = energyCreateViewModel.EnergyConsumption,
                Device = device,
            };

            await m_energyRepository.CreateAsync(newEnergy);
        }

        public async Task<List<int>> GetEnergiesAsync(EnergyGetViewModel energyGetViewModel)
        {
            var user = (m_userRepository.GetAllEntities()).GetWithFilter(x => x.Username == energyGetViewModel.Username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User does not exist!");
            }
            
            var energies = m_energyRepository.GetAllEntities()
                .IncludeMultiple(x => x.Include(x => x.Device).ThenInclude(x => x.User))
                .Where(x => energyGetViewModel.Timestamp.Date == x.Timestamp.AddHours(2).Date &&
                            x.Device.User.Username == energyGetViewModel.Username)

                .ToList();

            int[] values = new int[24];
            for (int i = 0; i < 24; i++)
            {
                values[i] = 0;
            }
            foreach(var energy in energies)
            {
                if (energy.Timestamp.AddHours(2).Hour > 0 && energy.Timestamp.AddHours(2).Hour < 24)
                {
                    values[energy.Timestamp.AddHours(2).Hour] += energy.EnergyConsumption;
                }
            }

            return values.ToList();
        }
    }
}
