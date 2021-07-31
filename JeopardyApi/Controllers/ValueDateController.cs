using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using System;

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


    }
}
