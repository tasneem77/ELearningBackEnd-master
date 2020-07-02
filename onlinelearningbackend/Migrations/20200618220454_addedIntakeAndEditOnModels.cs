using Microsoft.EntityFrameworkCore.Migrations;

namespace onlinelearningbackend.Migrations
{
    public partial class addedIntakeAndEditOnModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntakeId",
                table: "ProjectModels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProjectModels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IntakeId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Intakes",
                columns: table => new
                {
                    IntakeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntakeName = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intakes", x => x.IntakeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModels_IntakeId",
                table: "ProjectModels",
                column: "IntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IntakeId",
                table: "AspNetUsers",
                column: "IntakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Intakes_IntakeId",
                table: "AspNetUsers",
                column: "IntakeId",
                principalTable: "Intakes",
                principalColumn: "IntakeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectModels_Intakes_IntakeId",
                table: "ProjectModels",
                column: "IntakeId",
                principalTable: "Intakes",
                principalColumn: "IntakeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Intakes_IntakeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectModels_Intakes_IntakeId",
                table: "ProjectModels");

            migrationBuilder.DropTable(
                name: "Intakes");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModels_IntakeId",
                table: "ProjectModels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IntakeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IntakeId",
                table: "ProjectModels");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProjectModels");

            migrationBuilder.DropColumn(
                name: "IntakeId",
                table: "AspNetUsers");
        }
    }
}
