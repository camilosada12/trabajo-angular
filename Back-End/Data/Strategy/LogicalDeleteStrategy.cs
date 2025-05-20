using System.Threading.Tasks;
using Data.Interfaces;

namespace Data.Services
{
    public class LogicalDeleteStrategy<TEntity> : IDeleteStrategy<TEntity> where TEntity : class
    {
        public async Task<bool> DeleteAsync(int id, IRepository<TEntity> repository)
        {
            return await repository.DeleteLogicalAsync(id);
        }
    }
}
