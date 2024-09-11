using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSample.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_no = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    employee_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    current_address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    department = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_no);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}
