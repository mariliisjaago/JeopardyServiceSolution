using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoundName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowNumber = table.Column<int>(type: "int", nullable: false),
                    AirDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsAndAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsAndAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsAndAnswers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionsAndAnswers_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionsAndAnswers_Shows_ShowDataId",
                        column: x => x.ShowDataId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAndAnswers_CategoryId",
                table: "QuestionsAndAnswers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAndAnswers_RoundId",
                table: "QuestionsAndAnswers",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAndAnswers_ShowDataId",
                table: "QuestionsAndAnswers",
                column: "ShowDataId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAndAnswers_Value",
                table: "QuestionsAndAnswers",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_AirDate",
                table: "Shows",
                column: "AirDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsAndAnswers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
