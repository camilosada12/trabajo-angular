using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Enums;
using Entity.DTOs;
using Entity.Model;

namespace Business.Services
{
    public abstract class ABaseModelBusiness<T, D> : IBaseModelBusiness<T, D> where T : BaseModel where D : BaseDto
    {
        public abstract Task<List<D>> GetAllAsync();
        public abstract Task<D> GetByIdAsync(int id);
        public abstract Task<D> CreateAsync(D entityDto);
        public abstract Task UpdateAsync(D entityDto);
        public abstract Task<bool> DeleteAsync(int id, DeleteMode mode);
        public abstract Task<bool> PatchAsync(int id);

        public abstract Task<List<D>> GetDeletedAsync();
    }
}
