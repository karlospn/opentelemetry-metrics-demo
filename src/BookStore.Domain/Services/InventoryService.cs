using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly  IInventoryRepository _inventoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        public InventoryService(IInventoryRepository inventoryRepository, 
            IBookRepository bookRepository, 
            IOrderRepository orderRepository)
        {
            _inventoryRepository = inventoryRepository;
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Inventory> GetById(int id)
        {
            return await _inventoryRepository.GetById(id);
        }

        public async Task<Inventory> Add(Inventory inventory)
        {
            if (_inventoryRepository.Search(b => b.Id == inventory.Id).Result.Any())
                return null;

            if (await _bookRepository.GetById(inventory.Id) is null)
                return null;

            await _inventoryRepository.Add(inventory);
            return inventory;
        }

        public async Task<Inventory> Update(Inventory inventory)
        {
            if (_inventoryRepository.Search(b => b.Id != inventory.Id).Result.Any())
                return null;

            await _inventoryRepository.Update(inventory);
            return inventory;
        }

        public async Task<bool> Remove(Inventory inventory)
        {
            var orders = await _orderRepository.GetOrdersByBookId(inventory.Id);
            
            if (orders.Any(x => !x.IsAlreadyCancelled()))
                return false;

            await _inventoryRepository.Remove(inventory);
            return true;
        }


        public async Task<IEnumerable<Inventory>> SearchInventoryForBook(string bookName)
        {
            return await _inventoryRepository.SearchInventoryForBook(bookName);
        }
    }
}
