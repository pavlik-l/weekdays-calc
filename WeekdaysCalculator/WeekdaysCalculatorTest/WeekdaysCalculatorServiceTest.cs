using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WeekdaysCalculator.Services;

namespace WeekdaysCalculatorTest
{
    [TestClass]
    public class WeekdaysCalculatorServiceTest
    {

        private WeekdaysCalculatorService service;
        
        [TestInitialize]
        public void TestInit()
        {
            StaticHolidayProvider holidayProvider = new StaticHolidayProvider();
            service = new WeekdaysCalculatorService(holidayProvider);
        }

        [TestMethod]
        public void CalculateWeekdays_SameDate()
        {
            var from = new DateTime(2020, 12, 7);
            var to = new DateTime(2020, 12, 7);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateWeekdays_ConsecutiveDates()
        {
            var from = new DateTime(2020, 12, 7);
            var to = new DateTime(2020, 12, 7);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateWeekdays_FromGreaterThanTo()
        {
            var from = new DateTime(2020, 12, 8);
            var to = new DateTime(2020, 12, 7);
            
            service.CalculateWeekdays(from, to).GetAwaiter().GetResult();
         }

        [TestMethod]
        public void CalculateWeekdays_0Weeks0Weekends()
        {
            var from = new DateTime(2020, 12, 7);
            var to = new DateTime(2020, 12, 10);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CalculateWeekdays_0Weeks2Weekends()
        {
            var from = new DateTime(2014, 08, 07);
            var to = new DateTime(2014, 08, 11);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void CalculateWeekdays_1Week2Weekends()
        {
            var from = new DateTime(2014, 08, 13);
            var to = new DateTime(2014, 08, 21);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(5, result); 
        }

        [TestMethod]
        public void CalculateWeekdays_0Week2Weekends1Holiday()
        {
            var from = new DateTime(2020, 12, 31);
            var to = new DateTime(2021, 01 , 04);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateWeekdays_2Week6Weekends2Holiday()
        {
            var from = new DateTime(2020, 12, 18);
            var to = new DateTime(2021, 01, 04);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(7, result);
        }

        //According to https://excelnotes.com/working-days-new-south-wales-2020/
        [TestMethod]
        public void CalculateWeekdays_Whole2020Year()
        {
            var from = new DateTime(2019, 12, 31);
            var to = new DateTime(2021, 01, 01);

            var result = service.CalculateWeekdays(from, to).Result;

            Assert.AreEqual(253, result);
        }
    }
}
