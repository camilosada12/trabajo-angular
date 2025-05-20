using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data
{
    public class DeleteStrategy<TEntity> : IDeleteStrategy<TEntity> where TEntity : class
    {
        public async Task<bool> DeleteAsync(int id, IRepository<TEntity> repository)
        {
            return await repository.DeleteAsync(id);
        }
    }
}
