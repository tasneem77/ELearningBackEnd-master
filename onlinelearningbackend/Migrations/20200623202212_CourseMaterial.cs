using Microsoft.EntityFrameworkCore.Migrations;

namespace onlinelearningbackend.Migrations
{
    public partial class CourseMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMaterialModels_Courses_CourseId",
                table: "CourseMaterialModels");

            migrationBuilder.AlterColumn<string>(
                name: "PathOnServer",
                table: "CourseMaterialModels",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseMaterialModels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "CourseMaterialModels",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMaterialModels_Courses_CourseId",
                table: "CourseMaterialModels",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMaterialModels_Courses_CourseId",
                table: "CourseMaterialModels");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "CourseMaterialModels");

            migrationBuilder.AlterColumn<string>(
                name: "PathOnServer",
                table: "CourseMaterialModels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseMaterialModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMaterialModels_Courses_CourseId",
                table: "CourseMaterialModels",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
