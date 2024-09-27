using Microsoft.EntityFrameworkCore;

using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Context
{
    public class Taxisdb : DbContext
    {


        public Taxisdb(DbContextOptions<Taxisdb> options):base (options) 
        {
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<Taxi> Taxi { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<TripDetails> TripDetails { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserGroupDetails> UserGroupDetails { get; set; }
        public DbSet<UserGroupRequests> UserGroupRequests { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}
