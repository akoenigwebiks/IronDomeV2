using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronDomeV2.Migrations
{
    /// <inheritdoc />
    public partial class MethodOfAttackTemplateAddedOnCascadeDeletePrevent3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Range",
                table: "MethodOfAttack");

            migrationBuilder.DropColumn(
                name: "Velocity",
                table: "MethodOfAttack");

            migrationBuilder.AddColumn<int>(
                name: "MethodOfAttackTemplateId",
                table: "MethodOfAttack",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MethodOfAttackTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Velocity = table.Column<int>(type: "int", nullable: false),
                    AttackerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodOfAttackTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodOfAttackTemplate_Attacker_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Attacker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethodOfAttack_MethodOfAttackTemplateId",
                table: "MethodOfAttack",
                column: "MethodOfAttackTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodOfAttackTemplate_AttackerId",
                table: "MethodOfAttackTemplate",
                column: "AttackerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MethodOfAttack_MethodOfAttackTemplate_MethodOfAttackTemplateId",
                table: "MethodOfAttack",
                column: "MethodOfAttackTemplateId",
                principalTable: "MethodOfAttackTemplate",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MethodOfAttack_MethodOfAttackTemplate_MethodOfAttackTemplateId",
                table: "MethodOfAttack");

            migrationBuilder.DropTable(
                name: "MethodOfAttackTemplate");

            migrationBuilder.DropIndex(
                name: "IX_MethodOfAttack_MethodOfAttackTemplateId",
                table: "MethodOfAttack");

            migrationBuilder.DropColumn(
                name: "MethodOfAttackTemplateId",
                table: "MethodOfAttack");

            migrationBuilder.AddColumn<double>(
                name: "Range",
                table: "MethodOfAttack",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Velocity",
                table: "MethodOfAttack",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
