using Microsoft.EntityFrameworkCore.Migrations;

namespace Mc2.CrudAppTest.Api.Migrations
{
    public partial class addCountryCodeToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Customers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Customers");
        }
    }
}
