using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class OrderService(IOrderRepository orderRepository,
            IBookRepository bookRepository,
            IInventoryRepository inventoryRepository)
        : IOrderService
    {
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await orderRepository.GetAll();
        }

        public async Task<Order> GetById(int id)
        {
            return await orderRepository.GetById(id);
        }

        public async Task<Order> Add(Order order)
        {
            double sum = 0;
            List<Inventory> inventoryList = [];

            for (var i = 0; i < order.Books.Count; i++)
            {
                var orderingBook = await bookRepository.GetById(order.Books[i].Id);
                if (orderingBook is null)
                    return null;

                if (!orderingBook.HasPositivePrice())
                    return null;

                if (!orderingBook.HasCorrectPublishDate())
                    return null;

                var inventory = await inventoryRepository.GetById(order.Books[i].Id);
                if (inventory is null || !inventory.HasInventoryAvailable())
                    return null;

                order.Books[i] = orderingBook;
                sum += orderingBook.Value;
                inventoryList.Add(inventory);
            }

            foreach (var inventoryItem in inventoryList)
            {
                inventoryItem.DecreaseInventory();
                await inventoryRepository.Update(inventoryItem);
            }

            order.SetTotalAmount(sum);
            order.SetNewOrderStatus();
            await orderRepository.Add(order);

            return order;
        }

        public async Task<Order> Remove(Order order)
        {
            if (order.IsAlreadyCancelled())
                return null;

            order.SetCancelledStatus();
            await orderRepository.Update(order);

            foreach (var book in order.Books)
            {
                var inventory = await inventoryRepository.GetById(book.Id);
                inventory.IncreaseInventory();
                await inventoryRepository.Update(inventory);
            }

            return order;

        }
    }
}
