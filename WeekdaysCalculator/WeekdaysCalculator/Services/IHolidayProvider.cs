using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeekdaysCalculator.Models;

namespace WeekdaysCalculator.Services
{
    /// <summary>
    /// Notes:
    /// Provides the interface for retreival of holidays between two dates.
    /// The implementation can be SQL, Remote source or any other source.
    /// For the sake of the excercise two implementations provided one from json file and one hard-coded
    /// The function returns Task so it can be run asynchroniously
    /// </summary>
    public interface IHolidayProvider
    {
        Task<IEnumerable<Holiday>> GetHolidaysBetweenDates(DateTime fromDate, DateTime toDate);
    }
}
