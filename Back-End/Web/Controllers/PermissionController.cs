using Business.Interfaces;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PermissionController : GenericController<PermissionDto>
    {
        public PermissionController(IGenericService<PermissionDto> service) : base(service)
        {
        }
    }
}
