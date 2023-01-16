using Courier.Core.Tests.Mock;

namespace Courier.Core.Services.Tests
{
    public class CourierServiceTests
    {
        [Theory]
        [InlineData(0)]
        public async Task StartDayAsyncTest_IF_DAY_EXISTS(int initialValue)
        {
            //Arrange
            var db = new MockDatabaseService();
            db.Days.Add(new Models.Day
            {
                Id = 0,
                Date = DateTime.Now.Date,
                TimeStart = DateTime.Now,
            });
            var _service = new CourierService(db);

            //Act
            var result = await _service.StartDayAsync(DateTime.Now, initialValue);

            //Assert
            Assert.False(result);
            Assert.Single(db.Days);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(80)]
        public async Task StartDayAsyncTest_IF_DAY_NO_EXISTS(int initialValue)
        {
            //Arrange
            var db = new MockDatabaseService();
            db.Days.Add(new Models.Day
            {
                Id = 0,
                Date = DateTime.Now.AddDays(-1).Date,
                TimeStart = DateTime.Now.AddDays(-1),
            });
            var _service = new CourierService(db);

            //Act
            var result = await _service.StartDayAsync(DateTime.Now, initialValue);

            //Assert
            Assert.True(result);
            Assert.Equal(2, db.Days.Count);

            var day = await db.GetDayByDateAsync(DateTime.Now);
            Assert.NotNull(day);
            Assert.Equal(initialValue, day.CashMoney);
            Assert.Null(day.TimeFinish);
        }
    }
}