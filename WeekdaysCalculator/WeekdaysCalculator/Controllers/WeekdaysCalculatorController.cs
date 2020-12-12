using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeekdaysCalculator.Services;

namespace WeekdaysCalculator.Controllers
{
    [ApiController]
    [Route("weekdays")]
    public class WeekdaysCalculatorController : ControllerBase
    {
        private IWeekdaysCalculatorService weekdaysCalculator { get; set; }
        private readonly ILogger<WeekdaysCalculatorController> _logger;

        public WeekdaysCalculatorController(ILogger<WeekdaysCalculatorController> logger, IWeekdaysCalculatorService weekdaysCalculator)
        {
            _logger = logger;
            this.weekdaysCalculator = weekdaysCalculator;
        }

        [HttpGet]
        public string Get()
        {
            return @"Usage: https://{server}/weekdays/{fromDate}/{toDate}
Date format: yyyy-MM-dd
Examples : https://localhost:44307/weekdays/2014-08-07/2014-08-11
           https://localhost:44307/weekdays/2014-08-13/2014-08-21";
        }


        [HttpGet, Route("{fromStr}/{toStr}")]
        public async Task<IActionResult> GetWeekdays(string fromStr, string toStr)
        {
            try
            {
                var from = DateTime.ParseExact(fromStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var to = DateTime.ParseExact(toStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var weekDays = await weekdaysCalculator.CalculateWeekdays(from, to);
                return Ok(weekDays);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
