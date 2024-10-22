using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSharp.eShopWeb.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShopSharpInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogBrands_CatalogBrandId",
                table: "Catalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogTypes_CatalogTypeId",
                table: "Catalog");

            migrationBuilder.DropTable(
                name: "CatalogBrands");

            migrationBuilder.DropTable(
                name: "CatalogTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Catalog");

            migrationBuilder.RenameColumn(
                name: "CatalogTypeId",
                table: "Catalog",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "CatalogBrandId",
                table: "Catalog",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Catalog_CatalogTypeId",
                table: "Catalog",
                newName: "IX_Catalog_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Catalog_CatalogBrandId",
                table: "Catalog",
                newName: "IX_Catalog_BrandId");

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_State",
                table: "Orders",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_PictureUri",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemOrdered_CatalogItemId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_ProductBrands_BrandId",
                table: "Catalog",
                column: "BrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_ProductTypes_TypeId",
                table: "Catalog",
                column: "TypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_ProductBrands_BrandId",
                table: "Catalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_ProductTypes_TypeId",
                table: "Catalog");

            migrationBuilder.DropTable(
                name: "ProductBrands");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Catalog",
                newName: "CatalogTypeId");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Catalog",
                newName: "CatalogBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Catalog_TypeId",
                table: "Catalog",
                newName: "IX_Catalog_CatalogTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Catalog_BrandId",
                table: "Catalog",
                newName: "IX_Catalog_CatalogBrandId");

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_State",
                table: "Orders",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_PictureUri",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ItemOrdered_CatalogItemId",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Catalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CatalogBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogBrands_CatalogBrandId",
                table: "Catalog",
                column: "CatalogBrandId",
                principalTable: "CatalogBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogTypes_CatalogTypeId",
                table: "Catalog",
                column: "CatalogTypeId",
                principalTable: "CatalogTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
