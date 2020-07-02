using Microsoft.EntityFrameworkCore.Migrations;

namespace onlinelearningbackend.Migrations
{
    public partial class AddFileNameInProjMatModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PathOnServer",
                table: "ProjectMaterialModels",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ProjectMaterialModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ProjectMaterialModels");

            migrationBuilder.AlterColumn<string>(
                name: "PathOnServer",
                table: "ProjectMaterialModels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
