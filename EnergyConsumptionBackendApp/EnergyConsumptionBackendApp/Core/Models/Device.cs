namespace EnergyConsumptionBackendApp.Core.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MaximumHourlyEnergyConsumption { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<Energy> Energies { get; set; }
    }
}
