using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
                (
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
