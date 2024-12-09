using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployee.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("838ddb33-9e6a-48e5-a63f-286e46947bd4"), "Dhaka, Bangladesh", "Bangladesh", "IT Solution Ltd" },
                    { new Guid("b42606ee-bc60-49de-bbaf-9eb38cbd419c"), "321 Forest Avenue, BF 923", "USA", "Admin Solutions Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("025cd8ff-04aa-4a36-8c95-f69376f842a7"), 26, new Guid("838ddb33-9e6a-48e5-a63f-286e46947bd4"), "Sam Raiden", "Software Developer" },
                    { new Guid("8c0ba96a-e0fd-4136-988d-de204a6d889b"), 27, new Guid("838ddb33-9e6a-48e5-a63f-286e46947bd4"), "Jana McLeaf", "Software Developer" },
                    { new Guid("a4aef1ef-7fec-42d0-9ec0-6d25bd1d05b5"), 28, new Guid("b42606ee-bc60-49de-bbaf-9eb38cbd419c"), "Kane Miller", "Administrator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("025cd8ff-04aa-4a36-8c95-f69376f842a7"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("8c0ba96a-e0fd-4136-988d-de204a6d889b"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("a4aef1ef-7fec-42d0-9ec0-6d25bd1d05b5"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("838ddb33-9e6a-48e5-a63f-286e46947bd4"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("b42606ee-bc60-49de-bbaf-9eb38cbd419c"));
        }
    }
}
