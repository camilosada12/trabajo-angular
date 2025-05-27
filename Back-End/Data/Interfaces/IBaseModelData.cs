using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.DTOs;
using Entity.Model;
using static Dapper.SqlMapper;

namespace Data.Interfaces
{
    /// <summary>
    /// Define operaciones genéricas de acceso a datos para cualquier entidad.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad.</typeparam>
    public interface IBaseModelData<T, D> where T : BaseModel where D : BaseDto
    {
        /// <summary>
        /// Obtener
        /// </summary>
        /// <returns></returns>

        Task<IEnumerable<D>> GetAllAsync();

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        Task UpdateAsync(T entity);

        /// <summary>
        /// Elimina lógicamente una entidad de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<int> DeleteLogicalAsync(int id);

        /// <summary>
        /// Elimina de forma persistente una entidad de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<int> DeleteAsync(int id);

        Task<bool> PatchAsync(int id);

        Task<IEnumerable<D>> GetDeletedAsync();
    }
}