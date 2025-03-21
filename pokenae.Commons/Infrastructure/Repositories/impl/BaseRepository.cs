using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using pokenae.Commons.Domain.DTOs;
using pokenae.Commons.Domain.Repositories;
using pokenae.Commons.Infrastructure.Data;

namespace pokenae.Commons.Infrastructure.Repositories.impl
{
    /// <summary>
    /// DBテーブルを利用した汎用的なリポジトリクラス
    /// </summary>
    /// <typeparam name="T">インフラストラクチャ層のDTOの型</typeparam>
    public class BaseRepository<T, TContext> : IBaseRepository<T>
        where T : InfrastructureDto
        where TContext : DbContext
    {
        protected readonly TContext context;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public BaseRepository(TContext context)
        {
            this.context = context;
        }

        public T Find(Func<T, bool> predicate)
        {
            return FindInternal(predicate);
        }

        public async Task<T> FindAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => FindInternal(predicate));
        }

        /// <summary>
        /// 指定された条件に一致するエンティティを内部的に取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するエンティティ</returns>
        private T FindInternal(Func<T, bool> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return GetAllInternal();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => GetAllInternal());
        }

        /// <summary>
        /// すべてのエンティティを内部的に取得します。
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        private IEnumerable<T> GetAllInternal()
        {
            return context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            AddInternal(entity);
        }

        public async Task AddAsync(T entity)
        {
            await Task.Run(() => AddInternal(entity));
        }

        /// <summary>
        /// 新しいエンティティを内部的に追加します。
        /// </summary>
        /// <param name="entity">追加するエンティティ</param>
        private void AddInternal(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            UpdateInternal(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => UpdateInternal(entity));
        }

        /// <summary>
        /// 既存のエンティティを内部的に更新します。
        /// </summary>
        /// <param name="entity">更新するエンティティ</param>
        private void UpdateInternal(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DeleteInternal(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => DeleteInternal(entity));
        }

        /// <summary>
        /// エンティティを内部的に削除します。
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        private void DeleteInternal(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public void Upsert(T entity, Func<T, bool> predicate)
        {
            UpsertInternal(entity, predicate);
        }

        public async Task UpsertAsync(T entity, Func<T, bool> predicate)
        {
            await Task.Run(() => UpsertInternal(entity, predicate));
        }

        /// <summary>
        /// エンティティを内部的にアップサート（追加または更新）します。
        /// </summary>
        /// <param name="entity">追加または更新するエンティティ</param>
        /// <param name="predicate">検索条件を指定する関数</param>
        private void UpsertInternal(T entity, Func<T, bool> predicate)
        {
            if (context.Set<T>().Any(predicate))
            {
                UpdateInternal(entity);
            }
            else
            {
                AddInternal(entity);
            }
        }

        public IEnumerable<T> GetAllIncludingDeleted()
        {
            return GetAllIncludingDeletedInternal();
        }

        public async Task<IEnumerable<T>> GetAllIncludingDeletedAsync()
        {
            return await Task.Run(() => GetAllIncludingDeletedInternal());
        }

        /// <summary>
        /// 論理削除されたエンティティを含めてすべてのエンティティを内部的に取得します。
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        private IEnumerable<T> GetAllIncludingDeletedInternal()
        {
            return context.Set<T>().IgnoreQueryFilters().ToList();
        }

        public T FindIncludingDeleted(Func<T, bool> predicate)
        {
            return FindIncludingDeletedInternal(predicate);
        }

        public async Task<T> FindIncludingDeletedAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => FindIncludingDeletedInternal(predicate));
        }

        /// <summary>
        /// 論理削除されたエンティティを含めて、指定された条件に一致するエンティティを内部的に取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するエンティティ</returns>
        private T FindIncludingDeletedInternal(Func<T, bool> predicate)
        {
            return context.Set<T>().IgnoreQueryFilters().FirstOrDefault(predicate);
        }

        public bool IsExists(Func<T, bool> predicate)
        {
            return IsExistsInternal(predicate);
        }

        public async Task<bool> IsExistsAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => IsExistsInternal(predicate));
        }

        /// <summary>
        /// 指定された条件に一致するエンティティが存在するか内部的に確認します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>エンティティが存在する場合はtrue、それ以外の場合はfalse</returns>
        private bool IsExistsInternal(Func<T, bool> predicate)
        {
            return context.Set<T>().Any(predicate);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            AddRangeInternal(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => AddRangeInternal(entities));
        }

        /// <summary>
        /// 複数のエンティティを内部的に追加します。
        /// </summary>
        /// <param name="entities">追加するエンティティのリスト</param>
        private void AddRangeInternal(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            UpdateRangeInternal(entities);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => UpdateRangeInternal(entities));
        }

        /// <summary>
        /// 複数のエンティティを内部的に更新します。
        /// </summary>
        /// <param name="entities">更新するエンティティのリスト</param>
        private void UpdateRangeInternal(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
            context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            DeleteRangeInternal(entities);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => DeleteRangeInternal(entities));
        }

        /// <summary>
        /// 複数のエンティティを内部的に削除します。
        /// </summary>
        /// <param name="entities">削除するエンティティのリスト</param>
        private void DeleteRangeInternal(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
        }

        public void BeginTransaction()
        {
            BeginTransactionInternal();
        }

        public async Task BeginTransactionAsync()
        {
            await Task.Run(() => BeginTransactionInternal());
        }

        /// <summary>
        /// トランザクションを内部的に開始します。
        /// </summary>
        private void BeginTransactionInternal()
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            CommitTransactionInternal();
        }

        public async Task CommitTransactionAsync()
        {
            await Task.Run(() => CommitTransactionInternal());
        }

        /// <summary>
        /// トランザクションを内部的にコミットします。
        /// </summary>
        private void CommitTransactionInternal()
        {
            try
            {
                context.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                RollbackTransactionInternal();
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
            RollbackTransactionInternal();
        }

        public async Task RollbackTransactionAsync()
        {
            await Task.Run(() => RollbackTransactionInternal());
        }

        /// <summary>
        /// トランザクションを内部的にロールバックします。
        /// </summary>
        private void RollbackTransactionInternal()
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
