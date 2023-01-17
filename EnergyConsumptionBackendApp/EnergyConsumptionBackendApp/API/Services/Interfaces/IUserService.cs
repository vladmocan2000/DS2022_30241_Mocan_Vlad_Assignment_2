using EnergyConsumptionBackendApp.API.ViewModels;

namespace EnergyConsumptionBackendApp.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDetailsViewModel>> GetAllUsersDetailsAsync();
        Task<UserDetailsViewModel> GetUserDetailsByIdAsync(UserGetByIdViewModel userGetByIdViewModel);
        Task UpdateUserAsync(UserUpdateViewModel userBaseViewModel);
        Task DeleteUserById(UserDeleteViewModel userDeleteViewModel);
    }
}
