using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Configuration
{
    public class ProfileDataConfiguration : IEntityTypeConfiguration<ProfileData>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<ProfileData> builder)
        {
            builder.HasKey(x => x.ProfileId);
            builder.Property(x => x.ProfileId).ValueGeneratedNever();

            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.PhoneNumber);
            builder.Property(x => x.Email).IsRequired();

            builder.HasMany(x => x.Bonuses)
                .WithOne(y => y.ProfileData)
                .HasForeignKey(z => z.ProfileId);

            builder.ToTable("ProfileData");
        }
    }
}