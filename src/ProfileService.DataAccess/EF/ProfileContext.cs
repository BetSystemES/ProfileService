using Microsoft.EntityFrameworkCore;
using System.Numerics;
using ProfileService.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.DataAccess.Configuration;


namespace ProfileService.DataAccess.EF
{
    public class ProfileContext : DbContext
    {
        public DbSet<PersonalData> Profiles { get; set; }
        public DbSet<Bonus> Bonuses { get; set; }

        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new PersonalDataConfiguration())
                .ApplyConfiguration(new BonusConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=profiledb;Username=postgres;Password=postgres");
            }
        }
    }
}