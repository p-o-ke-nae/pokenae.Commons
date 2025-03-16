using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Attributes;
using pokenae.Commons.Entities;
using pokenae.Commons.Repositories;
using pokenae.Commons.Services.Domain;

namespace pokenae.Commons.Services.Domain.impl
{
    public class EntityService<T> : IEntityService<T> 
        where T : BaseEntity
    {
        protected readonly IEntityRepository<T> repository;

        public EntityService(IEntityRepository<T> repository)
        {
            this.repository = repository;
        }

        public T Find(Func<T, bool> predicate)
        {
            return repository.Find(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }

        [Transactional]
        public void Add(T entity)
        {
            repository.Add(entity);
        }

        [Transactional]
        public void Update(T entity)
        {
            repository.Update(entity);
        }

        [Transactional]
        public void Delete(T entity)
        {
            repository.Delete(entity);
        }

        [Transactional]
        public void Upsert(T entity, Func<T, bool> predicate)
        {
            repository.Upsert(entity, predicate);
        }

        public IEnumerable<T> GetAllIncludingDeleted()
        {
            return repository.GetAllIncludingDeleted();
        }

        public T FindIncludingDeleted(Func<T, bool> predicate)
        {
            return repository.FindIncludingDeleted(predicate);
        }

        public bool IsExists(Func<T, bool> predicate)
        {
            return repository.IsExists(predicate);
        }

        [Transactional]
        public void AddRange(IEnumerable<T> entities)
        {
            repository.AddRange(entities);
        }

        [Transactional]
        public void UpdateRange(IEnumerable<T> entities)
        {
            repository.UpdateRange(entities);
        }

        [Transactional]
        public void DeleteRange(IEnumerable<T> entities)
        {
            repository.DeleteRange(entities);
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

