using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Metrics;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly OtelMetrics _meters;

        public BookRepository(
            BookStoreDbContext context, 
            OtelMetrics meters) : base(context)
        {
            _meters = meters;
        }

        public override async Task<List<Book>> GetAll()
        {
            return await Db.Books.Include(b => b.Category)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public override async Task<Book> GetById(int id)
        {
            return await Db.Books.Include(b => b.Category)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await Search(b => b.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue)
        {
            return await Db.Books.AsNoTracking()
                .Include(b => b.Category)
                .Where(b => b.Name.Contains(searchedValue) || 
                            b.Author.Contains(searchedValue) ||
                            b.Description.Contains(searchedValue) ||
                            b.Category.Name.Contains(searchedValue))
                .ToListAsync();
        }

        public override async Task Add(Book entity)
        {
            await base.Add(entity);

            _meters.AddBook();
            _meters.IncreaseTotalBooks();
        }

        public override async Task Update(Book entity)
        {
            await base.Update(entity);

            _meters.UpdateBook();
        }

        public override async Task Remove(Book entity)
        {
            await base.Remove(entity);

            _meters.DeleteBook();
            _meters.DecreaseTotalBooks();
        }
    }
}