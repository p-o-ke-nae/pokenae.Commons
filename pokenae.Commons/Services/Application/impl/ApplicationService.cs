using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using pokenae.Commons.Services.Domain;
using pokenae.Commons.Entities;
using pokenae.Commons.DTOs;
using Microsoft.EntityFrameworkCore;

namespace pokenae.Commons.Services.Application.impl
{
    public abstract class ApplicationService<TEntity, TDto, TContext> : IApplicationService<TDto>
        where TEntity : BaseEntity
        where TDto : BaseDto
        where TContext : DbContext
    {
        protected readonly IEntityService<TEntity, TContext> entityService;
        protected readonly IMapper mapper;

        protected ApplicationService(IEntityService<TEntity, TContext> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }

        public TDto Find(Func<TDto, bool> predicate)
        {
            var entity = entityService.Find(e => predicate(mapper.Map<TDto>(e)));
            return mapper.Map<TDto>(entity);
        }

        public IEnumerable<TDto> GetAll()
        {
            var entities = entityService.GetAll();
            return mapper.Map<IEnumerable<TDto>>(entities);
        }

        public void Add(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            entityService.Add(entity);
        }

        public void Update(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            entityService.Update(entity);
        }

        public void Delete(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            entityService.Delete(entity);
        }

        public void Upsert(TDto dto, Func<TDto, bool> predicate)
        {
            var entity = mapper.Map<TEntity>(dto);
            entityService.Upsert(entity, e => predicate(mapper.Map<TDto>(e)));
        }

        public IEnumerable<TDto> GetAllIncludingDeleted()
        {
            var entities = entityService.GetAllIncludingDeleted();
            return mapper.Map<IEnumerable<TDto>>(entities);
        }

        public TDto FindIncludingDeleted(Func<TDto, bool> predicate)
        {
            var entity = entityService.FindIncludingDeleted(e => predicate(mapper.Map<TDto>(e)));
            return mapper.Map<TDto>(entity);
        }

        public bool IsExists(Func<TDto, bool> predicate)
        {
            return entityService.IsExists(e => predicate(mapper.Map<TDto>(e)));
        }

        public void AddRange(IEnumerable<TDto> dtos)
        {
            var entities = mapper.Map<IEnumerable<TEntity>>(dtos);
            entityService.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<TDto> dtos)
        {
            var entities = mapper.Map<IEnumerable<TEntity>>(dtos);
            entityService.UpdateRange(entities);
        }

        public void DeleteRange(IEnumerable<TDto> dtos)
        {
            var entities = mapper.Map<IEnumerable<TEntity>>(dtos);
            entityService.DeleteRange(entities);
        }
    }
}
