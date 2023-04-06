using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace BookStore.Infrastructure.Metrics
{
    public class OtelMetrics
    {
        //Books meters
        private  Counter<int> BooksAddedCounter { get; }
        private  Counter<int> BooksDeletedCounter { get; }
        private  Counter<int> BooksUpdatedCounter { get; }
        private  UpDownCounter<int> TotalBooksUpDownCounter { get; }

        //Categories meters
        private Counter<int> CategoriesAddedCounter { get; }
        private Counter<int> CategoriesDeletedCounter { get; }
        private Counter<int> CategoriesUpdatedCounter { get; }
        private ObservableGauge<int> TotalCategoriesGauge { get; }
        private int _totalCategories = 0;

        //Order meters
        private Histogram<double> OrdersPriceHistogram { get; }
        private Histogram<int> NumberOfBooksPerOrderHistogram { get; }
        private ObservableCounter<int> OrdersCanceledCounter { get; }
        private int _ordersCanceled = 0;
        private Counter<int> TotalOrdersCounter { get; }


        public string MetricName { get; }

        public OtelMetrics(string meterName = "BookStore")
        {
            var meter = new Meter(meterName);
            MetricName = meterName;

            BooksAddedCounter = meter.CreateCounter<int>("books-added", "Book");
            BooksDeletedCounter = meter.CreateCounter<int>("books-deleted", "Book");
            BooksUpdatedCounter = meter.CreateCounter<int>("books-updated", "Book");
            TotalBooksUpDownCounter = meter.CreateUpDownCounter<int>("total-books", "Book");
            
            CategoriesAddedCounter = meter.CreateCounter<int>("categories-added", "Category");
            CategoriesDeletedCounter = meter.CreateCounter<int>("categories-deleted", "Category");
            CategoriesUpdatedCounter = meter.CreateCounter<int>("categories-updated", "Category");
            TotalCategoriesGauge = meter.CreateObservableGauge<int>("total-categories", () => _totalCategories);

            OrdersPriceHistogram = meter.CreateHistogram<double>("orders-price", "Euros", "Price distribution of book orders");
            NumberOfBooksPerOrderHistogram = meter.CreateHistogram<int>("orders-number-of-books", "Books", "Number of books per order");
            OrdersCanceledCounter = meter.CreateObservableCounter<int>("orders-canceled", () => _ordersCanceled);
            TotalOrdersCounter = meter.CreateCounter<int>("total-orders", "Orders");
        }


        //Books meters
        public void AddBook() => BooksAddedCounter.Add(1);
        public void DeleteBook() => BooksDeletedCounter.Add(1);
        public void UpdateBook() => BooksUpdatedCounter.Add(1);
        public void IncreaseTotalBooks() => TotalBooksUpDownCounter.Add(1);
        public void DecreaseTotalBooks() => TotalBooksUpDownCounter.Add(-1);

        //Categories meters
        public void AddCategory() => CategoriesAddedCounter.Add(1);
        public void DeleteCategory() => CategoriesDeletedCounter.Add(1);
        public void UpdateCategory() => CategoriesUpdatedCounter.Add(1);
        public void IncreaseTotalCategories() => _totalCategories++;
        public void DecreaseTotalCategories() => _totalCategories--;

        //Orders meters
        public void RecordOrderTotalPrice(double price) => OrdersPriceHistogram.Record(price);
        public void RecordNumberOfBooks(int amount) => NumberOfBooksPerOrderHistogram.Record(amount);
        public void IncreaseOrdersCanceled() => _ordersCanceled++;
        public void IncreaseTotalOrders(string city) => TotalOrdersCounter.Add(1, KeyValuePair.Create<string, object>("City", city));

    }
}
