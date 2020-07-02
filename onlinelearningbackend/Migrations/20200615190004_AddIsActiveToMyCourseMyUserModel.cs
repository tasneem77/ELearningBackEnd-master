using Microsoft.EntityFrameworkCore.Migrations;

namespace onlinelearningbackend.Migrations
{
    public partial class AddIsActiveToMyCourseMyUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CourseMyUserModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CourseMyUserModel");
        }
    }
}
