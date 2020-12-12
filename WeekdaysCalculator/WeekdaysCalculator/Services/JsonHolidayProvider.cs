using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeekdaysCalculator.Models;
using WeekdaysCalculator.Options;

namespace WeekdaysCalculator.Services
{
    public class JsonHolidayProvider : IHolidayProvider
    {
        List<Holiday> holidays;
        public JsonHolidayProvider(IOptions<HolidayOptions> holidayOptions)
        {
            var path = holidayOptions.Value.JsonFilePath;


            using (var reader = new StreamReader(path))
            {
                var holidaysString = reader.ReadToEnd();

                holidays = JsonSerializer.Deserialize<List<Holiday>>(holidaysString);
            }
        }

        public async Task<IEnumerable<Holiday>> GetHolidaysBetweenDates(DateTime fromDate, DateTime toDate)
        {
            var result = from holiday in holidays
                         where holiday.Date >= fromDate && holiday.Date <= toDate
                         orderby holiday.Date
                         select holiday;

            return result;
        }
        
    }
}
