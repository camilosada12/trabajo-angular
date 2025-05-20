using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Enums;

namespace Business.Interfaces
{
    /// <summary>
    /// Interfaz genérica para servicios que manejan operaciones CRUD y otras acciones sobre DTOs.
    /// </summary>
    /// <typeparam name="TDto">Tipo de Data Transfer Object (DTO) con el que trabaja el servicio.</typeparam>
    public interface IGenericService<TDto>
    {
        /// <summary>
        /// Obtiene todos los registros del tipo especificado.
        /// </summary>
        /// <returns>Una colección de todos los DTOs.</returns>
        Task<IEnumerable<TDto>> GetAllAsync();

        /// <summary>
        /// Obtiene un registro por su identificador.
        /// </summary>
        /// <param name="id">Identificador del registro.</param>
        /// <returns>El DTO correspondiente o null si no se encuentra.</returns>
        Task<TDto?> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo registro con la información del DTO proporcionado.
        /// </summary>
        /// <param name="dto">DTO con los datos para crear el nuevo registro.</param>
        /// <returns>El DTO creado con los datos guardados.</returns>
        Task<TDto> CreateAsync(TDto dto);

        /// <summary>
        /// Actualiza un registro existente con la información del DTO proporcionado.
        /// </summary>
        /// <param name="dto">DTO con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa; false en caso contrario.</returns>
        Task<bool> UpdateAsync(TDto dto);

        /// <summary>
        /// Elimina un registro por su identificador, ya sea de forma física o lógica.
        /// </summary>
        /// <param name="id">Identificador del registro a eliminar.</param>
        /// <param name="mode">Modo de eliminación (físico o lógico).</param>
        /// <returns>True si la eliminación fue exitosa; false en caso contrario.</returns>
        Task<bool> DeleteAsync(int id, DeleteMode mode);

        /// <summary>
        /// Aplica un parche o cambio parcial sobre un registro específico.
        /// </summary>
        /// <param name="id">Identificador del registro a modificar.</param>
        /// <returns>True si el parche fue aplicado correctamente; false en caso contrario.</returns>
        Task<bool> PatchAsync(int id);

        /// <summary>
        /// Agrega una nueva entidad al sistema.
        /// </summary>
        /// <param name="entity">Entidad a agregar.</param>
        /// <returns>El DTO agregado con los datos guardados.</returns>
        Task<TDto> AddAsync(TDto entity);
    }
}
