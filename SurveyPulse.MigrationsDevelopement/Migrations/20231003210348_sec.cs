using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyPulse.MigrationsDevelopement.Migrations
{
    public partial class sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "105179e3-b53c-4cea-b3f3-532e497e4960");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e71f28e-adf7-4666-9e0d-6e22451ef4d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67ae0193-b509-49c8-ad65-6f4b184d6897");

            migrationBuilder.DropColumn(
                name: "UserScoreId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0142f200-efba-44c9-848c-45e1ffe35b15", "2", "AppRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2de0850c-3c54-47ab-a46e-070db3bdcc6f", "3", "AppRole", "NonUser", "NONUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "72831fd7-f2ad-4bbe-84ec-bc7ed9311a4e", "1", "AppRole", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0142f200-efba-44c9-848c-45e1ffe35b15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2de0850c-3c54-47ab-a46e-070db3bdcc6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72831fd7-f2ad-4bbe-84ec-bc7ed9311a4e");

            migrationBuilder.AddColumn<int>(
                name: "UserScoreId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "105179e3-b53c-4cea-b3f3-532e497e4960", "3", "AppRole", "NonUser", "NONUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2e71f28e-adf7-4666-9e0d-6e22451ef4d2", "2", "AppRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "67ae0193-b509-49c8-ad65-6f4b184d6897", "1", "AppRole", "Admin", "ADMIN" });
        }
    }
}
