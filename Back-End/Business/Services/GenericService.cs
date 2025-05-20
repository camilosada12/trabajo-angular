using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Enums;
using Business.Interfaces;
using Data;
using Data.Interfaces;
using Data.Services;
using Utilities.Exeptions;

namespace Business.Services
{
    /// <summary>
    /// Servicio genérico que implementa operaciones CRUD y otras para manejar DTOs y entidades.
    /// </summary>
    /// <typeparam name="TDto">Tipo del Data Transfer Object.</typeparam>
    /// <typeparam name="TEntity">Tipo de la entidad de la base de datos.</typeparam>
    public class GenericService<TDto, TEntity> : IGenericService<TDto>
        where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor que inicializa el repositorio y el mapeador.
        /// </summary>
        /// <param name="repository">Repositorio para la entidad.</param>
        /// <param name="mapper">Instancia de AutoMapper para conversión DTO - entidad.</param>
        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los registros y los mapea a DTOs.
        /// </summary>
        /// <returns>Una colección con todos los DTOs.</returns>
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        /// <summary>
        /// Obtiene un registro por su id y lo mapea a DTO.
        /// </summary>
        /// <param name="id">Identificador del registro.</param>
        /// <returns>DTO del registro solicitado o null si no existe.</returns>
        /// <exception cref="ValidationException">Si el id es menor o igual a cero.</exception>
        /// <exception cref="EntityNotFoundException">Si no se encuentra la entidad con el id dado.</exception>
        public async Task<TDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "el id debe ser mayor que cero.");

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return _mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// Crea un nuevo registro a partir de un DTO.
        /// </summary>
        /// <param name="dto">DTO con la información para crear la entidad.</param>
        /// <returns>DTO con la entidad creada.</returns>
        /// <exception cref="ValidationException">Si el DTO es nulo.</exception>
        /// <exception cref="BusinessException">Si ocurre un error durante la creación.</exception>
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

        /// <summary>
        /// Actualiza un registro existente basado en un DTO.
        /// </summary>
        /// <param name="dto">DTO con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        /// <exception cref="ValidationException">Si el DTO es nulo.</exception>
        /// <exception cref="BusinessRuleViolationException">Si no se pudo actualizar el registro.</exception>
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

        /// <summary>
        /// Elimina un registro usando el modo especificado (físico o lógico).
        /// </summary>
        /// <param name="id">Identificador del registro a eliminar.</param>
        /// <param name="mode">Modo de eliminación (físico o lógico).</param>
        /// <returns>True si la eliminación fue exitosa.</returns>
        /// <exception cref="ValidationException">Si el id es menor o igual a cero.</exception>
        /// <exception cref="BusinessException">Si el modo de eliminación no está soportado.</exception>
        /// <exception cref="EntityNotFoundException">Si no se encuentra la entidad a eliminar.</exception>
        public async Task<bool> DeleteAsync(int id, DeleteMode mode)
        {
            if (id <= 0)
                throw new ValidationException("id", "el id debe ser mayor que cero.");

            IDeleteStrategy<TEntity> strategy = mode switch
            {
                DeleteMode.fisico => new DeleteStrategy<TEntity>(),
                DeleteMode.Logical => new LogicalDeleteStrategy<TEntity>(),
                _ => throw new BusinessException("modo de eliminación no soportado")
            };

            var result = await strategy.DeleteAsync(id, _repository);

            if (!result)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return result;
        }

        /// <summary>
        /// Aplica un parche parcial a un registro.
        /// </summary>
        /// <param name="id">Identificador del registro a modificar.</param>
        /// <returns>True si el parche fue aplicado correctamente.</returns>
        /// <exception cref="ValidationException">Si el id es menor o igual a cero.</exception>
        /// <exception cref="EntityNotFoundException">Si no se encuentra la entidad para el parche.</exception>
        public async Task<bool> PatchAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "el id debe ser mayor que cero.");

            var result = await _repository.PatchAsync(id);

            if (!result)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return result;
        }

        /// <summary>
        /// Agrega una nueva entidad a partir de un DTO.
        /// </summary>
        /// <param name="dto">DTO con los datos para agregar.</param>
        /// <returns>DTO con la entidad agregada.</returns>
        /// <exception cref="ValidationException">Si el DTO es nulo.</exception>
        /// <exception cref="BusinessException">Si ocurre un error durante el agregado.</exception>
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
