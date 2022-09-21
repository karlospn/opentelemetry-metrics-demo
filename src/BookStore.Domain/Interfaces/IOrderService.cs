using System;
using BookStore.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task<Order> Add(Order order); 
        Task<Order> Remove(Order order);
    }
}
