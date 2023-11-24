using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        : IBookService
    {
        public async Task<IEnumerable<Book>> GetAll()
        {
            return await bookRepository.GetAll();
        }

        public async Task<Book> GetById(int id)
        {
            return await bookRepository.GetById(id);
        }

        public async Task<Book> Add(Book book)
        {
            if (bookRepository.Search(b => b.Name == book.Name).Result.Any())
                return null;

            var category = await categoryRepository.GetById(book.CategoryId);
            if (category is null)
                return null;

            if (!book.HasCorrectPublishDate())
                return null;

            if (!book.HasPositivePrice())
                return null;

            await bookRepository.Add(book);
            return book;
        }

        public async Task<Book> Update(Book book)
        {
            if (bookRepository.Search(b => b.Name == book.Name && b.Id != book.Id).Result.Any())
                return null;

            if (!book.HasCorrectPublishDate())
                return null;

            if (!book.HasPositivePrice())
                return null;

            await bookRepository.Update(book);
            return book;
        }

        public async Task<bool> Remove(Book book)
        {
            await bookRepository.Remove(book);
            return true;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await bookRepository.GetBooksByCategory(categoryId);
        }

        public async Task<IEnumerable<Book>> Search(string bookName)
        {
            return await bookRepository.Search(c => c.Name.Contains(bookName));
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue)
        {
            return await bookRepository.SearchBookWithCategory(searchedValue);
        }
    }
}