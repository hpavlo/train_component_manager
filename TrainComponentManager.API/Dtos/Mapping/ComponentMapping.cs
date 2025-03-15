using TrainComponentManager.API.Models;

namespace TrainComponentManager.API.Dtos.Mapping
{
    public static class ComponentMapping
    {
        public static Component ToModel(this ComponentRequestDto componentRequestDto)
        {
            return new Component
            {
                Name = componentRequestDto.Name,
                UniqueNumber = componentRequestDto.UniqueNumber,
                CanAssignQuantity = componentRequestDto.CanAssignQuantity,
                Quantity = componentRequestDto.Quantity
            };
        }

        public static ComponentResponseDto ToResponseDto(this Component component)
        {
            return new ComponentResponseDto
            {
                Id = component.Id,
                Name = component.Name,
                UniqueNumber = component.UniqueNumber,
                CanAssignQuantity = component.CanAssignQuantity,
                Quantity = component.Quantity
            };
        }
    }
}
