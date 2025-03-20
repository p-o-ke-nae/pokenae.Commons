using AutoMapper;
using pokenae.Commons.Domain.Attributes;
using pokenae.Commons.Domain.DTOs;
using pokenae.Commons.Domain.Entities;
using pokenae.Commons.Domain.Repositories;
using pokenae.Commons.Domain.Services;

namespace pokenae.Commons.Domain.Services.impl
{
    /// <summary>
    /// �G���e�B�e�B�ɑ΂����{�I�ȑ����񋟂���T�[�r�X�N���X
    /// </summary>
    /// <typeparam name="TEntity">�G���e�B�e�B�̌^</typeparam>
    /// <typeparam name="TDto">DTO�̌^</typeparam>
    public class EntityService<TEntity, TDto> : IEntityService<TEntity>
        where TEntity : BaseEntity
        where TDto : InfrastructureDto
    {
        protected readonly IBaseRepository<TDto> repository;
        protected readonly IMapper mapper;

        public EntityService(IBaseRepository<TDto> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public TEntity Find(Func<TEntity, bool> predicate)
        {
            return FindInternal(predicate);
        }

        public async Task<TEntity> FindAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => FindInternal(predicate));
        }

        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
        private TEntity FindInternal(Func<TEntity, bool> predicate)
        {
            var dto = repository.Find(e => predicate(mapper.Map<TEntity>(e)));
            return mapper.Map<TEntity>(dto);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetAllInternal();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => GetAllInternal());
        }

        /// <summary>
        /// ���ׂẴG���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃R���N�V����</returns>
        private IEnumerable<TEntity> GetAllInternal()
        {
            var dtos = repository.GetAll();
            return mapper.Map<IEnumerable<TEntity>>(dtos);
        }

        [Transactional]
        public void Add(TEntity entity)
        {
            AddInternal(entity);
        }

        [Transactional]
        public async Task AddAsync(TEntity entity)
        {
            await Task.Run(() => AddInternal(entity));
        }

        /// <summary>
        /// �V�����G���e�B�e�B������I�ɒǉ����܂��B
        /// </summary>
        /// <param name="entity">�ǉ�����G���e�B�e�B</param>
        private void AddInternal(TEntity entity)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Add(dto);
        }

        [Transactional]
        public void Update(TEntity entity)
        {
            UpdateInternal(entity);
        }

        [Transactional]
        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => UpdateInternal(entity));
        }

        /// <summary>
        /// �����̃G���e�B�e�B������I�ɍX�V���܂��B
        /// </summary>
        /// <param name="entity">�X�V����G���e�B�e�B</param>
        private void UpdateInternal(TEntity entity)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Update(dto);
        }

        [Transactional]
        public void Delete(TEntity entity)
        {
            DeleteInternal(entity);
        }

        [Transactional]
        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => DeleteInternal(entity));
        }

        /// <summary>
        /// �G���e�B�e�B������I�ɍ폜���܂��B
        /// </summary>
        /// <param name="entity">�폜����G���e�B�e�B</param>
        private void DeleteInternal(TEntity entity)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Delete(dto);
        }

        [Transactional]
        public void Upsert(TEntity entity, Func<TEntity, bool> predicate)
        {
            UpsertInternal(entity, predicate);
        }

        [Transactional]
        public async Task UpsertAsync(TEntity entity, Func<TEntity, bool> predicate)
        {
            await Task.Run(() => UpsertInternal(entity, predicate));
        }

        /// <summary>
        /// �G���e�B�e�B������I�ɃA�b�v�T�[�g�i�ǉ��܂��͍X�V�j���܂��B
        /// </summary>
        /// <param name="entity">�ǉ��܂��͍X�V����G���e�B�e�B</param>
        /// <param name="predicate">�����������w�肷��֐�</param>
        private void UpsertInternal(TEntity entity, Func<TEntity, bool> predicate)
        {
            var dto = mapper.Map<TDto>(entity);
            repository.Upsert(dto, d => predicate(mapper.Map<TEntity>(d)));
        }

        public IEnumerable<TEntity> GetAllIncludingDeleted()
        {
            return GetAllIncludingDeletedInternal();
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludingDeletedAsync()
        {
            return await Task.Run(() => GetAllIncludingDeletedInternal());
        }

        /// <summary>
        /// �폜���ꂽ���̂��܂ނ��ׂẴG���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃R���N�V����</returns>
        private IEnumerable<TEntity> GetAllIncludingDeletedInternal()
        {
            var dtos = repository.GetAllIncludingDeleted();
            return mapper.Map<IEnumerable<TEntity>>(dtos);
        }

        public TEntity FindIncludingDeleted(Func<TEntity, bool> predicate)
        {
            return FindIncludingDeletedInternal(predicate);
        }

        public async Task<TEntity> FindIncludingDeletedAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => FindIncludingDeletedInternal(predicate));
        }

        /// <summary>
        /// �폜���ꂽ���̂��܂ށA�w�肳�ꂽ�����Ɉ�v����G���e�B�e�B������I�Ɏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
        private TEntity FindIncludingDeletedInternal(Func<TEntity, bool> predicate)
        {
            var dto = repository.FindIncludingDeleted(e => predicate(mapper.Map<TEntity>(e)));
            return mapper.Map<TEntity>(dto);
        }

        public bool IsExists(Func<TEntity, bool> predicate)
        {
            return IsExistsInternal(predicate);
        }

        public async Task<bool> IsExistsAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => IsExistsInternal(predicate));
        }

        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B�����݂��邩�����I�Ɋm�F���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�G���e�B�e�B�����݂���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        private bool IsExistsInternal(Func<TEntity, bool> predicate)
        {
            return repository.IsExists(e => predicate(mapper.Map<TEntity>(e)));
        }

        [Transactional]
        public void AddRange(IEnumerable<TEntity> entities)
        {
            AddRangeInternal(entities);
        }

        [Transactional]
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => AddRangeInternal(entities));
        }

        /// <summary>
        /// �����̃G���e�B�e�B������I�ɒǉ����܂��B
        /// </summary>
        /// <param name="entities">�ǉ�����G���e�B�e�B�̃R���N�V����</param>
        private void AddRangeInternal(IEnumerable<TEntity> entities)
        {
            var dtos = entities.Select(mapper.Map<TDto>);
            repository.AddRange(dtos);
        }

        [Transactional]
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            UpdateRangeInternal(entities);
        }

        [Transactional]
        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => UpdateRangeInternal(entities));
        }

        /// <summary>
        /// �����̃G���e�B�e�B������I�ɍX�V���܂��B
        /// </summary>
        /// <param name="entities">�X�V����G���e�B�e�B�̃R���N�V����</param>
        private void UpdateRangeInternal(IEnumerable<TEntity> entities)
        {
            var dtos = entities.Select(mapper.Map<TDto>);
            repository.UpdateRange(dtos);
        }

        [Transactional]
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DeleteRangeInternal(entities);
        }

        [Transactional]
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => DeleteRangeInternal(entities));
        }

        /// <summary>
        /// �����̃G���e�B�e�B������I�ɍ폜���܂��B
        /// </summary>
        /// <param name="entities">�폜����G���e�B�e�B�̃R���N�V����</param>
        private void DeleteRangeInternal(IEnumerable<TEntity> entities)
        {
            var dtos = entities.Select(mapper.Map<TDto>);
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

