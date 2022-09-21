using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApi.Dtos.Inventory
{
    public class InventoryEditDto
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Amount { get; set; }
    }
}