using EnergyConsumptionBackendApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionBackendApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Energy> Energies { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
