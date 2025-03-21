using AutoMapper;
using pokenae.Commons.Domain.Attributes;
using pokenae.Commons.Domain.DTOs;
using pokenae.Commons.Domain.Entities;
using pokenae.Commons.Domain.Repositories;
using pokenae.Commons.Domain.Services;

namespace pokenae.Commons.Domain.Services.impl
{
    /// <summary>
    /// リポジトリに対する基本的な操作を提供するサービスクラス
    /// </summary>
    /// <typeparam name="T">DTOの型</typeparam>
    public class RepositoryService<T> : IRepositoryService<T>
        where T : InfrastructureDto
    {
        protected readonly IBaseRepository<T> repository;
        protected readonly IMapper mapper;

        public RepositoryService(IBaseRepository<T> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
        /// 指定された条件に一致するDTOを内部的に取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するDTO</returns>
        private T FindInternal(Func<T, bool> predicate)
        {
            return repository.Find(predicate);
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
        /// すべてのDTOを内部的に取得します。
        /// </summary>
        /// <returns>DTOのコレクション</returns>
        private IEnumerable<T> GetAllInternal()
        {
            return repository.GetAll();
        }

        [Transactional]
        public void Add(T dto)
        {
            AddInternal(dto);
        }

        [Transactional]
        public async Task AddAsync(T dto)
        {
            await Task.Run(() => AddInternal(dto));
        }

        /// <summary>
        /// 新しいDTOを内部的に追加します。
        /// </summary>
        /// <param name="dto">追加するDTO</param>
        private void AddInternal(T dto)
        {
            repository.Add(dto);
        }

        [Transactional]
        public void Update(T dto)
        {
            UpdateInternal(dto);
        }

        [Transactional]
        public async Task UpdateAsync(T dto)
        {
            await Task.Run(() => UpdateInternal(dto));
        }

        /// <summary>
        /// 既存のDTOを内部的に更新します。
        /// </summary>
        /// <param name="dto">更新するDTO</param>
        private void UpdateInternal(T dto)
        {
            repository.Update(dto);
        }

        [Transactional]
        public void Delete(T dto)
        {
            DeleteInternal(dto);
        }

        [Transactional]
        public async Task DeleteAsync(T dto)
        {
            await Task.Run(() => DeleteInternal(dto));
        }

        /// <summary>
        /// DTOを内部的に削除します。
        /// </summary>
        /// <param name="dto">削除するDTO</param>
        private void DeleteInternal(T dto)
        {
            repository.Delete(dto);
        }

        [Transactional]
        public void Upsert(T dto, Func<T, bool> predicate)
        {
            UpsertInternal(dto, predicate);
        }

        [Transactional]
        public async Task UpsertAsync(T dto, Func<T, bool> predicate)
        {
            await Task.Run(() => UpsertInternal(dto, predicate));
        }

        /// <summary>
        /// DTOを内部的にアップサート（追加または更新）します。
        /// </summary>
        /// <param name="dto">追加または更新するDTO</param>
        /// <param name="predicate">検索条件を指定する関数</param>
        private void UpsertInternal(T dto, Func<T, bool> predicate)
        {
            repository.Upsert(dto, predicate);
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
        /// 削除されたものを含むすべてのDTOを内部的に取得します。
        /// </summary>
        /// <returns>DTOのコレクション</returns>
        private IEnumerable<T> GetAllIncludingDeletedInternal()
        {
            return repository.GetAllIncludingDeleted();
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
        /// 削除されたものを含む、指定された条件に一致するDTOを内部的に取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するDTO</returns>
        private T FindIncludingDeletedInternal(Func<T, bool> predicate)
        {
            return repository.FindIncludingDeleted(predicate);
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
        /// 指定された条件に一致するDTOが存在するか内部的に確認します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>DTOが存在する場合はtrue、それ以外の場合はfalse</returns>
        private bool IsExistsInternal(Func<T, bool> predicate)
        {
            return repository.IsExists(predicate);
        }

        [Transactional]
        public void AddRange(IEnumerable<T> dtos)
        {
            AddRangeInternal(dtos);
        }

        [Transactional]
        public async Task AddRangeAsync(IEnumerable<T> dtos)
        {
            await Task.Run(() => AddRangeInternal(dtos));
        }

        /// <summary>
        /// 複数のDTOを内部的に追加します。
        /// </summary>
        /// <param name="dtos">追加するDTOのコレクション</param>
        private void AddRangeInternal(IEnumerable<T> dtos)
        {
            repository.AddRange(dtos);
        }

        [Transactional]
        public void UpdateRange(IEnumerable<T> dtos)
        {
            UpdateRangeInternal(dtos);
        }

        [Transactional]
        public async Task UpdateRangeAsync(IEnumerable<T> dtos)
        {
            await Task.Run(() => UpdateRangeInternal(dtos));
        }

        /// <summary>
        /// 複数のDTOを内部的に更新します。
        /// </summary>
        /// <param name="dtos">更新するDTOのコレクション</param>
        private void UpdateRangeInternal(IEnumerable<T> dtos)
        {
            repository.UpdateRange(dtos);
        }

        [Transactional]
        public void DeleteRange(IEnumerable<T> dtos)
        {
            DeleteRangeInternal(dtos);
        }

        [Transactional]
        public async Task DeleteRangeAsync(IEnumerable<T> dtos)
        {
            await Task.Run(() => DeleteRangeInternal(dtos));
        }

        /// <summary>
        /// 複数のDTOを内部的に削除します。
        /// </summary>
        /// <param name="dtos">削除するDTOのコレクション</param>
        private void DeleteRangeInternal(IEnumerable<T> dtos)
        {
            repository.DeleteRange(dtos);
        }

        public void BeginTransaction()
        {
            repository.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            await Task.Run(() => repository.BeginTransaction());
        }

        public void CommitTransaction()
        {
            repository.CommitTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await Task.Run(() => repository.CommitTransaction());
        }

        public void RollbackTransaction()
        {
            repository.RollbackTransaction();
        }

        public async Task RollbackTransactionAsync()
        {
            await Task.Run(() => repository.RollbackTransaction());
        }
    }
}

