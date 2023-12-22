using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StunnerApi.Migrations
{
    /// <inheritdoc />
    public partial class FixActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$17$RUUl3AB0HEBFQ/8qlKCSE.0qjWNfzSz5Vx172amorrgxKmgOUiJle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Activities");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$17$o9/zUzHLHmGzrJ4fsAYbnOGlFB6wNHmPHhgVcyvzOPJrbBOS9kexS");
        }
    }
}
