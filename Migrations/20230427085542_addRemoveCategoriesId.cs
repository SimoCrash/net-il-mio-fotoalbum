using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    public partial class addRemoveCategoriesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Photos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriesId",
                table: "Photos",
                type: "int",
                nullable: true);
        }
    }
}
