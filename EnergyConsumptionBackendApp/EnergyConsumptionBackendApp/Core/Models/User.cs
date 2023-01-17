using System.Data;

namespace EnergyConsumptionBackendApp.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public List<Device> Devices { get; set; }
    }
}
