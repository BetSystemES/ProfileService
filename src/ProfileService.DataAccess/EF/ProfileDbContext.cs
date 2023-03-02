using Microsoft.EntityFrameworkCore;
// TODO: remove unused/sort usings
using System.Numerics;
using ProfileService.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.DataAccess.Configuration;


// TODO: remove all empty lines
namespace ProfileService.DataAccess.EF
{
    // TODO: change file location to ProfileService.DataAccess
    public class ProfileDbContext : DbContext
    {
        // TODO: remove all comments
        //public DbSet<PersonalData> Profiles { get; set; }
        //public DbSet<Bonus> Bonuses { get; set; }

        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
        {
            // TODO: remove all comments
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new PersonalDataConfiguration())
                .ApplyConfiguration(new BonusConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        // TODO: remove all comments
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=profiledb;Username=postgres;Password=postgres");
        //    }
        //}
    }
}