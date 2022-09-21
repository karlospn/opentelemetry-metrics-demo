using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        new Task<List<Book>> GetAll();
        new Task<Book> GetById(int id);
        Task<IEnumerable<Book>> GetBooksByCategory(int categoryId);
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue);
    }
}