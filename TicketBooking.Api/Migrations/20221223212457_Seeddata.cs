using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketBooking.Api.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityID", "CityName", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("2762cec3-4ff9-421e-8107-7000b866ab07"), "Pune", new DateTime(2022, 12, 24, 2, 54, 56, 735, DateTimeKind.Local).AddTicks(8694) },
                    { new Guid("7a609501-e2fe-4de0-9bf6-b2c22c6646dd"), "Surat", new DateTime(2022, 12, 24, 2, 54, 56, 735, DateTimeKind.Local).AddTicks(8715) },
                    { new Guid("a0e62ab0-6af3-433e-9d65-13f9923bb1dd"), "Banglore", new DateTime(2022, 12, 24, 2, 54, 56, 735, DateTimeKind.Local).AddTicks(8711) }
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "CoachID", "CoachName", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("5054bf39-5259-4e46-9d4c-047bb00707c1"), "AC", new DateTime(2022, 12, 24, 2, 54, 56, 735, DateTimeKind.Local).AddTicks(8960) },
                    { new Guid("5fb09668-41b5-4886-9f58-8248f530119f"), "Non-AC", new DateTime(2022, 12, 24, 2, 54, 56, 735, DateTimeKind.Local).AddTicks(8964) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("16820d32-1f50-4d99-ce9a-12dae1f1f94c"), "writter" },
                    { new Guid("ef77dfc4-ceda-4f97-ce98-11dae1f1f94c"), "reader" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "EmailAddress", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("787bbf44-d8a6-4eeb-297a-08dae2067a33"), "readwrite@user.com", "Read Write", "User", "readwrite@user", "readwrite@user.com" },
                    { new Guid("d31d6cb7-36d2-4434-2f49-08dae211c0ab"), "radonly@user.com", "Read Only", "User", "radonly@user", "radonly@user.com" }
                });

            migrationBuilder.InsertData(
                table: "User_Roles",
                columns: new[] { "ID", "RoleID", "UserID" },
                values: new object[,]
                {
                    { new Guid("693575b0-7639-1010-f2b0-08dae1f58abf"), new Guid("ef77dfc4-ceda-4f97-ce98-11dae1f1f94c"), new Guid("d31d6cb7-36d2-4434-2f49-08dae211c0ab") },
                    { new Guid("706cdede-d481-4e2f-ce11-08dae1f1f94c"), new Guid("ef77dfc4-ceda-4f97-ce98-11dae1f1f94c"), new Guid("787bbf44-d8a6-4eeb-297a-08dae2067a33") },
                    { new Guid("787bbf44-d8a6-4eeb-297a-08dae1111a33"), new Guid("16820d32-1f50-4d99-ce9a-12dae1f1f94c"), new Guid("787bbf44-d8a6-4eeb-297a-08dae2067a33") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityID",
                keyValue: new Guid("2762cec3-4ff9-421e-8107-7000b866ab07"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityID",
                keyValue: new Guid("7a609501-e2fe-4de0-9bf6-b2c22c6646dd"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityID",
                keyValue: new Guid("a0e62ab0-6af3-433e-9d65-13f9923bb1dd"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachID",
                keyValue: new Guid("5054bf39-5259-4e46-9d4c-047bb00707c1"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachID",
                keyValue: new Guid("5fb09668-41b5-4886-9f58-8248f530119f"));

            migrationBuilder.DeleteData(
                table: "User_Roles",
                keyColumn: "ID",
                keyValue: new Guid("693575b0-7639-1010-f2b0-08dae1f58abf"));

            migrationBuilder.DeleteData(
                table: "User_Roles",
                keyColumn: "ID",
                keyValue: new Guid("706cdede-d481-4e2f-ce11-08dae1f1f94c"));

            migrationBuilder.DeleteData(
                table: "User_Roles",
                keyColumn: "ID",
                keyValue: new Guid("787bbf44-d8a6-4eeb-297a-08dae1111a33"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: new Guid("16820d32-1f50-4d99-ce9a-12dae1f1f94c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: new Guid("ef77dfc4-ceda-4f97-ce98-11dae1f1f94c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("787bbf44-d8a6-4eeb-297a-08dae2067a33"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("d31d6cb7-36d2-4434-2f49-08dae211c0ab"));
        }
    }
}
