using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IBookRepository bookRepository)
        : ICategoryService
    {
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await categoryRepository.GetById(id);
        }

        public async Task<Category> Add(Category category)
        {
            if (categoryRepository.Search(c => c.Name == category.Name).Result.Any())
                return null;

            await categoryRepository.Add(category);
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            if (categoryRepository.Search(c => c.Name == category.Name && c.Id != category.Id).Result.Any())
                return null;

            if (!categoryRepository.Search(c => c.Id == category.Id).Result.Any())
                return null;

            await categoryRepository.Update(category);
            return category;
        }

        public async Task<bool> Remove(Category category)
        {
            var books = await bookRepository.GetBooksByCategory(category.Id);
            if (books.Any()) return false;

            await categoryRepository.Remove(category);
            return true;
        }

        public async Task<IEnumerable<Category>> Search(string categoryName)
        {
            return await categoryRepository.Search(c => c.Name.Contains(categoryName));
        }
    }
}