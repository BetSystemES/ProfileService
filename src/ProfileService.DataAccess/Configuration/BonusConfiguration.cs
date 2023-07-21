using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Configuration
{
    public class BonusConfiguration : IEntityTypeConfiguration<Bonus>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Bonus> builder)
        {
            builder.HasKey(x => x.BonusId);
            builder.Property(x => x.BonusId).ValueGeneratedNever().HasColumnName("bonus_id");

            builder.Property(x => x.ProfileId).IsRequired().HasColumnName("profile_id");

            builder.HasOne(x => x.ProfileData)
                .WithMany(y => y.Bonuses)
                .HasForeignKey(z => z.ProfileId);

            builder.Property(x => x.IsAlreadyUsed).HasColumnName("is_already_used");
            builder.Property(x => x.IsEnabled).HasColumnName("is_enabled");
            builder.Property(x => x.DiscountType).HasColumnName("discount_type");
            builder.Property(x => x.Amount).HasColumnName("amount");

            builder.ToTable("Bonuses");
        }
    }
}