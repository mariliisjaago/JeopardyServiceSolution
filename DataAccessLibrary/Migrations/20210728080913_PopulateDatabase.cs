using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace DataAccessLibrary.Migrations
{
    public partial class PopulateDatabase : Migration
    {
        private List<RawQuestionModel> questions = new List<RawQuestionModel>();

        private List<RawShowModel> shows = new List<RawShowModel>();

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
            migrationBuilder.Sql("DELETE FROM dbo.QuestionsAndAnswers;");

            migrationBuilder.Sql("DELETE FROM dbo.Shows;");

            migrationBuilder.Sql("DELETE FROM dbo.Rounds;");

            migrationBuilder.Sql("DELETE FROM dbo.Categories;");
        }
    }
}
