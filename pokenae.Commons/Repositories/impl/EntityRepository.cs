using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using pokenae.Commons.Entities;
using pokenae.Commons.Data;

namespace pokenae.Commons.Repositories.impl
{
    /// <summary>
    /// DBテーブルを使用する運営で使用する共通のリポジトリクラス
    /// </summary>
    /// <typeparam name="T">エンティティの型</typeparam>
    public class EntityRepository<T, TContext> : IEntityRepository<T, TContext> 
        where T : BaseEntity
        where TContext : DbContext
    {
        protected readonly ApplicationDbContext<TContext> context;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public EntityRepository(ApplicationDbContext<TContext> context)
        {
            this.context = context;
        }

        public T Find(Func<T, bool> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public void Upsert(T entity, Func<T, bool> predicate)
        {
            if (context.Set<T>().Any(predicate))
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }
        }

        public IEnumerable<T> GetAllIncludingDeleted()
        {
            return context.Set<T>().IgnoreQueryFilters().ToList();
        }

        public T FindIncludingDeleted(Func<T, bool> predicate)
        {
            return context.Set<T>().IgnoreQueryFilters().FirstOrDefault(predicate);
        }

        public bool IsExists(Func<T, bool> predicate)
        {
            return context.Set<T>().Any(predicate);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
            context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                context.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }
    }
}
