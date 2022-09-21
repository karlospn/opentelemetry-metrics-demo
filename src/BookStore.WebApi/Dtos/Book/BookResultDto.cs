namespace BookStore.WebApi.Dtos.Book
{
    public class BookResultDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public double Value { get; set; }

        public DateTime PublishDate { get; set; }
    }
}