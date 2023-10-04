using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyPulse.MigrationsDevelopement.Migrations
{
    public partial class RemovedResponses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respondents_SurveyResponses_SurveyResponseId",
                table: "Respondents");

            migrationBuilder.DropForeignKey(
                name: "FK_Respondents_Surveys_SurveyId",
                table: "Respondents");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "SurveyResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Respondents",
                table: "Respondents");

            migrationBuilder.DropIndex(
                name: "IX_Respondents_SurveyResponseId",
                table: "Respondents");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cd78828-b257-4bee-97bb-c4e18e2bfa37");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e185edf-8644-4b1e-8d1f-a07913c10e70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a41a32b-df0e-43c5-abd4-62fdd019d4e4");

            migrationBuilder.DropColumn(
                name: "ResponsesDone",
                table: "SurveyDetails");

            migrationBuilder.DropColumn(
                name: "SurveyResponseId",
                table: "Respondents");

            migrationBuilder.RenameTable(
                name: "Respondents",
                newName: "Respondeds");

            migrationBuilder.RenameIndex(
                name: "IX_Respondents_SurveyId",
                table: "Respondeds",
                newName: "IX_Respondeds_SurveyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Respondeds",
                table: "Respondeds",
                column: "RespondentId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "1106094e-f84f-44f7-b7cc-d20fcf4ce85b", "1", "AppRole", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "18e21ab6-40a3-4899-94c9-05ad85112ed6", "3", "AppRole", "NonUser", "NONUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "502f3222-8e5d-4aea-916f-f4bbfc240af8", "2", "AppRole", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Respondeds_Surveys_SurveyId",
                table: "Respondeds",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respondeds_Surveys_SurveyId",
                table: "Respondeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Respondeds",
                table: "Respondeds");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1106094e-f84f-44f7-b7cc-d20fcf4ce85b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18e21ab6-40a3-4899-94c9-05ad85112ed6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "502f3222-8e5d-4aea-916f-f4bbfc240af8");

            migrationBuilder.RenameTable(
                name: "Respondeds",
                newName: "Respondents");

            migrationBuilder.RenameIndex(
                name: "IX_Respondeds_SurveyId",
                table: "Respondents",
                newName: "IX_Respondents_SurveyId");

            migrationBuilder.AddColumn<int>(
                name: "ResponsesDone",
                table: "SurveyDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyResponseId",
                table: "Respondents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Respondents",
                table: "Respondents",
                column: "RespondentId");

            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    SurveyResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.SurveyResponseId);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId");
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    ResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedQuestionOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_Responses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId");
                    table.ForeignKey(
                        name: "FK_Responses_SurveyResponses_SurveyResponseId",
                        column: x => x.SurveyResponseId,
                        principalTable: "SurveyResponses",
                        principalColumn: "SurveyResponseId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "1cd78828-b257-4bee-97bb-c4e18e2bfa37", "3", "AppRole", "NonUser", "NONUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "6e185edf-8644-4b1e-8d1f-a07913c10e70", "1", "AppRole", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8a41a32b-df0e-43c5-abd4-62fdd019d4e4", "2", "AppRole", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Respondents_SurveyResponseId",
                table: "Respondents",
                column: "SurveyResponseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_QuestionId",
                table: "Responses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_SurveyResponseId",
                table: "Responses",
                column: "SurveyResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_SurveyId",
                table: "SurveyResponses",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respondents_SurveyResponses_SurveyResponseId",
                table: "Respondents",
                column: "SurveyResponseId",
                principalTable: "SurveyResponses",
                principalColumn: "SurveyResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respondents_Surveys_SurveyId",
                table: "Respondents",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId");
        }
    }
}
