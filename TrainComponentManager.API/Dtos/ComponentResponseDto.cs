using System.ComponentModel.DataAnnotations;

namespace TrainComponentManager.API.Dtos
{
    public class ComponentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UniqueNumber { get; set; } = string.Empty;
        public bool CanAssignQuantity { get; set; }
        public uint Quantity { get; set; }
    }
}
