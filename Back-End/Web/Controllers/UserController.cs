using Business.Interfaces;
using Business.Services;
using Data.Services;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador API para gestionar usuarios.
    /// Hereda de GenericController para operaciones CRUD estándar sobre UserDto.
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : GenericController<UserDto>
    {
        /// <summary>
        /// Servicio extendido específico para operaciones avanzadas con usuarios,
        /// como obtener usuarios con datos relacionados de personas.
        /// </summary>
        private readonly UserRepository _extendedService;

        /// <summary>
        /// Constructor que inyecta el servicio genérico, el servicio extendido y el servicio de logs.
        /// </summary>
        /// <param name="service">Servicio genérico para operaciones CRUD estándar.</param>
        /// <param name="extendedService">Servicio extendido para funcionalidades específicas de usuario.</param>
        /// <param name="logService">Servicio para registrar logs de la aplicación.</param>
        public UserController(IGenericService<UserDto> service, UserRepository extendedService, LogService logService) : base(service, logService)
        {
            _extendedService = extendedService;
        }

        /// <summary>
        /// Obtiene todos los usuarios, incluyendo información adicional relacionada con la persona asociada.
        /// Utiliza un método extendido que realiza joins para obtener datos completos.
        /// </summary>
        /// <returns>Lista de UserDto con datos extendidos.</returns>
        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            var result = await _extendedService.GetAllWithPersonAsync();
            return Ok(result);
        }
    }
}
