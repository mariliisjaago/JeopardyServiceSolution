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
        private List<RawQuestionModel> questions = new List<RawQuestionModel>();

        private List<QuestionAndAnswer> questionsToDb = new List<QuestionAndAnswer>();

        private List<Show> shows = new List<Show>();

        private List<Round> rounds = new List<Round>();

        private List<Category> categories = new List<Category>();

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


            // go over Questions file,
            // 1. collect categories and insert to db

            categories = GetCategories();

            string[] categoryColumns = new string[] { "Id", "CategoryName" };

            categories.ForEach(x =>
            {
                migrationBuilder.InsertData("Categories", categoryColumns, new object[] { x.Id, x.CategoryName });
            });


            // 2. transform raw Q&A models to db-needed models and insert to db

            questionsToDb = TransformRawToDbModel();

            string[] questionsColumns = new string[] { "Id", "ShowDataId", "RoundId", "CategoryId", "Value", "Question", "Answer" };

            questionsToDb.ForEach(x =>
            {
                migrationBuilder.InsertData("QuestionsAndAnswers", questionsColumns, new object[] { x.Id, x.ShowData.Id, x.Round.Id, x.Category.Id, x.Value, x.Question, x.Answer });
            });
        }

        private List<QuestionAndAnswer> TransformRawToDbModel()
        {
            List<QuestionAndAnswer> output = new List<QuestionAndAnswer>();

            foreach (var question in questions)
            {
                QuestionAndAnswer oneQuestion = new QuestionAndAnswer
                {
                    Id = Guid.NewGuid(),
                    Value = question.Value,
                    Question = question.Question,
                    Answer = question.Answer
                };

                // some showNumbers, rounds, categories could not be parsed, so cannot find in db. Checking for nulls here. If object with given property not found, do not enter this question to db.
                Show? show = shows.Where(x => x.ShowNumber == question.ShowNumber).FirstOrDefault();
                Round? round = rounds.Where(x => x.RoundName == question.Round).FirstOrDefault();
                Category? category = categories.Where(x => x.CategoryName == question.Category).FirstOrDefault();

                if (show == null)
                {
                    continue;
                }

                oneQuestion.ShowData = new Show
                {
                    Id = show.Id,
                    ShowNumber = question.ShowNumber,
                    AirDate = shows.Where(x => x.ShowNumber == question.ShowNumber).FirstOrDefault().AirDate
                };

                if (round == null)
                {
                    continue;
                }

                oneQuestion.Round = new Round
                {
                    Id = round.Id,
                    RoundName = question.Round
                };

                if (category == null)
                {
                    continue;
                }

                oneQuestion.Category = new Category
                {
                    Id = category.Id,
                    CategoryName = question.Category
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

            string? line;
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

            StreamReader reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "Migrations/20210730100014_PopulateDatabase_questions.csv"));

            // read first line of header but don't do anything with it
            reader.ReadLine();

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineParts = line.Split(';');

                RawQuestionModel oneQuestion = new RawQuestionModel
                {
                    Round = lineParts[1],
                    Category = lineParts[2],
                    Question = lineParts[4],
                    Answer = lineParts[5]
                };

                oneQuestion.ShowNumber = ReturnValidIntOrMinusOne(lineParts[0]);
                oneQuestion.Value = ReturnValidIntOrMinusOne(lineParts[3]);

                output.Add(oneQuestion);
            }

            return output;
        }

        private int ReturnValidIntOrMinusOne(string numberAsString)
        {
            int number;
            bool validNumber = Int32.TryParse(numberAsString, out number);

            if (validNumber)
            {
                return number;
            }
            else
            {
                return -1;
            }
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
