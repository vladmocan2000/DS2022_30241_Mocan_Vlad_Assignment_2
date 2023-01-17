namespace EnergyConsumptionBackendApp.API.ViewModels
{
    public class UserDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public List<string> DevicesNames { get; set; }
    }
}
