using Data.Interfaces;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Utilities.Exeptions;

namespace Data.Services
{
    /// <summary>
    /// Repositorio genérico para manejar operaciones CRUD y lógica de eliminación para entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad manejado por el repositorio.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Obtiene todas las entidades que no han sido eliminadas lógicamente.
        /// </summary>
        /// <returns>Lista de entidades.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet
                    .Where(e => EF.Property<bool>(e, "isdeleted") == false)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException("Error al obtener todos los registros", ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID si no está eliminada lógicamente.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>Entidad encontrada o null.</returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) return null;

                var isDeleted = (bool?)entity.GetType().GetProperty("isdeleted")?.GetValue(entity);
                return isDeleted == false ? entity : null;
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException($"Error al obtener la entidad con ID {id}", ex);
            }
        }

        /// <summary>
        /// Crea una nueva entidad, asegurando que no esté marcada como eliminada.
        /// </summary>
        /// <param name="entity">Entidad a crear.</param>
        /// <returns>Entidad creada.</returns>
        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                entity.GetType().GetProperty("isdeleted")?.SetValue(entity, false);
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new DataIntegrityException("Violación de integridad al crear la entidad", ex);
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException("Error al crear la entidad", ex);
            }
        }

        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        /// <returns>True si fue exitosa.</returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("Conflicto de concurrencia al actualizar", ex);
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException("Error al actualizar la entidad", ex);
            }
        }

        /// <summary>
        /// Elimina físicamente una entidad de la base de datos.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>True si fue eliminada correctamente.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) return false;

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException($"Error al eliminar la entidad con ID {id}", ex);
            }
        }

        /// <summary>
        /// Marca lógicamente como eliminada una entidad.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>True si fue eliminada lógicamente.</returns>
        public async Task<bool> DeleteLogicalAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) return false;

                entity.GetType().GetProperty("isdeleted")?.SetValue(entity, true);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException($"Error al eliminar lógicamente la entidad con ID {id}", ex);
            }
        }

        /// <summary>
        /// Reactiva (desmarca como eliminada) una entidad.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>True si fue reactivada exitosamente.</returns>
        public async Task<bool> PatchAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) return true;

                entity.GetType().GetProperty("isdeleted")?.SetValue(entity, false);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException($"Error al hacer patch de la entidad con ID {id}", ex);
            }
        }
    }
}
