using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainComponentManager.API.Models;

namespace TrainComponentManager.API.Controllers
{
    [ApiController]
    [Route("api/components")]
    public class ComponentController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Component>> GetAll()
        {
            return [];
        }
    }
}
