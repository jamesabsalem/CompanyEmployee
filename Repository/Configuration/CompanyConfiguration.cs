using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            
            // Validation
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(60)
                .HasAnnotation("ErrorMessage", "Company name is a required field and must be at most 60 characters long.");

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(60)
                .HasAnnotation("ErrorMessage", "Company address is a required field and must be at most 60 characters long.");

            // Seed data
            builder.HasData(
                new Company
                {
                    Id = new Guid("838ddb33-9e6a-48e5-a63f-286e46947bd4"),
                    Name = "IT Solution Ltd",
                    Address = "Dhaka, Bangladesh",
                    Country = "Bangladesh"
                },
                new Company
                {
                    Id = new Guid("b42606ee-bc60-49de-bbaf-9eb38cbd419c"),
                    Name = "Admin Solutions Ltd",
                    Address = "321 Forest Avenue, BF 923",
                    Country = "USA"
                }
            );
        }
    }
}
