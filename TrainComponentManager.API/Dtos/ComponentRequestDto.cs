using System.ComponentModel.DataAnnotations;

namespace TrainComponentManager.API.Dtos
{
    public class ComponentRequestDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string UniqueNumber { get; set; } = string.Empty;

        [Required]
        public bool CanAssignQuantity { get; set; }

        public uint Quantity { get; set; }
    }
}
