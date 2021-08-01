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
            try
            {
                var results = _dbContext.QuestionsAndAnswers
                .Where(x => x.Question.Contains(keyword) | x.Answer.Contains(keyword))
                .Include(x => x.ShowData)
                .Include(x => x.Round)
                .Include(x => x.Category);

                if (results == null)
                {
                    return Ok("Your keyword was not found in given Jeopardy questions nor answers.");
                }
                else
                {
                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
