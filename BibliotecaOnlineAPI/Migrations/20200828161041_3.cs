using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaOnlineAPI.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "HashPassword",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SaltPass",
                table: "Usuarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SaltPass",
                table: "Usuarios");
        }
    }
}
