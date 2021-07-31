using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using System;

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


    }
}
