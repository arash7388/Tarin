using System;
using NUnit.Framework;
using Common;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        public static DateTime AdjustTimeOfDate(DateTime input)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            input += ts;
            return input;
        }

        [Test]
        public void TestEnDate()
        {
            var inputDate = "1399/04/31";
            var miladiDate = inputDate.ToEnDate();

            Assert.IsTrue(miladiDate.Year == 2020, "Wrong year");
            Assert.IsTrue(miladiDate.Month == 7, "Wrong month");
            Assert.IsTrue(miladiDate.Day == 21, "Wrong day");
        }
    }
}
