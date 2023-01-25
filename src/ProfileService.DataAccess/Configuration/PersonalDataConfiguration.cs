using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.BusinessLogic;
using System.ComponentModel.DataAnnotations;

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

            builder.ToTable("PersonalData");
        }
    }
}