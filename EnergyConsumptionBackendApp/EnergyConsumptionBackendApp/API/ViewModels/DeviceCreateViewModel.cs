namespace EnergyConsumptionBackendApp.API.ViewModels
{
    public class DeviceCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MaximumHourlyEnergyConsumption { get; set; }
        public string Username { get; set; }
    }
}
