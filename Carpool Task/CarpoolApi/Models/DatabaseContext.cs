using Microsoft.EntityFrameworkCore;

namespace CarpoolApi.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>Options):base(Options) 
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Offer> TotalOffers { get; set; }
        public DbSet<ActiveOffer> ActiveOffers { get; set; }
        
    }
}
