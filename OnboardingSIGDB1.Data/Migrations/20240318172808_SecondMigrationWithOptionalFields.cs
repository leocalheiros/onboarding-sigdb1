using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingSIGDB1.Data.Migrations
{
    public partial class SecondMigrationWithOptionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EmpresaId",
                table: "Funcionario",
                type: "BIGINT",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "BIGINT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EmpresaId",
                table: "Funcionario",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "BIGINT",
                oldNullable: true);
        }
    }
}
