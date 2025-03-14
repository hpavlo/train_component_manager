using System.ComponentModel.DataAnnotations;

namespace TrainComponentManager.API.Models
{
    public class Component
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string UniqueNumber { get; set; } = string.Empty;

        [Required]
        public bool CanAssignQuantity { get; set; }

        public uint Quantity { get; set; } = 0;
    }
}
