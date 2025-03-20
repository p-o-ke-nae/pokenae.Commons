using pokenae.Commons.Domain.Entities;

namespace pokenae.Commons.Domain.Services
{
    /// <summary>
    /// エンティティに対する基本的な操作を提供するサービスインターフェース
    /// </summary>
    /// <typeparam name="T">エンティティの型</typeparam>
    public interface IEntityService<T>
        where T : BaseEntity
    {
        /// <summary>
        /// 指定された条件に一致するエンティティを取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するエンティティ</returns>
        T Find(Func<T, bool> predicate);

        /// <summary>
        /// 指定された条件に一致するエンティティを非同期で取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するエンティティ</returns>
        Task<T> FindAsync(Func<T, bool> predicate);

        /// <summary>
        /// すべてのエンティティを取得します。
        /// </summary>
        /// <returns>エンティティのコレクション</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// すべてのエンティティを非同期で取得します。
        /// </summary>
        /// <returns>エンティティのコレクション</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// 新しいエンティティを追加します。
        /// </summary>
        /// <param name="entity">追加するエンティティ</param>
        void Add(T entity);

        /// <summary>
        /// 新しいエンティティを非同期で追加します。
        /// </summary>
        /// <param name="entity">追加するエンティティ</param>
        Task AddAsync(T entity);

        /// <summary>
        /// 既存のエンティティを更新します。
        /// </summary>
        /// <param name="entity">更新するエンティティ</param>
        void Update(T entity);

        /// <summary>
        /// 既存のエンティティを非同期で更新します。
        /// </summary>
        /// <param name="entity">更新するエンティティ</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// エンティティを削除します。
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        void Delete(T entity);

        /// <summary>
        /// エンティティを非同期で削除します。
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// エンティティを追加または更新します。
        /// </summary>
        /// <param name="entity">追加または更新するエンティティ</param>
        /// <param name="predicate">検索条件を指定する関数</param>
        void Upsert(T entity, Func<T, bool> predicate);

        /// <summary>
        /// エンティティを非同期で追加または更新します。
        /// </summary>
        /// <param name="entity">追加または更新するエンティティ</param>
        /// <param name="predicate">検索条件を指定する関数</param>
        Task UpsertAsync(T entity, Func<T, bool> predicate);

        /// <summary>
        /// 削除されたものを含むすべてのエンティティを取得します。
        /// </summary>
        /// <returns>エンティティのコレクション</returns>
        IEnumerable<T> GetAllIncludingDeleted();

        /// <summary>
        /// 削除されたものを含むすべてのエンティティを非同期で取得します。
        /// </summary>
        /// <returns>エンティティのコレクション</returns>
        Task<IEnumerable<T>> GetAllIncludingDeletedAsync();

        /// <summary>
        /// 削除されたものを含む、指定された条件に一致するエンティティを取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するエンティティ</returns>
        T FindIncludingDeleted(Func<T, bool> predicate);

        /// <summary>
        /// 削除されたものを含む、指定された条件に一致するエンティティを非同期で取得します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>条件に一致するエンティティ</returns>
        Task<T> FindIncludingDeletedAsync(Func<T, bool> predicate);

        /// <summary>
        /// 指定された条件に一致するエンティティが存在するか確認します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>エンティティが存在する場合はtrue、それ以外の場合はfalse</returns>
        bool IsExists(Func<T, bool> predicate);

        /// <summary>
        /// 指定された条件に一致するエンティティが存在するか非同期で確認します。
        /// </summary>
        /// <param name="predicate">検索条件を指定する関数</param>
        /// <returns>エンティティが存在する場合はtrue、それ以外の場合はfalse</returns>
        Task<bool> IsExistsAsync(Func<T, bool> predicate);

        /// <summary>
        /// 複数のエンティティを一度に追加します。
        /// </summary>
        /// <param name="entities">追加するエンティティのコレクション</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に非同期で追加します。
        /// </summary>
        /// <param name="entities">追加するエンティティのコレクション</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に更新します。
        /// </summary>
        /// <param name="entities">更新するエンティティのコレクション</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に非同期で更新します。
        /// </summary>
        /// <param name="entities">更新するエンティティのコレクション</param>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に削除します。
        /// </summary>
        /// <param name="entities">削除するエンティティのコレクション</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に非同期で削除します。
        /// </summary>
        /// <param name="entities">削除するエンティティのコレクション</param>
        Task DeleteRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// トランザクションを開始します。
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// トランザクションを非同期で開始します。
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// トランザクションをコミットします。
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// トランザクションを非同期でコミットします。
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// トランザクションをロールバックします。
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// トランザクションを非同期でロールバックします。
        /// </summary>
        Task RollbackTransactionAsync();
    }
}

