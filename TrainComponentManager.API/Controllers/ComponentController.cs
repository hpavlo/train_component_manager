using Microsoft.AspNetCore.Mvc;
using TrainComponentManager.API.Dtos;
using TrainComponentManager.API.Dtos.Mapping;
using TrainComponentManager.API.Interfaces;

namespace TrainComponentManager.API.Controllers
{
    [ApiController]
    [Route("api/components")]
    public class ComponentController(IComponentRepository componentRepository) : ControllerBase
    {
        private readonly IComponentRepository _componentRepository = componentRepository;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComponentRequestDto componentRequestDto)
        {
            var component = componentRequestDto.ToModel();
            await _componentRepository.CreateAsync(component);

            var responseDto = component.ToResponseDto();
            return CreatedAtAction(nameof(Get), new { id = component.Id }, responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("pageNumber and pageSize must be positive integers");
            }

            var (components, totalCount) = await _componentRepository.GetAllAsync(pageNumber, pageSize);
            var componentDtos = components.Select(c => c.ToResponseDto());

            var paginatedResponse = new PaginatedResponseDto<ComponentResponseDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = componentDtos
            };

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var component = await _componentRepository.GetAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return Ok(component.ToResponseDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComponentRequestDto componentRequestDto)
        {
            var component = componentRequestDto.ToModel();
            component.Id = id;

            var updatedComponent = await _componentRepository.UpdateAsync(component);

            if (updatedComponent != null)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _componentRepository.DeleteAsync(id))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchTerm, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("pageNumber and pageSize must be positive integers");
            }

            var (components, totalCount) = await _componentRepository.SearchAsync(searchTerm, pageNumber, pageSize);
            var componentDtos = components.Select(c => c.ToResponseDto());

            var paginatedResponse = new PaginatedResponseDto<ComponentResponseDto>
            {
                Items = componentDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(paginatedResponse);
        }
    }
}
