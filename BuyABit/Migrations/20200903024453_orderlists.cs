using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyABit.Migrations
{
    public partial class orderlists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillingAddresses",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "BaseAddressId",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "BaseAddressId",
                table: "BillingAddresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "ShippingAddresses",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ApartmentSuite",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "ShippingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DropDownOrder",
                table: "ProductSizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DropDownOrder",
                table: "ProductColours",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "BillingAddresses",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ApartmentSuite",
                table: "BillingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "BillingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "BillingAddresses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillingAddresses",
                table: "BillingAddresses",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillingAddresses",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "ApartmentSuite",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "DropDownOrder",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "DropDownOrder",
                table: "ProductColours");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "ApartmentSuite",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "BillingAddresses");

            migrationBuilder.AddColumn<int>(
                name: "BaseAddressId",
                table: "ShippingAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BaseAddressId",
                table: "BillingAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "BaseAddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillingAddresses",
                table: "BillingAddresses",
                column: "BaseAddressId");
        }
    }
}
