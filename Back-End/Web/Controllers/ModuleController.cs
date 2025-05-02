using Business.Interfaces;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ModuleController : GenericController<ModuleDto>
    {
        public ModuleController(IGenericService<ModuleDto> service) : base(service)
        {
        }
    }
}
