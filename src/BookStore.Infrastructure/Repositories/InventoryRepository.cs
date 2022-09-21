using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(BookStoreDbContext db) 
            : base(db)
        {
        }

        public async Task<IEnumerable<Inventory>> SearchInventoryForBook(string bookName)
        {
            return await Db.Inventories.AsNoTracking()
                .Include(b => b.Book)
                .Where(b => b.Book.Name.Contains(bookName))
                .ToListAsync();
        }
    }
}
