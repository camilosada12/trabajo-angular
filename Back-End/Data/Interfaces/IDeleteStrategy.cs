using System;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    /// <summary>
    /// Define una estrategia de eliminación para una entidad genérica.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad sobre la que se aplicará la eliminación.</typeparam>
    public interface IDeleteStrategy<TEntity> where TEntity : class
    {
        /// <summary>
        /// Ejecuta la lógica de eliminación para la entidad especificada.
        /// </summary>
        /// <param name="id">Identificador de la entidad a eliminar.</param>
        /// <param name="repository">Repositorio que maneja el acceso a la entidad.</param>
        /// <returns>True si la eliminación fue exitosa; false en caso contrario.</returns>
        Task<bool> DeleteAsync(int id, IRepository<TEntity> repository);
    }
}
