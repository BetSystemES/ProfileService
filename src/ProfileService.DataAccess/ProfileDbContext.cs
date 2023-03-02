using Microsoft.EntityFrameworkCore;
using ProfileService.DataAccess.Configuration;

namespace ProfileService.DataAccess
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new PersonalDataConfiguration())
                .ApplyConfiguration(new BonusConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}