using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Attributes;
using pokenae.Commons.DTOs;
using pokenae.Commons.Entities;
using pokenae.Commons.Repositories;

namespace pokenae.Commons.Services.Domain.impl
{
    /// <summary>
    /// エンティティに対する基本的な操作を提供するサービスクラス
    /// </summary>
    /// <typeparam name="TEntity">エンティティの型</typeparam>
    /// <typeparam name="TDto">DTOの型</typeparam>
    public class EntityService<TEntity, TDto> : IEntityService<TEntity>
            where TEntity : BaseEntity
            where TDto : InfrastructureDto
    {
        protected readonly IEntityRepository<TDto> repository;
        protected readonly IMapper mapper;

        public EntityService(IEntityRepository<TDto> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public TEntity Find(Func<TEntity, bool> predicate)
        {
            var dto = repository.Find(e => predicate(mapper.Map<TEntity>(e)));
            return mapper.Map<TEntity>(dto);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var dtos = repository.GetAll();
            return mapper.Map<IEnumerable<TEntity>>(dtos);
        }

        [Transactional]
        public void Add(TEntity entity)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Add(dto);
        }

        [Transactional]
        public void Update(TEntity entity)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Update(dto);
        }

        [Transactional]
        public void Delete(TEntity entity)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Delete(dto);
        }

        [Transactional]
        public void Upsert(TEntity entity, Func<TEntity, bool> predicate)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Upsert(dto, d => predicate(mapper.Map<TEntity>(d)));
        }

        public IEnumerable<TEntity> GetAllIncludingDeleted()
        {
            var dtos = repository.GetAllIncludingDeleted();
            return mapper.Map<IEnumerable<TEntity>>(dtos);
        }

        public TEntity FindIncludingDeleted(Func<TEntity, bool> predicate)
        {
            var dto = repository.FindIncludingDeleted(e => predicate(mapper.Map<TEntity>(e)));
            return mapper.Map<TEntity>(dto);
        }

        public bool IsExists(Func<TEntity, bool> predicate)
        {
            return repository.IsExists(e => predicate(mapper.Map<TEntity>(e)));
        }

        [Transactional]
        public void AddRange(IEnumerable<TEntity> entities)
        {
            var dtos = entities.Select(mapper.Map<TDto>);
            repository.AddRange(dtos);
        }

        [Transactional]
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            var dtos = entities.Select(mapper.Map<TDto>);
            repository.UpdateRange(dtos);
        }

        [Transactional]
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            var dtos = entities.Select(mapper.Map<TDto>);
            repository.DeleteRange(dtos);
        }

        public void BeginTransaction()
        {
            repository.BeginTransaction();
        }

        public void CommitTransaction()
        {
            repository.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            repository.RollbackTransaction();
        }
    }
}

