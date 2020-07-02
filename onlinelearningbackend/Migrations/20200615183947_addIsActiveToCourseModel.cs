using Microsoft.EntityFrameworkCore.Migrations;

namespace onlinelearningbackend.Migrations
{
    public partial class addIsActiveToCourseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Courses");
        }
    }
}
