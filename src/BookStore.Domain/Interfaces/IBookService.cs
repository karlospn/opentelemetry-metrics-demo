using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Add(Book book);
        Task<Book> Update(Book book);
        Task<bool> Remove(Book book);
        Task<IEnumerable<Book>> GetBooksByCategory(int categoryId);
        Task<IEnumerable<Book>> Search(string bookName);
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue);
    }
}