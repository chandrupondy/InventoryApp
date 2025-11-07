using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Models
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
