using Business.Interfaces;
using Business.Services;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador genérico para manejar operaciones CRUD de la entidad FormDto.
    /// Hereda de <see cref="GenericController{T}"/> para reutilizar la lógica común.
    /// </summary>
    [Route("api/[controller]")]
    public class FormControllerPrueba : GenericController<FormDto>
    {
        /// <summary>
        /// Constructor que recibe el servicio genérico para FormDto y un servicio de logs.
        /// </summary>
        /// <param name="service">Servicio genérico que implementa la lógica de negocio para FormDto.</param>
        /// <param name="logService">Servicio para registro de logs de la aplicación.</param>
        public FormControllerPrueba(IGenericService<FormDto> service, LogService logService)
            : base(service, logService)
        {
        }
    }
}
