using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JeopardyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordController : ControllerBase
    {
        private readonly IJeopardyContext _dbContext;

        public KeywordController(IJeopardyContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet("{keyword}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetByKeyword(string keyword)
        {
            var results = _dbContext.QuestionsAndAnswers
                .Where(x => x.Question.Contains(keyword) | x.Answer.Contains(keyword))
                .Include(x => x.ShowData)
                .Include(x => x.Round)
                .Include(x => x.Category);

            return Ok(results);
        }
    }
}
