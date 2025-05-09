using Business.Interfaces;
using Data.Services;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]

    public class UserController : GenericController<UserDto> 
    {

        private readonly UserRepository _extendedService;
        public UserController(IGenericService<UserDto> service, UserRepository extendedService) : base(service)
        {
            _extendedService = extendedService;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            var result = await _extendedService.GetAllWithPersonAsync();
            return Ok(result);
        }
    }



}
