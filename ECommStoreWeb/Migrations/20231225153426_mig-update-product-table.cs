using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommStoreWeb.Migrations
{
    public partial class migupdateproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductPhotoLink",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPhotoLink",
                table: "Products");
        }
    }
}
