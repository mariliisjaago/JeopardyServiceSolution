using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLibrary
{
    public class JeopardyContext : DbContext, IJeopardyContext
    {
        public DbSet<QuestionAndAnswer> QuestionsAndAnswers { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var config = builder.Build();

            options.UseSqlServer(config.GetConnectionString("Development"));
        }
    }
}
