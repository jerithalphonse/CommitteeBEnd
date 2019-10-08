using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Governorates> Governorates { get; set; }
        public DbSet<Wilayats> Wilayats { get; set; }
        public DbSet<CountingSoftwareUsers> CountingSoftwareUsers { get; set; }
        public DbSet<PollingStations> PollingStations { get; set; }
        public DbSet<Kiosks> Kiosks { get; set; }
        public DbSet<KiosksAssign> KiosksAssign { get; set; }
        public DbSet<Witness> Witness { get; set; }
        public DbSet<MessagingModel> Messaging { get; set; }
    }
}