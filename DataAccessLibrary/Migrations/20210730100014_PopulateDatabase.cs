using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataAccessLibrary.Migrations
{
    public partial class PopulateDatabase : Migration
    {
        private List<RawQuestionModel> questions;

        private List<QuestionAndAnswer> questionsToDb;

        private List<Show> shows;

        private List<Round> rounds;

        private List<Category> categories;

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // generate rounds data by hand (small amount of seed data) and insert to db

            rounds = GenerateRounds();

            string[] roundsColumns = new string[] { "Id", "RoundName" };

            rounds.ForEach(x =>
            {
                migrationBuilder.InsertData("Rounds", roundsColumns, new object[] { x.Id, x.RoundName });
            });


            // get seed data for Shows and Questions from CSV files (filepaths hardcoded in the methods)

            shows = GetShows();

            questions = GetQuestions();


            // go over Shows file, insert info to Shows table in db

            string[] showsColumns = new string[] { "Id", "ShowNumber", "AirDate" };

            shows.ForEach(x =>
            {
                migrationBuilder.InsertData("Shows", showsColumns, new object[] { x.Id, x.ShowNumber, x.AirDate });
            });


            // go over Questions file, collect

            // 1. collect categories and insert to db

            categories = GetCategories();

            string[] categoryColumns = new string[] { "Id", "CategoryName" };

            categories.ForEach(x =>
            {
                migrationBuilder.InsertData("Categories", categoryColumns, new object[] { x.Id, x.CategoryName });
            });

            // 2. transform raw Q&A models to db-needed models and insert to db

            questionsToDb = TransformRawToDbModel();

            string[] questionsColumns = new string[] { "ShowDataId", "RoundId", "CategoryId", "Value", "Question", "Answer" };

            questionsToDb.ForEach(x =>
            {
                migrationBuilder.InsertData("QuestionsAndAnswers", questionsColumns, new object[] { x.ShowData.Id, x.Round.Id, x.Category.Id, x.Value, x.Question, x.Answer });
            });
        }

        private List<QuestionAndAnswer> TransformRawToDbModel()
        {
            List<QuestionAndAnswer> output = new List<QuestionAndAnswer>();

            foreach (var question in questions)
            {
                QuestionAndAnswer oneQuestion = new QuestionAndAnswer
                {
                    ShowData = new Show
                    {
                        // get show Id from Shows list, because we know this current question's showNumber.
                        Id = shows.Where(x => x.ShowNumber == question.ShowNumber).FirstOrDefault().Id
                        // do I need to fill these two properties in?
                        // ShowNumber = question.ShowNumber,
                        // AirDate = shows.Where(x => x.ShowNumber == question.ShowNumber).FirstOrDefault().AirDate
                    },
                    Round = new Round
                    {
                        Id = rounds.Where(x => x.RoundName == question.Round).FirstOrDefault().Id
                        //RoundName = question.Round
                    },
                    Category = new Category
                    {
                        Id = categories.Where(x => x.CategoryName == question.Category).FirstOrDefault().Id
                        // CategoryName = question.Category
                    },
                    Value = question.Value,
                    Question = question.Question,
                    Answer = question.Answer
                };

                output.Add(oneQuestion);
            }

            return output;
        }

        private List<Category> GetCategories()
        {
            // gather all unique (dstinct) categories from the questions dataset
            List<string> categoryNames = questions.Select(x => x.Category).Distinct().ToList();

            List<Category> output = new List<Category>();

            categoryNames.ForEach(x => output.Add(new Category { Id = Guid.NewGuid(), CategoryName = x }));

            return output;
        }

        private List<Round> GenerateRounds()
        {
            List<Round> output = new List<Round>
            {
                new Round
                {
                    Id = Guid.NewGuid(),
                    RoundName = "Jeopardy!"
                },
                new Round
                {
                    Id = Guid.NewGuid(),
                    RoundName = "Double Jeopardy!"
                },
                new Round
                {
                    Id = Guid.NewGuid(),
                    RoundName = "Final Jeopardy!"
                }
            };

            return output;
        }

        private List<Show> GetShows()
        {
            List<Show> output = new List<Show>();

            StreamReader reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "Migrations/20210730100014_PopulateDatabase_shows.csv"));

            // read first line of header but don't do anything with it
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineParts = line.Split(';');

                CultureInfo cultureInfoProvider = CultureInfo.InvariantCulture;

                output.Add(new Show
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

            StreamReader reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "../../../Migrations/20210730100014_PopulateDatabase_questions.csv"));

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
