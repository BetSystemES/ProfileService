using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProfileService.DataAccess
{
    /// <summary>
    /// Auction service context factory
    /// </summary>
    public class ProfileServiceContextFactory : IDesignTimeDbContextFactory<ProfileDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        public ProfileDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProfileDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProfileDb;User Id=postgres;Password=postgres");

            return new ProfileDbContext(optionsBuilder.Options);
        }
    }
}