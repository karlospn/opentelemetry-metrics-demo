namespace BookStore.Domain.Models
{
    public class Inventory: Entity
    {
        public int Amount { get; set; }
        public virtual Book Book { get; set; }

        public bool HasInventoryAvailable()
        {
            return Amount > 0;
        }

        public void DecreaseInventory()
        {
            Amount -= 1;
        }

        public void IncreaseInventory()
        {
            Amount += 1;
        }
    }
}
