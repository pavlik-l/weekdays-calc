using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeekdaysCalculator.Services
{
    public interface IWeekdaysCalculatorService
    {
        Task<int> CalculateWeekdays(DateTime from, DateTime to);
    }
}
