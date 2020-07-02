using Microsoft.EntityFrameworkCore.Migrations;

namespace onlinelearningbackend.Migrations
{
    public partial class AddedProjectCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoMaterials_Courses_CourseId",
                table: "VideoMaterials");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "VideoMaterials",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ProjectMaterialModels",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoMaterials_Courses_CourseId",
                table: "VideoMaterials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoMaterials_Courses_CourseId",
                table: "VideoMaterials");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ProjectMaterialModels");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "VideoMaterials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_VideoMaterials_Courses_CourseId",
                table: "VideoMaterials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
