using Courier.Core.Services;
using Courier.Core.Tests.Mock;
using Xunit.Abstractions;

namespace Courier.Core.ViewModels.Tests
{
    public class StartDayPageViewModelTests
    {
        private readonly ITestOutputHelper _output;

        public StartDayPageViewModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(20)]
        [InlineData(57.67)]
        public void StartDayPageViewModelTest_IF_RIGHT_DATA(decimal money)
        {
            //Arrange
            var db = new MockDatabaseService();
            var service = new CourierService(db);
            var navigation = new MockNavigationService(_output);
            var dialog = new MockDialogService(_output, "", false);
            var vm = new StartDayPageViewModel(service, navigation, dialog)
            {
                InitialMoney = money
            };

            //Act
            var result = vm.StartDayCommand.CanExecute(null);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void StartDayPageViewModelTest_IF_WRONG_DATA()
        {
            //Arrange
            var db = new MockDatabaseService();
            var service = new CourierService(db);
            var navigation = new MockNavigationService(_output);
            var dialog = new MockDialogService(_output, "", false);
            var vm = new StartDayPageViewModel(service, navigation, dialog)
            {
                InitialMoney = -100
            };

            //Act
            var result = vm.StartDayCommand.CanExecute(null);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task StartDayPageViewModelTest_IF_DAY_NO_EXISTS()
        {
            //Arrange
            var db = new MockDatabaseService();
            var service = new CourierService(db);
            var navigation = new MockNavigationService(_output);
            var dialog = new MockDialogService(_output, "", false);
            var vm = new StartDayPageViewModel(service, navigation, dialog);

            //Act
            await vm.StartDayCommand.ExecuteAsync(null);

            //Assert
            Assert.Single(db.Days);
        }

        [Fact]
        public async Task StartDayPageViewModelTest_IF_DAY_EXISTS()
        {
            //Arrange
            var db = new MockDatabaseService();
            db.Days.Add(new Models.Day
            {
                Date = DateTime.Now.Date,
                TimeStart = DateTime.Now
            });
            var service = new CourierService(db);
            var navigation = new MockNavigationService(_output);
            var dialog = new MockDialogService(_output, "", false);
            var vm = new StartDayPageViewModel(service, navigation, dialog);

            //Act
            await vm.StartDayCommand.ExecuteAsync(null);

            //Assert
            Assert.Single(db.Days);
        }
    }
}