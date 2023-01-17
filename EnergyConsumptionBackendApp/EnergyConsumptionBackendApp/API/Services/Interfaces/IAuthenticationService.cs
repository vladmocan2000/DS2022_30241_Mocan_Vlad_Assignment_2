using EnergyConsumptionBackendApp.API.ViewModels;

namespace EnergyConsumptionBackendApp.API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task RegisterUserAsync(UserRegisterViewModel userRegisterViewModel);
        Task<UserDetailsViewModel> LoginUserAsync(UserLoginViewModel userLoginViewModel);
    }
}
