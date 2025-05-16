

using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class GenericController<TDto> : ControllerBase where TDto : class
    {
        private readonly IGenericService<TDto> _service;

        public GenericController(IGenericService<TDto> service)
        {
            _service = service;
        }

        
        [HttpGet]
        [Authorize]
        public virtual async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());



        [HttpGet("{id}")]
        [Authorize]
        public virtual async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        [Authorize]
        public virtual async Task<IActionResult> Create([FromBody] TDto dto) => Ok(await _service.CreateAsync(dto));

        [HttpPut]
        [Authorize]
        public virtual async Task<IActionResult> Update([FromBody] TDto dto) => Ok(await _service.UpdateAsync(dto));

        [HttpDelete("{id}")]
        [Authorize]
        public virtual async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteAsync(id));

        [HttpPut("logic/{id}")]
        [Authorize]
        public virtual async Task<IActionResult> DeleteLogic(int id)
        {
            var result = await _service.DeleteAsyncLogic(id);
            return result ? Ok() : NotFound();
        }

        [HttpPatch("recuperar/{id}")]
        [Authorize]
        public virtual async Task<IActionResult> Patch(int id)
        {
            var result = await _service.PatchAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
