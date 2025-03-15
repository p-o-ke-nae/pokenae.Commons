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
    public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext<DbContext> _context;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public EntityRepository(ApplicationDbContext<DbContext> context)
        {
            _context = context;
        }

        public T Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Upsert(T entity, Func<T, bool> predicate)
        {
            if (_context.Set<T>().Any(predicate))
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
            return _context.Set<T>().IgnoreQueryFilters().ToList();
        }

        public T FindIncludingDeleted(Func<T, bool> predicate)
        {
            return _context.Set<T>().IgnoreQueryFilters().FirstOrDefault(predicate);
        }

        public bool IsExists(Func<T, bool> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _context.SaveChanges();
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
