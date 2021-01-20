using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskWebApp1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EchangeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromCurrency = table.Column<short>(type: "smallint", nullable: false),
                    FromAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToCurrency = table.Column<short>(type: "smallint", nullable: false),
                    ToAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EchangeRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EchangeRecords");
        }
    }
}
