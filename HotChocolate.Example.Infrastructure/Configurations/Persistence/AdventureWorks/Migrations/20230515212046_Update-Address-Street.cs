using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolate.Example.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddressStreet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion",
                schema: "SalesLT",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                schema: "SalesLT",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                schema: "SalesLT",
                table: "Address",
                newName: "Street");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion",
                schema: "SalesLT",
                table: "Address",
                columns: new[] { "Street", "City", "StateProvince", "PostalCode", "CountryRegion" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion",
                schema: "SalesLT",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Street",
                schema: "SalesLT",
                table: "Address",
                newName: "AddressLine1");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                schema: "SalesLT",
                table: "Address",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "Second street address line.");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion",
                schema: "SalesLT",
                table: "Address",
                columns: new[] { "AddressLine1", "AddressLine2", "City", "StateProvince", "PostalCode", "CountryRegion" });
        }
    }
}
