using DataAccessLibrary;
using DataAccessLibrary.Models;
using JeopardyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JeopardyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueDateController : ControllerBase
    {
        private readonly IJeopardyContext _dbContext;

        public ValueDateController(IJeopardyContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get([FromBody] ValueDateLookupModel lookupModel)
        {
            IOrderedQueryable<ValueDateResultModel> intermediateData;

            try
            {
                if (lookupModel.IsFinalQuestion == true)
                {
                    intermediateData = _dbContext.QuestionsAndAnswers
                        .Where(x => x.Round.RoundName == "Final Jeopardy!")
                        .Where(x => x.ShowData.AirDate.Month == lookupModel.Date.Month && x.ShowData.AirDate.Year == lookupModel.Date.Year)
                        .Include(x => x.Round)
                        .Include(x => x.Category)
                        .Include(x => x.ShowData)
                        .Select(x => new ValueDateResultModel { DifferenceInDays = Math.Abs(EF.Functions.DateDiffDay(x.ShowData.AirDate, lookupModel.Date)), QuestionData = x })
                        .OrderBy(x => x.DifferenceInDays);
                }

                intermediateData = _dbContext.QuestionsAndAnswers
                    .Where(x => x.Value == lookupModel.Value)
                    .Where(x => x.ShowData.AirDate.Month == lookupModel.Date.Month && x.ShowData.AirDate.Year == lookupModel.Date.Year)
                    .Include(x => x.Round)
                    .Include(x => x.Category)
                    .Include(x => x.ShowData)
                    .Select(x => new ValueDateResultModel { DifferenceInDays = Math.Abs(EF.Functions.DateDiffDay(x.ShowData.AirDate, lookupModel.Date)), QuestionData = x })
                    .OrderBy(x => x.DifferenceInDays);

                DateTime closestShowDate = intermediateData.FirstOrDefault().QuestionData.ShowData.AirDate;

                List<QuestionAndAnswer> results = intermediateData.Where(x => x.QuestionData.ShowData.AirDate == closestShowDate).Select(x => x.QuestionData).ToList();

                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
