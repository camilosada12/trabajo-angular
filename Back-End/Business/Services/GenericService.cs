﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;

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
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? default : _mapper.Map<TDto>(entity);
        }

        public async Task<TDto?> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<TDto>(created);
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<bool> DeleteAsyncLogic(int id)
        {
            return await _repository.DeleteLogicalAsync(id);
        }

        public async Task<bool> PatchAsync(int id)
        {
            return await _repository.PatchAsync(id);
        }

        // Implementación de AddAsync
        public async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto); // Mapea el DTO a la entidad
            var addedEntity = await _repository.CreateAsync(entity); // Guarda la entidad en la base de datos

            return _mapper.Map<TDto>(addedEntity); // Retorna el DTO del nuevo objeto guardado
        }
    }
}
