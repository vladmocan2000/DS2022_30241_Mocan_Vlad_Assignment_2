using EnergyConsumptionBackendApp.Core.Models;

namespace EnergyConsumptionBackendApp.API.ViewModels
{
    public class EnergyCreateViewModel
    {
        public DateTime Timestamp { get; set; }
        public int EnergyConsumption { get; set; }
        public string Device { get; set; }
    }
}
