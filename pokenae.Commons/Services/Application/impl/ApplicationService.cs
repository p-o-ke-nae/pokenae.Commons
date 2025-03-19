using AutoMapper;
using pokenae.Commons.Attributes;
using pokenae.Commons.DTOs.Application;
using pokenae.Commons.Entities;
using pokenae.Commons.Services.Domain;

namespace pokenae.Commons.Services.Application.impl
{
    /// <summary>
    /// アプリケーション層のサービスクラス
    /// </summary>
    /// <typeparam name="TEntity">エンティティの型</typeparam>
    /// <typeparam name="TDto">DTOの型</typeparam>
    public abstract class ApplicationService<TEntity, TDto> : IApplicationService<TDto>
        where TEntity : BaseEntity
        where TDto : ApplicationDto
    {
        protected readonly IEntityService<TEntity> entityService;
        protected readonly IMapper mapper;

        protected ApplicationService(IEntityService<TEntity> entityService, IMapper mapper)
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

        [Transactional]
        public void Add(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            entityService.Add(entity);
        }

        [Transactional]
        public void Update(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            entityService.Update(entity);
        }

        [Transactional]
        public void Delete(TDto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            entityService.Delete(entity);
        }

        [Transactional]
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

        [Transactional]
        public void AddRange(IEnumerable<TDto> dtos)
        {
            var entities = mapper.Map<IEnumerable<TEntity>>(dtos);
            entityService.AddRange(entities);
        }

        [Transactional]
        public void UpdateRange(IEnumerable<TDto> dtos)
        {
            var entities = mapper.Map<IEnumerable<TEntity>>(dtos);
            entityService.UpdateRange(entities);
        }

        [Transactional]
        public void DeleteRange(IEnumerable<TDto> dtos)
        {
            var entities = mapper.Map<IEnumerable<TEntity>>(dtos);
            entityService.DeleteRange(entities);
        }
    }
}
