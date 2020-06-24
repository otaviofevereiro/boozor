using Curriculum.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(x => x.Email)
                   .IsRequired();
        }
    }
}
