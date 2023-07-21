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
            builder.Property(x => x.ProfileId).ValueGeneratedNever().HasColumnName("profile_id");

            builder.Property(x => x.FirstName).HasColumnName("first_name");
            builder.Property(x => x.LastName).HasColumnName("last_name");
            builder.Property(x => x.PhoneNumber).HasColumnName("phone_number");
            builder.Property(x => x.Email).IsRequired().HasColumnName("email");

            builder.HasMany(x => x.Bonuses)
                .WithOne(y => y.ProfileData)
                .HasForeignKey(z => z.ProfileId);

            builder.ToTable("ProfileDatas");
        }
    }
}