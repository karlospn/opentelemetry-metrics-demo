using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<Book> Add(Book book)
        {
            if (_bookRepository.Search(b => b.Name == book.Name).Result.Any())
                return null;

            var category = await _categoryRepository.GetById(book.CategoryId);
            if (category is null)
                return null;

            if (!book.HasCorrectPublishDate())
                return null;

            if (!book.HasPositivePrice())
                return null;

            await _bookRepository.Add(book);
            return book;
        }

        public async Task<Book> Update(Book book)
        {
            if (_bookRepository.Search(b => b.Name == book.Name && b.Id != book.Id).Result.Any())
                return null;

            if (!book.HasCorrectPublishDate())
                return null;

            if (!book.HasPositivePrice())
                return null;

            await _bookRepository.Update(book);
            return book;
        }

        public async Task<bool> Remove(Book book)
        {
            await _bookRepository.Remove(book);
            return true;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await _bookRepository.GetBooksByCategory(categoryId);
        }

        public async Task<IEnumerable<Book>> Search(string bookName)
        {
            return await _bookRepository.Search(c => c.Name.Contains(bookName));
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue)
        {
            return await _bookRepository.SearchBookWithCategory(searchedValue);
        }
    }
}