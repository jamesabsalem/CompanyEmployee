using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Repository.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
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
