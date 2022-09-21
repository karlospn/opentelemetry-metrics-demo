using BookStore.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<List<Order>> GetOrdersByBookId(int bookId);
    }
}
