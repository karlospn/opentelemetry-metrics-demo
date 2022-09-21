using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public OrderService(IOrderRepository orderRepository,
            IBookRepository bookRepository, 
            IInventoryRepository inventoryRepository)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _inventoryRepository = inventoryRepository;
        }


        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<Order> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task<Order> Add(Order order)
        {
            double sum = 0;
            List<Inventory> inventoryList = new();

            for (var i = 0; i < order.Books.Count; i++)
            {
                var orderingBook = await _bookRepository.GetById(order.Books[i].Id);
                if (orderingBook is null)
                    return null;

                if (!orderingBook.HasPositivePrice())
                    return null;

                if (!orderingBook.HasCorrectPublishDate())
                    return null;

                var inventory = await _inventoryRepository.GetById(order.Books[i].Id);
                if (inventory is null || !inventory.HasInventoryAvailable())
                    return null;

                order.Books[i] = orderingBook;
                sum += orderingBook.Value;
                inventoryList.Add(inventory);
            }

            foreach (var inventoryItem in inventoryList)
            {
                inventoryItem.DecreaseInventory();
                await _inventoryRepository.Update(inventoryItem);
            }

            order.SetTotalAmount(sum);
            order.SetNewOrderStatus();
            await _orderRepository.Add(order);

            return order;
        }

        public async Task<Order> Remove(Order order)
        {
            if (order.IsAlreadyCancelled())
                return null;

            order.SetCancelledStatus();
            await _orderRepository.Update(order);

            foreach (var book in order.Books)
            {
                var inventory = await _inventoryRepository.GetById(book.Id);
                inventory.IncreaseInventory();
                await _inventoryRepository.Update(inventory);
            }

            return order;

        }
    }
}
