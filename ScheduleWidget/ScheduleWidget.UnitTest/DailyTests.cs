﻿using System;
using System.Linq;
using NUnit.Framework;
using ScheduleWidget.ScheduledEvents;
using ScheduleWidget.Enums;
using ScheduleWidget.TemporalExpressions;

namespace ScheduleWidget.UnitTest
{
    [TestFixture]
    public class DailyTests
    {
        [Test]
        public void DailyEventTest1()
        {
            var aEvent = new Event()
            {
                ID = 1,
                Title = "Event 1",
                FrequencyTypeOptions = FrequencyTypeEnum.Daily,
                DaysOfWeekOptions = DayOfWeekEnum.EveryDay,
                StartDateTime = new DateTime(2013, 1, 1)
            };

            var schedule = new Schedule(aEvent);

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 2, 10)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 4, 29)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 11, 17)));
        }

        [Test]
        public void DailyEventTest2()
        {
            var aEvent = new Event()
            {
                ID = 1,
                Title = "Event 2",
                RangeInYear = null,
                FrequencyTypeOptions = FrequencyTypeEnum.Daily,
                DaysOfWeekOptions = DayOfWeekEnum.Thu,
                StartDateTime = new DateTime(2013, 1, 1)
            };

            var schedule = new Schedule(aEvent);

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 2, 14)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 4, 25)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 11, 7)));
        }

        [Test]
        public void DailyEventTest3()
        {
            var aEvent = new Event()
            {
                ID = 1,
                Title = "Event 3",
                FrequencyTypeOptions = FrequencyTypeEnum.Daily,
                RepeatInterval = 4,
                StartDateTime = new DateTime(2013, 1, 3)
            };

            var schedule = new Schedule(aEvent);

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 1, 7)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 1, 12)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 2, 4)));
        }

        [Test]
        public void DailyEventTest4()
        {
            var holidays = new UnionTE();
            holidays.Add(new FixedHolidayTE(2, 4));

            var aEvent = new Event()
            {
                ID = 1,
                Title = "Event 4",
                FrequencyTypeOptions = FrequencyTypeEnum.Daily,
                RepeatInterval = 4,
                StartDateTime = new DateTime(2013, 1, 3)
            };

            var schedule = new Schedule(aEvent, holidays);

            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 1, 7)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 1, 13)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 2, 4)));
        }

        [Test]
        public void DailyEventTest5()
        {

            var aEvent = new Event()
            {
                ID = 5,
                Title = "Event 5",
                FrequencyTypeOptions = FrequencyTypeEnum.Daily,
                RepeatInterval = 1,
                StartDateTime = new DateTime(2013, 8, 8)
            };

            var schedule = new Schedule(aEvent);

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 5)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 6)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 7)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 8)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 9)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 10)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 11)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 12)));

        }

        [Test]
        public void DailyEventTest6()
        {

            var aEvent = new Event()
            {
                ID = 6,
                Title = "Event 6",
                FrequencyTypeOptions = FrequencyTypeEnum.Daily,
                RepeatInterval = 2,
                StartDateTime = new DateTime(2013, 8, 8)
            };

            var schedule = new Schedule(aEvent);

            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 5)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 6)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 7)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 8)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 9)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 10)));
            Assert.IsFalse(schedule.IsOccurring(new DateTime(2013, 8, 11)));
            Assert.IsTrue(schedule.IsOccurring(new DateTime(2013, 8, 12)));

        }

        [Test]
        public void DailyEventTest7()
        {
            // FirstDateTime should be optional for daily events
            var aEvent = new Event()
            {
                ID = 1,
                Title = "Event 1",
                FrequencyTypeOptions = FrequencyTypeEnum.Daily,
                DaysOfWeekOptions = DayOfWeekEnum.EveryDay
            };

            var schedule = new Schedule(aEvent);

            var during = new DateRange()
            {
                StartDateTime = DateTime.Now.AddDays(-30),
                EndDateTime = DateTime.Now.AddDays(30)
            };

            var occurrences = schedule.Occurrences(during);

            Assert.IsTrue(occurrences.Any());
        }
    }
}
