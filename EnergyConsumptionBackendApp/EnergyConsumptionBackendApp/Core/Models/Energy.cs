namespace EnergyConsumptionBackendApp.Core.Models
{
    public class Energy
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int EnergyConsumption { get; set; }

        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
