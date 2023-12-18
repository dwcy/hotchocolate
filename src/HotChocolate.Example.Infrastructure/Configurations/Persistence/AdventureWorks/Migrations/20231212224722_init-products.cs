using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolate.Example.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SalesLT");

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductCategory records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentProductCategoryID = table.Column<int>(type: "int", nullable: true, comment: "Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category description."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date and time the record was last updated.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory_ProductCategoryID", x => x.ProductCategoryID);
                    table.ForeignKey(
                        name: "FK_ProductCategory_ProductCategory_ParentProductCategoryID_ProductCategoryID",
                        column: x => x.ParentProductCategoryID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID");
                },
                comment: "High-level product categorization.");

            migrationBuilder.CreateTable(
                name: "ProductDescription",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductDescriptionID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductDescription records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, comment: "Description of the product."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date and time the record was last updated.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDescription_ProductDescriptionID", x => x.ProductDescriptionID);
                },
                comment: "Product descriptions in several languages.");

            migrationBuilder.CreateTable(
                name: "ProductModel",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CatalogDescription = table.Column<string>(type: "xml", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModel_ProductModelID", x => x.ProductModelID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for Product records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the product."),
                    ProductNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Unique product identification number."),
                    Color = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, comment: "Product color."),
                    StandardCost = table.Column<decimal>(type: "money", nullable: false, comment: "Standard cost of the product."),
                    ListPrice = table.Column<decimal>(type: "money", nullable: false, comment: "Selling price."),
                    Size = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, comment: "Product size."),
                    Weight = table.Column<decimal>(type: "decimal(8,2)", nullable: true, comment: "Product weight."),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: true, comment: "Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. "),
                    ProductModelID = table.Column<int>(type: "int", nullable: true, comment: "Product is a member of this product model. Foreign key to ProductModel.ProductModelID."),
                    SellStartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date the product was available for sale."),
                    SellEndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the product was no longer available for sale."),
                    DiscontinuedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the product was discontinued."),
                    ThumbNailPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true, comment: "Small image of the product."),
                    ThumbnailPhotoFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Small image file name."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date and time the record was last updated.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductID", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID");
                    table.ForeignKey(
                        name: "FK_Product_ProductModel_ProductModelID",
                        column: x => x.ProductModelID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelID");
                },
                comment: "Products sold or used in the manfacturing of sold products.");

            migrationBuilder.CreateTable(
                name: "ProductModelProductDescription",
                schema: "SalesLT",
                columns: table => new
                {
                    ProductModelID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to ProductModel.ProductModelID."),
                    ProductDescriptionID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to ProductDescription.ProductDescriptionID."),
                    Culture = table.Column<string>(type: "nchar(6)", fixedLength: true, maxLength: 6, nullable: false, comment: "The culture for which the description is written"),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date and time the record was last updated.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture", x => new { x.ProductModelID, x.ProductDescriptionID, x.Culture });
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescription_ProductDescription_ProductDescriptionID",
                        column: x => x.ProductDescriptionID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductDescription",
                        principalColumn: "ProductDescriptionID");
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescription_ProductModel_ProductModelID",
                        column: x => x.ProductModelID,
                        principalSchema: "SalesLT",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelID");
                },
                comment: "Cross-reference table mapping product descriptions and the language the description is written in.");

            migrationBuilder.CreateIndex(
                name: "AK_Product_Name",
                schema: "SalesLT",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Product_ProductNumber",
                schema: "SalesLT",
                table: "Product",
                column: "ProductNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryID",
                schema: "SalesLT",
                table: "Product",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductModelID",
                schema: "SalesLT",
                table: "Product",
                column: "ProductModelID");

            migrationBuilder.CreateIndex(
                name: "AK_ProductCategory_Name",
                schema: "SalesLT",
                table: "ProductCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductCategory_rowguid",
                schema: "SalesLT",
                table: "ProductCategory",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ParentProductCategoryID",
                schema: "SalesLT",
                table: "ProductCategory",
                column: "ParentProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "AK_ProductDescription_rowguid",
                schema: "SalesLT",
                table: "ProductDescription",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductModel_Name",
                schema: "SalesLT",
                table: "ProductModel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductModel_rowguid",
                schema: "SalesLT",
                table: "ProductModel",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductModelProductDescription_rowguid",
                schema: "SalesLT",
                table: "ProductModelProductDescription",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelProductDescription_ProductDescriptionID",
                schema: "SalesLT",
                table: "ProductModelProductDescription",
                column: "ProductDescriptionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductModelProductDescription",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductCategory",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductDescription",
                schema: "SalesLT");

            migrationBuilder.DropTable(
                name: "ProductModel",
                schema: "SalesLT");
        }
    }
}
