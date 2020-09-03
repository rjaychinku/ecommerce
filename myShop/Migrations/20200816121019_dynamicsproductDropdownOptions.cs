using Microsoft.EntityFrameworkCore.Migrations;

namespace myShop.Migrations
{
    public partial class dynamicsproductDropdownOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailOrder",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ProductColours",
                columns: table => new
                {
                    ProductColourId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColours", x => x.ProductColourId);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    ProductSizeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.ProductSizeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductColours");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmailOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
