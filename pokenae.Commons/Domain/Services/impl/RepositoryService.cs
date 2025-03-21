using AutoMapper;
using pokenae.Commons.Domain.Attributes;
using pokenae.Commons.Domain.DTOs;
using pokenae.Commons.Domain.Entities;
using pokenae.Commons.Domain.Repositories;
using pokenae.Commons.Domain.Services;

namespace pokenae.Commons.Domain.Services.impl
{
    /// <summary>
    /// ���|�W�g���ɑ΂����{�I�ȑ����񋟂���T�[�r�X�N���X
    /// </summary>
    /// <typeparam name="T">DTO�̌^</typeparam>
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
        /// �w�肳�ꂽ�����Ɉ�v����DTO������I�Ɏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����DTO</returns>
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
        /// ���ׂĂ�DTO������I�Ɏ擾���܂��B
        /// </summary>
        /// <returns>DTO�̃R���N�V����</returns>
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
        /// �V����DTO������I�ɒǉ����܂��B
        /// </summary>
        /// <param name="dto">�ǉ�����DTO</param>
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
        /// ������DTO������I�ɍX�V���܂��B
        /// </summary>
        /// <param name="dto">�X�V����DTO</param>
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
        /// DTO������I�ɍ폜���܂��B
        /// </summary>
        /// <param name="dto">�폜����DTO</param>
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
        /// DTO������I�ɃA�b�v�T�[�g�i�ǉ��܂��͍X�V�j���܂��B
        /// </summary>
        /// <param name="dto">�ǉ��܂��͍X�V����DTO</param>
        /// <param name="predicate">�����������w�肷��֐�</param>
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
        /// �폜���ꂽ���̂��܂ނ��ׂĂ�DTO������I�Ɏ擾���܂��B
        /// </summary>
        /// <returns>DTO�̃R���N�V����</returns>
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
        /// �폜���ꂽ���̂��܂ށA�w�肳�ꂽ�����Ɉ�v����DTO������I�Ɏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����DTO</returns>
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
        /// �w�肳�ꂽ�����Ɉ�v����DTO�����݂��邩�����I�Ɋm�F���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>DTO�����݂���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
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
        /// ������DTO������I�ɒǉ����܂��B
        /// </summary>
        /// <param name="dtos">�ǉ�����DTO�̃R���N�V����</param>
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
        /// ������DTO������I�ɍX�V���܂��B
        /// </summary>
        /// <param name="dtos">�X�V����DTO�̃R���N�V����</param>
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
        /// ������DTO������I�ɍ폜���܂��B
        /// </summary>
        /// <param name="dtos">�폜����DTO�̃R���N�V����</param>
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

