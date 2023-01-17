using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using EnergyConsumptionBackendApp.Core.Models;
using EnergyConsumptionBackendApp.Data.Repositories.Interfaces;
using EnergyConsumptionBackendApp.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionBackendApp.API.Services
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> m_userRepository;
        
        public UserService(IRepository<User> repository)
        {
            m_userRepository = repository;
        }

        public async Task<List<UserDetailsViewModel>> GetAllUsersDetailsAsync()
        {
            var users = await  (m_userRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.Devices)).ToListAsync();

            var usersDetailsViewModel = new List<UserDetailsViewModel>();

            foreach (var user in users)
            {
                var devicesNames = new List<string>();
                foreach(var device in user.Devices)
                {
                    devicesNames.Add(device.Name);
                }
                usersDetailsViewModel.Add(new UserDetailsViewModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Role = user.Role,
                    DevicesNames = devicesNames
                });
            }

            return usersDetailsViewModel;
        }

        public async Task<UserDetailsViewModel> GetUserDetailsByIdAsync(UserGetByIdViewModel userGetByIdViewModel)
        {
            var user = (m_userRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.Devices)).GetWithFilter(x => x.Id == userGetByIdViewModel.Id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            var devicesNames = new List<string>();
            foreach (var device in user.Devices)
            {
                devicesNames.Add(device.Name);
            }

            return new UserDetailsViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Role = user.Role,
                DevicesNames = devicesNames
            };
        }

        public async Task UpdateUserAsync(UserUpdateViewModel userUpdateViewModel)
        {
            var user = (m_userRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.Devices)).GetWithFilter(x => x.Id == userUpdateViewModel.Id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            user.Username = userUpdateViewModel.Username;
            user.Firstname = userUpdateViewModel.Firstname;
            user.Lastname = userUpdateViewModel.Lastname;
            user.Role = userUpdateViewModel.Role;
            await m_userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserById(UserDeleteViewModel userDeleteViewModel)
        {
            var user = (m_userRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.Devices)).GetWithFilter(x => x.Id == userDeleteViewModel.Id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            await m_userRepository.DeleteAsync(user);
        }
    }
}
