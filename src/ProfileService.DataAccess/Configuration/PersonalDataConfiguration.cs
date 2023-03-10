using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Configuration
{
    public class PersonalDataConfiguration : IEntityTypeConfiguration<PersonalData>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<PersonalData> builder)
        {
            builder.HasKey(x => x.PersonalId);
            builder.Property(x => x.PersonalId).ValueGeneratedNever();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Surname);
            builder.Property(x => x.PhoneNumber);
            builder.Property(x => x.Email).IsRequired();

            builder.HasMany(x => x.Bonuses)
                .WithOne(y => y.PersonalData)
                .HasForeignKey(z => z.PersonalId);

            builder.ToTable("PersonalData");
        }
    }
}