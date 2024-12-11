using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            // Validation
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasAnnotation("ErrorMessage", "Employee name is a required field and must be at most 30 characters long.");

            builder.Property(e => e.Age)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Age is a required field.");

            builder.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(20)
                .HasAnnotation("ErrorMessage", "Position is a required field and must be at most 20 characters long.");

            // Relationships
            builder.HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .IsRequired()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            builder.HasData(
                new Employee
                {
                    Id = new Guid("025cd8ff-04aa-4a36-8c95-f69376f842a7"),
                    Name = "Sam Raiden",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = new Guid("838ddb33-9e6a-48e5-a63f-286e46947bd4")
                },
                new Employee
                {
                    Id = new Guid("8c0ba96a-e0fd-4136-988d-de204a6d889b"),
                    Name = "Jana McLeaf",
                    Age = 27,
                    Position = "Software Developer",
                    CompanyId = new Guid("838ddb33-9e6a-48e5-a63f-286e46947bd4")
                },
                new Employee
                {
                    Id = new Guid("a4aef1ef-7fec-42d0-9ec0-6d25bd1d05b5"),
                    Name = "Kane Miller",
                    Age = 28,
                    Position = "Administrator",
                    CompanyId = new Guid("b42606ee-bc60-49de-bbaf-9eb38cbd419c")
                }
            );
        }
    }
}
