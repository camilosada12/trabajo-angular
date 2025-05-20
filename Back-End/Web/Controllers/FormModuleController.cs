using Business.Interfaces;
using Business.Services;
using Data.Services;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador para la entidad <see cref="FormModuleDto"/> que extiende la funcionalidad CRUD
    /// básica del <see cref="GenericController{T}"/> con métodos personalizados.
    /// </summary>
    [Route("api/[controller]")]
    public class FormModuleController : GenericController<FormModuleDto>
    {
        private readonly FormModuleRepository _extendedService;

        /// <summary>
        /// Constructor que recibe los servicios necesarios para el controlador.
        /// </summary>
        /// <param name="service">Servicio genérico para operaciones CRUD básicas.</param>
        /// <param name="extendedService">Repositorio específico con métodos extendidos para <see cref="FormModuleDto"/>.</param>
        /// <param name="logService">Servicio para registro de logs.</param>
        public FormModuleController(
            IGenericService<FormModuleDto> service,
            FormModuleRepository extendedService,
            LogService logService) : base(service, logService)
        {
            _extendedService = extendedService;
        }

        /// <summary>
        /// Obtiene todos los registros de <see cref="FormModuleDto"/> incluyendo datos relacionados mediante una consulta extendida.
        /// Requiere autorización.
        /// </summary>
        /// <returns>Una lista con todos los registros enriquecidos con datos relacionados.</returns>
        [HttpGet]
        [Authorize]
        public override async Task<IActionResult> GetAll()
        {
            var result = await _extendedService.GetAllJoinAsync();
            return Ok(result);
        }
    }
}
