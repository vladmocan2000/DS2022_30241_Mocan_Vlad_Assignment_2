using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using EnergyConsumptionBackendApp.Core.Models;
using EnergyConsumptionBackendApp.Data.Repositories.Interfaces;
using EnergyConsumptionBackendApp.Data.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionBackendApp.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> m_userRepository;

        public AuthenticationService(IRepository<User> repository)
        {
            m_userRepository = repository;
        }

        public async Task RegisterUserAsync(UserRegisterViewModel userRegisterViewModel)
        {
            var user = (m_userRepository.GetAllEntities()).GetWithFilter(x => x.Username == userRegisterViewModel.Username).FirstOrDefault();
            if (user != null)
            {
                throw new Exception("Username already exists!");
            }

            userRegisterViewModel.Role = userRegisterViewModel.Role.ToLower();
            if(!string.Equals(userRegisterViewModel.Role, "client") && !string.Equals(userRegisterViewModel.Role, "administrator"))
            {
                throw new Exception("Incorrect role!");
            }

            var newUser = new User
            {
                Username = userRegisterViewModel.Username,
                Firstname = userRegisterViewModel.Firstname,
                Lastname = userRegisterViewModel.Lastname,
                Role = userRegisterViewModel.Role,
                Password = userRegisterViewModel.Password
            };

            await m_userRepository.CreateAsync(newUser);
        }

        public async Task<UserDetailsViewModel> LoginUserAsync(UserLoginViewModel userLoginViewModel)
        {
            var user = (m_userRepository.GetAllEntities()).IncludeMultiple(x => x.Include(x => x.Devices)).GetWithFilter(x => x.Username == userLoginViewModel.Username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("Username does not exist!");
            }

            if(user.Password != userLoginViewModel.Password)
            {
                throw new Exception("Incorrect password!");
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
    }
}
