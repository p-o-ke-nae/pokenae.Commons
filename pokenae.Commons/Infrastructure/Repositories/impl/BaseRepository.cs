using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using pokenae.Commons.Domain.DTOs;
using pokenae.Commons.Domain.Repositories;
using pokenae.Commons.Infrastructure.Data;

namespace pokenae.Commons.Infrastructure.Repositories.impl
{
    /// <summary>
    /// DB�e�[�u���𗘗p�����ėp�I�ȃ��|�W�g���N���X
    /// </summary>
    /// <typeparam name="T">�C���t���X�g���N�`���w��DTO�̌^</typeparam>
    public class BaseRepository<T, TContext> : IBaseRepository<T>
        where T : InfrastructureDto
        where TContext : DbContext
    {
        protected readonly TContext context;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="context">�f�[�^�x�[�X�R���e�L�X�g</param>
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
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
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
        /// ���ׂẴG���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃��X�g</returns>
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
        /// �V�����G���e�B�e�B������I�ɒǉ����܂��B
        /// </summary>
        /// <param name="entity">�ǉ�����G���e�B�e�B</param>
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
        /// �����̃G���e�B�e�B������I�ɍX�V���܂��B
        /// </summary>
        /// <param name="entity">�X�V����G���e�B�e�B</param>
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
        /// �G���e�B�e�B������I�ɍ폜���܂��B
        /// </summary>
        /// <param name="entity">�폜����G���e�B�e�B</param>
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
        /// �G���e�B�e�B������I�ɃA�b�v�T�[�g�i�ǉ��܂��͍X�V�j���܂��B
        /// </summary>
        /// <param name="entity">�ǉ��܂��͍X�V����G���e�B�e�B</param>
        /// <param name="predicate">�����������w�肷��֐�</param>
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
        /// �_���폜���ꂽ�G���e�B�e�B���܂߂Ă��ׂẴG���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃��X�g</returns>
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
        /// �_���폜���ꂽ�G���e�B�e�B���܂߂āA�w�肳�ꂽ�����Ɉ�v����G���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
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
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B�����݂��邩�����I�Ɋm�F���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�G���e�B�e�B�����݂���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
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
        /// �����̃G���e�B�e�B������I�ɒǉ����܂��B
        /// </summary>
        /// <param name="entities">�ǉ�����G���e�B�e�B�̃��X�g</param>
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
        /// �����̃G���e�B�e�B������I�ɍX�V���܂��B
        /// </summary>
        /// <param name="entities">�X�V����G���e�B�e�B�̃��X�g</param>
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
        /// �����̃G���e�B�e�B������I�ɍ폜���܂��B
        /// </summary>
        /// <param name="entities">�폜����G���e�B�e�B�̃��X�g</param>
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
        /// �g�����U�N�V����������I�ɊJ�n���܂��B
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
        /// �g�����U�N�V����������I�ɃR�~�b�g���܂��B
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
        /// �g�����U�N�V����������I�Ƀ��[���o�b�N���܂��B
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
