using pokenae.Commons.Domain.Entities;

namespace pokenae.Commons.Domain.Services
{
    /// <summary>
    /// �G���e�B�e�B�ɑ΂����{�I�ȑ����񋟂���T�[�r�X�C���^�[�t�F�[�X
    /// </summary>
    /// <typeparam name="T">�G���e�B�e�B�̌^</typeparam>
    public interface IEntityService<T>
        where T : BaseEntity
    {
        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B���擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
        T Find(Func<T, bool> predicate);

        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B��񓯊��Ŏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
        Task<T> FindAsync(Func<T, bool> predicate);

        /// <summary>
        /// ���ׂẴG���e�B�e�B���擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃R���N�V����</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// ���ׂẴG���e�B�e�B��񓯊��Ŏ擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃R���N�V����</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// �V�����G���e�B�e�B��ǉ����܂��B
        /// </summary>
        /// <param name="entity">�ǉ�����G���e�B�e�B</param>
        void Add(T entity);

        /// <summary>
        /// �V�����G���e�B�e�B��񓯊��Œǉ����܂��B
        /// </summary>
        /// <param name="entity">�ǉ�����G���e�B�e�B</param>
        Task AddAsync(T entity);

        /// <summary>
        /// �����̃G���e�B�e�B���X�V���܂��B
        /// </summary>
        /// <param name="entity">�X�V����G���e�B�e�B</param>
        void Update(T entity);

        /// <summary>
        /// �����̃G���e�B�e�B��񓯊��ōX�V���܂��B
        /// </summary>
        /// <param name="entity">�X�V����G���e�B�e�B</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// �G���e�B�e�B���폜���܂��B
        /// </summary>
        /// <param name="entity">�폜����G���e�B�e�B</param>
        void Delete(T entity);

        /// <summary>
        /// �G���e�B�e�B��񓯊��ō폜���܂��B
        /// </summary>
        /// <param name="entity">�폜����G���e�B�e�B</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// �G���e�B�e�B��ǉ��܂��͍X�V���܂��B
        /// </summary>
        /// <param name="entity">�ǉ��܂��͍X�V����G���e�B�e�B</param>
        /// <param name="predicate">�����������w�肷��֐�</param>
        void Upsert(T entity, Func<T, bool> predicate);

        /// <summary>
        /// �G���e�B�e�B��񓯊��Œǉ��܂��͍X�V���܂��B
        /// </summary>
        /// <param name="entity">�ǉ��܂��͍X�V����G���e�B�e�B</param>
        /// <param name="predicate">�����������w�肷��֐�</param>
        Task UpsertAsync(T entity, Func<T, bool> predicate);

        /// <summary>
        /// �폜���ꂽ���̂��܂ނ��ׂẴG���e�B�e�B���擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃R���N�V����</returns>
        IEnumerable<T> GetAllIncludingDeleted();

        /// <summary>
        /// �폜���ꂽ���̂��܂ނ��ׂẴG���e�B�e�B��񓯊��Ŏ擾���܂��B
        /// </summary>
        /// <returns>�G���e�B�e�B�̃R���N�V����</returns>
        Task<IEnumerable<T>> GetAllIncludingDeletedAsync();

        /// <summary>
        /// �폜���ꂽ���̂��܂ށA�w�肳�ꂽ�����Ɉ�v����G���e�B�e�B���擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
        T FindIncludingDeleted(Func<T, bool> predicate);

        /// <summary>
        /// �폜���ꂽ���̂��܂ށA�w�肳�ꂽ�����Ɉ�v����G���e�B�e�B��񓯊��Ŏ擾���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����G���e�B�e�B</returns>
        Task<T> FindIncludingDeletedAsync(Func<T, bool> predicate);

        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B�����݂��邩�m�F���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�G���e�B�e�B�����݂���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        bool IsExists(Func<T, bool> predicate);

        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����G���e�B�e�B�����݂��邩�񓯊��Ŋm�F���܂��B
        /// </summary>
        /// <param name="predicate">�����������w�肷��֐�</param>
        /// <returns>�G���e�B�e�B�����݂���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        Task<bool> IsExistsAsync(Func<T, bool> predicate);

        /// <summary>
        /// �����̃G���e�B�e�B����x�ɒǉ����܂��B
        /// </summary>
        /// <param name="entities">�ǉ�����G���e�B�e�B�̃R���N�V����</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// �����̃G���e�B�e�B����x�ɔ񓯊��Œǉ����܂��B
        /// </summary>
        /// <param name="entities">�ǉ�����G���e�B�e�B�̃R���N�V����</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// �����̃G���e�B�e�B����x�ɍX�V���܂��B
        /// </summary>
        /// <param name="entities">�X�V����G���e�B�e�B�̃R���N�V����</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// �����̃G���e�B�e�B����x�ɔ񓯊��ōX�V���܂��B
        /// </summary>
        /// <param name="entities">�X�V����G���e�B�e�B�̃R���N�V����</param>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// �����̃G���e�B�e�B����x�ɍ폜���܂��B
        /// </summary>
        /// <param name="entities">�폜����G���e�B�e�B�̃R���N�V����</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// �����̃G���e�B�e�B����x�ɔ񓯊��ō폜���܂��B
        /// </summary>
        /// <param name="entities">�폜����G���e�B�e�B�̃R���N�V����</param>
        Task DeleteRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// �g�����U�N�V�������J�n���܂��B
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// �g�����U�N�V������񓯊��ŊJ�n���܂��B
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// �g�����U�N�V�������R�~�b�g���܂��B
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// �g�����U�N�V������񓯊��ŃR�~�b�g���܂��B
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// �g�����U�N�V���������[���o�b�N���܂��B
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// �g�����U�N�V������񓯊��Ń��[���o�b�N���܂��B
        /// </summary>
        Task RollbackTransactionAsync();
    }
}

