using System;
using System.Globalization;
using DemoUnitTests.API.Utils;
using Moq;
using Xunit;

namespace DemoUnitTests.Tests.API.Utils
{
    public class DateUtilsTests
    {
        [Theory]
        [InlineData("2000-01-01T12:00:00", "2000-01-02T12:00:00", "2000-01-03T12:00:00", "2000-01-02T13:00:00")] // partial overlap
        [InlineData("2000-01-01T00:00:00", "2000-01-01T00:00:00", "2000-01-02T00:00:00", "2000-01-02T00:00:00")] // identical intervals
        public void Overlap_withOverlap(string s1, string s2, string e1, string e2)
        {
            var (start1, start2, end1, end2) = Convert(s1, s2, e1, e2);
            bool result = DateUtils.Overlap(start1, start2, end1, end2);
            Assert.True(result);
        }

        [Theory]
        [InlineData("2000-01-01T00:00:00", "2000-01-02T00:00:00", "2000-01-10T00:00:00", "2000-01-03T00:00:00")] // interval1 contains interval2
        [InlineData("2000-01-02T12:00:00", "2000-01-02T13:00:00", "2000-01-03T12:00:00", "2000-01-02T14:00:00")] // small inside larger (unordered display)
        public void Overlap_withContains(string s1, string s2, string e1, string e2)
        {
            var (start1, start2, end1, end2) = Convert(s1, s2, e1, e2);
            bool result = DateUtils.Overlap(start1, start2, end1, end2);
            Assert.True(result);
        }

        [Theory]
        [InlineData("2000-01-01T12:00:00", "2000-01-02T12:00:00", "2000-01-01T13:00:00", "2000-01-03T12:00:00")] // non overlapping (example in source)
        [InlineData("2000-01-01T12:00:00", "2000-01-01T13:00:00", "2000-01-01T13:00:00", "2000-01-01T14:00:00")] // touching boundaries (end == start) -> no overlap
        public void Overlap_withoutOverlap(string s1, string s2, string e1, string e2)
        {
            var (start1, start2, end1, end2) = Convert(s1, s2, e1, e2);
            bool result = DateUtils.Overlap(start1, start2, end1, end2);
            Assert.False(result);
        }

        [Theory]
        [InlineData("2000-01-05T00:00:00", "2000-01-02T00:00:00", "2000-01-04T00:00:00", "2000-01-06T00:00:00")] // start1 after end1
        [InlineData("2000-01-01T00:00:00", "2000-01-05T00:00:00", "2000-01-04T00:00:00", "2000-01-03T00:00:00")] // start2 after end2
        public void Overlap_withStartAfterEnd(string s1, string s2, string e1, string e2)
        {
            var (start1, start2, end1, end2) = Convert(s1, s2, e1, e2);
            Assert.Throws<ArgumentException>(() => DateUtils.Overlap(start1, start2, end1, end2));
        }


        private (DateTime, DateTime, DateTime, DateTime) Convert(string s1, string s2, string s3, string s4)
        {
            return (DateTime.Parse(s1), DateTime.Parse(s2), DateTime.Parse(s3), DateTime.Parse(s4));
        }

        [Theory]
        [InlineData("1982-05-06", 17)]
        [InlineData("1982-01-01", 18)]
        [InlineData("1982-02-17", 17)]
        [InlineData("1982-02-16", 18)]
        public void GetAge(string date, int expected)
        {
            // arrange
            DateTime bd = DateTime.Parse(date);
            DateUtils utils = new DateUtils();
            Mock<DateProvider> mock = new Mock<DateProvider>();
            mock.Setup(dp => dp.GetDate()).Returns(new DateTime(2000, 02, 16));
            utils._dateProvider = mock.Object;
            int result = utils.GetAge(bd);
            Assert.Equal(expected, result);
        }
    }
}
