using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeekdaysCalculator.Models;


namespace PublicHolidaysDBGeneratorUtil
{
    class Program
    {
        static List<Holiday> holidays;
        static void Main(string[] args)
        {

            var dir = Environment.CurrentDirectory + "\\CSV";

            var files = Directory.GetFiles(dir, "*.csv");

            holidays = new List<Holiday>();

            foreach (var filePath in files)
            {
                ReadSingleFile(filePath);
            }

            var jsonString = JsonSerializer.Serialize(holidays);
            File.WriteAllText("holidays.json", jsonString);
        }

        private static void ReadSingleFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line.Contains("nsw", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var holiday = ParseLine(line);

                        holidays.Add(holiday);
                    }
                }
            }
        }

        private static Holiday ParseLine(string line)
        {
            var split = line.Split(",");
            DateTime date;

            var i = 0;
            if (!DateTime.TryParseExact(split[i], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                i++;
                date = DateTime.ParseExact(split[i], "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            var name = split[i + 1];

            return new Holiday { Date = date, Name = name };
        }


    }
}
