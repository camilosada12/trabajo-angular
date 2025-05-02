using Business.Interfaces;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]

    public class RolController : GenericController<rolDto>
    {
        public RolController(IGenericService<rolDto> service) : base(service)
        {
        }
    }
}
