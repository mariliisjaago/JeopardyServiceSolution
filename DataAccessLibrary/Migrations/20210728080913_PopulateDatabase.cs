using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DataAccessLibrary.Migrations
{
    public partial class PopulateDatabase : Migration
    {
        private List<RawQuestionModel> questions;

        private List<RawShowModel> shows;

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // add rounds data by hand (small amount of seed data)

            migrationBuilder.InsertData("Rounds", "RoundName", "Jeopardy!");
            migrationBuilder.InsertData("Rounds", "RoundName", "Double Jeopardy!");
            migrationBuilder.InsertData("Rounds", "RoundName", "Final Jeopardy!");

            // get seed data for Shows and Questions from CSV files (filepaths hardcoded in the methods)

            shows = GetShows();

            questions = GetQuestions();


            // go over Shows file, insert info to Shows table in db

            string[] showsColumns = new string[] { "Id", "ShowNumber", "AirDate" };

            shows.ForEach(x =>
            {
                migrationBuilder.InsertData("Shows", showsColumns, new object[] { Guid.NewGuid(), x.ShowNumber, x.AirDate });
            });



            // go over Questions file, collect

            // 2. categories

            // 3. Q&A

        }

        private List<RawShowModel> GetShows()
        {
            List<RawShowModel> output = new List<RawShowModel>();

            StreamReader reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "Migrations/20210728080913_PopulateDatabase_shows.csv"));

            // read first line of header but don't do anything with it
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineParts = line.Split(';');

                CultureInfo cultureInfoProvider = CultureInfo.InvariantCulture;

                output.Add(new RawShowModel
                {
                    Id = Guid.NewGuid(),
                    ShowNumber = Int32.Parse(lineParts[0]),
                    AirDate = DateTime.ParseExact(lineParts[1], "dd.mm.yyyy", cultureInfoProvider)
                });
            }

            return output;
        }

        private List<RawQuestionModel> GetQuestions()
        {
            List<RawQuestionModel> output = new List<RawQuestionModel>();

            StreamReader reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "../../../Migrations/20210728080913_PopulateDatabase_questions.csv"));

            // read first line of header but don't do anything with it
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineParts = line.Split(';');

                output.Add(new RawQuestionModel
                {
                    ShowNumber = Int32.Parse(lineParts[0]),
                    Round = lineParts[1],
                    Category = lineParts[2],
                    Value = Int32.Parse(lineParts[3]),
                    Question = lineParts[4],
                    Answer = lineParts[5]
                });
            }

            return output;
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
