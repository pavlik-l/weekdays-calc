using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WeekdaysCalculator.Models;

namespace WeekdaysCalculator.Services
{
    public class WeekdaysCalculatorService : IWeekdaysCalculatorService
    {
        private IHolidayProvider holidaysProvider { get; set; }
        public WeekdaysCalculatorService(IHolidayProvider holidaysProvider)
        {
            this.holidaysProvider = holidaysProvider;
        }

        public async Task<int> CalculateWeekdays(DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentException("From date cannot be after target date");
            }
            from = from.AddDays(1);
            to = to.AddDays(-1);

            if (from > to || from == to)
                return 0;

            var holidays = await holidaysProvider.GetHolidaysBetweenDates(from, to);

            var totalDays = (int)Math.Floor((to - from).TotalDays) + 1;
            var fullWeeks = totalDays / 7;
            var remainingDays = totalDays % 7;

            if (remainingDays != 0)
            {
                remainingDays -= DetermineNumberOfWeekendDays(from.DayOfWeek, to.DayOfWeek);
            }

            var result = fullWeeks * 5 + remainingDays - DetermineNumberWeekdayHolidays(holidays);

            return result;
        }

        private int DetermineNumberOfWeekendDays(DayOfWeek first, DayOfWeek second)
        {
            if (first > second)
                return 2;

            if (first == DayOfWeek.Sunday || second == DayOfWeek.Saturday)
                return 1;

            return 0;
        }

        private int DetermineNumberWeekdayHolidays(IEnumerable<Holiday> holidays)
        {
            var count = holidays.Count(h => h.Date.DayOfWeek != DayOfWeek.Sunday &&
                                            h.Date.DayOfWeek != DayOfWeek.Saturday);

            return count;
        }
    }
}
