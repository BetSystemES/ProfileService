using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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
            builder.Property(x => x.BonusId).ValueGeneratedNever();


            builder.Property(x => x.PersonalId).IsRequired();

            builder.HasOne(x => x.PersonalData)
                .WithMany(y => y.Bonuses)
                .HasForeignKey(z => z.PersonalId);

            builder.Property(x => x.isAlreadyUsed);
            builder.Property(x => x.DiscountType);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Discount);

            builder.ToTable("Bonus");
        }
    }
}
