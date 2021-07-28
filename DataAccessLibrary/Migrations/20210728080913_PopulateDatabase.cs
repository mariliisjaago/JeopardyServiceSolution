using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class PopulateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // read in the csv files

            // go over Shows file, put to table

            // go over Data file, collect

            // 1. rounds

            // 2. categories

            // 3. Q&A


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // truncate tables to empties
            migrationBuilder.Sql("TRUNCATE TABLE QuestionsAndAnswers;");

            migrationBuilder.Sql("TRUNCATE TABLE Shows;");

            migrationBuilder.Sql("TRUNCATE TABLE Rounds;");

            migrationBuilder.Sql("TRUNCATE TABLE Categories;");
        }
    }
}
