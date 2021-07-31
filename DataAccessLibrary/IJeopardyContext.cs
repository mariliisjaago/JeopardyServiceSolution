using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary
{
    public interface IJeopardyContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<QuestionAndAnswer> QuestionsAndAnswers { get; set; }
        DbSet<Round> Rounds { get; set; }
        DbSet<Show> Shows { get; set; }
    }
}