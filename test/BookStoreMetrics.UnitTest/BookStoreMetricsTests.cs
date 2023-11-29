using System.Diagnostics.Metrics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics.Testing;

namespace BookStoreMetrics.UnitTest
{
    public class BookStoreMetricsTests
    {
        private static IServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            var config = CreateIConfiguration();
            serviceCollection.AddMetrics();
            serviceCollection.AddSingleton(config);
            serviceCollection.AddSingleton<BookStore.Infrastructure.Metrics.BookStoreMetrics>();
            return serviceCollection.BuildServiceProvider();
        }

        private static IConfiguration CreateIConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"BookStoreMeterName", "BookStore"}
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();
        }

        [Fact]
        public void GivenTheTotalNumberOfBooksOnTheStore_WhenWeRecordThemOnAHistogram_ThenTheValueGetsRecordedSuccessfully()
        {
            //Arrange
            var services = CreateServiceProvider();
            var metrics = services.GetRequiredService<BookStore.Infrastructure.Metrics.BookStoreMetrics>();
            var meterFactory = services.GetRequiredService<IMeterFactory>();
            var collector = new MetricCollector<int>(meterFactory, "BookStore", "orders-number-of-books");

            // Act
            metrics.RecordNumberOfBooks(35);

            // Assert
            var measurements = collector.GetMeasurementSnapshot();
            Assert.Equal(35, measurements[0].Value);
        }

        [Fact]
        public void GivenASetOfBooks_WhenWeIncreaseAndDecreaseTheInventory_ThenTheTotalAmountOfBooksIsRecordedSuccessfully()
        {
            //Arrange
            var services = CreateServiceProvider();
            var metrics = services.GetRequiredService<BookStore.Infrastructure.Metrics.BookStoreMetrics>();
            var meterFactory = services.GetRequiredService<IMeterFactory>();
            var collector = new MetricCollector<int>(meterFactory, "BookStore", "total-books");

            // Act
            metrics.IncreaseTotalBooks();
            metrics.IncreaseTotalBooks();
            metrics.DecreaseTotalBooks();
            metrics.IncreaseTotalBooks();
            metrics.IncreaseTotalBooks();
            metrics.DecreaseTotalBooks();
            metrics.DecreaseTotalBooks();
            metrics.IncreaseTotalBooks();

            // Assert
            var measurements = collector.GetMeasurementSnapshot();
            Assert.Equal(2, measurements.EvaluateAsCounter());
        }

        [Fact]
        public void GivenSomeNewBookOrders_WhenWeIncreaseTheTotalOrdersCounter_ThenTheCountryGetsStoredAsATag()
        {
            //Arrange
            var services = CreateServiceProvider();
            var metrics = services.GetRequiredService<BookStore.Infrastructure.Metrics.BookStoreMetrics>();
            var meterFactory = services.GetRequiredService<IMeterFactory>();
            var collector = new MetricCollector<int>(meterFactory, "BookStore", "total-orders");

            // Act
            metrics.IncreaseTotalOrders("Barcelona");
            metrics.IncreaseTotalOrders("Paris");

            // Assert
            var measurements = collector.GetMeasurementSnapshot();

            Assert.True(measurements.ContainsTags("City").Any());
            Assert.Equal(2, measurements.EvaluateAsCounter());
        }

        [Fact]
        public void GivenSomeNewBookCategories_WhenWeIncreaseAndDecreaseTheObservableGauge_ThenTheLastMeasurementOnTheCollectorIsCorrect()
        {
            //Arrange
            var services = CreateServiceProvider();
            var metrics = services.GetRequiredService<BookStore.Infrastructure.Metrics.BookStoreMetrics>();
            var meterFactory = services.GetRequiredService<IMeterFactory>();
            var collector = new MetricCollector<int>(meterFactory, "BookStore", "total-categories");

            // Act
            metrics.IncreaseTotalCategories();
            metrics.DecreaseTotalCategories();
            metrics.IncreaseTotalCategories();
            metrics.IncreaseTotalCategories();

            // Assert
            collector.RecordObservableInstruments();
            Assert.Equal(2, collector.LastMeasurement?.Value);
        }
    }
}