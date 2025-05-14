using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Utilities.Exeptions;


namespace Business.Services
{
    public class GenericService<TDto, TEntity> : IGenericService<TDto>
        where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "el id debe ser mayor que cero.");

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto?> CreateAsync(TDto dto)
        {
            if (dto == null)
                throw new ValidationException(nameof(dto), "el objeto dto no puede ser nulo.");

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var created = await _repository.CreateAsync(entity);
                return _mapper.Map<TDto>(created);
            }
            catch (Exception ex)
            {
                throw new BusinessException("error al crear la entidad.", ex);
            }
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            if (dto == null)
                throw new ValidationException(nameof(dto), "el objeto dto no puede ser nulo.");

            var entity = _mapper.Map<TEntity>(dto);
            var result = await _repository.UpdateAsync(entity);

            if (!result)
                throw new BusinessRuleViolationException("UPDATE_FAILED", "no se pudo actualizar el registro.");

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "el id debe ser mayor que cero.");

            var result = await _repository.DeleteAsync(id);

            if (!result)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return result;
        }

        public async Task<bool> DeleteAsyncLogic(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "el id debe ser mayor que cero.");

            var result = await _repository.DeleteLogicalAsync(id);

            if (!result)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return result;
        }

        public async Task<bool> PatchAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "el id debe ser mayor que cero.");

            var result = await _repository.PatchAsync(id);

            if (!result)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return result;
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            if (dto == null)
                throw new ValidationException(nameof(dto), "el objeto dto no puede ser nulo.");

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var addedEntity = await _repository.CreateAsync(entity);
                return _mapper.Map<TDto>(addedEntity);
            }
            catch (Exception ex)
            {
                throw new BusinessException("error al agregar la entidad.", ex);
            }
        }
    }
}
