using Data.Interfaces;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Utilities.Exeptions;

namespace Data.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

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
                throw new QueryExecutionException("error al obtener todos los registros", ex);
            }
        }

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
                throw new QueryExecutionException($"error al obtener la entidad con id {id}", ex);
            }
        }

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
                throw new DataIntegrityException("violación de integridad al crear la entidad", ex);
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException("error al crear la entidad", ex);
            }
        }

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
                throw new ConcurrencyException("conflicto de concurrencia al actualizar", ex);
            }
            catch (Exception ex)
            {
                throw new QueryExecutionException("error al actualizar la entidad", ex);
            }
        }

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
                throw new QueryExecutionException($"error al eliminar la entidad con id {id}", ex);
            }
        }

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
                throw new QueryExecutionException($"error al eliminar lógicamente la entidad con id {id}", ex);
            }
        }

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
                throw new QueryExecutionException($"error al hacer patch de la entidad con id {id}", ex);
            }
        }
    }
}
